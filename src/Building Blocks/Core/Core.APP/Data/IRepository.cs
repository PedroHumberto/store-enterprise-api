using Core.APP.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.APP.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot//apenas entidades podem ser relacionadas.
    {

    }
}
