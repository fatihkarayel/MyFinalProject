using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public List<Category> GetAll()
        {
            //iş kodaltını yazacağız
            return _categoryDal.GetAll();
            
        }

        public Category GetById(int categoryId)
        {
            //iş kodlarını yazacağız
            return _categoryDal.Get(c => c.CategoryId == categoryId);

        }
    }
}
