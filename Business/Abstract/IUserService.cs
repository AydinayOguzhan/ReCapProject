using Core.Entities.Concrete;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<User>> GetAll();
        IDataResult<User> GetById(int id);
        Result Add(User user);
        Result Update(User user);
        Result Delete(User user);

        IDataResult<User> GetUserByMail(string email);
        IDataResult<List<OperationClaim>> GetClaims(User user);
    }
}
