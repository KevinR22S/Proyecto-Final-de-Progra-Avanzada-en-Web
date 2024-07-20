using Entities.Entities;

namespace BackEnd.Services.Interfaces
{
    public interface IMazoService
    {
        bool Add(Mazo mazo);
        bool Remove(Mazo mazo);
        bool Update(Mazo mazo);

        Mazo Get(int id);
        IEnumerable<Mazo> Get();
    }
}

