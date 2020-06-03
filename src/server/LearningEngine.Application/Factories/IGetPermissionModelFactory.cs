using LearningEngine.Domain.DTO;
using LearningEngine.Domain.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Application.Factories
{
    public interface IGetPermissionModelFactory
    {
        IRequest GetModel(int objectId, int userId, TypeAccess access, ObjectType objectType);
    }
}
