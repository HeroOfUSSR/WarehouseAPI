using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Domain.Exceptions
{
    public class DuplicateEmailException : ConflictException
    {
        public DuplicateEmailException(string email) : base($"Пользователь с email '{email}' уже существует") { }
    }
}
