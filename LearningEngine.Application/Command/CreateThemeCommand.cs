using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Application.Command
{
    class CreateThemeCommand : IRequest<bool>
    {
        public string UserName { get; }
        public CreateThemeCommand()
        {

        }
    }
}
