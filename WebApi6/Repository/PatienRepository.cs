using WebApi6.Data;

namespace WebApi6.Repository
{
    public class PatienRepository : IPatienRepository
    {
        private PatienDbContext _context;

        public PatienRepository(PatienDbContext patienDbContext)
        {
            _context = patienDbContext;
        }
        public IEnumerable<Patien> GetPatiens()
        {
            return _context.Patients.ToList();
        }

        public int DeletePatien(int id)
        { 
            var obj = _context.Patients.FirstOrDefault(p => p.Id == id);
            if (obj == null)
                return -1;

            _context.Patients.Remove(obj);
            return _context.SaveChanges();

        }

        public int AddPatien(Patien patien)
        {
            _context.Patients.Add(patien);

            return _context.SaveChanges();
        }

        public int UpdatePatien(Patien patien)
        {
            var obj = _context.Patients.FirstOrDefault(x => x.Id == patien.Id);
            
            if(obj == null) 
                return -1;

            obj.Email = patien.Email;
            obj.Name = patien.Name;

            _context.Patients.Update(obj);
            return _context.SaveChanges();
        }
    }
}
