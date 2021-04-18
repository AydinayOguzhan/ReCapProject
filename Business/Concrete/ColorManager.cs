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
    public class ColorManager : IColorService
    {
        IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        [SecuredOperations("admin")]
        [CacheRemoveAspect("IColorService.Get")]
        [TransactionScopeAspect]
        [ValidationAspect(typeof(ColorValidator))]
        public Result Add(Color color)
        {
            _colorDal.Add(color);
            return new SuccessResult(Messages.ColorAdded);
        }

        [SecuredOperations("admin")]
        [CacheRemoveAspect("IColorService.Get")]
        [TransactionScopeAspect]
        public Result Delete(Color color)
        {
            _colorDal.Delete(color);
            return new SuccessResult(Messages.CarDeleted);
        }

        [CacheAspect]
        public IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll(),Messages.ColorsListed);
        }

        [CacheAspect]
        public IDataResult<Color> GetById(int id)
        {
            return new SuccessDataResult<Color>(_colorDal.Get(c => c.Id == id),Messages.ColorListed);
        }

        [SecuredOperations("admin")]
        [CacheRemoveAspect("IColorService.Get")]
        [TransactionScopeAspect]
        public Result Update(Color color)
        {
            _colorDal.Update(color);
            return new SuccessResult(Messages.CarUpdated);
        }
    }
}
