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
                    page.Margin(40);
                    page.DefaultTextStyle(x => x.FontSize(11));

                    // En-tête avec bordure
                    page.Header().Column(col =>
                    {
                        col.Item().Background(Colors.Blue.Medium).Padding(15).Text($"Fiche Employé")
                            .FontSize(22).Bold().FontColor(Colors.White).AlignCenter();
                        col.Item().PaddingTop(10).Text($"{e.Nom} {e.Prenom}")
                            .FontSize(18).SemiBold().FontColor(Colors.Blue.Darken2).AlignCenter();
                        col.Item().PaddingTop(5).LineHorizontal(2).LineColor(Colors.Blue.Medium);
                    });

                    // Contenu en tableau
                    page.Content().PaddingVertical(20).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(150);
                            columns.RelativeColumn();
                        });

                        void AddRow(string label, string value, bool withLine = true)
                        {
                            table.Cell().Background(Colors.Grey.Lighten3).Padding(10)
                                .Text(label).SemiBold().FontSize(12);
                            table.Cell().Padding(10).Text(value).FontSize(12);
                            
                            if (withLine)
                            {
                                table.Cell().ColumnSpan(2).PaddingVertical(2)
                                    .LineHorizontal(0.5f).LineColor(Colors.Grey.Lighten1);
                            }
                        }

                        AddRow("Nom", e.Nom);
                        AddRow("Prénom", e.Prenom);
                        AddRow("Poste", e.Poste);
                        AddRow("Email", e.Email);
                        AddRow("Téléphone", e.Telephone ?? "N/A");
                        AddRow("Département", e.Departement ?? "N/A");
                        AddRow("Date d'embauche", e.DateEmbauche.ToString("dd/MM/yyyy"));
                        AddRow("Statut", e.IsActif ? "Actif" : "Inactif", false);
                    });

                    // Pied de page
                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.Span("Document généré le ");
                        x.Span(DateTime.Now.ToString("dd/MM/yyyy à HH:mm")).Bold();
                        x.Span(" | Page ");
                        x.CurrentPageNumber();
                    });
                });
            });

            return document.GeneratePdf();
        }
    }

    public class EquipementPdfService
    {
        public EquipementPdfService()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public byte[] Generatepdf(Equipment e)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(40);
                    page.DefaultTextStyle(x => x.FontSize(11));

                    // En-tête avec bordure
                    page.Header().Column(col =>
                    {
                        col.Item().Background(Colors.Green.Medium).Padding(15).Text($"Fiche Équipement")
                            .FontSize(22).Bold().FontColor(Colors.White).AlignCenter();
                        col.Item().PaddingTop(10).Text($"{e.Type} - {e.Marque}")
                            .FontSize(18).SemiBold().FontColor(Colors.Green.Darken2).AlignCenter();
                        col.Item().PaddingTop(5).LineHorizontal(2).LineColor(Colors.Green.Medium);
                    });

                    // Contenu en tableau
                    page.Content().PaddingVertical(20).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(150);
                            columns.RelativeColumn();
                        });

                        void AddRow(string label, string value, bool withLine = true)
                        {
                            table.Cell().Background(Colors.Grey.Lighten3).Padding(10)
                                .Text(label).SemiBold().FontSize(12);
                            table.Cell().Padding(10).Text(value).FontSize(12);
                            
                            if (withLine)
                            {
                                table.Cell().ColumnSpan(2).PaddingVertical(2)
                                    .LineHorizontal(0.5f).LineColor(Colors.Grey.Lighten1);
                            }
                        }

                        AddRow("Type", e.Type);
                        AddRow("Marque", e.Marque);
                        AddRow("Modèle", e.Modele ?? "N/A");
                        AddRow("Numéro de série", e.NumeroSerie ?? "N/A");
                        AddRow("Statut", e.Statut);
                        AddRow("Date d'achat", e.DateAchat == null ? "N/A" : e.DateAchat.Value.ToString("dd/MM/yyyy"));
                        AddRow("Description", e.Description ?? "N/A", false);
                    });

                    // Pied de page
                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.Span("Document généré le ");
                        x.Span(DateTime.Now.ToString("dd/MM/yyyy à HH:mm")).Bold();
                        x.Span(" | Page ");
                        x.CurrentPageNumber();
                    });
                });
            });

            return document.GeneratePdf();
        }
    }
}