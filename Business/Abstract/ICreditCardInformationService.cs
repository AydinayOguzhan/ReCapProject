using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICreditCardInformationService
    {
        IResult Add(CreditCardInformation creditCardInformation);
        IResult Update(CreditCardInformation creditCardInformation);
        IResult Delete(CreditCardInformation creditCardInformation);

        IDataResult<List<CreditCardInformation>> GetAll();
        IDataResult<CreditCardInformation> GetByUserId(int userId);
        IDataResult<CreditCardInformation> GetById(int id);

        IResult CheckIfCreditCardLegit(CreditCardInformation creditCardInformation);
    }
}
