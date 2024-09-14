using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Framework.Data.Interfaces
{
    public interface IUnitOfWork
    {
        //IBooksRepository BooksRepository { get; }
        //IRolesRepository RolesRepository { get; }
        IUserRepository UserRepository { get; }
        Task<bool> Complete();
        bool HasChanges();
    }
}
