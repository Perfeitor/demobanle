using DBDATA.Models;

namespace MUDTEMPLATE.Services
{
    public interface IKhohangService
    {
        Task AddKhohang(Khohang khohang);
        Task DeleteKhohang(Khohang khohang);
        Task EditKhohang(Khohang khohang);
        Task<List<Khohang>> GetAllKhohang();
    }
}
