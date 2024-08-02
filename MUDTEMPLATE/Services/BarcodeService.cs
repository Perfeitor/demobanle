using DBDATA.Context;
using DBDATA.Models;
using Microsoft.EntityFrameworkCore;

namespace MUDTEMPLATE.Services
{
    public class BarcodeService : IBarcodeService
    {
        private readonly DATAContext db;
        public BarcodeService(DATAContext _db)
        {
            db = _db;
        }

        #region Get data
        public async Task<List<Barcode>> GetAllBarcode()
        {
            return await db.Barcodes.ToListAsync();
        }
        public async Task<List<Barcode>> GetBarcodeByInfo(string masieuthi, string madonvi)
        {
            return await db.Barcodes.Where(x => x.Masieuthi.ToUpper() == masieuthi.ToUpper() && x.Madonvi == madonvi.ToUpper()).ToListAsync();
        }
        #endregion
        #region Thêm, xoá data
        public async Task AddBarcode(string newBarcodeText, string madonvi, string masieuthi)
        {
            Barcode newBarcode = new Barcode
            {
                Barcode1 = newBarcodeText,
                Madonvi = madonvi,
                Masieuthi = masieuthi
            };
            await db.Barcodes.AddAsync(newBarcode);
            await db.SaveChangesAsync();
        }
        public async Task RemoveBarcode(Barcode barcode)
        {
            db.Barcodes.Remove(barcode);
            await db.SaveChangesAsync();
        }
        #endregion
    }
}
