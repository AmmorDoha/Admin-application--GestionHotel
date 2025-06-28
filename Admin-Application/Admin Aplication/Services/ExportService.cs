// Services/ExportService.cs
using OfficeOpenXml;
using CsvHelper;
using System.IO;
using System.Globalization;
using AdminApplication.Data;

namespace AdminApplication.Services
{
    public class ExportService
    {
        private readonly AdminHotelDbContext _context;

        public ExportService(AdminHotelDbContext context)
        {
            _context = context;
        }

        public void ExportToExcel(string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                // Customers worksheet
                var customersSheet = package.Workbook.Worksheets.Add("Customers");
                var customers = _context.Customers.ToList();
                customersSheet.Cells["A1"].LoadFromCollection(customers, true);

                // Rooms worksheet
                var roomsSheet = package.Workbook.Worksheets.Add("Rooms");
                var rooms = _context.Rooms.ToList();
                roomsSheet.Cells["A1"].LoadFromCollection(rooms, true);

                // Save the file
                FileInfo excelFile = new FileInfo(filePath);
                package.SaveAs(excelFile);
            }
        }

        public void ExportToCsv<T>(string filePath, IEnumerable<T> data)
        {
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(data);
            }
        }
    }
}