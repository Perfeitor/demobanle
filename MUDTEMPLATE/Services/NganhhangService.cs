using DBDATA.Context;
using DBDATA.Models;
using Microsoft.EntityFrameworkCore;

namespace MUDTEMPLATE.Services
{
    public class NganhhangService : INganhhangService
    {
        private readonly DATAContext db;
        private readonly INguoidungService nguoidungService;
        public NganhhangService(DATAContext _db, INguoidungService _nguoidungService)
        {
            db = _db;
            nguoidungService = _nguoidungService;
        }

        #region Thêm, sửa, xoá dữ liệu
        public async Task AddItem(Nganhhang nganhhang)
        {
            nganhhang.Tendangnhap = nguoidungService.GetCookieName();
            nganhhang.Madonvi = nguoidungService.GetCookieUnit();
            var newItem = new Nganhhang
            {
                Tennganh = nganhhang.Tennganh,
                Manganh = nganhhang.Manganh,
                Loai = nganhhang.Loai,
                Ngaytao = DateTime.UtcNow,
                Tendangnhap = nganhhang.Tendangnhap,
                Madonvi = nganhhang.Madonvi,
            };
            await db.Nganhhangs.AddAsync(newItem);
            await db.SaveChangesAsync();
        }

        public async Task DeleteItem(Nganhhang nganhhang)
        {
            db.Nganhhangs.Remove(nganhhang);
            await db.SaveChangesAsync();
        }

        public async Task EditItem(Nganhhang nganhhang)
        {
            db.Nganhhangs.Update(nganhhang);
            await db.SaveChangesAsync();
        }
        #endregion

        #region Lấy data
        public async Task<List<Nganhhang>> GetAllNganhhang()
        {
            return await db.Nganhhangs.ToListAsync();
        }
        #endregion

        #region Tìm kiếm dữ liệu
        public async Task<Nganhhang> findByID(string id)
        {
            return await db.Nganhhangs.FirstOrDefaultAsync(x => x.Manganh == id);
        }
        #endregion
    }
}
