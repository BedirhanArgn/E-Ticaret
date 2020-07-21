using System;
using System.Collections.Generic;
using System.Text;

namespace E_ticaret.business.Abstract
{
    public interface IValidator<T>
    {
        string ErrorMessage { get; set; }
        bool Validation(T entity);
    }
}
