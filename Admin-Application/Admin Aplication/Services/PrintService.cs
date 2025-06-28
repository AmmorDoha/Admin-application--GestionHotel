using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using AdminApplication.Models;

namespace AdminApplication.Services
{
    public class PrintService
    {
        public void PrintReservationVoucher(Reservation reservation, string filePath)
        {
            using (var writer = new PdfWriter(filePath))
            using (var pdf = new PdfDocument(writer))
            using (var document = new Document(pdf))
            {
                document.Add(new Paragraph()
                    .Add("HOTEL RESERVATION VOUCHER")
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(20));

                document.Add(new Paragraph()
                    .Add("Customer Information")
                    .SetFontSize(14));

                document.Add(new Paragraph()
                    .Add($"Name: {reservation.Customer?.FirstName} {reservation.Customer?.LastName}"));
                document.Add(new Paragraph()
                    .Add($"Email: {reservation.Customer?.Email}"));
                document.Add(new Paragraph()
                    .Add($"Phone: {reservation.Customer?.Phone}"));

                document.Add(new Paragraph()
                    .Add("Reservation Details")
                    .SetFontSize(14));

                document.Add(new Paragraph()
                    .Add($"Room Number: {reservation.Room?.RoomNumber}"));
                document.Add(new Paragraph()
                    .Add($"Check-in Date: {reservation.CheckInDate:dd/MM/yyyy}"));
                document.Add(new Paragraph()
                    .Add($"Check-out Date: {reservation.CheckOutDate:dd/MM/yyyy}"));
                document.Add(new Paragraph()
                    .Add($"Total Price: ${reservation.TotalPrice:F2}"));

                document.Add(new Paragraph()
                    .Add($"Generated on: {DateTime.Now:dd/MM/yyyy HH:mm}")
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .SetFontSize(10));
            }
        }
    }
}