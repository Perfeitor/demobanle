using DBDATA.Context;
using DBDATA.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace MUDTEMPLATE.Services
{
    public class NhomhangService : INhomhangService
    {
        private readonly DATAContext db;
        private readonly INguoidungService nguoidungService;
        public NhomhangService(DATAContext _db, INguoidungService _nguoidungService)
        {
            db = _db;
            nguoidungService = _nguoidungService;
        }

        #region Thêm, sửa, xoá dữ liệu
        public async Task AddNhomhang(Nhomhang nhomhang)
        {
            //nhomhang.Tendangnhap = nguoidungService.GetCookieName();
            //nhomhang.Madonvi = nguoidungService.GetCookieUnit();
            //var newItem = new Nhomhang
            //{
            //    Manhomhang = nhomhang.Manhomhang,
            //    Tennhomhang = nhomhang.Tennhomhang,
            //    Tylecpvc = nhomhang.Tylecpvc,
            //    Manganh = nhomhang.Manganh,
            //    Ngaytao = DateTime.UtcNow,
            //    Tendangnhap = nhomhang.Tendangnhap,
            //    Mota = nhomhang.Mota,
            //};
            //await db.Nhomhangs.AddAsync(newItem);
            await db.SaveChangesAsync();
        }

        public async Task DeleteNhomhang(Nhomhang nhomhang)
        {
            db.Nhomhangs.Remove(nhomhang);
            await db.SaveChangesAsync();
        }

        public async Task EditNhomhang(Nhomhang nhomhang)
        {
            db.Nhomhangs.Update(nhomhang);
            await db.SaveChangesAsync();
        }
        #endregion

        #region Get data
        public async Task<List<Nhomhang>> GetAllNhomhang()
        {
            return await db.Nhomhangs.ToListAsync();
        }
        #endregion
    }
}
