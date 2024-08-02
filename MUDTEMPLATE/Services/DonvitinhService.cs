using DBDATA.Context;
using DBDATA.Models;
using Microsoft.EntityFrameworkCore;

namespace MUDTEMPLATE.Services
{
    public class DonvitinhService : IDonvitinhService
    {
        private readonly DATAContext db;
        public DonvitinhService(DATAContext _db)
        {
            db = _db;
        }

        #region Get data
        public async Task<List<Donvitinh>> GetAllDonvitinh()
        {
            return await db.Donvitinhs.ToListAsync();
        }
        #endregion
    }
}
