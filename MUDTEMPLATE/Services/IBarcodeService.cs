using DBDATA.Models;

namespace MUDTEMPLATE.Services
{
    public interface IBarcodeService
    {
        Task AddBarcode(string newBarcodeText, string madonvi, string masieuthi);
        Task<List<Barcode>> GetAllBarcode();
        Task<List<Barcode>> GetBarcodeByInfo(string masieuthi, string madonvi);
        Task RemoveBarcode(Barcode barcode);
    }
}
