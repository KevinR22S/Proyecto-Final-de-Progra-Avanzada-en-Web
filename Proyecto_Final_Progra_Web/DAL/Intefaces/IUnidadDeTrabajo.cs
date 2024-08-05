using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Intefaces
{
    public interface IUnidadDeTrabajo : IDisposable
    {
        ICartaDAL CartaDAL { get; }
        IMazoDAL MazoDAL { get; }
        bool Complete();
    }
}
