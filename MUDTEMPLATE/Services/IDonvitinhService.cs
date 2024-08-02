using DBDATA.Models;

namespace MUDTEMPLATE.Services
{
    public interface IDonvitinhService
    {
        Task<List<Donvitinh>> GetAllDonvitinh();
    }
}
