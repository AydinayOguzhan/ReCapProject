using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCustomerDal : EfEntityRepositoryBase<Customer, ReCapProjectContext>, ICustomerDal
    {
        public List<CustomerDetailDto> GetAllCustomerDetail()
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from c in context.Customers
                             join u in context.Users
                             on c.UserId equals u.Id
                             select new CustomerDetailDto()
                             {
                                 CompanyName = c.CompanyName,
                                 CustomerId = c.Id,
                                 Email = u.Email,
                                 FirstName = u.FirstName,
                                 LastName = u.LastName,
                                 UserId = u.Id
                             };
                return result.ToList();
            }
        }

        public CustomerDetailDto GetCustomerDetailByLastName(string lastName)
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = (from c in context.Customers
                              join u in context.Users
                              on c.UserId equals u.Id
                              where u.LastName == lastName
                              select new CustomerDetailDto()
                              {
                                  CompanyName = c.CompanyName,
                                  CustomerId = c.Id,
                                  Email = u.Email,
                                  FirstName = u.FirstName,
                                  LastName = u.LastName,
                                  UserId = u.Id
                              }).FirstOrDefault();
                return result;
            }
        }
    }
}
