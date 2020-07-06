using E_ticaret.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_ticaret.data.Abstract
{
   public interface ICategoryRepository:IRepository<Category>
    {
        List<Category> getPopularCategory();

    }
}
