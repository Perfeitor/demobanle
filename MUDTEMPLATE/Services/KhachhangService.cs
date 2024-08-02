using DBDATA.Context;
using DBDATA.Models;
using Microsoft.EntityFrameworkCore;

namespace MUDTEMPLATE.Services
{
    public class KhachhangService : IKhachhangService
    {
        private readonly DATAContext db;
        public KhachhangService(DATAContext _db)
        {
            db = _db;
        }
        #region Get data
        public async Task<List<Khachhang>> GetAllKhachhang()
        {
            var t = await db.Khachhangs.ToListAsync();
            return t;
        }
        #endregion
    }
}
