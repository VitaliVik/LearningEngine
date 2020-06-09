using LearningEngine.Domain.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Application.UseCase.Query
{
    public class GetStatisticPermissionQuery : IRequest
    {
        public int StatisticId { get; private set; }

        public int UserId { get; private set; }

        public TypeAccess Access { get; private set; }

        public GetStatisticPermissionQuery(int cardId, TypeAccess access, int userId)
        {
            StatisticId = cardId;
            Access = access;
            UserId = userId;
        }
    }
}
