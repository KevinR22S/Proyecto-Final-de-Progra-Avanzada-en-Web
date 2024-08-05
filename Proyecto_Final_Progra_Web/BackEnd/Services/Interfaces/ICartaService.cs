using Entities.Entities;
using BackEnd.Model;

namespace BackEnd.Services.Interfaces
{
    public interface ICartaService
    {
        bool Add(CartaModel carta);
        bool Remove(CartaModel carta);
        bool Update(CartaModel carta);

        CartaModel Get(int id);
        IEnumerable<CartaModel> Get();
    }
}
