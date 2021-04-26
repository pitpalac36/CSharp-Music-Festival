using System.Collections.Generic;
using Model.domain;

namespace Persistance
{
    public interface IRepository<T>
    {
        List<T> FindAll();
    }
}
