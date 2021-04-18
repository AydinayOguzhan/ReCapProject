using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Aspects.Autofac.Transaction;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class OperationClaimManager : IOperationClaimService
    {
        IOperationClaimDal _operationClaimDal;

        public OperationClaimManager(IOperationClaimDal operationClaimDal)
        {
            _operationClaimDal = operationClaimDal;
        }

        [SecuredOperations("admin")]
        [TransactionScopeAspect]
        public IResult Add(OperationClaim operationClaim)
        {
            _operationClaimDal.Add(operationClaim);
            return new SuccessResult(Messages.Successful);
        }

        [SecuredOperations("admin")]
        [TransactionScopeAspect]
        public IResult Delete(OperationClaim operationClaim)
        {
            _operationClaimDal.Delete(operationClaim);
            return new SuccessResult(Messages.Successful);
        }

        public IDataResult<List<OperationClaim>> GetAll()
        {
            return new SuccessDataResult<List<OperationClaim>>(_operationClaimDal.GetAll(), Messages.Successful);
        }

        public IDataResult<OperationClaim> GetById(int id)
        {
            return new SuccessDataResult<OperationClaim>(_operationClaimDal.Get(o => o.Id == id));
        }

        [SecuredOperations("admin")]
        [TransactionScopeAspect]
        public IResult Update(OperationClaim operationClaim)
        {
            _operationClaimDal.Update(operationClaim);
            return new SuccessResult(Messages.Successful);
        }
    }
}
