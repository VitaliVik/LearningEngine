using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Application.Exceptions
{
    public class UseCaseTransactionException : Exception
    {
        public UseCaseTransactionException(Exception exception): base("транзакция была прервана", exception)
        {
        }
    }
}
