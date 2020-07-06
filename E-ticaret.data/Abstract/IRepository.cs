using System;
using System.Collections.Generic;
using System.Text;

namespace E_ticaret.data.Abstract
{


    //Generic bir interface ürettik.Category ve Product için ayrı ayrı üretmeye gerek yok.
    public interface IRepository<T>
    {
        T getById(int id);
        List<T> getAll();
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
