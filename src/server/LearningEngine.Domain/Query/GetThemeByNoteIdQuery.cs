using FluentValidation;
using LearningEngine.Domain.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Domain.Query
{
    public class GetThemeByNoteIdQuery : IRequest<ThemeDto>
    {
        public int NoteId { get; set; }

        public GetThemeByNoteIdQuery(int noteId)
        {
            NoteId = noteId;
        }
    }

    public class GetThemeByNoteIdQueryValidator : AbstractValidator<GetThemeByNoteIdQuery>
    {
        public GetThemeByNoteIdQueryValidator()
        {
            RuleFor(note => note.NoteId).GreaterThan(0);
        }
    }
}
