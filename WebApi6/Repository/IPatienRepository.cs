using WebApi6.Data;

namespace WebApi6.Repository
{
    public interface IPatienRepository
    {
        IEnumerable<Patien> GetPatiens();

        int DeletePatien(int id);

        int AddPatien(Patien patien);

        int UpdatePatien(Patien patien);
    }
}
