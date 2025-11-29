using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Webapp.Models;

namespace Webapp.Services
{
    public class EmployeePdfService
    {
        public EmployeePdfService()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public byte[] GeneratePdf(Employee e)
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(20);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(12));

                page.Header()
                    .Text("Employee Details")
                    .FontSize(20)
                    .Bold()
                    .AlignCenter();

                page.Content()
                    .PaddingVertical(15)
                    .Column(column =>
                    {
                        column.Item().Text($"Name: {e.Nom}");
                        column.Item().Text($"Position: {e.Prenom}");
                        column.Item().Text($"Department: {e.Poste}");
                        column.Item().Text($"Email: {e.Email}");
                    });

                page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.Span("Generated on ");
                        x.Span(DateTime.Now.ToString("MM/dd/yyyy")).Bold();
                    });
            });
        });

        return document.GeneratePdf();
    }
} 
}