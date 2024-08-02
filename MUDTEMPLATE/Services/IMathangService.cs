using DBDATA.Models;

namespace MUDTEMPLATE.Services
{
    public interface IMathangService
    {
        Task AddMathang(Mathang newMathang);
        Task EditMathang(Mathang newMathang);
        Task<List<Mathang>> GetAllMathang();
        Task RemoveMathang(Mathang mathang);
    }
}
