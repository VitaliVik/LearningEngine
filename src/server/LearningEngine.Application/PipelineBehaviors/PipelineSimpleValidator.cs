using FluentValidation;
using LearningEngine.Application.Exceptions;
using MediatR;
using MediatR.Pipeline;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LearningEngine.Application.PipelineBehaviors
{
    public class PipelineSimpleValidator<TRequest, TResponce> : IPipelineBehavior<TRequest, TResponce> 
    {
        private readonly IEnumerable<IValidator<TRequest>> validators;

        public PipelineSimpleValidator(IEnumerable<IValidator<TRequest>> validators)
        {
            this.validators = validators;
        }

        public Task<TResponce> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponce> next)
        {
            var context = new ValidationContext(request);
            var failures = validators
                .Select(x => x.Validate(context))
                .SelectMany(x => x.Errors)
                .Where(x => x != null)
                .ToList();

            if (failures.Any())
            {
                throw new SimpleValidationException(failures.First().ErrorMessage);
            }

            return next();
        }

        //public Task Process(TRequest request, CancellationToken cancellationToken)
        //{
        //    var context = new ValidationContext(request);
        //    var failures = validators
        //        .Select(x => x.Validate(context))
        //        .SelectMany(x => x.Errors)
        //        .Where(x => x != null)
        //        .ToList();

        //    if (failures.Any())
        //    {
        //        throw new SimpleValidationException(failures.First().ErrorMessage);
        //    }

        //    return Task.CompletedTask;
        //}
    }
}
