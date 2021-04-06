using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
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
            _creditCardInformationDal = creditCardInformationDal;
        }

        [TransactionScopeAspect]
        [ValidationAspect(typeof(CreditCardValidator))]
        public IResult Add(CreditCardInformation creditCardInformation)
        {
            var result = BusinessRules.Run(CheckIfHasCard(creditCardInformation.UserId));
            if (result != null)
            {
                _creditCardInformationDal.Add(creditCardInformation);
                return new SuccessResult(Messages.Successful);
            }
            var oldCreditCard = _creditCardInformationDal.Get(c => c.UserId == creditCardInformation.UserId);
            CreditCardInformation newCreditCard = new CreditCardInformation()
            {
                Id = oldCreditCard.Id,
                UserId = creditCardInformation.UserId,
                CardMonth = creditCardInformation.CardMonth,
                CardName = creditCardInformation.CardName,
                CardNumber = creditCardInformation.CardNumber,
                CardSecurityNumber = creditCardInformation.CardSecurityNumber,
                CardYear = creditCardInformation.CardYear
            };
            _creditCardInformationDal.Update(newCreditCard);
            return new SuccessResult(Messages.Successful);
        }

        [ValidationAspect(typeof(CreditCardValidator))]
        public IResult CheckIfCreditCardLegit(CreditCardInformation creditCardInformation)
        {
            return new SuccessResult();
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

        public IDataResult<CreditCardInformation> GetByUserId(int userId)
        {
            return new SuccessDataResult<CreditCardInformation>(_creditCardInformationDal.Get(c => c.UserId == userId));
        }

        [TransactionScopeAspect]
        public IResult Update(CreditCardInformation creditCardInformation)
        {
            _creditCardInformationDal.Update(creditCardInformation);
            return new SuccessResult(Messages.Successful);
        }

        private IResult CheckIfHasCard(int userId)
        {
            var result = _creditCardInformationDal.Get(c => c.UserId == userId);
            if (result != null)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }
    }
}
