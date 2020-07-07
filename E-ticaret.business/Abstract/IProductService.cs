﻿using E_ticaret.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_ticaret.business.Abstract
{
   public interface IProductService
    {
       Product getById(int id);
        List<Product> getAll();
        void Create(Product entity);
        void Update(Product entity);
        void Delete(Product entity);

    }
}