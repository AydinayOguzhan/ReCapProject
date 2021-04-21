using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserOperationClaimDal : EfEntityRepositoryBase<UserOperationClaim, ReCapProjectContext>, IUserOperationClaimDal
    {

        public List<UserOperationClaimDetailDto> GetUserOperationClaimDetailByUserId(int userId)
        {
            using (var context = new ReCapProjectContext())
            {
                var result = (from userOperationClaim in context.UserOperationClaims
                              join operationClaim in context.OperationClaims
                              on userOperationClaim.OperationClaimId equals operationClaim.Id
                              where userOperationClaim.UserId == userId
                              select new UserOperationClaimDetailDto
                              {
                                  Id = userOperationClaim.Id,
                                  UserId = userOperationClaim.UserId,
                                  OperationClaimId = userOperationClaim.OperationClaimId,
                                  OperationClaimName = operationClaim.Name
                              }).ToList();
                return result;
            }
        }
    }
}
