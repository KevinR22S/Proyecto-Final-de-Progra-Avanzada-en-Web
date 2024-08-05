using Entities.Entities;
using System.Collections.Generic;

namespace BackEnd.Services.Interfaces
{
    public interface ICartaService
    {
        bool Add(Carta carta);
        bool Remove(Carta carta);
        bool Update(Carta carta);
        Carta Get(int id);
        IEnumerable<Carta> Get();
    }
}
