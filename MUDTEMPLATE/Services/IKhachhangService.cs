using DBDATA.Context;
using DBDATA.Models;

namespace MUDTEMPLATE.Services
{
    public interface IKhachhangService
    {
        Task<List<Khachhang>> GetAllKhachhang();
    }
}
