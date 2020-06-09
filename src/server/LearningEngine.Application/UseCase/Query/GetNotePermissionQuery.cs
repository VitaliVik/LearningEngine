using LearningEngine.Domain.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Application.UseCase.Query
{
    public class GetNotePermissionQuery : IRequest
    {
        public int NoteId { get; private set; }

        public int UserId { get; private set; }

        public TypeAccess Access { get; private set; }

        public GetNotePermissionQuery(int noteId, TypeAccess access, int userId)
        {
            NoteId = noteId;
            Access = access;
            UserId = userId;
        }
    }
}
