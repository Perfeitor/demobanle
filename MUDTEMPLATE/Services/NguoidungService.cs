using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using MUDTEMPLATE.Services;
using System.Security.Claims;
using DBDATA.Models;
using DBDATA.Context;
using Microsoft.EntityFrameworkCore;
using MUDTEMPLATE.Shared.Lib;
using Microsoft.AspNetCore.Authorization;

public class NguoidungService : INguoidungService
{
    #region Khởi tạo
    private readonly DATAContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public NguoidungService(DATAContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }
    #endregion

    #region Đăng nhập, đăng xuất
    public async Task<bool> LoginAsync(string username, string password, string unit)
    {
        try
        {
            // Tìm người dùng trong cơ sở dữ liệu
            var user = await FindByInfo(username, password, unit);

            if (user != null)
            {
                // Tạo các claim cho việc xác thực
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Tendangnhap),
                    new Claim(ClaimTypes.Role, user.Manhomquyen),
                    new Claim("unit", user.Madonvi)
                    // Thêm các claim khác nếu cần
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    // Cấu hình các thuộc tính như hết hạn, ...
                    ExpiresUtc = DateTime.UtcNow.AddDays(7),
                };

                // Đăng nhập thành công, lưu thông tin vào cookie
                await _httpContextAccessor.HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return true;
            }

            return false;
        }
        catch(Exception ex)
        {
            return false;
        }
    }

    public async Task LogoutAsync()
    {
        // Đăng xuất, xóa cookie
        await _httpContextAccessor.HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);
    }
    #endregion

    #region Cookie
    public string GetCookieName()
    {
        return _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "";
    }

    public string GetCookieUnit()
    {
        var unitClaim = _httpContextAccessor?.HttpContext?.User?.FindFirst("unit") ?? null;
        var t = unitClaim?.Value ?? "0003";
        return t;
    }
    #endregion

    public async Task<Nguoidung> FindByInfo(string username, string password, string unit)
    {
        try
        {
            // Tìm người dùng trong cơ sở dữ liệu
            var EnMatkhau = new DatSymmetric().EncryptData(string.Empty, password);
            var user = await _dbContext.Nguoidungs.FirstOrDefaultAsync(u => u.Tendangnhap == username && u.Matkhau == EnMatkhau && u.Madonvi == unit);
            return user;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public async Task<List<Donvi>> GetAllDonvi()
    {
        try
        {
            var result = await _dbContext.Donvis.ToListAsync();
            return result;
        }
        catch (Exception)
        {
            return new List<Donvi>();
        }
    }

    public string EncryptPass(string pass)
    {
        return new DatSymmetric().EncryptData(string.Empty, pass);
    }


    #region Phương thức yêu cầu bảo mật
    [Authorize]

    public string DecryptPass(string pass)
    {
        return new DatSymmetric().DecryptData(string.Empty, pass);
    }
    public async Task<List<Nguoidung>> GetAllNguoidung()
    {
        try
        {
            var result = await _dbContext.Nguoidungs.ToListAsync();
            return result;
        }
        catch (Exception)
        {
            return new List<Nguoidung>();
        }
    }
    public async Task<List<Nhomquyen>> GetAllNhomquyen()
    {
        try
        {
            var result = await _dbContext.Nhomquyens.ToListAsync();
            return result;
        }
        catch (Exception)
        {
            return new List<Nhomquyen>();
        }
    }
    public async Task<List<Nhanvien>> GetAllNhanvien()
    {
        try
        {
            var result = await _dbContext.Nhanviens.ToListAsync();
            return result;
        }
        catch (Exception)
        {
            return new List<Nhanvien>();
        }
    }
    public async Task<List<Khohang>> GetAllKhohang()
    {
        try
        {
            var result = await _dbContext.Khohangs.ToListAsync();
            return result;
        }
        catch (Exception)
        {
            return new List<Khohang>();
        }
    }
    public async Task AddNguoidung(Nguoidung nguoidung)
    {
        nguoidung.Matkhau = new DatSymmetric().EncryptData(string.Empty, nguoidung.Matkhau);
        await _dbContext.Nguoidungs.AddAsync(nguoidung);
        await _dbContext.SaveChangesAsync();
    }
    public async Task UpdateNguoidung(Nguoidung nguoidung, string? newPass)
    {
        nguoidung.Matkhau = new DatSymmetric().EncryptData(string.Empty, newPass);
        _dbContext.Nguoidungs.Update(nguoidung);
        await _dbContext.SaveChangesAsync();
    }
    public async Task DeleteNguoidung(Nguoidung nguoidung)
    {
        _dbContext.Nguoidungs.Remove(nguoidung);
        await _dbContext.SaveChangesAsync();
    }
    #endregion
}
