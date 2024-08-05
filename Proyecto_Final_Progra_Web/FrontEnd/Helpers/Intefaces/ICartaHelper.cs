using FrontEnd.Models;
using System.Threading.Tasks;

namespace FrontEnd.Helpers.Intefaces
{
    public interface ICartaHelper
    {
        Task<List<CartaViewModel>> GetCartas();
        Task<CartaViewModel> GetCarta(int id);
        Task<CartaViewModel> Add(CartaViewModel carta);
        Task<CartaViewModel> Remove(int id);
        Task<CartaViewModel> Update(CartaViewModel carta);
    }
}
