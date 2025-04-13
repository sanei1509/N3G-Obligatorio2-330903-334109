using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasoUsoCompartida.InterfacesCU
{
    public interface IUpdate<T>
    {
        void Execute(int id, T obj);
    }
}
