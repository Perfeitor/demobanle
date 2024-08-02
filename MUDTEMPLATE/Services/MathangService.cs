using DBDATA.Context;
using DBDATA.Models;
using Microsoft.EntityFrameworkCore;

namespace MUDTEMPLATE.Services
{
    public class MathangService : IMathangService
    {
        private readonly DATAContext db;
        public MathangService(DATAContext _db)
        {
            db = _db;
        }

        #region Get data
        public async Task<List<Mathang>> GetAllMathang()
        {
            return await db.Mathangs.ToListAsync();
        }
        #endregion

        #region Thêm, sửa, xoá data
        public async Task AddMathang(Mathang newMathang)
        {
            await db.Mathangs.AddAsync(newMathang);
            await db.SaveChangesAsync();
        }
        public async Task EditMathang(Mathang newMathang)
        {
            db.Mathangs.Update(newMathang);
            await db.SaveChangesAsync();
        }
        public async Task RemoveMathang(Mathang mathang)
        {
            db.Mathangs.Remove(mathang);
            await db.SaveChangesAsync();
        }
        #endregion
    }
}
