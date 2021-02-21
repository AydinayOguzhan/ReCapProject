﻿using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        IDataResult<List<Customer>> GetAll();
        IDataResult<Customer> GetById(int id);
        Result Add(Customer customer);
        Result Update(Customer customer);
        Result Delete(Customer customer);
        IDataResult<List<CustomerDetailDto>> GetAllCustomerDetail();
        IDataResult<CustomerDetailDto> GetCustomerDetailByLastName(string lastName);
    }
}