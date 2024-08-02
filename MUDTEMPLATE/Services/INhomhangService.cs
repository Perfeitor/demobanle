using DBDATA.Models;

namespace MUDTEMPLATE.Services
{
    public interface INhomhangService
    {
        Task AddNhomhang(Nhomhang nhomhang);
        Task DeleteNhomhang(Nhomhang nhomhang);
        Task EditNhomhang(Nhomhang nhomhang);
        Task<List<Nhomhang>> GetAllNhomhang();
    }
}