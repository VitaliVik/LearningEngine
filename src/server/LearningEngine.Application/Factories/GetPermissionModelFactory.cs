using LearningEngine.Domain.DTO;
using LearningEngine.Domain.Enum;
using LearningEngine.Domain.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Application.Factories
{
    public class GetPermissionModelFactory : IGetPermissionModelFactory
    {
        private readonly Dictionary<ObjectType, Func<int, int, TypeAccess, IRequest>> themeIdQueryGetter;
        public GetPermissionModelFactory()
        {
            themeIdQueryGetter = new Dictionary<ObjectType, Func<int, int, TypeAccess, IRequest>>();
        }
        public IRequest GetModel(int objectId, int userId, TypeAccess access, ObjectType objectType)
        {
            return themeIdQueryGetter[objectType].Invoke(objectId, userId, access);
        }
        public GetPermissionModelFactory AddQuery(ObjectType objectType, Func<int, int, TypeAccess, IRequest> func)
        {
            themeIdQueryGetter.Add(objectType, func);

            return this;
        }
    }
}
