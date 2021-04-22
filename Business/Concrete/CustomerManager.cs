using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _customerDal;
        IUserOperationClaimService _userOperationClaimService;

        public CustomerManager(ICustomerDal customerDal, IUserOperationClaimService userOperationClaimService)
        {
            _customerDal = customerDal;
            _userOperationClaimService = userOperationClaimService;
        }

        [CacheRemoveAspect("ICustomerService.Get")]
        [TransactionScopeAspect]
        [ValidationAspect(typeof(CustomerValidator))]
        public Result Add(Customer customer)
        {
            int userClaim = 2;
            UserOperationClaim defaultUserClaims = new UserOperationClaim { OperationClaimId = userClaim, UserId = customer.UserId };

            _customerDal.Add(customer);
            _userOperationClaimService.Add(defaultUserClaims);
            return new SuccessResult(Messages.CustomerAdded);
        }

        [CacheRemoveAspect("ICustomerService.Get")]
        [TransactionScopeAspect]
        public Result Delete(Customer customer)
        {
            _customerDal.Delete(customer);
            return new SuccessResult(Messages.CustomerDeleted);
        }

        [CacheAspect]
        public IDataResult<List<Customer>> GetAll()
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll(), Messages.CustomersListed);
        }

        [CacheAspect]
        public IDataResult<List<CustomerDetailDto>> GetAllCustomerDetail()
        {
            return new SuccessDataResult<List<CustomerDetailDto>>(_customerDal.GetAllCustomerDetail(),Messages.CustomersListed);
        }

        [CacheAspect]
        public IDataResult<Customer> GetById(int id)
        {
            return new SuccessDataResult<Customer>(_customerDal.Get(c => c.Id == id), Messages.CustomerListed);
        }

        [CacheAspect]
        public IDataResult<Customer> GetCustomerByUserId(int userId)
        {
            return new SuccessDataResult<Customer>(_customerDal.Get(c => c.UserId == userId), Messages.CustomerListed);
        }

        [CacheAspect]
        public IDataResult<CustomerDetailDto> GetCustomerDetailByLastName(string lastName)
        {
            return new SuccessDataResult<CustomerDetailDto>(_customerDal.GetCustomerDetailByLastName(lastName),Messages.CustomerListed);
        }

        [CacheAspect]
        public IDataResult<CustomerDetailDto> GetCustomerDetailByUserId(int userId)
        {
            return new SuccessDataResult<CustomerDetailDto>(_customerDal.GetCustomerDetailByUserId(userId));
        }

        [CacheRemoveAspect("ICustomerService.Get")]
        [TransactionScopeAspect]
        public Result Update(Customer customer)
        {
            _customerDal.Update(customer);
            return new SuccessResult(Messages.CustomerUpdated);
        }
    }
}
