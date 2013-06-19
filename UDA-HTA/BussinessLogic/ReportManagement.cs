using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using Entities;
using DataAccess;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using DocumentFormat.OpenXml.Wordprocessing;
using PageSize = DocumentFormat.OpenXml.Wordprocessing.PageSize;

namespace BussinessLogic
{
    public class ReportManagement
    {
        public Report getReport(int idReport)
        {
            UdaHtaDataAccess uhda = new UdaHtaDataAccess();
            return uhda.getReport(idReport);
        }

        public void AddDrug(int type, string name)
        {
            UdaHtaDataAccess uhda = new UdaHtaDataAccess();
            uhda.insertDrug(name, type);
        }

        public void DeleteDrug(string name)
        {
        }

        public void EditDrug(string type, string name)
        {
        }


        public ICollection<Report> ListPatientReports(long idPatient)
        {
            UdaHtaDataAccess uda = new UdaHtaDataAccess();
            return uda.GetReportsByPatientId(idPatient);
        }

        public void PrintReport(int idReport)
        {
        }

        public bool generateDocument(Report report, string filePath)
        {
            using (
                WordprocessingDocument document = WordprocessingDocument.Create(filePath,
                                                                                WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainDocumentPart1 = document.AddMainDocumentPart();
                Document document1 = new Document();
                Body body1 = new Body();
                //Create a Table
                Table table1 = new Table();


                /********************************************************/

                TableProperties tableProperties1 = new TableProperties();
                TableStyle tableStyle1 = new TableStyle() {Val = "TableGrid"};
                TableWidth tableWidth1 = new TableWidth() {Width = "0", Type = TableWidthUnitValues.Auto};
                TableLook tableLook1 = new TableLook() {Val = "04A0"};

                tableProperties1.Append(tableStyle1);
                tableProperties1.Append(tableWidth1);
                tableProperties1.Append(tableLook1);

                TableGrid tableGrid1 = new TableGrid();
                GridColumn gridColumn1 = new GridColumn() {Width = "4788"};
                GridColumn gridColumn2 = new GridColumn() {Width = "4788"};

                tableGrid1.Append(gridColumn1);
                tableGrid1.Append(gridColumn2);

                TableRow tableRow1 = new TableRow()
                    {
                        RsidTableRowAddition = "004D2B75",
                        RsidTableRowProperties = "00EC01A4"
                    };

                TableRowProperties tableRowProperties1 = new TableRowProperties();
                TableRowHeight tableRowHeight1 = new TableRowHeight() {Val = (UInt32Value) 547U};

                tableRowProperties1.Append(tableRowHeight1);

                TableCell tableCell1 = new TableCell();

                TableCellProperties tableCellProperties1 = new TableCellProperties();
                TableCellWidth tableCellWidth1 = new TableCellWidth() {Width = "9576", Type = TableWidthUnitValues.Dxa};
                GridSpan gridSpan1 = new GridSpan() {Val = 2};

                TableCellBorders tableCellBorders1 = new TableCellBorders();
                
                tableCellBorders1.Append(new TopBorder() { Val = BorderValues.Nil });
                tableCellBorders1.Append(new LeftBorder() { Val = BorderValues.Nil });
                tableCellBorders1.Append(new BottomBorder() { Val = BorderValues.Nil });
                tableCellBorders1.Append(new RightBorder() { Val = BorderValues.Nil });
                TableCellVerticalAlignment tableCellVerticalAlignment1 = new TableCellVerticalAlignment()
                    {
                        Val = TableVerticalAlignmentValues.Center
                    };

                tableCellProperties1.Append(tableCellWidth1);
                tableCellProperties1.Append(gridSpan1);
                tableCellProperties1.Append(tableCellBorders1);
                tableCellProperties1.Append(tableCellVerticalAlignment1);

                Paragraph paragraph1 = new Paragraph()
                    {
                        RsidParagraphMarkRevision = "004D2B75",
                        RsidParagraphAddition = "004D2B75",
                        RsidParagraphProperties = "004D2B75",
                        RsidRunAdditionDefault = "004D2B75"
                    };

                ParagraphProperties paragraphProperties1 = new ParagraphProperties();
                Justification justification1 = new Justification() {Val = JustificationValues.Center};

                ParagraphMarkRunProperties paragraphMarkRunProperties1 = new ParagraphMarkRunProperties();
                
                paragraphMarkRunProperties1.Append(new Bold());
                paragraphMarkRunProperties1.Append(new FontSize() { Val = "32" });
                paragraphMarkRunProperties1.Append(new FontSizeComplexScript() { Val = "32" });

                paragraphProperties1.Append(justification1);
                paragraphProperties1.Append(paragraphMarkRunProperties1);

                Run run1 = new Run() {RsidRunProperties = "004D2B75"};

                RunProperties runProperties1 = new RunProperties();

                runProperties1.Append(new Bold());
                runProperties1.Append(new FontSize() {Val = "32"});
                runProperties1.Append(new FontSizeComplexScript() {Val = "32"});
                Text text1 = new Text();
                text1.Text = "Informe de Hipertension Arterial";

                run1.Append(runProperties1);
                run1.Append(text1);

                paragraph1.Append(paragraphProperties1);
                paragraph1.Append(run1);

                tableCell1.Append(tableCellProperties1);
                tableCell1.Append(paragraph1);

                tableRow1.Append(tableRowProperties1);
                tableRow1.Append(tableCell1);

                TableRow tableRow2 = new TableRow()
                    {
                        RsidTableRowAddition = "004D2B75",
                        RsidTableRowProperties = "00AA46B2"
                    };

                TableCell tableCell2 = new TableCell();

                TableCellProperties tableCellProperties2 = new TableCellProperties();

                TableCellBorders tableCellBorders2 = new TableCellBorders();
                BottomBorder bottomBorder2 = new BottomBorder()
                    {
                        Val = BorderValues.Single,
                        Color = "auto",
                        Size = (UInt32Value) 4U,
                        Space = (UInt32Value) 0U
                    };

                tableCellBorders2.Append(new TopBorder() {Val = BorderValues.Nil});
                tableCellBorders2.Append(new LeftBorder() {Val = BorderValues.Nil});
                tableCellBorders2.Append(bottomBorder2);
                tableCellBorders2.Append(new RightBorder() {Val = BorderValues.Nil});

                tableCellProperties2.Append(new TableCellWidth() {Width = "4788", Type = TableWidthUnitValues.Dxa});
                tableCellProperties2.Append(tableCellBorders2);
                Paragraph paragraph2 = new Paragraph()
                    {
                        RsidParagraphAddition = "004D2B75",
                        RsidParagraphProperties = "00DF0ACA",
                        RsidRunAdditionDefault = "004D2B75"
                    };

                tableCell2.Append(tableCellProperties2);
                tableCell2.Append(paragraph2);

                TableCell tableCell3 = new TableCell();

                TableCellProperties tableCellProperties3 = new TableCellProperties();

                TableCellBorders tableCellBorders3 = new TableCellBorders();
                BottomBorder bottomBorder3 = new BottomBorder()
                    {
                        Val = BorderValues.Single,
                        Color = "auto",
                        Size = (UInt32Value) 4U,
                        Space = (UInt32Value) 0U
                    };

                tableCellBorders3.Append(new TopBorder() {Val = BorderValues.Nil});
                tableCellBorders3.Append(new LeftBorder() {Val = BorderValues.Nil});
                tableCellBorders3.Append(bottomBorder3);
                tableCellBorders3.Append(new RightBorder() {Val = BorderValues.Nil});

                tableCellProperties3.Append(new TableCellWidth() { Width = "4788", Type = TableWidthUnitValues.Dxa });
                tableCellProperties3.Append(tableCellBorders3);
                Paragraph paragraph3 = new Paragraph()
                    {
                        RsidParagraphAddition = "004D2B75",
                        RsidParagraphProperties = "00DF0ACA",
                        RsidRunAdditionDefault = "004D2B75"
                    };

                tableCell3.Append(tableCellProperties3);
                tableCell3.Append(paragraph3);

                tableRow2.Append(tableCell2);
                tableRow2.Append(tableCell3);

                TableRow tableRow3 = new TableRow()
                    {
                        RsidTableRowAddition = "00AA46B2",
                        RsidTableRowProperties = "00EC01A4"
                    };

                TableCell tableCell4 = new TableCell();

                TableCellProperties tableCellProperties4 = new TableCellProperties();
                
                TableCellBorders tableCellBorders4 = new TableCellBorders();
                TopBorder topBorder4 = new TopBorder()
                    {
                        Val = BorderValues.Single,
                        Color = "auto",
                        Size = (UInt32Value) 4U,
                        Space = (UInt32Value) 0U
                    };

                tableCellBorders4.Append(topBorder4);
                tableCellBorders4.Append(new LeftBorder() {Val = BorderValues.Nil});
                tableCellBorders4.Append(new BottomBorder() {Val = BorderValues.Nil});
                tableCellBorders4.Append(new RightBorder() {Val = BorderValues.Nil});

                tableCellProperties4.Append(new TableCellWidth() {Width = "4788", Type = TableWidthUnitValues.Dxa});
                tableCellProperties4.Append(tableCellBorders4);

                Paragraph paragraph4 = new Paragraph()
                    {
                        RsidParagraphAddition = "00AA46B2",
                        RsidParagraphProperties = "00DF0ACA",
                        RsidRunAdditionDefault = "00AA46B2"
                    };

                Run run5 = new Run();
                Text text5 = new Text();
                if (report.Patient.RegisterNumer != null)
                {
                    text5.Text = "Nro. Registro: " + report.Patient.RegisterNumer.ToString();
                }
                else
                {
                    text5.Text = "Nro. Registro: N/E";
                }

                run5.Append(text5);
                paragraph4.Append(run5);
                tableCell4.Append(tableCellProperties4);
                tableCell4.Append(paragraph4);

                TableCell tableCell5 = new TableCell();

                TableCellProperties tableCellProperties5 = new TableCellProperties();
                
                TableCellBorders tableCellBorders5 = new TableCellBorders();
                TopBorder topBorder5 = new TopBorder()
                    {
                        Val = BorderValues.Single,
                        Color = "auto",
                        Size = (UInt32Value) 4U,
                        Space = (UInt32Value) 0U
                    };

                tableCellBorders5.Append(topBorder5);
                tableCellBorders5.Append(new LeftBorder() {Val = BorderValues.Nil});
                tableCellBorders5.Append(new BottomBorder() {Val = BorderValues.Nil});
                tableCellBorders5.Append(new RightBorder() {Val = BorderValues.Nil});

                tableCellProperties5.Append(new TableCellWidth() { Width = "4788", Type = TableWidthUnitValues.Dxa });
                tableCellProperties5.Append(tableCellBorders5);
                Paragraph paragraph5 = new Paragraph()
                    {
                        RsidParagraphAddition = "00AA46B2",
                        RsidParagraphProperties = "00DF0ACA",
                        RsidRunAdditionDefault = "00AA46B2"
                    };

                tableCell5.Append(tableCellProperties5);
                tableCell5.Append(paragraph5);

                tableRow3.Append(tableCell4);
                tableRow3.Append(tableCell5);

                TableRow tableRow4 = new TableRow()
                    {
                        RsidTableRowAddition = "004D2B75",
                        RsidTableRowProperties = "00AA46B2"
                    };

                TableCell tableCell6 = new TableCell();

                TableCellProperties tableCellProperties6 = new TableCellProperties();

                TableCellBorders tableCellBorders6 = new TableCellBorders();

                tableCellBorders6.Append(new TopBorder() {Val = BorderValues.Nil});
                tableCellBorders6.Append(new LeftBorder() {Val = BorderValues.Nil});
                tableCellBorders6.Append(new BottomBorder() {Val = BorderValues.Nil});
                tableCellBorders6.Append(new RightBorder() {Val = BorderValues.Nil});

                tableCellProperties6.Append(new TableCellWidth() {Width = "4788", Type = TableWidthUnitValues.Dxa});
                tableCellProperties6.Append(tableCellBorders6);

                Paragraph paragraph6 = new Paragraph()
                    {
                        RsidParagraphAddition = "004D2B75",
                        RsidParagraphProperties = "00AA46B2",
                        RsidRunAdditionDefault = "00AA46B2"
                    };

                Run run9 = new Run();
                Text text9 = new Text();
                if (report.Patient.DocumentId != null)
                {
                    text9.Text = "Documento: " + report.Patient.DocumentId;
                }
                else
                {
                    text9.Text = "Documento: N/E";
                }
                
                run9.Append(text9);
                paragraph6.Append(run9);
                tableCell6.Append(tableCellProperties6);
                tableCell6.Append(paragraph6);

                TableCell tableCell7 = new TableCell();

                TableCellProperties tableCellProperties7 = new TableCellProperties();

                TableCellBorders tableCellBorders7 = new TableCellBorders();
                
                tableCellBorders7.Append(new TopBorder() {Val = BorderValues.Nil});
                tableCellBorders7.Append(new LeftBorder() {Val = BorderValues.Nil});
                tableCellBorders7.Append(new BottomBorder() {Val = BorderValues.Nil});
                tableCellBorders7.Append(new RightBorder() {Val = BorderValues.Nil});

                tableCellProperties7.Append(new TableCellWidth() {Width = "4788", Type = TableWidthUnitValues.Dxa});
                tableCellProperties7.Append(tableCellBorders7);

                Paragraph paragraph7 = new Paragraph()
                    {
                        RsidParagraphAddition = "004D2B75",
                        RsidParagraphProperties = "00DF0ACA",
                        RsidRunAdditionDefault = "004D2B75"
                    };

                Run run11 = new Run();
                Text text11 = new Text();
                if (report.Patient.BirthDate != null)
                {
                    text11.Text = "Fecha de Nacimiento: " + report.Patient.BirthDate.Value.ToString(ConfigurationManager.AppSettings["ShortDateString"]);
                }
                else
                {
                    text11.Text = "Fecha de Nacimiento: N/E";    
                }
                
                run11.Append(text11);

                paragraph7.Append(run11);
                tableCell7.Append(tableCellProperties7);
                tableCell7.Append(paragraph7);

                tableRow4.Append(tableCell6);
                tableRow4.Append(tableCell7);

                TableRow tableRow5 = new TableRow()
                    {
                        RsidTableRowAddition = "004D2B75",
                        RsidTableRowProperties = "00EC01A4"
                    };

                TableCell tableCell8 = new TableCell();

                TableCellProperties tableCellProperties8 = new TableCellProperties();

                TableCellBorders tableCellBorders8 = new TableCellBorders();

                tableCellBorders8.Append(new TopBorder() {Val = BorderValues.Nil});
                tableCellBorders8.Append(new LeftBorder() {Val = BorderValues.Nil});
                tableCellBorders8.Append(new BottomBorder() {Val = BorderValues.Nil});
                tableCellBorders8.Append(new RightBorder() {Val = BorderValues.Nil});

                tableCellProperties8.Append(new TableCellWidth() {Width = "4788", Type = TableWidthUnitValues.Dxa});
                tableCellProperties8.Append(tableCellBorders8);

                Paragraph paragraph8 = new Paragraph()
                    {
                        RsidParagraphAddition = "004D2B75",
                        RsidParagraphProperties = "00DF0ACA",
                        RsidRunAdditionDefault = "004D2B75"
                    };

                Run run15 = new Run();
                Text text15 = new Text();
                if (report.Patient.Surnames != null)
                {
                    text15.Text = "Apellidos: " + report.Patient.Surnames;
                }
                else
                {
                    text15.Text = "Apellidos: N/E";
                }
                

                run15.Append(text15);
                paragraph8.Append(run15);

                tableCell8.Append(tableCellProperties8);
                tableCell8.Append(paragraph8);

                TableCell tableCell9 = new TableCell();

                TableCellProperties tableCellProperties9 = new TableCellProperties();

                TableCellBorders tableCellBorders9 = new TableCellBorders();

                tableCellBorders9.Append(new TopBorder() {Val = BorderValues.Nil});
                tableCellBorders9.Append(new LeftBorder() {Val = BorderValues.Nil});
                tableCellBorders9.Append(new BottomBorder() {Val = BorderValues.Nil});
                tableCellBorders9.Append(new RightBorder() {Val = BorderValues.Nil});

                tableCellProperties9.Append(new TableCellWidth() {Width = "4788", Type = TableWidthUnitValues.Dxa});
                tableCellProperties9.Append(tableCellBorders9);

                Paragraph paragraph9 = new Paragraph()
                    {
                        RsidParagraphAddition = "004D2B75",
                        RsidParagraphProperties = "00DF0ACA",
                        RsidRunAdditionDefault = "004D2B75"
                    };

                Run run17 = new Run();
                Text text17 = new Text();
                if (report.TemporaryData.Weight != null)
                {
                    text17.Text = "Peso: " + report.TemporaryData.Weight.ToString();
                }
                else
                {
                    text17.Text = "Peso: N/E";
                }
                
                run17.Append(text17);

                paragraph9.Append(run17);

                tableCell9.Append(tableCellProperties9);
                tableCell9.Append(paragraph9);

                tableRow5.Append(tableCell8);
                tableRow5.Append(tableCell9);

                TableRow tableRow6 = new TableRow()
                    {
                        RsidTableRowAddition = "004D2B75",
                        RsidTableRowProperties = "00EC01A4"
                    };

                TableCell tableCell10 = new TableCell();

                TableCellProperties tableCellProperties10 = new TableCellProperties();

                TableCellBorders tableCellBorders10 = new TableCellBorders();
                
                tableCellBorders10.Append(new TopBorder() {Val = BorderValues.Nil});
                tableCellBorders10.Append(new LeftBorder() {Val = BorderValues.Nil});
                tableCellBorders10.Append(new BottomBorder() {Val = BorderValues.Nil});
                tableCellBorders10.Append(new RightBorder() {Val = BorderValues.Nil});

                tableCellProperties10.Append(new TableCellWidth() {Width = "4788", Type = TableWidthUnitValues.Dxa});
                tableCellProperties10.Append(tableCellBorders10);

                Paragraph paragraph10 = new Paragraph()
                    {
                        RsidParagraphAddition = "004D2B75",
                        RsidParagraphProperties = "00DF0ACA",
                        RsidRunAdditionDefault = "004D2B75"
                    };

                Run run18 = new Run();
                Text text18 = new Text();
                if (report.Patient.Names != null)
                {
                    text18.Text = "Nombre: " + report.Patient.Names;
                }
                else
                {
                    text18.Text = "Nombre: N/E";
                }
                
                run18.Append(text18);

                paragraph10.Append(run18);

                tableCell10.Append(tableCellProperties10);
                tableCell10.Append(paragraph10);

                TableCell tableCell11 = new TableCell();

                TableCellProperties tableCellProperties11 = new TableCellProperties();
                TableCellWidth tableCellWidth11 = new TableCellWidth() {Width = "4788", Type = TableWidthUnitValues.Dxa};

                TableCellBorders tableCellBorders11 = new TableCellBorders();
                TopBorder topBorder11 = new TopBorder() {Val = BorderValues.Nil};
                LeftBorder leftBorder11 = new LeftBorder() {Val = BorderValues.Nil};
                BottomBorder bottomBorder11 = new BottomBorder() {Val = BorderValues.Nil};
                RightBorder rightBorder11 = new RightBorder() {Val = BorderValues.Nil};

                tableCellBorders11.Append(topBorder11);
                tableCellBorders11.Append(leftBorder11);
                tableCellBorders11.Append(bottomBorder11);
                tableCellBorders11.Append(rightBorder11);

                tableCellProperties11.Append(tableCellWidth11);
                tableCellProperties11.Append(tableCellBorders11);

                Paragraph paragraph11 = new Paragraph()
                    {
                        RsidParagraphAddition = "004D2B75",
                        RsidParagraphProperties = "00DF0ACA",
                        RsidRunAdditionDefault = "004D2B75"
                    };

                Run run20 = new Run();
                Text text20 = new Text();
                if (report.TemporaryData.Height != null)
                {
                    text20.Text = "Altura: " + report.TemporaryData.Height.ToString();
                }
                else
                {
                    text20.Text = "Altura: N/E";
                }
                
                run20.Append(text20);
                paragraph11.Append(run20);

                tableCell11.Append(tableCellProperties11);
                tableCell11.Append(paragraph11);

                tableRow6.Append(tableCell10);
                tableRow6.Append(tableCell11);

                TableRow tableRow7 = new TableRow()
                    {
                        RsidTableRowAddition = "004D2B75",
                        RsidTableRowProperties = "00EC01A4"
                    };

                TableCell tableCell12 = new TableCell();

                TableCellProperties tableCellProperties12 = new TableCellProperties();
                TableCellWidth tableCellWidth12 = new TableCellWidth() {Width = "4788", Type = TableWidthUnitValues.Dxa};

                TableCellBorders tableCellBorders12 = new TableCellBorders();
                TopBorder topBorder12 = new TopBorder() {Val = BorderValues.Nil};
                LeftBorder leftBorder12 = new LeftBorder() {Val = BorderValues.Nil};
                BottomBorder bottomBorder12 = new BottomBorder() {Val = BorderValues.Nil};
                RightBorder rightBorder12 = new RightBorder() {Val = BorderValues.Nil};

                tableCellBorders12.Append(topBorder12);
                tableCellBorders12.Append(leftBorder12);
                tableCellBorders12.Append(bottomBorder12);
                tableCellBorders12.Append(rightBorder12);

                tableCellProperties12.Append(tableCellWidth12);
                tableCellProperties12.Append(tableCellBorders12);

                Paragraph paragraph12 = new Paragraph()
                    {
                        RsidParagraphAddition = "004D2B75",
                        RsidParagraphProperties = "00DF0ACA",
                        RsidRunAdditionDefault = "004D2B75"
                    };

                Run run22 = new Run();
                Text text22 = new Text();
                if (report.Patient.Address != null)
                {
                    text22.Text = "Domicilio: " + report.Patient.Address;
                }
                else
                {
                    text22.Text = "Domicilio: N/E";
                }
                
                run22.Append(text22);
                paragraph12.Append(run22);

                tableCell12.Append(tableCellProperties12);
                tableCell12.Append(paragraph12);

                TableCell tableCell13 = new TableCell();

                TableCellProperties tableCellProperties13 = new TableCellProperties();
                TableCellWidth tableCellWidth13 = new TableCellWidth() {Width = "4788", Type = TableWidthUnitValues.Dxa};

                TableCellBorders tableCellBorders13 = new TableCellBorders();
                TopBorder topBorder13 = new TopBorder() {Val = BorderValues.Nil};
                LeftBorder leftBorder13 = new LeftBorder() {Val = BorderValues.Nil};
                BottomBorder bottomBorder13 = new BottomBorder() {Val = BorderValues.Nil};
                RightBorder rightBorder13 = new RightBorder() {Val = BorderValues.Nil};

                tableCellBorders13.Append(topBorder13);
                tableCellBorders13.Append(leftBorder13);
                tableCellBorders13.Append(bottomBorder13);
                tableCellBorders13.Append(rightBorder13);

                tableCellProperties13.Append(tableCellWidth13);
                tableCellProperties13.Append(tableCellBorders13);

                Paragraph paragraph13 = new Paragraph()
                    {
                        RsidParagraphAddition = "004D2B75",
                        RsidParagraphProperties = "00DF0ACA",
                        RsidRunAdditionDefault = "004D2B75"
                    };

                Run run24 = new Run();
                Text text24 = new Text();
                if (report.Patient.Sex != null)
                {
                    text24.Text = "Sexo: " + report.Patient.Sex.ToString();
                }
                else
                {
                    text24.Text = "Sexo: N/E";
                }
                
                run24.Append(text24);
                paragraph13.Append(run24);

                tableCell13.Append(tableCellProperties13);
                tableCell13.Append(paragraph13);

                tableRow7.Append(tableCell12);
                tableRow7.Append(tableCell13);

                TableRow tableRow8 = new TableRow()
                    {
                        RsidTableRowAddition = "004D2B75",
                        RsidTableRowProperties = "00EC01A4"
                    };

                TableCell tableCell14 = new TableCell();

                TableCellProperties tableCellProperties14 = new TableCellProperties();
                TableCellWidth tableCellWidth14 = new TableCellWidth() {Width = "4788", Type = TableWidthUnitValues.Dxa};

                TableCellBorders tableCellBorders14 = new TableCellBorders();
                TopBorder topBorder14 = new TopBorder() {Val = BorderValues.Nil};
                LeftBorder leftBorder14 = new LeftBorder() {Val = BorderValues.Nil};
                BottomBorder bottomBorder14 = new BottomBorder() {Val = BorderValues.Nil};
                RightBorder rightBorder14 = new RightBorder() {Val = BorderValues.Nil};

                tableCellBorders14.Append(topBorder14);
                tableCellBorders14.Append(leftBorder14);
                tableCellBorders14.Append(bottomBorder14);
                tableCellBorders14.Append(rightBorder14);

                tableCellProperties14.Append(tableCellWidth14);
                tableCellProperties14.Append(tableCellBorders14);

                Paragraph paragraph14 = new Paragraph()
                    {
                        RsidParagraphAddition = "004D2B75",
                        RsidParagraphProperties = "00DF0ACA",
                        RsidRunAdditionDefault = "004D2B75"
                    };

                Run run26 = new Run();
                Text text26 = new Text();
                if (report.Patient.Phone != null)
                {
                    text26.Text = "Teléfono: " + report.Patient.Phone;
                }
                else
                {
                    text26.Text = "Teléfono: N/E";
                }
                
                run26.Append(text26);
                paragraph14.Append(run26);

                tableCell14.Append(tableCellProperties14);
                tableCell14.Append(paragraph14);

                TableCell tableCell15 = new TableCell();

                TableCellProperties tableCellProperties15 = new TableCellProperties();
                TableCellWidth tableCellWidth15 = new TableCellWidth() {Width = "4788", Type = TableWidthUnitValues.Dxa};

                TableCellBorders tableCellBorders15 = new TableCellBorders();
                TopBorder topBorder15 = new TopBorder() {Val = BorderValues.Nil};
                LeftBorder leftBorder15 = new LeftBorder() {Val = BorderValues.Nil};
                BottomBorder bottomBorder15 = new BottomBorder() {Val = BorderValues.Nil};
                RightBorder rightBorder15 = new RightBorder() {Val = BorderValues.Nil};

                tableCellBorders15.Append(topBorder15);
                tableCellBorders15.Append(leftBorder15);
                tableCellBorders15.Append(bottomBorder15);
                tableCellBorders15.Append(rightBorder15);

                tableCellProperties15.Append(tableCellWidth15);
                tableCellProperties15.Append(tableCellBorders15);

                Paragraph paragraph15 = new Paragraph()
                    {
                        RsidParagraphAddition = "004D2B75",
                        RsidParagraphProperties = "00DF0ACA",
                        RsidRunAdditionDefault = "004D2B75"
                    };

                Run run28 = new Run();
                Text text28 = new Text();
                if (report.Patient.Email != null)
                {
                    text28.Text = "E-mail: " + report.Patient.Email;
                }
                else
                {
                    text28.Text = "E-mail: N/E";

                }

                run28.Append(text28);

                paragraph15.Append(run28);

                tableCell15.Append(tableCellProperties15);
                tableCell15.Append(paragraph15);

                tableRow8.Append(tableCell14);
                tableRow8.Append(tableCell15);

                TableRow tableRow9 = new TableRow()
                    {
                        RsidTableRowAddition = "004D2B75",
                        RsidTableRowProperties = "00EC01A4"
                    };

                TableCell tableCell16 = new TableCell();

                TableCellProperties tableCellProperties16 = new TableCellProperties();
                TableCellWidth tableCellWidth16 = new TableCellWidth() {Width = "9576", Type = TableWidthUnitValues.Dxa};
                GridSpan gridSpan2 = new GridSpan() {Val = 2};

                TableCellBorders tableCellBorders16 = new TableCellBorders();
                TopBorder topBorder16 = new TopBorder() {Val = BorderValues.Nil};
                LeftBorder leftBorder16 = new LeftBorder() {Val = BorderValues.Nil};
                RightBorder rightBorder16 = new RightBorder() {Val = BorderValues.Nil};

                tableCellBorders16.Append(topBorder16);
                tableCellBorders16.Append(leftBorder16);
                tableCellBorders16.Append(rightBorder16);

                tableCellProperties16.Append(tableCellWidth16);
                tableCellProperties16.Append(gridSpan2);
                tableCellProperties16.Append(tableCellBorders16);

                Paragraph paragraph16 = new Paragraph()
                    {
                        RsidParagraphAddition = "004D2B75",
                        RsidParagraphProperties = "00DF0ACA",
                        RsidRunAdditionDefault = "004D2B75"
                    };
                BookmarkStart bookmarkStart1 = new BookmarkStart() {Name = "_GoBack", Id = "0"};
                BookmarkEnd bookmarkEnd1 = new BookmarkEnd() {Id = "0"};

                paragraph16.Append(bookmarkStart1);
                paragraph16.Append(bookmarkEnd1);

                tableCell16.Append(tableCellProperties16);
                tableCell16.Append(paragraph16);

                tableRow9.Append(tableCell16);

                TableRow tableRow10 = new TableRow()
                    {
                        RsidTableRowMarkRevision = "004D2B75",
                        RsidTableRowAddition = "004D2B75",
                        RsidTableRowProperties = "00EC01A4"
                    };

                TableCell tableCell17 = new TableCell();

                TableCellProperties tableCellProperties17 = new TableCellProperties();
                TableCellWidth tableCellWidth17 = new TableCellWidth() {Width = "4788", Type = TableWidthUnitValues.Dxa};

                TableCellBorders tableCellBorders17 = new TableCellBorders();
                LeftBorder leftBorder17 = new LeftBorder() {Val = BorderValues.Nil};
                BottomBorder bottomBorder16 = new BottomBorder()
                    {
                        Val = BorderValues.Single,
                        Color = "auto",
                        Size = (UInt32Value) 4U,
                        Space = (UInt32Value) 0U
                    };

                tableCellBorders17.Append(leftBorder17);
                tableCellBorders17.Append(bottomBorder16);

                tableCellProperties17.Append(tableCellWidth17);
                tableCellProperties17.Append(tableCellBorders17);
                Paragraph paragraph17 = new Paragraph()
                    {
                        RsidParagraphAddition = "004D2B75",
                        RsidParagraphProperties = "00DF0ACA",
                        RsidRunAdditionDefault = "004D2B75"
                    };

                tableCell17.Append(tableCellProperties17);
                tableCell17.Append(paragraph17);

                TableCell tableCell18 = new TableCell();

                TableCellProperties tableCellProperties18 = new TableCellProperties();
                TableCellWidth tableCellWidth18 = new TableCellWidth() {Width = "4788", Type = TableWidthUnitValues.Dxa};

                TableCellBorders tableCellBorders18 = new TableCellBorders();
                BottomBorder bottomBorder17 = new BottomBorder()
                    {
                        Val = BorderValues.Single,
                        Color = "auto",
                        Size = (UInt32Value) 4U,
                        Space = (UInt32Value) 0U
                    };
                RightBorder rightBorder17 = new RightBorder() {Val = BorderValues.Nil};

                tableCellBorders18.Append(bottomBorder17);
                tableCellBorders18.Append(rightBorder17);

                tableCellProperties18.Append(tableCellWidth18);
                tableCellProperties18.Append(tableCellBorders18);

                Paragraph paragraph18 = new Paragraph()
                    {
                        RsidParagraphMarkRevision = "004D2B75",
                        RsidParagraphAddition = "004D2B75",
                        RsidParagraphProperties = "00DF0ACA",
                        RsidRunAdditionDefault = "004D2B75"
                    };

                ParagraphProperties paragraphProperties2 = new ParagraphProperties();

                ParagraphMarkRunProperties paragraphMarkRunProperties2 = new ParagraphMarkRunProperties();
                Bold bold6 = new Bold();

                paragraphMarkRunProperties2.Append(bold6);

                paragraphProperties2.Append(paragraphMarkRunProperties2);

                Run run29 = new Run() {RsidRunProperties = "004D2B75"};

                RunProperties runProperties5 = new RunProperties();
                Bold bold7 = new Bold();

                runProperties5.Append(bold7);
                Text text29 = new Text();
                text29.Text = "24h ABDM";

                run29.Append(runProperties5);
                run29.Append(text29);

                paragraph18.Append(paragraphProperties2);
                paragraph18.Append(run29);

                Paragraph paragraph19 = new Paragraph()
                    {
                        RsidParagraphAddition = "004D2B75",
                        RsidParagraphProperties = "004D2B75",
                        RsidRunAdditionDefault = "004D2B75"
                    };

                ParagraphProperties paragraphProperties3 = new ParagraphProperties();

                ParagraphMarkRunProperties paragraphMarkRunProperties3 = new ParagraphMarkRunProperties();


                paragraphProperties3.Append(paragraphMarkRunProperties3);

                Run run30 = new Run() {RsidRunProperties = "004D2B75"};

                RunProperties runProperties6 = new RunProperties();

                Text text30 = new Text() {Space = SpaceProcessingModeValues.Preserve};
                text30.Text = "Fecha y hora de inicio: ";

                run30.Append(runProperties6);
                run30.Append(text30);

                paragraph19.Append(paragraphProperties3);
                paragraph19.Append(run30);

                Paragraph paragraph20 = new Paragraph()
                    {
                        RsidParagraphMarkRevision = "004D2B75",
                        RsidParagraphAddition = "004D2B75",
                        RsidParagraphProperties = "004D2B75",
                        RsidRunAdditionDefault = "004D2B75"
                    };

                ParagraphProperties paragraphProperties4 = new ParagraphProperties();

                ParagraphMarkRunProperties paragraphMarkRunProperties4 = new ParagraphMarkRunProperties();

                paragraphProperties4.Append(paragraphMarkRunProperties4);

                Run run31 = new Run();

                RunProperties runProperties7 = new RunProperties();

                Text text31 = new Text();
                text31.Text = "Fecha y hora de fin:";

                run31.Append(runProperties7);
                run31.Append(text31);

                paragraph20.Append(paragraphProperties4);
                paragraph20.Append(run31);

                tableCell18.Append(tableCellProperties18);
                tableCell18.Append(paragraph18);
                tableCell18.Append(paragraph19);
                tableCell18.Append(paragraph20);

                tableRow10.Append(tableCell17);
                tableRow10.Append(tableCell18);

                TableRow tableRow11 = new TableRow()
                    {
                        RsidTableRowMarkRevision = "004D2B75",
                        RsidTableRowAddition = "00EC01A4",
                        RsidTableRowProperties = "00EC01A4"
                    };

                TableRowProperties tableRowProperties2 = new TableRowProperties();
                TableRowHeight tableRowHeight2 = new TableRowHeight() {Val = (UInt32Value) 242U};

                tableRowProperties2.Append(tableRowHeight2);

                TableCell tableCell19 = new TableCell();

                TableCellProperties tableCellProperties19 = new TableCellProperties();
                TableCellWidth tableCellWidth19 = new TableCellWidth() {Width = "9576", Type = TableWidthUnitValues.Dxa};
                GridSpan gridSpan3 = new GridSpan() {Val = 2};

                TableCellBorders tableCellBorders19 = new TableCellBorders();
                LeftBorder leftBorder18 = new LeftBorder() {Val = BorderValues.Nil};
                BottomBorder bottomBorder18 = new BottomBorder() {Val = BorderValues.Nil};
                RightBorder rightBorder18 = new RightBorder() {Val = BorderValues.Nil};

                tableCellBorders19.Append(leftBorder18);
                tableCellBorders19.Append(bottomBorder18);
                tableCellBorders19.Append(rightBorder18);
                TableCellVerticalAlignment tableCellVerticalAlignment2 = new TableCellVerticalAlignment()
                    {
                        Val = TableVerticalAlignmentValues.Center
                    };

                tableCellProperties19.Append(tableCellWidth19);
                tableCellProperties19.Append(gridSpan3);
                tableCellProperties19.Append(tableCellBorders19);
                tableCellProperties19.Append(tableCellVerticalAlignment2);

                Paragraph paragraph21 = new Paragraph()
                    {
                        RsidParagraphMarkRevision = "00EC01A4",
                        RsidParagraphAddition = "00EC01A4",
                        RsidParagraphProperties = "00EC01A4",
                        RsidRunAdditionDefault = "00EC01A4"
                    };

                ParagraphProperties paragraphProperties5 = new ParagraphProperties();

                ParagraphMarkRunProperties paragraphMarkRunProperties5 = new ParagraphMarkRunProperties();
                Bold bold8 = new Bold();

                paragraphMarkRunProperties5.Append(bold8);

                paragraphProperties5.Append(paragraphMarkRunProperties5);

                Run run32 = new Run() {RsidRunProperties = "00EC01A4"};

                RunProperties runProperties8 = new RunProperties();
                Bold bold9 = new Bold();

                runProperties8.Append(bold9);
                Text text32 = new Text();
                text32.Text = "Resumen de Medidas";

                run32.Append(runProperties8);
                run32.Append(text32);

                paragraph21.Append(paragraphProperties5);
                paragraph21.Append(run32);

                tableCell19.Append(tableCellProperties19);
                tableCell19.Append(paragraph21);

                tableRow11.Append(tableRowProperties2);
                tableRow11.Append(tableCell19);

                TableRow tableRow12 = new TableRow()
                    {
                        RsidTableRowMarkRevision = "004D2B75",
                        RsidTableRowAddition = "004D2B75",
                        RsidTableRowProperties = "00EC01A4"
                    };

                TableRowProperties tableRowProperties3 = new TableRowProperties();
                TableRowHeight tableRowHeight3 = new TableRowHeight() {Val = (UInt32Value) 1383U};

                tableRowProperties3.Append(tableRowHeight3);

                TableCell tableCell20 = new TableCell();

                TableCellProperties tableCellProperties20 = new TableCellProperties();
                TableCellWidth tableCellWidth20 = new TableCellWidth() {Width = "9576", Type = TableWidthUnitValues.Dxa};
                GridSpan gridSpan4 = new GridSpan() {Val = 2};

                TableCellBorders tableCellBorders20 = new TableCellBorders();
                TopBorder topBorder17 = new TopBorder() {Val = BorderValues.Nil};
                LeftBorder leftBorder19 = new LeftBorder() {Val = BorderValues.Nil};
                BottomBorder bottomBorder19 = new BottomBorder()
                    {
                        Val = BorderValues.Single,
                        Color = "auto",
                        Size = (UInt32Value) 4U,
                        Space = (UInt32Value) 0U
                    };
                RightBorder rightBorder19 = new RightBorder() {Val = BorderValues.Nil};

                tableCellBorders20.Append(topBorder17);
                tableCellBorders20.Append(leftBorder19);
                tableCellBorders20.Append(bottomBorder19);
                tableCellBorders20.Append(rightBorder19);
                TableCellVerticalAlignment tableCellVerticalAlignment3 = new TableCellVerticalAlignment()
                    {
                        Val = TableVerticalAlignmentValues.Center
                    };

                tableCellProperties20.Append(tableCellWidth20);
                tableCellProperties20.Append(gridSpan4);
                tableCellProperties20.Append(tableCellBorders20);
                tableCellProperties20.Append(tableCellVerticalAlignment3);

                Paragraph paragraph22 = new Paragraph()
                    {
                        RsidParagraphMarkRevision = "004D2B75",
                        RsidParagraphAddition = "004D2B75",
                        RsidParagraphProperties = "004D2B75",
                        RsidRunAdditionDefault = "004D2B75"
                    };

                ParagraphProperties paragraphProperties6 = new ParagraphProperties();
                Justification justification2 = new Justification() {Val = JustificationValues.Center};

                ParagraphMarkRunProperties paragraphMarkRunProperties6 = new ParagraphMarkRunProperties();

                paragraphProperties6.Append(justification2);
                paragraphProperties6.Append(paragraphMarkRunProperties6);

                Run run33 = new Run();

                RunProperties runProperties9 = new RunProperties();

                Text text33 = new Text();
                text33.Text = "<Resumen de medidas>";

                run33.Append(runProperties9);
                run33.Append(text33);

                paragraph22.Append(paragraphProperties6);
                paragraph22.Append(run33);

                tableCell20.Append(tableCellProperties20);
                tableCell20.Append(paragraph22);

                tableRow12.Append(tableRowProperties3);
                tableRow12.Append(tableCell20);

                TableRow tableRow13 = new TableRow()
                    {
                        RsidTableRowMarkRevision = "004D2B75",
                        RsidTableRowAddition = "00EC01A4",
                        RsidTableRowProperties = "00EC01A4"
                    };

                TableCell tableCell21 = new TableCell();

                TableCellProperties tableCellProperties21 = new TableCellProperties();
                TableCellWidth tableCellWidth21 = new TableCellWidth() {Width = "9576", Type = TableWidthUnitValues.Dxa};
                GridSpan gridSpan5 = new GridSpan() {Val = 2};

                TableCellBorders tableCellBorders21 = new TableCellBorders();
                LeftBorder leftBorder20 = new LeftBorder() {Val = BorderValues.Nil};
                BottomBorder bottomBorder20 = new BottomBorder() {Val = BorderValues.Nil};
                RightBorder rightBorder20 = new RightBorder() {Val = BorderValues.Nil};

                tableCellBorders21.Append(leftBorder20);
                tableCellBorders21.Append(bottomBorder20);
                tableCellBorders21.Append(rightBorder20);

                tableCellProperties21.Append(tableCellWidth21);
                tableCellProperties21.Append(gridSpan5);
                tableCellProperties21.Append(tableCellBorders21);

                Paragraph paragraph23 = new Paragraph()
                    {
                        RsidParagraphMarkRevision = "004D2B75",
                        RsidParagraphAddition = "00EC01A4",
                        RsidRunAdditionDefault = "00EC01A4"
                    };

                ParagraphProperties paragraphProperties7 = new ParagraphProperties();

                ParagraphMarkRunProperties paragraphMarkRunProperties7 = new ParagraphMarkRunProperties();

                paragraphProperties7.Append(paragraphMarkRunProperties7);

                Run run34 = new Run() {RsidRunProperties = "00EC01A4"};

                RunProperties runProperties10 = new RunProperties();
                Bold bold10 = new Bold();

                runProperties10.Append(bold10);
                Text text34 = new Text();
                text34.Text = "Diagnóstico";

                run34.Append(runProperties10);
                run34.Append(text34);

                paragraph23.Append(paragraphProperties7);
                paragraph23.Append(run34);

                tableCell21.Append(tableCellProperties21);
                tableCell21.Append(paragraph23);

                tableRow13.Append(tableCell21);

                TableRow tableRow14 = new TableRow()
                    {
                        RsidTableRowMarkRevision = "004D2B75",
                        RsidTableRowAddition = "00EC01A4",
                        RsidTableRowProperties = "00EC01A4"
                    };

                TableCell tableCell22 = new TableCell();

                TableCellProperties tableCellProperties22 = new TableCellProperties();
                TableCellWidth tableCellWidth22 = new TableCellWidth() {Width = "9576", Type = TableWidthUnitValues.Dxa};
                GridSpan gridSpan6 = new GridSpan() {Val = 2};

                TableCellBorders tableCellBorders22 = new TableCellBorders();
                TopBorder topBorder18 = new TopBorder() {Val = BorderValues.Nil};
                LeftBorder leftBorder21 = new LeftBorder() {Val = BorderValues.Nil};
                BottomBorder bottomBorder21 = new BottomBorder()
                    {
                        Val = BorderValues.Single,
                        Color = "auto",
                        Size = (UInt32Value) 4U,
                        Space = (UInt32Value) 0U
                    };
                RightBorder rightBorder21 = new RightBorder() {Val = BorderValues.Nil};

                tableCellBorders22.Append(topBorder18);
                tableCellBorders22.Append(leftBorder21);
                tableCellBorders22.Append(bottomBorder21);
                tableCellBorders22.Append(rightBorder21);

                tableCellProperties22.Append(tableCellWidth22);
                tableCellProperties22.Append(gridSpan6);
                tableCellProperties22.Append(tableCellBorders22);

                Paragraph paragraph24 = new Paragraph()
                    {
                        RsidParagraphMarkRevision = "004D2B75",
                        RsidParagraphAddition = "00EC01A4",
                        RsidRunAdditionDefault = "00EC01A4"
                    };

                ParagraphProperties paragraphProperties8 = new ParagraphProperties();

                ParagraphMarkRunProperties paragraphMarkRunProperties8 = new ParagraphMarkRunProperties();

                paragraphProperties8.Append(paragraphMarkRunProperties8);

                Run run35 = new Run();

                RunProperties runProperties11 = new RunProperties();

                Text text35 = new Text();
                if (report.Diagnosis != null)
                {
                    text35.Text = report.Diagnosis;
                }
                else
                {
                    text35.Text = "<No se ha realizado el diagnóstico aún>";
                }

                run35.Append(runProperties11);
                run35.Append(text35);

                paragraph24.Append(paragraphProperties8);
                paragraph24.Append(run35);

                tableCell22.Append(tableCellProperties22);
                tableCell22.Append(paragraph24);

                tableRow14.Append(tableCell22);

                TableRow tableRow15 = new TableRow()
                    {
                        RsidTableRowMarkRevision = "004D2B75",
                        RsidTableRowAddition = "00EC01A4",
                        RsidTableRowProperties = "00EC01A4"
                    };

                TableCell tableCell23 = new TableCell();

                TableCellProperties tableCellProperties23 = new TableCellProperties();
                TableCellWidth tableCellWidth23 = new TableCellWidth() {Width = "9576", Type = TableWidthUnitValues.Dxa};
                GridSpan gridSpan7 = new GridSpan() {Val = 2};

                TableCellBorders tableCellBorders23 = new TableCellBorders();
                LeftBorder leftBorder22 = new LeftBorder() {Val = BorderValues.Nil};
                BottomBorder bottomBorder22 = new BottomBorder() {Val = BorderValues.Nil};
                RightBorder rightBorder22 = new RightBorder() {Val = BorderValues.Nil};

                tableCellBorders23.Append(leftBorder22);
                tableCellBorders23.Append(bottomBorder22);
                tableCellBorders23.Append(rightBorder22);

                tableCellProperties23.Append(tableCellWidth23);
                tableCellProperties23.Append(gridSpan7);
                tableCellProperties23.Append(tableCellBorders23);

                Paragraph paragraph25 = new Paragraph()
                    {
                        RsidParagraphMarkRevision = "00EC01A4",
                        RsidParagraphAddition = "00EC01A4",
                        RsidRunAdditionDefault = "00EC01A4"
                    };

                ParagraphProperties paragraphProperties9 = new ParagraphProperties();

                ParagraphMarkRunProperties paragraphMarkRunProperties9 = new ParagraphMarkRunProperties();
                Bold bold11 = new Bold();

                paragraphMarkRunProperties9.Append(bold11);

                paragraphProperties9.Append(paragraphMarkRunProperties9);

                Run run36 = new Run() {RsidRunProperties = "00EC01A4"};

                RunProperties runProperties12 = new RunProperties();
                Bold bold12 = new Bold();

                runProperties12.Append(bold12);
                Text text36 = new Text();
                text36.Text = "Perfil de la Tensión Arterial";

                run36.Append(runProperties12);
                run36.Append(text36);

                paragraph25.Append(paragraphProperties9);
                paragraph25.Append(run36);

                tableCell23.Append(tableCellProperties23);
                tableCell23.Append(paragraph25);

                tableRow15.Append(tableCell23);

                TableRow tableRow16 = new TableRow()
                    {
                        RsidTableRowMarkRevision = "004D2B75",
                        RsidTableRowAddition = "00EC01A4",
                        RsidTableRowProperties = "00EC01A4"
                    };

                TableCell tableCell24 = new TableCell();

                TableCellProperties tableCellProperties24 = new TableCellProperties();
                TableCellWidth tableCellWidth24 = new TableCellWidth() {Width = "9576", Type = TableWidthUnitValues.Dxa};
                GridSpan gridSpan8 = new GridSpan() {Val = 2};

                TableCellBorders tableCellBorders24 = new TableCellBorders();
                TopBorder topBorder19 = new TopBorder() {Val = BorderValues.Nil};
                LeftBorder leftBorder23 = new LeftBorder() {Val = BorderValues.Nil};
                BottomBorder bottomBorder23 = new BottomBorder()
                    {
                        Val = BorderValues.Single,
                        Color = "auto",
                        Size = (UInt32Value) 4U,
                        Space = (UInt32Value) 0U
                    };
                RightBorder rightBorder23 = new RightBorder() {Val = BorderValues.Nil};

                tableCellBorders24.Append(topBorder19);
                tableCellBorders24.Append(leftBorder23);
                tableCellBorders24.Append(bottomBorder23);
                tableCellBorders24.Append(rightBorder23);

                tableCellProperties24.Append(tableCellWidth24);
                tableCellProperties24.Append(gridSpan8);
                tableCellProperties24.Append(tableCellBorders24);

                Paragraph paragraph26 = new Paragraph()
                    {
                        RsidParagraphMarkRevision = "004D2B75",
                        RsidParagraphAddition = "00EC01A4",
                        RsidRunAdditionDefault = "00EC01A4"
                    };

                ParagraphProperties paragraphProperties10 = new ParagraphProperties();

                ParagraphMarkRunProperties paragraphMarkRunProperties10 = new ParagraphMarkRunProperties();

                paragraphProperties10.Append(paragraphMarkRunProperties10);

                Run run37 = new Run();

                RunProperties runProperties13 = new RunProperties();

                Text text37 = new Text();
                text37.Text = "<Gráfica tensión arterial/tiempo>";

                run37.Append(runProperties13);
                run37.Append(text37);

                paragraph26.Append(paragraphProperties10);
                paragraph26.Append(run37);

                tableCell24.Append(tableCellProperties24);
                tableCell24.Append(paragraph26);

                tableRow16.Append(tableCell24);

                TableRow tableRow17 = new TableRow()
                    {
                        RsidTableRowMarkRevision = "004D2B75",
                        RsidTableRowAddition = "00EC01A4",
                        RsidTableRowProperties = "00EC01A4"
                    };

                TableCell tableCell25 = new TableCell();

                TableCellProperties tableCellProperties25 = new TableCellProperties();
                TableCellWidth tableCellWidth25 = new TableCellWidth() {Width = "9576", Type = TableWidthUnitValues.Dxa};
                GridSpan gridSpan9 = new GridSpan() {Val = 2};

                TableCellBorders tableCellBorders25 = new TableCellBorders();
                LeftBorder leftBorder24 = new LeftBorder() {Val = BorderValues.Nil};
                BottomBorder bottomBorder24 = new BottomBorder() {Val = BorderValues.Nil};
                RightBorder rightBorder24 = new RightBorder() {Val = BorderValues.Nil};

                tableCellBorders25.Append(leftBorder24);
                tableCellBorders25.Append(bottomBorder24);
                tableCellBorders25.Append(rightBorder24);

                tableCellProperties25.Append(tableCellWidth25);
                tableCellProperties25.Append(gridSpan9);
                tableCellProperties25.Append(tableCellBorders25);

                Paragraph paragraph27 = new Paragraph()
                    {
                        RsidParagraphMarkRevision = "00EC01A4",
                        RsidParagraphAddition = "00EC01A4",
                        RsidRunAdditionDefault = "00EC01A4"
                    };

                ParagraphProperties paragraphProperties11 = new ParagraphProperties();

                ParagraphMarkRunProperties paragraphMarkRunProperties11 = new ParagraphMarkRunProperties();
                Bold bold13 = new Bold();

                paragraphMarkRunProperties11.Append(bold13);

                paragraphProperties11.Append(paragraphMarkRunProperties11);

                Run run38 = new Run() {RsidRunProperties = "00EC01A4"};

                RunProperties runProperties14 = new RunProperties();
                Bold bold14 = new Bold();

                runProperties14.Append(bold14);
                Text text38 = new Text();
                text38.Text = "Valores por encima del límite";

                run38.Append(runProperties14);
                run38.Append(text38);

                paragraph27.Append(paragraphProperties11);
                paragraph27.Append(run38);

                tableCell25.Append(tableCellProperties25);
                tableCell25.Append(paragraph27);

                tableRow17.Append(tableCell25);

                TableRow tableRow18 = new TableRow()
                    {
                        RsidTableRowMarkRevision = "004D2B75",
                        RsidTableRowAddition = "00EC01A4",
                        RsidTableRowProperties = "00EC01A4"
                    };

                TableCell tableCell26 = new TableCell();

                TableCellProperties tableCellProperties26 = new TableCellProperties();
                TableCellWidth tableCellWidth26 = new TableCellWidth() {Width = "9576", Type = TableWidthUnitValues.Dxa};
                GridSpan gridSpan10 = new GridSpan() {Val = 2};

                TableCellBorders tableCellBorders26 = new TableCellBorders();
                TopBorder topBorder20 = new TopBorder() {Val = BorderValues.Nil};
                LeftBorder leftBorder25 = new LeftBorder() {Val = BorderValues.Nil};
                BottomBorder bottomBorder25 = new BottomBorder() {Val = BorderValues.Nil};
                RightBorder rightBorder25 = new RightBorder() {Val = BorderValues.Nil};

                tableCellBorders26.Append(topBorder20);
                tableCellBorders26.Append(leftBorder25);
                tableCellBorders26.Append(bottomBorder25);
                tableCellBorders26.Append(rightBorder25);

                tableCellProperties26.Append(tableCellWidth26);
                tableCellProperties26.Append(gridSpan10);
                tableCellProperties26.Append(tableCellBorders26);

                Paragraph paragraph28 = new Paragraph()
                    {
                        RsidParagraphMarkRevision = "004D2B75",
                        RsidParagraphAddition = "00EC01A4",
                        RsidRunAdditionDefault = "00EC01A4"
                    };

                ParagraphProperties paragraphProperties12 = new ParagraphProperties();

                ParagraphMarkRunProperties paragraphMarkRunProperties12 = new ParagraphMarkRunProperties();

                paragraphProperties12.Append(paragraphMarkRunProperties12);

                Run run39 = new Run();

                RunProperties runProperties15 = new RunProperties();

                Text text39 = new Text();
                text39.Text = "<Gráfico de torta>";

                run39.Append(runProperties15);
                run39.Append(text39);

                paragraph28.Append(paragraphProperties12);
                paragraph28.Append(run39);

                tableCell26.Append(tableCellProperties26);
                tableCell26.Append(paragraph28);

                tableRow18.Append(tableCell26);

                table1.Append(tableProperties1);
                table1.Append(tableGrid1);
                table1.Append(tableRow1);
                table1.Append(tableRow2);
                table1.Append(tableRow3);
                table1.Append(tableRow4);
                table1.Append(tableRow5);
                table1.Append(tableRow6);
                table1.Append(tableRow7);
                table1.Append(tableRow8);
                table1.Append(tableRow9);
                table1.Append(tableRow10);
                table1.Append(tableRow11);
                table1.Append(tableRow12);
                table1.Append(tableRow13);
                table1.Append(tableRow14);
                table1.Append(tableRow15);
                table1.Append(tableRow16);
                table1.Append(tableRow17);
                table1.Append(tableRow18);

                Paragraph paragraph29 = new Paragraph()
                    {
                        RsidParagraphMarkRevision = "004D2B75",
                        RsidParagraphAddition = "00E11155",
                        RsidRunAdditionDefault = "00B86685"
                    };

                ParagraphProperties paragraphProperties13 = new ParagraphProperties();

                ParagraphMarkRunProperties paragraphMarkRunProperties13 = new ParagraphMarkRunProperties();

                paragraphProperties13.Append(paragraphMarkRunProperties13);

                paragraph29.Append(paragraphProperties13);

                SectionProperties sectionProperties1 = new SectionProperties()
                    {
                        RsidRPr = "004D2B75",
                        RsidR = "00E11155"
                    };
                /*HeaderReference headerReference1 = new HeaderReference()
                    {
                        Type = HeaderFooterValues.Default,
                        Id = "rId8"
                    };
                FooterReference footerReference1 = new FooterReference()
                    {
                        Type = HeaderFooterValues.Default,
                        Id = "rId9"
                    };*/
                PageSize pageSize1 = new PageSize() {Width = (UInt32Value) 12240U, Height = (UInt32Value) 15840U};
                PageMargin pageMargin1 = new PageMargin()
                    {
                        Top = 1440,
                        Right = (UInt32Value) 1440U,
                        Bottom = 1440,
                        Left = (UInt32Value) 1440U,
                        Header = (UInt32Value) 720U,
                        Footer = (UInt32Value) 720U,
                        Gutter = (UInt32Value) 0U
                    };
                Columns columns1 = new Columns() {Space = "720"};
                DocGrid docGrid1 = new DocGrid() {LinePitch = 360};

                //sectionProperties1.Append(headerReference1);
                //sectionProperties1.Append(footerReference1);
                sectionProperties1.Append(pageSize1);
                sectionProperties1.Append(pageMargin1);
                sectionProperties1.Append(columns1);
                sectionProperties1.Append(docGrid1);

                body1.Append(table1);
                body1.Append(paragraph29);
                body1.Append(sectionProperties1);

                document1.Append(body1);

                mainDocumentPart1.Document = document1;
                mainDocumentPart1.Document.Save();

                /********************************************************/




                //Generate Header
                //HeaderPart headerPart1 = mainDocumentPart1.AddNewPart<HeaderPart>("rId8");
                //GenerateHeader(headerPart1);

                return true;
            }
        }

        private void GenerateHeader(HeaderPart header)
        {
            
        }

        public void exportReportPDF(Report report, string fileName)
        {/*
            //Crear documento PDF
            PdfDocument doc = new PdfDocument();
            doc.Info.Title = "Informe de Hipertensión Arterial";

            //Crear pagina vacia
            PdfPage page = doc.AddPage();

            // Drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Crear fuente
            XFont font = new XFont("Verdana", 20, XFontStyle.Bold);

            // Escribe texto
            gfx.DrawString("Hello, World!", font, XBrushes.Black, new XRect(0, 0, page.Width, page.Height), XStringFormat.Center);

            //Guardar el documento
            doc.Save(fileName);
            // Muestra archivo
            Process.Start(fileName);
          */
        }

        public void AddReport(Report report)
        {
            var uhda = new UdaHtaDataAccess();
            uhda.insertReport(report);
        }

    }
}
