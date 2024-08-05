using BackEnd.Model;

namespace BackEnd.Services.Interfaces
{
    public interface IMazoService
    {
        bool Add(MazoModel mazo);
        bool Remove(MazoModel mazo);
        bool Edit(MazoModel mazo);

        MazoModel Get(int id);
        IEnumerable<MazoModel> Get();
    }
}

