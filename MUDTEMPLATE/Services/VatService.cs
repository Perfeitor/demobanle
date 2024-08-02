using DBDATA.Context;
using DBDATA.Models;
using Microsoft.EntityFrameworkCore;

namespace MUDTEMPLATE.Services
{
    public class VatService : IVatService
    {
        private readonly DATAContext db;
        public VatService(DATAContext _db)
        {
            db = _db;
        }

        public async Task<List<Vat>> GetAllVat()
        {
            return await db.Vats.ToListAsync();
        }
    }
}
