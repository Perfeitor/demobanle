using DBDATA.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;



namespace MUDTEMPLATE.Services
{
    public interface INguoidungService
    {
        Task<bool> LoginAsync(string username, string password, string unit);

        Task LogoutAsync();
        Task<List<Donvi>> GetAllDonvi();
        Task<Nguoidung> FindByInfo(string username, string password, string unit);
        Task<List<Nguoidung>> GetAllNguoidung();
        Task<List<Nhomquyen>> GetAllNhomquyen();
        string EncryptPass(string pass);
        string DecryptPass(string pass);
        Task<List<Nhanvien>> GetAllNhanvien();
        Task<List<Khohang>> GetAllKhohang();
        Task AddNguoidung(Nguoidung nguoidung);
        Task DeleteNguoidung(Nguoidung nguoidung);
        string GetCookieName();
        string GetCookieUnit();
        Task UpdateNguoidung(Nguoidung nguoidung, string? newPass);
    }
}
