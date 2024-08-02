using DBDATA.Models;

namespace MUDTEMPLATE.Services
{
    public interface INganhhangService
    {
        Task AddItem(Nganhhang nganhhang);
        Task DeleteItem(Nganhhang nganhhang);
        Task EditItem(Nganhhang nganhhang);
        Task<Nganhhang> findByID(string id);
        Task<List<Nganhhang>> GetAllNganhhang();
    }
}
