using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        [CacheRemoveAspect("IBrandService.Get")]
        [SecuredOperations("admin")]
        [TransactionScopeAspect]
        [ValidationAspect(typeof(BrandValidator))]
        public Result Add(Brand brand)
        {
            _brandDal.Add(brand);
            return new SuccessResult(Messages.BrandAdded);
        }

        [CacheRemoveAspect("IBrandService.Get")]
        [SecuredOperations("admin")]
        [TransactionScopeAspect]
        public Result Delete(Brand brand)
        {
            _brandDal.Delete(brand);
            return new SuccessResult(Messages.BrandDeleted);
        }

        [CacheAspect]
        public IDataResult<List<Brand>> GetAll()
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(),Messages.BrandsListed);
        }

        [CacheAspect]
        public IDataResult<List<Brand>> GetAllById(int brandId)
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(b => b.Id == brandId));
        }

        [CacheAspect]
        public IDataResult<Brand> GetById(int id)
        {
            return new SuccessDataResult<Brand>(_brandDal.Get(b => b.Id == id),Messages.BrandListed);
        }

        [CacheRemoveAspect("IBrandService.Get")]
        [SecuredOperations("admin")]
        [TransactionScopeAspect]
        public Result Update(Brand brand)
        {
            _brandDal.Update(brand);
            return new SuccessResult(Messages.BrandUpdated);
        }
    }
}
