using DBDATA.Models;

namespace MUDTEMPLATE.Services
{
    public interface IVatService
    {
        Task<List<Vat>> GetAllVat();
    }
}
