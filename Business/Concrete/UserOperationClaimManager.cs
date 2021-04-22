using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Aspects.Autofac.Transaction;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Business.Concrete
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        IUserOperationClaimDal _userOperationClaimDal;

        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal)
        {
            _userOperationClaimDal = userOperationClaimDal;
        }

        //[SecuredOperations("admin")]
        [TransactionScopeAspect]
        public IResult Add(UserOperationClaim userOperationClaim)
        {
            var result = BusinessRules.Run(CheckIfClaimExist(userOperationClaim));
            if (result == null)
            {
                _userOperationClaimDal.Add(userOperationClaim);
                return new SuccessResult(Messages.Successful);
            }
            return new ErrorResult(Messages.ClaimAlreadyExist);
        }

        public IResult CheckIfItsAdmin(int userId)
        {
            int adminClaimId = 1;
            var claims = _userOperationClaimDal.GetAll(u => u.UserId == userId);
            if (claims == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            var adminClaim = claims.SingleOrDefault(c => c.OperationClaimId == adminClaimId);
            if (adminClaim == null)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }

        [SecuredOperations("admin")]
        [TransactionScopeAspect]
        public IResult Delete(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimDal.Delete(userOperationClaim);
            return new SuccessResult(Messages.Successful);
        }

        public IDataResult<List<UserOperationClaim>> GetAll()
        {
            return new SuccessDataResult<List<UserOperationClaim>>(_userOperationClaimDal.GetAll());
        }

        public IDataResult<UserOperationClaim> GetById(int id)
        {
            return new SuccessDataResult<UserOperationClaim>(_userOperationClaimDal.Get(u => u.Id == id));
        }

        public IDataResult<List<UserOperationClaim>> GetByUserId(int userId)
        {
            return new SuccessDataResult<List<UserOperationClaim>>(_userOperationClaimDal.GetAll(u => u.UserId == userId));
        }

        [SecuredOperations("admin")]
        [TransactionScopeAspect]
        public IResult Update(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimDal.Update(userOperationClaim);
            return new SuccessResult(Messages.Successful);
        }

        private IResult CheckIfClaimExist(UserOperationClaim userOperationClaim)
        {
            var result = _userOperationClaimDal.GetAll(c => c.UserId == userOperationClaim.UserId);
            foreach (var item in result)
            {
                if (item.OperationClaimId == userOperationClaim.OperationClaimId)
                {
                    return new ErrorResult();
                }
            }
            return new SuccessResult();
        }

        public IDataResult<List<UserOperationClaimDetailDto>> GetUserOperationClaimsByUserId(int userId)
        {
            return new SuccessDataResult<List<UserOperationClaimDetailDto>>(_userOperationClaimDal.GetUserOperationClaimDetailByUserId(userId));
        }
    }
}
