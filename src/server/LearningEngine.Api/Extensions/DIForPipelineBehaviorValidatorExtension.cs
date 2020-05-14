using LearningEngine.Application.PipelineValidators;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEngine.Api.Extensions
{
    public static class DIForPipelineBehaviorValidatorExtension
    {
        public static void RegisterAllAssignableType<T>(this IServiceCollection services, string assemblyName)
        {
            var assembly = AppDomain.CurrentDomain.Load(assemblyName);
            var types = assembly.GetTypes().Where(p => typeof(T).IsAssignableFrom(p)).ToArray();

            foreach (var type in types)
            {
                if (type.IsInterface)
                {
                    continue;
                }

                var genericArg = type.GetInterfaces().FirstOrDefault(inter => inter.Name == "IRequest`1").GenericTypeArguments[0];

                services.AddTransient(typeof(IPipelineBehavior<,>).MakeGenericType(type, genericArg),
                                      typeof(PipelinePermissionValidator<>).MakeGenericType(genericArg));
            }
        }
    }
}
