using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CreditCardInformationManager : ICreditCardInformationService
    {
        ICreditCardInformationDal _creditCardInformationDal;

        public CreditCardInformationManager(ICreditCardInformationDal creditCardInformationDal)
        {
            this._creditCardInformationDal = creditCardInformationDal;
        }

        [TransactionScopeAspect]
        [ValidationAspect(typeof(CreditCardValidator))]
        public IResult Add(CreditCardInformation creditCardInformation)
        {
            _creditCardInformationDal.Add(creditCardInformation);
            return new SuccessResult(Messages.Successful);
        }

        [TransactionScopeAspect]
        public IResult Delete(CreditCardInformation creditCardInformation)
        {
            _creditCardInformationDal.Delete(creditCardInformation);
            return new SuccessResult(Messages.Successful);
        }

        public IDataResult<List<CreditCardInformation>> GetAll()
        {
            return new SuccessDataResult<List<CreditCardInformation>>(_creditCardInformationDal.GetAll());
        }

        public IDataResult<CreditCardInformation> GetById(int id)
        {
            return new SuccessDataResult<CreditCardInformation>(_creditCardInformationDal.Get(c => c.Id == id));
        }

        public IDataResult<List<CreditCardInformation>> GetByUserId(int userId)
        {
            return new SuccessDataResult<List<CreditCardInformation>>(_creditCardInformationDal.GetAll(c => c.UserId == userId));
        }

        [TransactionScopeAspect]
        public IResult Update(CreditCardInformation creditCardInformation)
        {
            _creditCardInformationDal.Update(creditCardInformation);
            return new SuccessResult(Messages.Successful);
        }
    }
}
