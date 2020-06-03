using LearningEngine.Application.Factories;
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
            getPermissionModelFactory.AddQuery
                (ObjectType.Card, (cardId) => new GetThemeByCardIdQuery(cardId));
            getPermissionModelFactory.AddQuery
                (ObjectType.Note, (noteId) => new GetThemeByNoteIdQuery(noteId));
            getPermissionModelFactory.AddQuery
                (ObjectType.Statistic, (statisticId) => new GetThemeByStatisticIdQuery(statisticId));

            return getPermissionModelFactory;
        }
    }
}
