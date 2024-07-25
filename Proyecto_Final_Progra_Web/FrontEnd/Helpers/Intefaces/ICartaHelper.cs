using FrontEnd.Models;

namespace FrontEnd.Helpers.Intefaces
{
    public interface ICartaHelper
    {
        List<CartaViewModel> GetCartas();
        CartaViewModel GetCarta(int id);
        CartaViewModel Add(CartaViewModel carta);
        CartaViewModel Remove(int id);
        CartaViewModel Update(CartaViewModel carta);
    }
}
