﻿using LearningEngine.Domain.Query;
using LearningEngine.Persistence.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using LearningEngine.Domain.DTO;

namespace LearningEngine.Persistence.Handlers
{
    class GetUserByNameHandler : IRequestHandler<GetUserByNameQuery, UserDto>
    {
        private readonly LearnEngineContext _context;
        public GetUserByNameHandler(LearnEngineContext context)
        {
            _context = context;
        }

        public async Task<UserDto> Handle(GetUserByNameQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(usr => usr.UserName == request.UserName);

            return user != null? 
                new UserDto { UserName = user.UserName, Email = user.Email } 
                : null;
        }
    }
}
