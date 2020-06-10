using LearningEngine.Application.Factories;
using LearningEngine.Application.UseCase.Query;
using LearningEngine.Domain.DTO;
using LearningEngine.Domain.Enum;
using LearningEngine.Domain.Query;
using LearningEngine.Persistence.Handlers;
using MediatR;
using System;

namespace LearningEngine.Api.Extensions
{
    public static class GetPermissionModelFactoryExtension
    {
        public static GetPermissionModelFactory RegisterQuery(this GetPermissionModelFactory getPermissionModelFactory)
        {
            return getPermissionModelFactory
                .AddQuery(ObjectType.Card, (cardId, userId, access) =>
                    new GetCardPermissionQuery(cardId, access, userId))
                .AddQuery(ObjectType.Note, (noteId, userId, access) => 
                    new GetNotePermissionQuery(noteId, access, userId))
                .AddQuery(ObjectType.Statistic, (statisticId, userId, access) => 
                    new GetStatisticPermissionQuery(statisticId, access, userId))
                .AddQuery(ObjectType.Theme, (themeId, userId, access) => 
                    new CheckUserThemePermissionsQuery(userId, themeId, access));
        }
    }
}
