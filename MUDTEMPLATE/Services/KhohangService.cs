using DBDATA.Context;
using DBDATA.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace MUDTEMPLATE.Services
{
    public class KhohangService : IKhohangService
    {
        private readonly DATAContext db;
        private readonly INguoidungService nguoidungService;

        public KhohangService(DATAContext _db, INguoidungService _nguoidungService)
        {
            db = _db;
            nguoidungService = _nguoidungService;
        }

        #region Thêm, sửa, xoá dữ liệu
        public async Task AddKhohang(Khohang khohang)
        {
            //khohang.Tendangnhap = nguoidungService.GetCookieName();
            //khohang.Madonvi = nguoidungService.GetCookieUnit();
            //var newItem = new Khohang
            //{
            //    Makhohang = khohang.Makhohang,
            //    Tenkhohang = khohang.Tenkhohang,
            //    Maloaikho = khohang.Maloaikho,
            //    Dienthoai = khohang.Dienthoai,
            //    Diachi = khohang.Diachi,
            //    Ghichu = khohang.Ghichu,
            //    Tenhienthihoadon = khohang.Tenhienthihoadon,
            //};
            //await db.Khohangs.AddAsync(newItem);
            await db.SaveChangesAsync();
        }

        public async Task DeleteKhohang(Khohang khohang)
        {
            db.Khohangs.Remove(khohang);
            await db.SaveChangesAsync();
        }

        public async Task EditKhohang(Khohang khohang)
        {
            db.Khohangs.Update(khohang);
            await db.SaveChangesAsync();
        }
        #endregion

        #region Get data
        public async Task<List<Khohang>> GetAllKhohang()
        {
            return await db.Khohangs.ToListAsync();
        }
        #endregion
    }
}
   

