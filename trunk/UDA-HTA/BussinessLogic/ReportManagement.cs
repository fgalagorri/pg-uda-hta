using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using V = DocumentFormat.OpenXml.Vml;
using Ovml = DocumentFormat.OpenXml.Vml.Office;
using PageSize = DocumentFormat.OpenXml.Wordprocessing.PageSize;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Office.Interop.Word;
using Entities;
using DataAccess;
using Break = DocumentFormat.OpenXml.Wordprocessing.Break;
using Columns = DocumentFormat.OpenXml.Wordprocessing.Columns;
using Document = Microsoft.Office.Interop.Word.Document;
using Paragraph = DocumentFormat.OpenXml.Wordprocessing.Paragraph;
using Shading = DocumentFormat.OpenXml.Wordprocessing.Shading;
using Table = DocumentFormat.OpenXml.Wordprocessing.Table;
using TableStyle = DocumentFormat.OpenXml.Wordprocessing.TableStyle;

namespace BussinessLogic
{
    public class ReportManagement
    {
        public Report GetReport(int idReport)
        {
            UdaHtaDataAccess uhda = new UdaHtaDataAccess();
            return uhda.GetReport(idReport);
        }

        public long AddReport(Report report)
        {
            var uhda = new UdaHtaDataAccess();
            try
            {
                return uhda.InsertReport(report);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateDiagnosis(long reportId, string diagnosis, DateTime diagnosisDate, string doctor)
        {
            UdaHtaDataAccess uda = new UdaHtaDataAccess();
            try
            {
                uda.UpdateDiagnosis(reportId, diagnosis, diagnosisDate, doctor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ICollection<Report> ListPatientReports(long idPatient)
        {
            UdaHtaDataAccess uda = new UdaHtaDataAccess();
            return uda.GetReportsByPatientId(idPatient);
        }

        #region Drugs Region
        public void AddDrug(int type, string name)
        {
            UdaHtaDataAccess uhda = new UdaHtaDataAccess();
            try
            {
                uhda.InsertDrug(name, type);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public void DeleteDrug(string name)
        {
            // TODO
        }

        public void EditDrug(string type, string name)
        {
            // TODO
        }
        #endregion


        #region Export Region

        public void PrintReport(int idReport)
        {
        }

        public void ExportReportDocx(Report report, string filePath)
        {
            using (var document = WordprocessingDocument.Create(filePath, WordprocessingDocumentType.Document))
            {
                DocumentFormat.OpenXml.Packaging.MainDocumentPart mainDocumentPart1 = document.AddMainDocumentPart();
                DocumentFormat.OpenXml.Wordprocessing.Document document1 =
                    new DocumentFormat.OpenXml.Wordprocessing.Document();
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
                TableCellWidth tableCellWidth1 = new TableCellWidth()
                    {
                        Width = "9576",
                        Type = TableWidthUnitValues.Dxa
                    };
                GridSpan gridSpan1 = new GridSpan() {Val = 2};

                TableCellBorders tableCellBorders1 = new TableCellBorders();

                tableCellBorders1.Append(new TopBorder() {Val = BorderValues.Nil});
                tableCellBorders1.Append(new LeftBorder() {Val = BorderValues.Nil});
                tableCellBorders1.Append(new BottomBorder() {Val = BorderValues.Nil});
                tableCellBorders1.Append(new RightBorder() {Val = BorderValues.Nil});
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
                paragraphMarkRunProperties1.Append(new FontSize() {Val = "32"});
                paragraphMarkRunProperties1.Append(new FontSizeComplexScript() {Val = "32"});

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

                tableCellProperties3.Append(new TableCellWidth() {Width = "4788", Type = TableWidthUnitValues.Dxa});
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
                if (report.Patient.RegisterNumber != null)
                {
                    text5.Text = "Nro. Registro: " + report.Patient.RegisterNumber.ToString();
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

                tableCellProperties5.Append(new TableCellWidth() {Width = "4788", Type = TableWidthUnitValues.Dxa});
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
                    text11.Text = "Fecha de Nacimiento: " +
                                  report.Patient.BirthDate.Value.ToString(
                                      ConfigurationManager.AppSettings["ShortDateString"]);
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
                TableCellWidth tableCellWidth11 = new TableCellWidth()
                    {
                        Width = "4788",
                        Type = TableWidthUnitValues.Dxa
                    };

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
                TableCellWidth tableCellWidth12 = new TableCellWidth()
                    {
                        Width = "4788",
                        Type = TableWidthUnitValues.Dxa
                    };

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
                TableCellWidth tableCellWidth13 = new TableCellWidth()
                    {
                        Width = "4788",
                        Type = TableWidthUnitValues.Dxa
                    };

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
                TableCellWidth tableCellWidth14 = new TableCellWidth()
                    {
                        Width = "4788",
                        Type = TableWidthUnitValues.Dxa
                    };

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
                TableCellWidth tableCellWidth15 = new TableCellWidth()
                    {
                        Width = "4788",
                        Type = TableWidthUnitValues.Dxa
                    };

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
                TableCellWidth tableCellWidth16 = new TableCellWidth()
                    {
                        Width = "9576",
                        Type = TableWidthUnitValues.Dxa
                    };
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
                TableCellWidth tableCellWidth17 = new TableCellWidth()
                    {
                        Width = "4788",
                        Type = TableWidthUnitValues.Dxa
                    };

                TableCellBorders tableCellBorders17 = new TableCellBorders();
                LeftBorder leftBorder17 = new LeftBorder() {Val = BorderValues.Nil};
                TopBorder topBorder16_1 = new TopBorder()
                    {
                        Val = BorderValues.Single,
                        Color = "auto",
                        Size = (UInt32Value) 4U,
                        Space = (UInt32Value) 0U
                    };
                BottomBorder bottomBorder16 = new BottomBorder()
                    {
                        Val = BorderValues.Single,
                        Color = "auto",
                        Size = (UInt32Value) 4U,
                        Space = (UInt32Value) 0U
                    };

                tableCellBorders17.Append(leftBorder17);
                tableCellBorders17.Append(bottomBorder16);
                tableCellBorders17.Append(topBorder16_1);

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
                TableCellWidth tableCellWidth18 = new TableCellWidth()
                    {
                        Width = "4788",
                        Type = TableWidthUnitValues.Dxa
                    };

                TableCellBorders tableCellBorders18 = new TableCellBorders();
                TopBorder topBorder17_1 = new TopBorder()
                    {
                        Val = BorderValues.Single,
                        Color = "auto",
                        Size = (UInt32Value) 4U,
                        Space = (UInt32Value) 0U
                    };
                BottomBorder bottomBorder17 = new BottomBorder()
                    {
                        Val = BorderValues.Single,
                        Color = "auto",
                        Size = (UInt32Value) 4U,
                        Space = (UInt32Value) 0U
                    };
                RightBorder rightBorder17 = new RightBorder() {Val = BorderValues.Nil};

                tableCellBorders18.Append(bottomBorder17);
                tableCellBorders18.Append(topBorder17_1);
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
                text30.Text = "Fecha y hora de inicio: " + report.BeginDate.ToString();

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
                text31.Text = "Fecha y hora de fin: " + report.EndDate;

                run31.Append(runProperties7);
                run31.Append(text31);

                paragraph20.Append(paragraphProperties4);
                paragraph20.Append(run31);

                //-----
                Paragraph paragraph3_1 = new Paragraph()
                    {
                        RsidParagraphMarkRevision = "004D2B75",
                        RsidParagraphAddition = "004D2B75",
                        RsidParagraphProperties = "004D2B75",
                        RsidRunAdditionDefault = "004D2B75"
                    };

                ParagraphProperties paragraphProperties3_1 = new ParagraphProperties();
                ParagraphMarkRunProperties paragraphMarkRunProperties3_1 = new ParagraphMarkRunProperties();

                paragraphProperties3_1.Append(paragraphMarkRunProperties3_1);

                Run run3_1 = new Run();

                RunProperties runProperties3_1 = new RunProperties();

                Text text3_1 = new Text();
                text3_1.Text = "\n Hora inicio noche: " + report.Carnet.SleepTimeStart.ToString();

                run3_1.Append(runProperties3_1);
                run3_1.Append(text3_1);

                paragraph3_1.Append(paragraphProperties3_1);
                paragraph3_1.Append(run3_1);

                //-----
                Paragraph paragraph3_2 = new Paragraph()
                    {
                        RsidParagraphMarkRevision = "004D2B75",
                        RsidParagraphAddition = "004D2B75",
                        RsidParagraphProperties = "004D2B75",
                        RsidRunAdditionDefault = "004D2B75"
                    };

                ParagraphProperties paragraphProperties3_2 = new ParagraphProperties();
                ParagraphMarkRunProperties paragraphMarkRunProperties3_2 = new ParagraphMarkRunProperties();

                paragraphProperties3_2.Append(paragraphMarkRunProperties3_2);

                Run run3_2 = new Run();

                RunProperties runProperties3_2 = new RunProperties();

                Text text3_2 = new Text();
                text3_2.Text = "Hora fin noche: " + report.Carnet.SleepTimeEnd.ToString();

                run3_2.Append(runProperties3_2);
                run3_2.Append(text3_2);

                paragraph3_2.Append(paragraphProperties3_2);
                paragraph3_2.Append(run3_2);

                //-----
                tableCell18.Append(tableCellProperties18);
                tableCell18.Append(paragraph18);
                tableCell18.Append(paragraph19);
                tableCell18.Append(paragraph20);
                tableCell18.Append(paragraph3_1);
                tableCell18.Append(paragraph3_2);

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
                TableCellWidth tableCellWidth19 = new TableCellWidth()
                    {
                        Width = "9576",
                        Type = TableWidthUnitValues.Dxa
                    };
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

                //Resumen de medidas
                var tableRow12 = MeasuresSummary(report);

                TableRow tableRow13 = new TableRow()
                    {
                        RsidTableRowMarkRevision = "004D2B75",
                        RsidTableRowAddition = "00EC01A4",
                        RsidTableRowProperties = "00EC01A4"
                    };

                TableCell tableCell21 = new TableCell();

                TableCellProperties tableCellProperties21 = new TableCellProperties();
                TableCellWidth tableCellWidth21 = new TableCellWidth()
                    {
                        Width = "9576",
                        Type = TableWidthUnitValues.Dxa
                    };
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
                TableCellWidth tableCellWidth22 = new TableCellWidth()
                    {
                        Width = "9576",
                        Type = TableWidthUnitValues.Dxa
                    };
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

                if (report.Diagnosis != null)
                {
                    string[] diagnosis = report.Diagnosis.Split("\r\n".ToCharArray());
                    foreach (string d in diagnosis)
                    {
                        run35.AppendChild(new Text(d));
                        RunProperties runProp = run35.AppendChild(new RunProperties());
                        Break lineBreak = new Break();
                        runProp.AppendChild(lineBreak);
                    }
                }
                else
                {
                    Text text35 = new Text();
                    text35.Text = "<No se ha realizado el diagnóstico aún>";
                }


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
                TableCellWidth tableCellWidth23 = new TableCellWidth()
                    {
                        Width = "9576",
                        Type = TableWidthUnitValues.Dxa
                    };
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
                TableCellWidth tableCellWidth24 = new TableCellWidth()
                    {
                        Width = "9576",
                        Type = TableWidthUnitValues.Dxa
                    };
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
                TableCellWidth tableCellWidth25 = new TableCellWidth()
                    {
                        Width = "9576",
                        Type = TableWidthUnitValues.Dxa
                    };
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
                TableCellWidth tableCellWidth26 = new TableCellWidth()
                    {
                        Width = "9576",
                        Type = TableWidthUnitValues.Dxa
                    };
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
                        RsidR = "00E11155",
                    };


                PageSize pageSize1 = new PageSize() {Width = 12240U, Height = 15840U};
                PageMargin pageMargin1 = new PageMargin()
                    {
                        Top = 1440,
                        Right = 1440U,
                        Bottom = 1440,
                        Left = 1440U,
                        Header = 720U,
                        Footer = 720U,
                        Gutter = 0U
                    };
                Columns columns1 = new Columns() {Space = "720"};
                DocGrid docGrid1 = new DocGrid() {LinePitch = 360};

                HeaderReference headerReference1 = new HeaderReference()
                    {
                        Type = HeaderFooterValues.Default,
                        Id = "rId7"
                    };
                HeaderReference coverHeaderReference = new HeaderReference()
                    {
                        Type = HeaderFooterValues.First,
                        Id = "rId8"
                    };

                TitlePage titlepage1 = new TitlePage();

                sectionProperties1.Append(coverHeaderReference);
                sectionProperties1.Append(headerReference1);
                //sectionProperties1.Append(footerReference1);
                sectionProperties1.Append(pageSize1);
                sectionProperties1.Append(pageMargin1);
                sectionProperties1.Append(columns1);
                sectionProperties1.Append(titlepage1);
                sectionProperties1.Append(docGrid1);

                body1.Append(table1);
                body1.Append(paragraph29);
                body1.Append(sectionProperties1);

                document1.Append(body1);


                mainDocumentPart1.Document = document1;
                HeaderPart coverHeader = mainDocumentPart1.AddNewPart<HeaderPart>("rId8");
                HeaderPart headerPart1 = mainDocumentPart1.AddNewPart<HeaderPart>("rId7");


                GenerateCoverHeader(coverHeader);
                GenerateHeader(headerPart1, report.Patient);


                mainDocumentPart1.Document.Save();
            }
        }

        private TableRow MeasuresSummary(Report report)
        {
            TableRow tableRow12 = new TableRow() { RsidTableRowMarkRevision = "00653833", RsidTableRowAddition = "004D2B75", RsidTableRowProperties = "00EC01A4" };

            TableRowProperties tableRowProperties3 = new TableRowProperties();
            TableRowHeight tableRowHeight3 = new TableRowHeight() { Val = (UInt32Value)1383U };

            tableRowProperties3.Append(tableRowHeight3);

            TableCell tableCell20 = new TableCell();

            TableCellProperties tableCellProperties20 = new TableCellProperties();
            TableCellWidth tableCellWidth20 = new TableCellWidth() { Width = "9576", Type = TableWidthUnitValues.Dxa };
            GridSpan gridSpan4 = new GridSpan() { Val = 2 };

            TableCellBorders tableCellBorders20 = new TableCellBorders();
            TopBorder topBorder17 = new TopBorder() { Val = BorderValues.Nil };
            LeftBorder leftBorder19 = new LeftBorder() { Val = BorderValues.Nil };
            BottomBorder bottomBorder19 = new BottomBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };
            RightBorder rightBorder19 = new RightBorder() { Val = BorderValues.Nil };

            tableCellBorders20.Append(topBorder17);
            tableCellBorders20.Append(leftBorder19);
            tableCellBorders20.Append(bottomBorder19);
            tableCellBorders20.Append(rightBorder19);
            TableCellVerticalAlignment tableCellVerticalAlignment3 = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };

            tableCellProperties20.Append(tableCellWidth20);
            tableCellProperties20.Append(gridSpan4);
            tableCellProperties20.Append(tableCellBorders20);
            tableCellProperties20.Append(tableCellVerticalAlignment3);

            Paragraph paragraph25 = new Paragraph() { RsidParagraphAddition = "004D2B75", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "004D2B75" };

            ParagraphProperties paragraphProperties9 = new ParagraphProperties();
            Justification justification2 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties9 = new ParagraphMarkRunProperties();

            paragraphProperties9.Append(justification2);
            paragraphProperties9.Append(paragraphMarkRunProperties9);

            paragraph25.Append(paragraphProperties9);

            Table table2 = new Table();

            TableProperties tableProperties2 = new TableProperties();
            TableStyle tableStyle2 = new TableStyle() { Val = "Tablaconcuadrcula" };
            TableWidth tableWidth2 = new TableWidth() { Width = "0", Type = TableWidthUnitValues.Auto };
            TableLook tableLook2 = new TableLook() { Val = "04A0" };

            tableProperties2.Append(tableStyle2);
            tableProperties2.Append(tableWidth2);
            tableProperties2.Append(tableLook2);

            TableGrid tableGrid2 = new TableGrid();
            GridColumn gridColumn3 = new GridColumn() { Width = "2336" };
            GridColumn gridColumn4 = new GridColumn() { Width = "2336" };
            GridColumn gridColumn5 = new GridColumn() { Width = "2336" };
            GridColumn gridColumn6 = new GridColumn() { Width = "2337" };

            tableGrid2.Append(gridColumn3);
            tableGrid2.Append(gridColumn4);
            tableGrid2.Append(gridColumn5);
            tableGrid2.Append(gridColumn6);

            TableRow tableRow13 = new TableRow() { RsidTableRowAddition = "00A6498A", RsidTableRowProperties = "009C2A39" };

            TableCell tableCell21 = new TableCell();

            TableCellProperties tableCellProperties21 = new TableCellProperties();
            TableCellWidth tableCellWidth21 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            TableCellBorders tableCellBorders21 = new TableCellBorders();
            TopBorder topBorder18 = new TopBorder() { Val = BorderValues.Nil };
            LeftBorder leftBorder20 = new LeftBorder() { Val = BorderValues.Nil };

            tableCellBorders21.Append(topBorder18);
            tableCellBorders21.Append(leftBorder20);

            tableCellProperties21.Append(tableCellWidth21);
            tableCellProperties21.Append(tableCellBorders21);

            Paragraph paragraph26 = new Paragraph() { RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "00A6498A" };

            ParagraphProperties paragraphProperties10 = new ParagraphProperties();
            Justification justification3 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties10 = new ParagraphMarkRunProperties();

            paragraphProperties10.Append(justification3);
            paragraphProperties10.Append(paragraphMarkRunProperties10);

            paragraph26.Append(paragraphProperties10);

            tableCell21.Append(tableCellProperties21);
            tableCell21.Append(paragraph26);

            TableCell tableCell22 = new TableCell();

            TableCellProperties tableCellProperties22 = new TableCellProperties();
            TableCellWidth tableCellWidth22 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };
            Shading shading1 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "BFBFBF", ThemeFill = ThemeColorValues.Background1, ThemeFillShade = "BF" };

            tableCellProperties22.Append(tableCellWidth22);
            tableCellProperties22.Append(shading1);

            Paragraph paragraph27 = new Paragraph() { RsidParagraphMarkRevision = "00A6498A", RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "00A6498A" };

            ParagraphProperties paragraphProperties11 = new ParagraphProperties();
            Justification justification4 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties11 = new ParagraphMarkRunProperties();
            Bold bold10 = new Bold();

            paragraphMarkRunProperties11.Append(bold10);
            paragraphProperties11.Append(justification4);
            paragraphProperties11.Append(paragraphMarkRunProperties11);

            Run run35 = new Run() { RsidRunProperties = "00A6498A" };

            RunProperties runProperties11 = new RunProperties();
            Bold bold11 = new Bold();

            runProperties11.Append(bold11);
            Text text35 = new Text();
            text35.Text = "Total";

            run35.Append(runProperties11);
            run35.Append(text35);

            paragraph27.Append(paragraphProperties11);
            paragraph27.Append(run35);

            tableCell22.Append(tableCellProperties22);
            tableCell22.Append(paragraph27);

            TableCell tableCell23 = new TableCell();

            TableCellProperties tableCellProperties23 = new TableCellProperties();
            TableCellWidth tableCellWidth23 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };
            Shading shading2 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "BFBFBF", ThemeFill = ThemeColorValues.Background1, ThemeFillShade = "BF" };

            tableCellProperties23.Append(tableCellWidth23);
            tableCellProperties23.Append(shading2);

            Paragraph paragraph28 = new Paragraph() { RsidParagraphMarkRevision = "00A6498A", RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "00A6498A" };

            ParagraphProperties paragraphProperties12 = new ParagraphProperties();
            Justification justification5 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties12 = new ParagraphMarkRunProperties();
            Bold bold12 = new Bold();

            paragraphMarkRunProperties12.Append(bold12);

            paragraphProperties12.Append(justification5);
            paragraphProperties12.Append(paragraphMarkRunProperties12);

            Run run36 = new Run() { RsidRunProperties = "00A6498A" };

            RunProperties runProperties12 = new RunProperties();
            Bold bold13 = new Bold();

            runProperties12.Append(bold13);
            Text text36 = new Text();
            text36.Text = "Día";

            run36.Append(runProperties12);
            run36.Append(text36);

            paragraph28.Append(paragraphProperties12);
            paragraph28.Append(run36);

            tableCell23.Append(tableCellProperties23);
            tableCell23.Append(paragraph28);

            TableCell tableCell24 = new TableCell();

            TableCellProperties tableCellProperties24 = new TableCellProperties();
            TableCellWidth tableCellWidth24 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };
            Shading shading3 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "BFBFBF", ThemeFill = ThemeColorValues.Background1, ThemeFillShade = "BF" };

            tableCellProperties24.Append(tableCellWidth24);
            tableCellProperties24.Append(shading3);

            Paragraph paragraph29 = new Paragraph() { RsidParagraphMarkRevision = "00A6498A", RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "00A6498A" };

            ParagraphProperties paragraphProperties13 = new ParagraphProperties();
            Justification justification6 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties13 = new ParagraphMarkRunProperties();
            Bold bold14 = new Bold();

            paragraphMarkRunProperties13.Append(bold14);

            paragraphProperties13.Append(justification6);
            paragraphProperties13.Append(paragraphMarkRunProperties13);

            Run run37 = new Run() { RsidRunProperties = "00A6498A" };

            RunProperties runProperties13 = new RunProperties();
            Bold bold15 = new Bold();

            runProperties13.Append(bold15);
            Text text37 = new Text();
            text37.Text = "Noche";

            run37.Append(runProperties13);
            run37.Append(text37);

            paragraph29.Append(paragraphProperties13);
            paragraph29.Append(run37);

            tableCell24.Append(tableCellProperties24);
            tableCell24.Append(paragraph29);

            tableRow13.Append(tableCell21);
            tableRow13.Append(tableCell22);
            tableRow13.Append(tableCell23);
            tableRow13.Append(tableCell24);

            TableRow tableRow14 = new TableRow() { RsidTableRowAddition = "00A6498A", RsidTableRowProperties = "00A6498A" };

            TableCell tableCell25 = new TableCell();

            TableCellProperties tableCellProperties25 = new TableCellProperties();
            TableCellWidth tableCellWidth25 = new TableCellWidth() { Width = "9345", Type = TableWidthUnitValues.Dxa };
            GridSpan gridSpan5 = new GridSpan() { Val = 4 };
            Shading shading4 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "BFBFBF", ThemeFill = ThemeColorValues.Background1, ThemeFillShade = "BF" };

            tableCellProperties25.Append(tableCellWidth25);
            tableCellProperties25.Append(gridSpan5);
            tableCellProperties25.Append(shading4);

            Paragraph paragraph30 = new Paragraph() { RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "00A6498A", RsidRunAdditionDefault = "00A6498A" };

            ParagraphProperties paragraphProperties14 = new ParagraphProperties();

            ParagraphMarkRunProperties paragraphMarkRunProperties14 = new ParagraphMarkRunProperties();

            paragraphProperties14.Append(paragraphMarkRunProperties14);

            Run run38 = new Run() { RsidRunProperties = "00A6498A" };

            RunProperties runProperties14 = new RunProperties();
            Bold bold16 = new Bold();

            runProperties14.Append(bold16);
            Text text38 = new Text();
            text38.Text = "Mediciones";

            run38.Append(runProperties14);
            run38.Append(text38);

            paragraph30.Append(paragraphProperties14);
            paragraph30.Append(run38);

            tableCell25.Append(tableCellProperties25);
            tableCell25.Append(paragraph30);

            tableRow14.Append(tableCell25);

            TableRow tableRow15 = new TableRow() { RsidTableRowAddition = "00A6498A", RsidTableRowProperties = "00A6498A" };

            TableCell tableCell26 = new TableCell();

            TableCellProperties tableCellProperties26 = new TableCellProperties();
            TableCellWidth tableCellWidth26 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties26.Append(tableCellWidth26);

            Paragraph paragraph31 = new Paragraph() { RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "00A6498A" };

            ParagraphProperties paragraphProperties15 = new ParagraphProperties();
            Justification justification7 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties15 = new ParagraphMarkRunProperties();

            paragraphProperties15.Append(justification7);
            paragraphProperties15.Append(paragraphMarkRunProperties15);

            Run run39 = new Run();

            RunProperties runProperties15 = new RunProperties();
            Text text39 = new Text();
            text39.Text = "Total";

            run39.Append(runProperties15);
            run39.Append(text39);

            paragraph31.Append(paragraphProperties15);
            paragraph31.Append(run39);

            tableCell26.Append(tableCellProperties26);
            tableCell26.Append(paragraph31);

            TableCell tableCell27 = new TableCell();

            TableCellProperties tableCellProperties27 = new TableCellProperties();
            TableCellWidth tableCellWidth27 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties27.Append(tableCellWidth27);

            Paragraph paragraph32 = new Paragraph() { RsidParagraphMarkRevision = "00A6498A", RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "00A6498A" };

            ParagraphProperties paragraphProperties16 = new ParagraphProperties();
            Justification justification8 = new Justification() { Val = JustificationValues.Center };

            paragraphProperties16.Append(justification8);

            Run run40 = new Run();

            RunProperties runProperties16 = new RunProperties();
            Text text40 = new Text();
            // Cantidad total de medidas
            var cantMeasureTotal = report.Measures.Count;
            text40.Text = cantMeasureTotal.ToString();

            run40.Append(runProperties16);
            run40.Append(text40);

            paragraph32.Append(paragraphProperties16);
            paragraph32.Append(run40);

            tableCell27.Append(tableCellProperties27);
            tableCell27.Append(paragraph32);

            TableCell tableCell28 = new TableCell();

            TableCellProperties tableCellProperties28 = new TableCellProperties();
            TableCellWidth tableCellWidth28 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties28.Append(tableCellWidth28);

            Paragraph paragraph33 = new Paragraph() { RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "00A6498A" };

            ParagraphProperties paragraphProperties17 = new ParagraphProperties();
            Justification justification9 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties16 = new ParagraphMarkRunProperties();

            paragraphProperties17.Append(justification9);
            paragraphProperties17.Append(paragraphMarkRunProperties16);

            Run run45 = new Run();

            RunProperties runProperties21 = new RunProperties();
            Text text45 = new Text();
            //Cantidad de medidas durante el dia
            var cantMeasureDay = report.Measures.Count(m => !m.Asleep.Value);
            text45.Text = cantMeasureDay.ToString();

            run45.Append(runProperties21);
            run45.Append(text45);

            paragraph33.Append(paragraphProperties17);
            paragraph33.Append(run45);

            tableCell28.Append(tableCellProperties28);
            tableCell28.Append(paragraph33);

            TableCell tableCell29 = new TableCell();

            TableCellProperties tableCellProperties29 = new TableCellProperties();
            TableCellWidth tableCellWidth29 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };

            tableCellProperties29.Append(tableCellWidth29);

            Paragraph paragraph34 = new Paragraph() { RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "00A6498A" };

            ParagraphProperties paragraphProperties18 = new ParagraphProperties();
            Justification justification10 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties17 = new ParagraphMarkRunProperties();

            paragraphProperties18.Append(justification10);
            paragraphProperties18.Append(paragraphMarkRunProperties17);

            Run run50 = new Run();

            RunProperties runProperties26 = new RunProperties();
            Text text50 = new Text();
            //Cantidad medidas durante la noche
            var cantMeasureNight = report.Measures.Count(m => m.Asleep.Value);
            text50.Text = cantMeasureNight.ToString();

            run50.Append(runProperties26);
            run50.Append(text50);

            paragraph34.Append(paragraphProperties18);
            paragraph34.Append(run50);

            tableCell29.Append(tableCellProperties29);
            tableCell29.Append(paragraph34);

            tableRow15.Append(tableCell26);
            tableRow15.Append(tableCell27);
            tableRow15.Append(tableCell28);
            tableRow15.Append(tableCell29);

            TableRow tableRow16 = new TableRow() { RsidTableRowAddition = "00A6498A", RsidTableRowProperties = "00A6498A" };

            TableCell tableCell30 = new TableCell();

            TableCellProperties tableCellProperties30 = new TableCellProperties();
            TableCellWidth tableCellWidth30 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties30.Append(tableCellWidth30);

            Paragraph paragraph35 = new Paragraph() { RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "00A6498A" };

            ParagraphProperties paragraphProperties19 = new ParagraphProperties();
            Justification justification11 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties18 = new ParagraphMarkRunProperties();

            paragraphProperties19.Append(justification11);
            paragraphProperties19.Append(paragraphMarkRunProperties18);

            Run run53 = new Run();

            RunProperties runProperties29 = new RunProperties();
            Text text53 = new Text();
            text53.Text = "Válido";

            run53.Append(runProperties29);
            run53.Append(text53);

            paragraph35.Append(paragraphProperties19);
            paragraph35.Append(run53);

            tableCell30.Append(tableCellProperties30);
            tableCell30.Append(paragraph35);

            TableCell tableCell31 = new TableCell();

            TableCellProperties tableCellProperties31 = new TableCellProperties();
            TableCellWidth tableCellWidth31 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties31.Append(tableCellWidth31);

            Paragraph paragraph36 = new Paragraph() { RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "00A6498A" };

            ParagraphProperties paragraphProperties20 = new ParagraphProperties();
            Justification justification12 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties19 = new ParagraphMarkRunProperties();

            paragraphProperties20.Append(justification12);
            paragraphProperties20.Append(paragraphMarkRunProperties19);

            Run run54 = new Run();

            RunProperties runProperties30 = new RunProperties();
            Text text54 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            //Cantidad total de medidas validas
            var cantValidTot = report.Measures.Count(m => m.Valid);
            text54.Text = cantValidTot.ToString();

            run54.Append(runProperties30);
            run54.Append(text54);

            paragraph36.Append(paragraphProperties20);
            paragraph36.Append(run54);

            tableCell31.Append(tableCellProperties31);
            tableCell31.Append(paragraph36);

            TableCell tableCell32 = new TableCell();

            TableCellProperties tableCellProperties32 = new TableCellProperties();
            TableCellWidth tableCellWidth32 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties32.Append(tableCellWidth32);

            Paragraph paragraph37 = new Paragraph() { RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "00A6498A" };

            ParagraphProperties paragraphProperties21 = new ParagraphProperties();
            Justification justification13 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties20 = new ParagraphMarkRunProperties();

            paragraphProperties21.Append(justification13);
            paragraphProperties21.Append(paragraphMarkRunProperties20);

            Run run57 = new Run();

            RunProperties runProperties33 = new RunProperties();
            Text text57 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            //Cantidad de medidas validas tomadas durante el dia
            var cantValidDay = report.Measures.Count(m => (m.Valid && (bool)(!m.Asleep)));
            text57.Text = cantValidDay.ToString();

            run57.Append(runProperties33);
            run57.Append(text57);

            paragraph37.Append(paragraphProperties21);
            paragraph37.Append(run57);

            tableCell32.Append(tableCellProperties32);
            tableCell32.Append(paragraph37);

            TableCell tableCell33 = new TableCell();

            TableCellProperties tableCellProperties33 = new TableCellProperties();
            TableCellWidth tableCellWidth33 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };

            tableCellProperties33.Append(tableCellWidth33);

            Paragraph paragraph38 = new Paragraph() { RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "00A6498A" };

            ParagraphProperties paragraphProperties22 = new ParagraphProperties();
            Justification justification14 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties21 = new ParagraphMarkRunProperties();

            paragraphProperties22.Append(justification14);
            paragraphProperties22.Append(paragraphMarkRunProperties21);

            Run run60 = new Run();

            RunProperties runProperties36 = new RunProperties();
            Text text60 = new Text();
            //Cantidad de medidas validas tomadas durante la noche
            var cantValidNight = report.Measures.Count(m => m.Valid && (bool)m.Asleep);
            text60.Text = cantValidNight.ToString();

            run60.Append(runProperties36);
            run60.Append(text60);

            paragraph38.Append(paragraphProperties22);
            paragraph38.Append(run60);

            tableCell33.Append(tableCellProperties33);
            tableCell33.Append(paragraph38);

            tableRow16.Append(tableCell30);
            tableRow16.Append(tableCell31);
            tableRow16.Append(tableCell32);
            tableRow16.Append(tableCell33);

            TableRow tableRow17 = new TableRow() { RsidTableRowAddition = "00A6498A", RsidTableRowProperties = "00A6498A" };

            TableCell tableCell34 = new TableCell();

            TableCellProperties tableCellProperties34 = new TableCellProperties();
            TableCellWidth tableCellWidth34 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties34.Append(tableCellWidth34);

            Paragraph paragraph39 = new Paragraph() { RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "00A6498A" };

            ParagraphProperties paragraphProperties23 = new ParagraphProperties();
            Justification justification15 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties22 = new ParagraphMarkRunProperties();

            paragraphProperties23.Append(justification15);
            paragraphProperties23.Append(paragraphMarkRunProperties22);

            Run run61 = new Run();

            RunProperties runProperties37 = new RunProperties();
            Text text61 = new Text();
            text61.Text = "% Válido";

            run61.Append(runProperties37);
            run61.Append(text61);

            paragraph39.Append(paragraphProperties23);
            paragraph39.Append(run61);

            tableCell34.Append(tableCellProperties34);
            tableCell34.Append(paragraph39);

            TableCell tableCell35 = new TableCell();

            TableCellProperties tableCellProperties35 = new TableCellProperties();
            TableCellWidth tableCellWidth35 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties35.Append(tableCellWidth35);

            Paragraph paragraph40 = new Paragraph() { RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "00A6498A" };

            ParagraphProperties paragraphProperties24 = new ParagraphProperties();
            Justification justification16 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties23 = new ParagraphMarkRunProperties();

            paragraphProperties24.Append(justification16);
            paragraphProperties24.Append(paragraphMarkRunProperties23);

            Run run62 = new Run();

            RunProperties runProperties38 = new RunProperties();
            Text text62 = new Text();
            //Porcentaje de medidas validas totales
            var percentageTotal = (cantValidTot * 100) / cantMeasureTotal;
            text62.Text = percentageTotal.ToString();

            run62.Append(runProperties38);
            run62.Append(text62);

            paragraph40.Append(paragraphProperties24);
            paragraph40.Append(run62);

            tableCell35.Append(tableCellProperties35);
            tableCell35.Append(paragraph40);

            TableCell tableCell36 = new TableCell();

            TableCellProperties tableCellProperties36 = new TableCellProperties();
            TableCellWidth tableCellWidth36 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties36.Append(tableCellWidth36);

            Paragraph paragraph41 = new Paragraph() { RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "00A6498A" };

            ParagraphProperties paragraphProperties25 = new ParagraphProperties();
            Justification justification17 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties24 = new ParagraphMarkRunProperties();

            paragraphProperties25.Append(justification17);
            paragraphProperties25.Append(paragraphMarkRunProperties24);

            Run run63 = new Run();

            RunProperties runProperties39 = new RunProperties();
            Text text63 = new Text();
            //Porcentage de medidas validas durante el dia
            var percentageDay = (cantValidDay * 100) / cantMeasureDay;
            text63.Text = percentageDay.ToString();

            run63.Append(runProperties39);
            run63.Append(text63);

            paragraph41.Append(paragraphProperties25);
            paragraph41.Append(run63);

            tableCell36.Append(tableCellProperties36);
            tableCell36.Append(paragraph41);

            TableCell tableCell37 = new TableCell();

            TableCellProperties tableCellProperties37 = new TableCellProperties();
            TableCellWidth tableCellWidth37 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };

            tableCellProperties37.Append(tableCellWidth37);

            Paragraph paragraph42 = new Paragraph() { RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "00C204AF" };

            ParagraphProperties paragraphProperties26 = new ParagraphProperties();
            Justification justification18 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties25 = new ParagraphMarkRunProperties();

            paragraphProperties26.Append(justification18);
            paragraphProperties26.Append(paragraphMarkRunProperties25);

            Run run67 = new Run();

            RunProperties runProperties43 = new RunProperties();
            Text text67 = new Text();
            //Porcentaje de medidas validas durante la noche
            var percentageNight = (cantValidNight * 100) / cantMeasureNight;
            text67.Text = percentageNight.ToString();

            run67.Append(runProperties43);
            run67.Append(text67);

            paragraph42.Append(paragraphProperties26);
            paragraph42.Append(run67);

            tableCell37.Append(tableCellProperties37);
            tableCell37.Append(paragraph42);

            tableRow17.Append(tableCell34);
            tableRow17.Append(tableCell35);
            tableRow17.Append(tableCell36);
            tableRow17.Append(tableCell37);

            TableRow tableRow18 = new TableRow() { RsidTableRowAddition = "00C204AF", RsidTableRowProperties = "00C204AF" };

            TableCell tableCell38 = new TableCell();

            TableCellProperties tableCellProperties38 = new TableCellProperties();
            TableCellWidth tableCellWidth38 = new TableCellWidth() { Width = "9345", Type = TableWidthUnitValues.Dxa };
            GridSpan gridSpan6 = new GridSpan() { Val = 4 };
            Shading shading5 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "BFBFBF", ThemeFill = ThemeColorValues.Background1, ThemeFillShade = "BF" };

            tableCellProperties38.Append(tableCellWidth38);
            tableCellProperties38.Append(gridSpan6);
            tableCellProperties38.Append(shading5);

            Paragraph paragraph43 = new Paragraph() { RsidParagraphMarkRevision = "00C204AF", RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "00C204AF", RsidRunAdditionDefault = "00C204AF" };

            ParagraphProperties paragraphProperties27 = new ParagraphProperties();

            ParagraphMarkRunProperties paragraphMarkRunProperties26 = new ParagraphMarkRunProperties();
            Bold bold17 = new Bold();
            paragraphMarkRunProperties26.Append(bold17);

            paragraphProperties27.Append(paragraphMarkRunProperties26);

            Run run68 = new Run() { RsidRunProperties = "00C204AF" };

            RunProperties runProperties44 = new RunProperties();
            Bold bold18 = new Bold();
            runProperties44.Append(bold18);
            Text text68 = new Text();
            text68.Text = "Promedio";

            run68.Append(runProperties44);
            run68.Append(text68);

            paragraph43.Append(paragraphProperties27);
            paragraph43.Append(run68);

            tableCell38.Append(tableCellProperties38);
            tableCell38.Append(paragraph43);

            tableRow18.Append(tableCell38);

            TableRow tableRow19 = new TableRow() { RsidTableRowAddition = "00C204AF", RsidTableRowProperties = "0075392D" };

            TableCell tableCell39 = new TableCell();

            TableCellProperties tableCellProperties39 = new TableCellProperties();
            TableCellWidth tableCellWidth39 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties39.Append(tableCellWidth39);

            Paragraph paragraph44 = new Paragraph() { RsidParagraphMarkRevision = "00C204AF", RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "00C204AF" };

            ParagraphProperties paragraphProperties28 = new ParagraphProperties();
            Justification justification19 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties27 = new ParagraphMarkRunProperties();

            paragraphProperties28.Append(justification19);
            paragraphProperties28.Append(paragraphMarkRunProperties27);

            Run run69 = new Run();

            RunProperties runProperties45 = new RunProperties();
            Text text69 = new Text();
            text69.Text = "Sistole";

            run69.Append(runProperties45);
            run69.Append(text69);

            paragraph44.Append(paragraphProperties28);
            paragraph44.Append(run69);

            tableCell39.Append(tableCellProperties39);
            tableCell39.Append(paragraph44);

            TableCell tableCell40 = new TableCell();

            TableCellProperties tableCellProperties40 = new TableCellProperties();
            TableCellWidth tableCellWidth40 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties40.Append(tableCellWidth40);

            Paragraph paragraph45 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties29 = new ParagraphProperties();
            Justification justification20 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties28 = new ParagraphMarkRunProperties();

            paragraphProperties29.Append(justification20);
            paragraphProperties29.Append(paragraphMarkRunProperties28);

            Run run71 = new Run();

            RunProperties runProperties47 = new RunProperties();
            Text text71 = new Text();
            //Promedio total de sistolica
            text71.Text = report.SystolicTotalAvg.ToString();

            run71.Append(runProperties47);
            run71.Append(text71);

            paragraph45.Append(paragraphProperties29);
            paragraph45.Append(run71);

            tableCell40.Append(tableCellProperties40);
            tableCell40.Append(paragraph45);

            TableCell tableCell41 = new TableCell();

            TableCellProperties tableCellProperties41 = new TableCellProperties();
            TableCellWidth tableCellWidth41 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties41.Append(tableCellWidth41);

            Paragraph paragraph46 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties30 = new ParagraphProperties();
            Justification justification21 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties29 = new ParagraphMarkRunProperties();

            paragraphProperties30.Append(justification21);
            paragraphProperties30.Append(paragraphMarkRunProperties29);

            Run run78 = new Run();

            RunProperties runProperties54 = new RunProperties();
            Text text78 = new Text();
            //Promedio dia sistolica
            text78.Text = report.SystolicDayAvg.ToString();

            run78.Append(runProperties54);
            run78.Append(text78);

            paragraph46.Append(paragraphProperties30);
            paragraph46.Append(run78);

            tableCell41.Append(tableCellProperties41);
            tableCell41.Append(paragraph46);

            TableCell tableCell42 = new TableCell();

            TableCellProperties tableCellProperties42 = new TableCellProperties();
            TableCellWidth tableCellWidth42 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };

            tableCellProperties42.Append(tableCellWidth42);

            Paragraph paragraph47 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties31 = new ParagraphProperties();
            Justification justification22 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties30 = new ParagraphMarkRunProperties();

            paragraphProperties31.Append(justification22);
            paragraphProperties31.Append(paragraphMarkRunProperties30);

            Run run85 = new Run();

            RunProperties runProperties61 = new RunProperties();
            Text text85 = new Text();
            //Promedio noche sistolica
            text85.Text = report.SystolicNightAvg.ToString();

            run85.Append(runProperties61);
            run85.Append(text85);

            paragraph47.Append(paragraphProperties31);
            paragraph47.Append(run85);

            tableCell42.Append(tableCellProperties42);
            tableCell42.Append(paragraph47);

            tableRow19.Append(tableCell39);
            tableRow19.Append(tableCell40);
            tableRow19.Append(tableCell41);
            tableRow19.Append(tableCell42);

            TableRow tableRow20 = new TableRow() { RsidTableRowAddition = "00C204AF", RsidTableRowProperties = "0075392D" };

            TableCell tableCell43 = new TableCell();

            TableCellProperties tableCellProperties43 = new TableCellProperties();
            TableCellWidth tableCellWidth43 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties43.Append(tableCellWidth43);

            Paragraph paragraph48 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "00C204AF" };

            ParagraphProperties paragraphProperties32 = new ParagraphProperties();
            Justification justification23 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties31 = new ParagraphMarkRunProperties();

            paragraphProperties32.Append(justification23);
            paragraphProperties32.Append(paragraphMarkRunProperties31);

            Run run90 = new Run();

            RunProperties runProperties66 = new RunProperties();
            Text text90 = new Text();
            text90.Text = "Diástole";

            run90.Append(runProperties66);
            run90.Append(text90);

            paragraph48.Append(paragraphProperties32);
            paragraph48.Append(run90);

            tableCell43.Append(tableCellProperties43);
            tableCell43.Append(paragraph48);

            TableCell tableCell44 = new TableCell();

            TableCellProperties tableCellProperties44 = new TableCellProperties();
            TableCellWidth tableCellWidth44 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties44.Append(tableCellWidth44);

            Paragraph paragraph49 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties33 = new ParagraphProperties();
            Justification justification24 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties32 = new ParagraphMarkRunProperties();

            paragraphProperties33.Append(justification24);
            paragraphProperties33.Append(paragraphMarkRunProperties32);

            Run run91 = new Run();

            RunProperties runProperties67 = new RunProperties();
            Text text91 = new Text();
            //Promedio del total de medidas diastolicas
            text91.Text = report.DiastolicTotalAvg.ToString();

            run91.Append(runProperties67);
            run91.Append(text91);

            paragraph49.Append(paragraphProperties33);
            paragraph49.Append(run91);

            tableCell44.Append(tableCellProperties44);
            tableCell44.Append(paragraph49);

            TableCell tableCell45 = new TableCell();

            TableCellProperties tableCellProperties45 = new TableCellProperties();
            TableCellWidth tableCellWidth45 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties45.Append(tableCellWidth45);

            Paragraph paragraph50 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties34 = new ParagraphProperties();
            Justification justification25 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties33 = new ParagraphMarkRunProperties();

            paragraphProperties34.Append(justification25);
            paragraphProperties34.Append(paragraphMarkRunProperties33);

            Run run98 = new Run();

            RunProperties runProperties74 = new RunProperties();
            Text text98 = new Text();
            //Promedio dia diastolica
            text98.Text = report.DiastolicDayAvg.ToString();

            run98.Append(runProperties74);
            run98.Append(text98);

            paragraph50.Append(paragraphProperties34);
            paragraph50.Append(run98);

            tableCell45.Append(tableCellProperties45);
            tableCell45.Append(paragraph50);

            TableCell tableCell46 = new TableCell();

            TableCellProperties tableCellProperties46 = new TableCellProperties();
            TableCellWidth tableCellWidth46 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };

            tableCellProperties46.Append(tableCellWidth46);

            Paragraph paragraph51 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties35 = new ParagraphProperties();
            Justification justification26 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties34 = new ParagraphMarkRunProperties();

            paragraphProperties35.Append(justification26);
            paragraphProperties35.Append(paragraphMarkRunProperties34);

            Run run105 = new Run();

            RunProperties runProperties81 = new RunProperties();
            Text text105 = new Text();
            //Promedio noche diastolica
            text105.Text = report.DiastolicNightAvg.ToString();

            run105.Append(runProperties81);
            run105.Append(text105);

            paragraph51.Append(paragraphProperties35);
            paragraph51.Append(run105);

            tableCell46.Append(tableCellProperties46);
            tableCell46.Append(paragraph51);

            tableRow20.Append(tableCell43);
            tableRow20.Append(tableCell44);
            tableRow20.Append(tableCell45);
            tableRow20.Append(tableCell46);

            TableRow tableRow21 = new TableRow() { RsidTableRowAddition = "00C204AF", RsidTableRowProperties = "0075392D" };

            TableCell tableCell47 = new TableCell();

            TableCellProperties tableCellProperties47 = new TableCellProperties();
            TableCellWidth tableCellWidth47 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties47.Append(tableCellWidth47);

            Paragraph paragraph52 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "00C204AF" };

            ParagraphProperties paragraphProperties36 = new ParagraphProperties();
            Justification justification27 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties35 = new ParagraphMarkRunProperties();

            paragraphProperties36.Append(justification27);
            paragraphProperties36.Append(paragraphMarkRunProperties35);

            Run run110 = new Run();

            RunProperties runProperties86 = new RunProperties();
            Text text110 = new Text();
            text110.Text = "TAM";

            run110.Append(runProperties86);
            run110.Append(text110);

            paragraph52.Append(paragraphProperties36);
            paragraph52.Append(run110);

            tableCell47.Append(tableCellProperties47);
            tableCell47.Append(paragraph52);

            TableCell tableCell48 = new TableCell();

            TableCellProperties tableCellProperties48 = new TableCellProperties();
            TableCellWidth tableCellWidth48 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties48.Append(tableCellWidth48);

            Paragraph paragraph53 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties37 = new ParagraphProperties();
            Justification justification28 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties36 = new ParagraphMarkRunProperties();

            paragraphProperties37.Append(justification28);
            paragraphProperties37.Append(paragraphMarkRunProperties36);

            Run run111 = new Run();

            RunProperties runProperties87 = new RunProperties();
            Text text111 = new Text();
            //Pormedio total tam
            text111.Text = report.MiddleTotalAvg.ToString();

            run111.Append(runProperties87);
            run111.Append(text111);

            paragraph53.Append(paragraphProperties37);
            paragraph53.Append(run111);

            tableCell48.Append(tableCellProperties48);
            tableCell48.Append(paragraph53);

            TableCell tableCell49 = new TableCell();

            TableCellProperties tableCellProperties49 = new TableCellProperties();
            TableCellWidth tableCellWidth49 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties49.Append(tableCellWidth49);

            Paragraph paragraph54 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties38 = new ParagraphProperties();
            Justification justification29 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties37 = new ParagraphMarkRunProperties();

            paragraphProperties38.Append(justification29);
            paragraphProperties38.Append(paragraphMarkRunProperties37);

            Run run118 = new Run();

            RunProperties runProperties94 = new RunProperties();
            Text text118 = new Text();
            //Pormedio dia TAM
            text118.Text = report.MiddleDayAvg.ToString();

            run118.Append(runProperties94);
            run118.Append(text118);

            paragraph54.Append(paragraphProperties38);
            paragraph54.Append(run118);

            tableCell49.Append(tableCellProperties49);
            tableCell49.Append(paragraph54);

            TableCell tableCell50 = new TableCell();

            TableCellProperties tableCellProperties50 = new TableCellProperties();
            TableCellWidth tableCellWidth50 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };

            tableCellProperties50.Append(tableCellWidth50);

            Paragraph paragraph55 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties39 = new ParagraphProperties();
            Justification justification30 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties38 = new ParagraphMarkRunProperties();

            paragraphProperties39.Append(justification30);
            paragraphProperties39.Append(paragraphMarkRunProperties38);

            Run run125 = new Run();

            RunProperties runProperties101 = new RunProperties();
            Text text125 = new Text();
            //Promedio noche TAM
            text125.Text = report.MiddleNightAvg.ToString();

            run125.Append(runProperties101);
            run125.Append(text125);

            paragraph55.Append(paragraphProperties39);
            paragraph55.Append(run125);

            tableCell50.Append(tableCellProperties50);
            tableCell50.Append(paragraph55);

            tableRow21.Append(tableCell47);
            tableRow21.Append(tableCell48);
            tableRow21.Append(tableCell49);
            tableRow21.Append(tableCell50);

            TableRow tableRow22 = new TableRow() { RsidTableRowAddition = "00C204AF", RsidTableRowProperties = "0075392D" };

            TableCell tableCell51 = new TableCell();

            TableCellProperties tableCellProperties51 = new TableCellProperties();
            TableCellWidth tableCellWidth51 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties51.Append(tableCellWidth51);

            Paragraph paragraph56 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "00C204AF" };

            ParagraphProperties paragraphProperties40 = new ParagraphProperties();
            Justification justification31 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties39 = new ParagraphMarkRunProperties();

            paragraphProperties40.Append(justification31);
            paragraphProperties40.Append(paragraphMarkRunProperties39);

            Run run130 = new Run();

            RunProperties runProperties106 = new RunProperties();
            Text text130 = new Text();
            text130.Text = "FC";

            run130.Append(runProperties106);
            run130.Append(text130);

            paragraph56.Append(paragraphProperties40);
            paragraph56.Append(run130);

            tableCell51.Append(tableCellProperties51);
            tableCell51.Append(paragraph56);

            TableCell tableCell52 = new TableCell();

            TableCellProperties tableCellProperties52 = new TableCellProperties();
            TableCellWidth tableCellWidth52 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties52.Append(tableCellWidth52);

            Paragraph paragraph57 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties41 = new ParagraphProperties();
            Justification justification32 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties40 = new ParagraphMarkRunProperties();

            paragraphProperties41.Append(justification32);
            paragraphProperties41.Append(paragraphMarkRunProperties40);

            Run run131 = new Run();

            RunProperties runProperties107 = new RunProperties();
            Text text131 = new Text();
            //Pormedio total frecuencia cardiaca
            text131.Text = report.HeartRateTotalAvg.ToString();

            run131.Append(runProperties107);
            run131.Append(text131);

            paragraph57.Append(paragraphProperties41);
            paragraph57.Append(run131);

            tableCell52.Append(tableCellProperties52);
            tableCell52.Append(paragraph57);

            TableCell tableCell53 = new TableCell();

            TableCellProperties tableCellProperties53 = new TableCellProperties();
            TableCellWidth tableCellWidth53 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties53.Append(tableCellWidth53);

            Paragraph paragraph58 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties42 = new ParagraphProperties();
            Justification justification33 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties41 = new ParagraphMarkRunProperties();
            paragraphProperties42.Append(justification33);
            paragraphProperties42.Append(paragraphMarkRunProperties41);

            Run run138 = new Run();

            RunProperties runProperties114 = new RunProperties();
            Text text138 = new Text();
            //Promedio dia frecuencia cardiaca
            text138.Text = report.HeartRateDayAvg.ToString();

            run138.Append(runProperties114);
            run138.Append(text138);

            paragraph58.Append(paragraphProperties42);
            paragraph58.Append(run138);

            tableCell53.Append(tableCellProperties53);
            tableCell53.Append(paragraph58);

            TableCell tableCell54 = new TableCell();

            TableCellProperties tableCellProperties54 = new TableCellProperties();
            TableCellWidth tableCellWidth54 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };

            tableCellProperties54.Append(tableCellWidth54);

            Paragraph paragraph59 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties43 = new ParagraphProperties();
            Justification justification34 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties42 = new ParagraphMarkRunProperties();

            paragraphProperties43.Append(justification34);
            paragraphProperties43.Append(paragraphMarkRunProperties42);

            Run run145 = new Run();

            RunProperties runProperties121 = new RunProperties();
            Text text145 = new Text();
            //Promedio noche frecuencia cardiaca
            text145.Text = report.HeartRateNightAvg.ToString();

            run145.Append(runProperties121);
            run145.Append(text145);

            paragraph59.Append(paragraphProperties43);
            paragraph59.Append(run145);

            tableCell54.Append(tableCellProperties54);
            tableCell54.Append(paragraph59);

            tableRow22.Append(tableCell51);
            tableRow22.Append(tableCell52);
            tableRow22.Append(tableCell53);
            tableRow22.Append(tableCell54);

            TableRow tableRow23 = new TableRow() { RsidTableRowAddition = "00C204AF", RsidTableRowProperties = "00C204AF" };

            TableCell tableCell55 = new TableCell();

            TableCellProperties tableCellProperties55 = new TableCellProperties();
            TableCellWidth tableCellWidth55 = new TableCellWidth() { Width = "9345", Type = TableWidthUnitValues.Dxa };
            GridSpan gridSpan7 = new GridSpan() { Val = 4 };
            Shading shading6 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "BFBFBF", ThemeFill = ThemeColorValues.Background1, ThemeFillShade = "BF" };

            tableCellProperties55.Append(tableCellWidth55);
            tableCellProperties55.Append(gridSpan7);
            tableCellProperties55.Append(shading6);

            Paragraph paragraph60 = new Paragraph() { RsidParagraphMarkRevision = "00C204AF", RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "00C204AF", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties44 = new ParagraphProperties();

            ParagraphMarkRunProperties paragraphMarkRunProperties43 = new ParagraphMarkRunProperties();
            Bold bold19 = new Bold();
            paragraphMarkRunProperties43.Append(bold19);

            paragraphProperties44.Append(paragraphMarkRunProperties43);

            Run run150 = new Run();

            RunProperties runProperties126 = new RunProperties();
            Bold bold20 = new Bold();
            runProperties126.Append(bold20);
            Text text150 = new Text();
            text150.Text = "Desviación Estandar";

            run150.Append(runProperties126);
            run150.Append(text150);

            paragraph60.Append(paragraphProperties44);
            paragraph60.Append(run150);

            tableCell55.Append(tableCellProperties55);
            tableCell55.Append(paragraph60);

            tableRow23.Append(tableCell55);

            TableRow tableRow24 = new TableRow() { RsidTableRowAddition = "00C204AF", RsidTableRowProperties = "0075392D" };

            TableCell tableCell56 = new TableCell();

            TableCellProperties tableCellProperties56 = new TableCellProperties();
            TableCellWidth tableCellWidth56 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties56.Append(tableCellWidth56);

            Paragraph paragraph61 = new Paragraph() { RsidParagraphMarkRevision = "00C204AF", RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "00C204AF" };

            ParagraphProperties paragraphProperties45 = new ParagraphProperties();
            Justification justification35 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties44 = new ParagraphMarkRunProperties();

            paragraphProperties45.Append(justification35);
            paragraphProperties45.Append(paragraphMarkRunProperties44);

            Run run154 = new Run();

            RunProperties runProperties130 = new RunProperties();
            Text text154 = new Text();
            text154.Text = "Sistole";

            run154.Append(runProperties130);
            run154.Append(text154);

            paragraph61.Append(paragraphProperties45);
            paragraph61.Append(run154);

            tableCell56.Append(tableCellProperties56);
            tableCell56.Append(paragraph61);

            TableCell tableCell57 = new TableCell();

            TableCellProperties tableCellProperties57 = new TableCellProperties();
            TableCellWidth tableCellWidth57 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties57.Append(tableCellWidth57);

            Paragraph paragraph62 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties46 = new ParagraphProperties();
            Justification justification36 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties45 = new ParagraphMarkRunProperties();

            paragraphProperties46.Append(justification36);
            paragraphProperties46.Append(paragraphMarkRunProperties45);

            Run run156 = new Run();

            RunProperties runProperties132 = new RunProperties();
            Text text156 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            //Desviacion estandar total sistolica 
            text156.Text = report.StandardDeviationSysTotal.ToString();

            run156.Append(runProperties132);
            run156.Append(text156);

            paragraph62.Append(paragraphProperties46);
            paragraph62.Append(run156);

            tableCell57.Append(tableCellProperties57);
            tableCell57.Append(paragraph62);

            TableCell tableCell58 = new TableCell();

            TableCellProperties tableCellProperties58 = new TableCellProperties();
            TableCellWidth tableCellWidth58 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties58.Append(tableCellWidth58);

            Paragraph paragraph63 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties47 = new ParagraphProperties();
            Justification justification37 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties46 = new ParagraphMarkRunProperties();

            paragraphProperties47.Append(justification37);
            paragraphProperties47.Append(paragraphMarkRunProperties46);

            Run run161 = new Run();

            RunProperties runProperties137 = new RunProperties();
            Text text161 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            //Desviacion estandar sistolica del dia
            text161.Text = report.StandardDeviationSysDay.ToString();

            run161.Append(runProperties137);
            run161.Append(text161);

            paragraph63.Append(paragraphProperties47);
            paragraph63.Append(run161);

            tableCell58.Append(tableCellProperties58);
            tableCell58.Append(paragraph63);

            TableCell tableCell59 = new TableCell();

            TableCellProperties tableCellProperties59 = new TableCellProperties();
            TableCellWidth tableCellWidth59 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };

            tableCellProperties59.Append(tableCellWidth59);

            Paragraph paragraph64 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties48 = new ParagraphProperties();
            Justification justification38 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties47 = new ParagraphMarkRunProperties();

            paragraphProperties48.Append(justification38);
            paragraphProperties48.Append(paragraphMarkRunProperties47);

            Run run166 = new Run();

            RunProperties runProperties142 = new RunProperties();
            Text text166 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            //Desvacion estandar noche sistolica
            text166.Text = report.StandardDeviationSysNight.ToString();

            run166.Append(runProperties142);
            run166.Append(text166);

            paragraph64.Append(paragraphProperties48);
            paragraph64.Append(run166);

            tableCell59.Append(tableCellProperties59);
            tableCell59.Append(paragraph64);

            tableRow24.Append(tableCell56);
            tableRow24.Append(tableCell57);
            tableRow24.Append(tableCell58);
            tableRow24.Append(tableCell59);

            TableRow tableRow25 = new TableRow() { RsidTableRowAddition = "00C204AF", RsidTableRowProperties = "0075392D" };

            TableCell tableCell60 = new TableCell();

            TableCellProperties tableCellProperties60 = new TableCellProperties();
            TableCellWidth tableCellWidth60 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties60.Append(tableCellWidth60);

            Paragraph paragraph65 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "00C204AF" };

            ParagraphProperties paragraphProperties49 = new ParagraphProperties();
            Justification justification39 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties48 = new ParagraphMarkRunProperties();

            paragraphProperties49.Append(justification39);
            paragraphProperties49.Append(paragraphMarkRunProperties48);

            Run run169 = new Run();

            RunProperties runProperties145 = new RunProperties();
            Text text169 = new Text();
            text169.Text = "Diástole";

            run169.Append(runProperties145);
            run169.Append(text169);

            paragraph65.Append(paragraphProperties49);
            paragraph65.Append(run169);

            tableCell60.Append(tableCellProperties60);
            tableCell60.Append(paragraph65);

            TableCell tableCell61 = new TableCell();

            TableCellProperties tableCellProperties61 = new TableCellProperties();
            TableCellWidth tableCellWidth61 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties61.Append(tableCellWidth61);

            Paragraph paragraph66 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties50 = new ParagraphProperties();
            Justification justification40 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties49 = new ParagraphMarkRunProperties();

            paragraphProperties50.Append(justification40);
            paragraphProperties50.Append(paragraphMarkRunProperties49);

            Run run170 = new Run();

            RunProperties runProperties146 = new RunProperties();
            Text text170 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            //desviacion estandar diastolica total
            text170.Text = report.StandardDeviationDiasTotal.ToString();

            run170.Append(runProperties146);
            run170.Append(text170);

            paragraph66.Append(paragraphProperties50);
            paragraph66.Append(run170);

            tableCell61.Append(tableCellProperties61);
            tableCell61.Append(paragraph66);

            TableCell tableCell62 = new TableCell();

            TableCellProperties tableCellProperties62 = new TableCellProperties();
            TableCellWidth tableCellWidth62 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties62.Append(tableCellWidth62);

            Paragraph paragraph67 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties51 = new ParagraphProperties();
            Justification justification41 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties50 = new ParagraphMarkRunProperties();

            paragraphProperties51.Append(justification41);
            paragraphProperties51.Append(paragraphMarkRunProperties50);

            Run run175 = new Run();

            RunProperties runProperties151 = new RunProperties();
            Text text175 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            //Desviacion estandar dia diastolica
            text175.Text = report.StandardDeviationDiasDay.ToString();

            run175.Append(runProperties151);
            run175.Append(text175);

            paragraph67.Append(paragraphProperties51);
            paragraph67.Append(run175);

            tableCell62.Append(tableCellProperties62);
            tableCell62.Append(paragraph67);

            TableCell tableCell63 = new TableCell();

            TableCellProperties tableCellProperties63 = new TableCellProperties();
            TableCellWidth tableCellWidth63 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };

            tableCellProperties63.Append(tableCellWidth63);

            Paragraph paragraph68 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties52 = new ParagraphProperties();
            Justification justification42 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties51 = new ParagraphMarkRunProperties();

            paragraphProperties52.Append(justification42);
            paragraphProperties52.Append(paragraphMarkRunProperties51);

            Run run180 = new Run();

            RunProperties runProperties156 = new RunProperties();
            Text text180 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            //Desviacion estandar noche diastolica
            text180.Text = report.StandardDeviationDiasNight.ToString();

            run180.Append(runProperties156);
            run180.Append(text180);

            paragraph68.Append(paragraphProperties52);
            paragraph68.Append(run180);

            tableCell63.Append(tableCellProperties63);
            tableCell63.Append(paragraph68);

            tableRow25.Append(tableCell60);
            tableRow25.Append(tableCell61);
            tableRow25.Append(tableCell62);
            tableRow25.Append(tableCell63);

            TableRow tableRow26 = new TableRow() { RsidTableRowAddition = "00C204AF", RsidTableRowProperties = "0075392D" };

            TableCell tableCell64 = new TableCell();

            TableCellProperties tableCellProperties64 = new TableCellProperties();
            TableCellWidth tableCellWidth64 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties64.Append(tableCellWidth64);

            Paragraph paragraph69 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "00C204AF" };

            ParagraphProperties paragraphProperties53 = new ParagraphProperties();
            Justification justification43 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties52 = new ParagraphMarkRunProperties();

            paragraphProperties53.Append(justification43);
            paragraphProperties53.Append(paragraphMarkRunProperties52);

            Run run183 = new Run();

            RunProperties runProperties159 = new RunProperties();
            Text text183 = new Text();
            text183.Text = "TAM";

            run183.Append(runProperties159);
            run183.Append(text183);

            paragraph69.Append(paragraphProperties53);
            paragraph69.Append(run183);

            tableCell64.Append(tableCellProperties64);
            tableCell64.Append(paragraph69);

            TableCell tableCell65 = new TableCell();

            TableCellProperties tableCellProperties65 = new TableCellProperties();
            TableCellWidth tableCellWidth65 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties65.Append(tableCellWidth65);

            Paragraph paragraph70 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties54 = new ParagraphProperties();
            Justification justification44 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties53 = new ParagraphMarkRunProperties();

            paragraphProperties54.Append(justification44);
            paragraphProperties54.Append(paragraphMarkRunProperties53);

            Run run184 = new Run();

            RunProperties runProperties160 = new RunProperties();
            Text text184 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            //Desviacion estandar total TAM
            text184.Text = report.StandardDeviationTamTotal.ToString();

            run184.Append(runProperties160);
            run184.Append(text184);

            paragraph70.Append(paragraphProperties54);
            paragraph70.Append(run184);

            tableCell65.Append(tableCellProperties65);
            tableCell65.Append(paragraph70);

            TableCell tableCell66 = new TableCell();

            TableCellProperties tableCellProperties66 = new TableCellProperties();
            TableCellWidth tableCellWidth66 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties66.Append(tableCellWidth66);

            Paragraph paragraph71 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties55 = new ParagraphProperties();
            Justification justification45 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties54 = new ParagraphMarkRunProperties();

            paragraphProperties55.Append(justification45);
            paragraphProperties55.Append(paragraphMarkRunProperties54);

            Run run189 = new Run();

            RunProperties runProperties165 = new RunProperties();
            Text text189 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            //Desviacion estandar dia TAM
            text189.Text = report.StandardDeviationTamDay.ToString();

            run189.Append(runProperties165);
            run189.Append(text189);

            paragraph71.Append(paragraphProperties55);
            paragraph71.Append(run189);

            tableCell66.Append(tableCellProperties66);
            tableCell66.Append(paragraph71);

            TableCell tableCell67 = new TableCell();

            TableCellProperties tableCellProperties67 = new TableCellProperties();
            TableCellWidth tableCellWidth67 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };

            tableCellProperties67.Append(tableCellWidth67);

            Paragraph paragraph72 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties56 = new ParagraphProperties();
            Justification justification46 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties55 = new ParagraphMarkRunProperties();

            paragraphProperties56.Append(justification46);
            paragraphProperties56.Append(paragraphMarkRunProperties55);

            Run run194 = new Run();

            RunProperties runProperties170 = new RunProperties();
            Text text194 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            //Desviacion estandar noche TAM
            text194.Text = report.StandarDeviationTamNight.ToString();

            run194.Append(runProperties170);
            run194.Append(text194);

            paragraph72.Append(paragraphProperties56);
            paragraph72.Append(run194);

            tableCell67.Append(tableCellProperties67);
            tableCell67.Append(paragraph72);

            tableRow26.Append(tableCell64);
            tableRow26.Append(tableCell65);
            tableRow26.Append(tableCell66);
            tableRow26.Append(tableCell67);

            TableRow tableRow27 = new TableRow() { RsidTableRowAddition = "00C204AF", RsidTableRowProperties = "0075392D" };

            TableCell tableCell68 = new TableCell();

            TableCellProperties tableCellProperties68 = new TableCellProperties();
            TableCellWidth tableCellWidth68 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties68.Append(tableCellWidth68);

            Paragraph paragraph73 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "00C204AF" };

            ParagraphProperties paragraphProperties57 = new ParagraphProperties();
            Justification justification47 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties56 = new ParagraphMarkRunProperties();

            paragraphProperties57.Append(justification47);
            paragraphProperties57.Append(paragraphMarkRunProperties56);

            Run run197 = new Run();

            RunProperties runProperties173 = new RunProperties();
            Text text197 = new Text();
            text197.Text = "FC";

            run197.Append(runProperties173);
            run197.Append(text197);

            paragraph73.Append(paragraphProperties57);
            paragraph73.Append(run197);

            tableCell68.Append(tableCellProperties68);
            tableCell68.Append(paragraph73);

            TableCell tableCell69 = new TableCell();

            TableCellProperties tableCellProperties69 = new TableCellProperties();
            TableCellWidth tableCellWidth69 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties69.Append(tableCellWidth69);

            Paragraph paragraph74 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties58 = new ParagraphProperties();
            Justification justification48 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties57 = new ParagraphMarkRunProperties();

            paragraphProperties58.Append(justification48);
            paragraphProperties58.Append(paragraphMarkRunProperties57);

            Run run198 = new Run();

            RunProperties runProperties174 = new RunProperties();
            Text text198 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            //Desviacion estandar total frecuencia cardiaca
            text198.Text = report.StandardDeviationHeartRateTotal.ToString();

            run198.Append(runProperties174);
            run198.Append(text198);

            paragraph74.Append(paragraphProperties58);
            paragraph74.Append(run198);

            tableCell69.Append(tableCellProperties69);
            tableCell69.Append(paragraph74);

            TableCell tableCell70 = new TableCell();

            TableCellProperties tableCellProperties70 = new TableCellProperties();
            TableCellWidth tableCellWidth70 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties70.Append(tableCellWidth70);

            Paragraph paragraph75 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties59 = new ParagraphProperties();
            Justification justification49 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties58 = new ParagraphMarkRunProperties();

            paragraphProperties59.Append(justification49);
            paragraphProperties59.Append(paragraphMarkRunProperties58);

            Run run203 = new Run();

            RunProperties runProperties179 = new RunProperties();
            Text text203 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            //Desviacion estandar frecuencia cardiaca dia
            text203.Text = report.StandardDeviationHeartRateDay.ToString();

            run203.Append(runProperties179);
            run203.Append(text203);

            paragraph75.Append(paragraphProperties59);
            paragraph75.Append(run203);

            tableCell70.Append(tableCellProperties70);
            tableCell70.Append(paragraph75);

            TableCell tableCell71 = new TableCell();

            TableCellProperties tableCellProperties71 = new TableCellProperties();
            TableCellWidth tableCellWidth71 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };

            tableCellProperties71.Append(tableCellWidth71);

            Paragraph paragraph76 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties60 = new ParagraphProperties();
            Justification justification50 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties59 = new ParagraphMarkRunProperties();

            paragraphProperties60.Append(justification50);
            paragraphProperties60.Append(paragraphMarkRunProperties59);

            Run run208 = new Run();

            RunProperties runProperties184 = new RunProperties();
            Text text208 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            //Desviacion estandar frecuencia cardiaca noche
            text208.Text = report.StandardDeviationHeartRateNight.ToString();

            run208.Append(runProperties184);
            run208.Append(text208);

            paragraph76.Append(paragraphProperties60);
            paragraph76.Append(run208);

            tableCell71.Append(tableCellProperties71);
            tableCell71.Append(paragraph76);

            tableRow27.Append(tableCell68);
            tableRow27.Append(tableCell69);
            tableRow27.Append(tableCell70);
            tableRow27.Append(tableCell71);

            TableRow tableRow28 = new TableRow() { RsidTableRowAddition = "00C204AF", RsidTableRowProperties = "00C204AF" };

            TableCell tableCell72 = new TableCell();

            TableCellProperties tableCellProperties72 = new TableCellProperties();
            TableCellWidth tableCellWidth72 = new TableCellWidth() { Width = "9345", Type = TableWidthUnitValues.Dxa };
            GridSpan gridSpan8 = new GridSpan() { Val = 4 };
            Shading shading7 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "BFBFBF", ThemeFill = ThemeColorValues.Background1, ThemeFillShade = "BF" };

            tableCellProperties72.Append(tableCellWidth72);
            tableCellProperties72.Append(gridSpan8);
            tableCellProperties72.Append(shading7);

            Paragraph paragraph77 = new Paragraph() { RsidParagraphMarkRevision = "00C204AF", RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "00C204AF", RsidRunAdditionDefault = "00C204AF" };

            ParagraphProperties paragraphProperties61 = new ParagraphProperties();

            ParagraphMarkRunProperties paragraphMarkRunProperties60 = new ParagraphMarkRunProperties();
            Bold bold24 = new Bold();

            paragraphMarkRunProperties60.Append(bold24);

            paragraphProperties61.Append(paragraphMarkRunProperties60);

            Run run211 = new Run() { RsidRunProperties = "00C204AF" };

            RunProperties runProperties187 = new RunProperties();
            Bold bold25 = new Bold();
            runProperties187.Append(bold25);
            Text text211 = new Text();
            text211.Text = "Valores por encima del límite";

            run211.Append(runProperties187);
            run211.Append(text211);

            paragraph77.Append(paragraphProperties61);
            paragraph77.Append(run211);

            tableCell72.Append(tableCellProperties72);
            tableCell72.Append(paragraph77);

            tableRow28.Append(tableCell72);

            TableRow tableRow29 = new TableRow() { RsidTableRowAddition = "00C204AF", RsidTableRowProperties = "0075392D" };

            TableCell tableCell73 = new TableCell();

            TableCellProperties tableCellProperties73 = new TableCellProperties();
            TableCellWidth tableCellWidth73 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties73.Append(tableCellWidth73);

            Paragraph paragraph78 = new Paragraph() { RsidParagraphMarkRevision = "00C204AF", RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "00C204AF" };

            ParagraphProperties paragraphProperties62 = new ParagraphProperties();
            Justification justification51 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties61 = new ParagraphMarkRunProperties();

            paragraphProperties62.Append(justification51);
            paragraphProperties62.Append(paragraphMarkRunProperties61);

            Run run212 = new Run();

            RunProperties runProperties188 = new RunProperties();
            Text text212 = new Text();
            text212.Text = "Sistole";

            run212.Append(runProperties188);
            run212.Append(text212);

            paragraph78.Append(paragraphProperties62);
            paragraph78.Append(run212);

            tableCell73.Append(tableCellProperties73);
            tableCell73.Append(paragraph78);

            TableCell tableCell74 = new TableCell();

            TableCellProperties tableCellProperties74 = new TableCellProperties();
            TableCellWidth tableCellWidth74 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties74.Append(tableCellWidth74);

            Paragraph paragraph79 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties63 = new ParagraphProperties();
            Justification justification52 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties62 = new ParagraphMarkRunProperties();

            paragraphProperties63.Append(justification52);
            paragraphProperties63.Append(paragraphMarkRunProperties62);

            Run run214 = new Run();

            RunProperties runProperties190 = new RunProperties();
            Text text214 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            //Porcentaje de valores por encima del limite sistolica
            text214.Text = "";

            run214.Append(runProperties190);
            run214.Append(text214);

            paragraph79.Append(paragraphProperties63);
            paragraph79.Append(run214);

            tableCell74.Append(tableCellProperties74);
            tableCell74.Append(paragraph79);

            TableCell tableCell75 = new TableCell();

            TableCellProperties tableCellProperties75 = new TableCellProperties();
            TableCellWidth tableCellWidth75 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties75.Append(tableCellWidth75);

            Paragraph paragraph80 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties64 = new ParagraphProperties();
            Justification justification53 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties63 = new ParagraphMarkRunProperties();

            paragraphProperties64.Append(justification53);
            paragraphProperties64.Append(paragraphMarkRunProperties63);

            Run run221 = new Run();

            RunProperties runProperties197 = new RunProperties();
            Text text221 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            //Porcentaje de valores por encima del limite sistolica
            text221.Text = "< val lim sis>";

            run221.Append(runProperties197);
            run221.Append(text221);

            paragraph80.Append(paragraphProperties64);
            paragraph80.Append(run221);

            tableCell75.Append(tableCellProperties75);
            tableCell75.Append(paragraph80);

            TableCell tableCell76 = new TableCell();

            TableCellProperties tableCellProperties76 = new TableCellProperties();
            TableCellWidth tableCellWidth76 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };

            tableCellProperties76.Append(tableCellWidth76);

            Paragraph paragraph81 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "001B715B", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties65 = new ParagraphProperties();
            Justification justification54 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties64 = new ParagraphMarkRunProperties();

            paragraphProperties65.Append(justification54);
            paragraphProperties65.Append(paragraphMarkRunProperties64);

            Run run228 = new Run();

            RunProperties runProperties204 = new RunProperties();
            Text text228 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            //Porcentaje de valores por encima del limite sistolica
            text228.Text = "< val lim noche sis>";

            run228.Append(runProperties204);
            run228.Append(text228);

            paragraph81.Append(paragraphProperties65);
            paragraph81.Append(run228);

            tableCell76.Append(tableCellProperties76);
            tableCell76.Append(paragraph81);

            tableRow29.Append(tableCell73);
            tableRow29.Append(tableCell74);
            tableRow29.Append(tableCell75);
            tableRow29.Append(tableCell76);

            TableRow tableRow30 = new TableRow() { RsidTableRowAddition = "00C204AF", RsidTableRowProperties = "0075392D" };

            TableCell tableCell77 = new TableCell();

            TableCellProperties tableCellProperties77 = new TableCellProperties();
            TableCellWidth tableCellWidth77 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties77.Append(tableCellWidth77);

            Paragraph paragraph82 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "00C204AF" };

            ParagraphProperties paragraphProperties66 = new ParagraphProperties();
            Justification justification55 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties65 = new ParagraphMarkRunProperties();

            paragraphProperties66.Append(justification55);
            paragraphProperties66.Append(paragraphMarkRunProperties65);

            Run run233 = new Run();

            RunProperties runProperties209 = new RunProperties();
            Text text233 = new Text();
            text233.Text = "Diástole";

            run233.Append(runProperties209);
            run233.Append(text233);

            paragraph82.Append(paragraphProperties66);
            paragraph82.Append(run233);

            tableCell77.Append(tableCellProperties77);
            tableCell77.Append(paragraph82);

            TableCell tableCell78 = new TableCell();

            TableCellProperties tableCellProperties78 = new TableCellProperties();
            TableCellWidth tableCellWidth78 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties78.Append(tableCellWidth78);

            Paragraph paragraph83 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties67 = new ParagraphProperties();
            Justification justification56 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties66 = new ParagraphMarkRunProperties();

            paragraphProperties67.Append(justification56);
            paragraphProperties67.Append(paragraphMarkRunProperties66);

            Run run234 = new Run();

            RunProperties runProperties210 = new RunProperties();
            Text text234 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            //Porcentaje de valores por encima del limite sistolica
            text234.Text = "";

            run234.Append(runProperties210);
            run234.Append(text234);

            paragraph83.Append(paragraphProperties67);
            paragraph83.Append(run234);

            tableCell78.Append(tableCellProperties78);
            tableCell78.Append(paragraph83);

            TableCell tableCell79 = new TableCell();

            TableCellProperties tableCellProperties79 = new TableCellProperties();
            TableCellWidth tableCellWidth79 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties79.Append(tableCellWidth79);

            Paragraph paragraph84 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties68 = new ParagraphProperties();
            Justification justification57 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties67 = new ParagraphMarkRunProperties();

            paragraphProperties68.Append(justification57);
            paragraphProperties68.Append(paragraphMarkRunProperties67);

            Run run241 = new Run();

            RunProperties runProperties217 = new RunProperties();
            Text text241 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            //Porcentaje de valores por encima del limite sistolica
            text241.Text = "< val lim dia dias>";

            run241.Append(runProperties217);
            run241.Append(text241);

            paragraph84.Append(paragraphProperties68);
            paragraph84.Append(run241);

            tableCell79.Append(tableCellProperties79);
            tableCell79.Append(paragraph84);

            TableCell tableCell80 = new TableCell();

            TableCellProperties tableCellProperties80 = new TableCellProperties();
            TableCellWidth tableCellWidth80 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };

            tableCellProperties80.Append(tableCellWidth80);

            Paragraph paragraph85 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties69 = new ParagraphProperties();
            Justification justification58 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties68 = new ParagraphMarkRunProperties();

            paragraphProperties69.Append(justification58);
            paragraphProperties69.Append(paragraphMarkRunProperties68);

            Run run248 = new Run();

            RunProperties runProperties224 = new RunProperties();
            Text text248 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            //Porcentaje de valores por encima del limite sistolica
            text248.Text = "< val lim noche dias>";

            run248.Append(runProperties224);
            run248.Append(text248);

            paragraph85.Append(paragraphProperties69);
            paragraph85.Append(run248);

            tableCell80.Append(tableCellProperties80);
            tableCell80.Append(paragraph85);

            tableRow30.Append(tableCell77);
            tableRow30.Append(tableCell78);
            tableRow30.Append(tableCell79);
            tableRow30.Append(tableCell80);

            TableRow tableRow31 = new TableRow() { RsidTableRowMarkRevision = "00C204AF", RsidTableRowAddition = "00C204AF", RsidTableRowProperties = "003A3FD4" };

            TableCell tableCell81 = new TableCell();

            TableCellProperties tableCellProperties81 = new TableCellProperties();
            TableCellWidth tableCellWidth81 = new TableCellWidth() { Width = "9345", Type = TableWidthUnitValues.Dxa };
            GridSpan gridSpan9 = new GridSpan() { Val = 4 };
            Shading shading8 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "BFBFBF", ThemeFill = ThemeColorValues.Background1, ThemeFillShade = "BF" };

            tableCellProperties81.Append(tableCellWidth81);
            tableCellProperties81.Append(gridSpan9);
            tableCellProperties81.Append(shading8);

            Paragraph paragraph86 = new Paragraph() { RsidParagraphMarkRevision = "00C204AF", RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "00C204AF", RsidRunAdditionDefault = "00C204AF" };

            ParagraphProperties paragraphProperties70 = new ParagraphProperties();

            ParagraphMarkRunProperties paragraphMarkRunProperties69 = new ParagraphMarkRunProperties();
            Bold bold26 = new Bold();
            paragraphMarkRunProperties69.Append(bold26);

            paragraphProperties70.Append(paragraphMarkRunProperties69);

            Run run253 = new Run() { RsidRunProperties = "00C204AF" };

            RunProperties runProperties229 = new RunProperties();
            Bold bold27 = new Bold();
            runProperties229.Append(bold27);
            Text text253 = new Text();
            text253.Text = "Máximo";

            run253.Append(runProperties229);
            run253.Append(text253);

            paragraph86.Append(paragraphProperties70);
            paragraph86.Append(run253);

            tableCell81.Append(tableCellProperties81);
            tableCell81.Append(paragraph86);

            tableRow31.Append(tableCell81);

            TableRow tableRow32 = new TableRow() { RsidTableRowAddition = "00C204AF", RsidTableRowProperties = "0075392D" };

            TableCell tableCell82 = new TableCell();

            TableCellProperties tableCellProperties82 = new TableCellProperties();
            TableCellWidth tableCellWidth82 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties82.Append(tableCellWidth82);

            Paragraph paragraph87 = new Paragraph() { RsidParagraphMarkRevision = "00C204AF", RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "00C204AF" };

            ParagraphProperties paragraphProperties71 = new ParagraphProperties();
            Justification justification59 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties70 = new ParagraphMarkRunProperties();

            paragraphProperties71.Append(justification59);
            paragraphProperties71.Append(paragraphMarkRunProperties70);

            Run run254 = new Run();

            RunProperties runProperties230 = new RunProperties();
            Text text254 = new Text();
            text254.Text = "Sistole";

            run254.Append(runProperties230);
            run254.Append(text254);

            paragraph87.Append(paragraphProperties71);
            paragraph87.Append(run254);

            tableCell82.Append(tableCellProperties82);
            tableCell82.Append(paragraph87);

            TableCell tableCell83 = new TableCell();

            TableCellProperties tableCellProperties83 = new TableCellProperties();
            TableCellWidth tableCellWidth83 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties83.Append(tableCellWidth83);

            Paragraph paragraph88 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties72 = new ParagraphProperties();
            Justification justification60 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties71 = new ParagraphMarkRunProperties();

            paragraphProperties72.Append(justification60);
            paragraphProperties72.Append(paragraphMarkRunProperties71);

            Run run256 = new Run();

            RunProperties runProperties232 = new RunProperties();
            Text text256 = new Text();
            //Sistolica maxima total
            text256.Text = "";

            run256.Append(runProperties232);
            run256.Append(text256);

            paragraph88.Append(paragraphProperties72);
            paragraph88.Append(run256);

            tableCell83.Append(tableCellProperties83);
            tableCell83.Append(paragraph88);

            TableCell tableCell84 = new TableCell();

            TableCellProperties tableCellProperties84 = new TableCellProperties();
            TableCellWidth tableCellWidth84 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties84.Append(tableCellWidth84);

            Paragraph paragraph89 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties73 = new ParagraphProperties();
            Justification justification61 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties72 = new ParagraphMarkRunProperties();

            paragraphProperties73.Append(justification61);
            paragraphProperties73.Append(paragraphMarkRunProperties72);

            Run run263 = new Run();

            RunProperties runProperties239 = new RunProperties();
            Text text263 = new Text();
            //Sistolica maxima del dia
            text263.Text = report.SystolicDayMax.ToString();

            run263.Append(runProperties239);
            run263.Append(text263);

            paragraph89.Append(paragraphProperties73);
            paragraph89.Append(run263);

            tableCell84.Append(tableCellProperties84);
            tableCell84.Append(paragraph89);

            TableCell tableCell85 = new TableCell();

            TableCellProperties tableCellProperties85 = new TableCellProperties();
            TableCellWidth tableCellWidth85 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };

            tableCellProperties85.Append(tableCellWidth85);

            Paragraph paragraph90 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties74 = new ParagraphProperties();
            Justification justification62 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties73 = new ParagraphMarkRunProperties();

            paragraphProperties74.Append(justification62);
            paragraphProperties74.Append(paragraphMarkRunProperties73);

            Run run270 = new Run();

            RunProperties runProperties246 = new RunProperties();
            Text text270 = new Text();
            //Sistolica maxima de la noche
            text270.Text = report.SystolicNightMax.ToString();

            run270.Append(runProperties246);
            run270.Append(text270);

            paragraph90.Append(paragraphProperties74);
            paragraph90.Append(run270);

            tableCell85.Append(tableCellProperties85);
            tableCell85.Append(paragraph90);

            tableRow32.Append(tableCell82);
            tableRow32.Append(tableCell83);
            tableRow32.Append(tableCell84);
            tableRow32.Append(tableCell85);

            TableRow tableRow33 = new TableRow() { RsidTableRowAddition = "00C204AF", RsidTableRowProperties = "0075392D" };

            TableCell tableCell86 = new TableCell();

            TableCellProperties tableCellProperties86 = new TableCellProperties();
            TableCellWidth tableCellWidth86 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties86.Append(tableCellWidth86);

            Paragraph paragraph91 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "00C204AF" };

            ParagraphProperties paragraphProperties75 = new ParagraphProperties();
            Justification justification63 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties74 = new ParagraphMarkRunProperties();

            paragraphProperties75.Append(justification63);
            paragraphProperties75.Append(paragraphMarkRunProperties74);

            Run run275 = new Run();

            RunProperties runProperties251 = new RunProperties();
            Text text275 = new Text();
            text275.Text = "Diástole";

            run275.Append(runProperties251);
            run275.Append(text275);

            paragraph91.Append(paragraphProperties75);
            paragraph91.Append(run275);

            tableCell86.Append(tableCellProperties86);
            tableCell86.Append(paragraph91);

            TableCell tableCell87 = new TableCell();

            TableCellProperties tableCellProperties87 = new TableCellProperties();
            TableCellWidth tableCellWidth87 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties87.Append(tableCellWidth87);

            Paragraph paragraph92 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties76 = new ParagraphProperties();
            Justification justification64 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties75 = new ParagraphMarkRunProperties();

            paragraphProperties76.Append(justification64);
            paragraphProperties76.Append(paragraphMarkRunProperties75);

            Run run276 = new Run();

            RunProperties runProperties252 = new RunProperties();
            Text text276 = new Text();
            //Diastolica maxima total
            text276.Text = report.DiastolicTotalMax.ToString();

            run276.Append(runProperties252);
            run276.Append(text276);

            paragraph92.Append(paragraphProperties76);
            paragraph92.Append(run276);

            tableCell87.Append(tableCellProperties87);
            tableCell87.Append(paragraph92);

            TableCell tableCell88 = new TableCell();

            TableCellProperties tableCellProperties88 = new TableCellProperties();
            TableCellWidth tableCellWidth88 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties88.Append(tableCellWidth88);

            Paragraph paragraph93 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties77 = new ParagraphProperties();
            Justification justification65 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties76 = new ParagraphMarkRunProperties();

            paragraphProperties77.Append(justification65);
            paragraphProperties77.Append(paragraphMarkRunProperties76);

            Run run283 = new Run();

            RunProperties runProperties259 = new RunProperties();
            Text text283 = new Text();
            //Diastolica maxima del dia
            text283.Text = report.DiastolicDayMax.ToString();

            run283.Append(runProperties259);
            run283.Append(text283);

            paragraph93.Append(paragraphProperties77);
            paragraph93.Append(run283);

            tableCell88.Append(tableCellProperties88);
            tableCell88.Append(paragraph93);

            TableCell tableCell89 = new TableCell();

            TableCellProperties tableCellProperties89 = new TableCellProperties();
            TableCellWidth tableCellWidth89 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };

            tableCellProperties89.Append(tableCellWidth89);

            Paragraph paragraph94 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties78 = new ParagraphProperties();
            Justification justification66 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties77 = new ParagraphMarkRunProperties();

            paragraphProperties78.Append(justification66);
            paragraphProperties78.Append(paragraphMarkRunProperties77);

            Run run290 = new Run();

            RunProperties runProperties266 = new RunProperties();
            Text text290 = new Text();
            //Diastolica maxima de la noche
            text290.Text = report.DiastolicNightMax.ToString();

            run290.Append(runProperties266);
            run290.Append(text290);

            paragraph94.Append(paragraphProperties78);
            paragraph94.Append(run290);

            tableCell89.Append(tableCellProperties89);
            tableCell89.Append(paragraph94);

            tableRow33.Append(tableCell86);
            tableRow33.Append(tableCell87);
            tableRow33.Append(tableCell88);
            tableRow33.Append(tableCell89);

            TableRow tableRow34 = new TableRow() { RsidTableRowMarkRevision = "00C204AF", RsidTableRowAddition = "00C204AF", RsidTableRowProperties = "009E0479" };

            TableCell tableCell90 = new TableCell();

            TableCellProperties tableCellProperties90 = new TableCellProperties();
            TableCellWidth tableCellWidth90 = new TableCellWidth() { Width = "9345", Type = TableWidthUnitValues.Dxa };
            GridSpan gridSpan10 = new GridSpan() { Val = 4 };
            Shading shading9 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "BFBFBF", ThemeFill = ThemeColorValues.Background1, ThemeFillShade = "BF" };

            tableCellProperties90.Append(tableCellWidth90);
            tableCellProperties90.Append(gridSpan10);
            tableCellProperties90.Append(shading9);

            Paragraph paragraph95 = new Paragraph() { RsidParagraphMarkRevision = "00C204AF", RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "00C204AF", RsidRunAdditionDefault = "00C204AF" };

            ParagraphProperties paragraphProperties79 = new ParagraphProperties();

            ParagraphMarkRunProperties paragraphMarkRunProperties78 = new ParagraphMarkRunProperties();
            Bold bold28 = new Bold();
            paragraphMarkRunProperties78.Append(bold28);

            paragraphProperties79.Append(paragraphMarkRunProperties78);

            Run run295 = new Run() { RsidRunProperties = "00C204AF" };

            RunProperties runProperties271 = new RunProperties();
            Bold bold29 = new Bold();

            runProperties271.Append(bold29);
            Text text295 = new Text();
            text295.Text = "Mínimo";

            run295.Append(runProperties271);
            run295.Append(text295);

            paragraph95.Append(paragraphProperties79);
            paragraph95.Append(run295);

            tableCell90.Append(tableCellProperties90);
            tableCell90.Append(paragraph95);

            tableRow34.Append(tableCell90);

            TableRow tableRow35 = new TableRow() { RsidTableRowAddition = "001B715B", RsidTableRowProperties = "0075392D" };

            TableCell tableCell91 = new TableCell();

            TableCellProperties tableCellProperties91 = new TableCellProperties();
            TableCellWidth tableCellWidth91 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties91.Append(tableCellWidth91);

            Paragraph paragraph96 = new Paragraph() { RsidParagraphMarkRevision = "00C204AF", RsidParagraphAddition = "001B715B", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties80 = new ParagraphProperties();
            Justification justification67 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties79 = new ParagraphMarkRunProperties();

            paragraphProperties80.Append(justification67);
            paragraphProperties80.Append(paragraphMarkRunProperties79);

            Run run296 = new Run();

            RunProperties runProperties272 = new RunProperties();
            Text text296 = new Text();
            text296.Text = "Sistole";

            run296.Append(runProperties272);
            run296.Append(text296);

            paragraph96.Append(paragraphProperties80);
            paragraph96.Append(run296);

            tableCell91.Append(tableCellProperties91);
            tableCell91.Append(paragraph96);

            TableCell tableCell92 = new TableCell();

            TableCellProperties tableCellProperties92 = new TableCellProperties();
            TableCellWidth tableCellWidth92 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties92.Append(tableCellWidth92);

            Paragraph paragraph97 = new Paragraph() { RsidParagraphAddition = "001B715B", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties81 = new ParagraphProperties();
            Justification justification68 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties80 = new ParagraphMarkRunProperties();

            paragraphProperties81.Append(justification68);
            paragraphProperties81.Append(paragraphMarkRunProperties80);

            Run run298 = new Run();

            RunProperties runProperties274 = new RunProperties();
            Text text298 = new Text();
            //Sistole total minimo
            text298.Text = "";

            run298.Append(runProperties274);
            run298.Append(text298);

            paragraph97.Append(paragraphProperties81);
            paragraph97.Append(run298);

            tableCell92.Append(tableCellProperties92);
            tableCell92.Append(paragraph97);

            TableCell tableCell93 = new TableCell();

            TableCellProperties tableCellProperties93 = new TableCellProperties();
            TableCellWidth tableCellWidth93 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties93.Append(tableCellWidth93);

            Paragraph paragraph98 = new Paragraph() { RsidParagraphAddition = "001B715B", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties82 = new ParagraphProperties();
            Justification justification69 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties81 = new ParagraphMarkRunProperties();

            paragraphProperties82.Append(justification69);
            paragraphProperties82.Append(paragraphMarkRunProperties81);

            Run run305 = new Run();

            RunProperties runProperties281 = new RunProperties();
            Text text305 = new Text();
            //Sistole minimo dia
            text305.Text = report.SystolicDayMin.ToString();

            run305.Append(runProperties281);
            run305.Append(text305);

            paragraph98.Append(paragraphProperties82);
            paragraph98.Append(run305);

            tableCell93.Append(tableCellProperties93);
            tableCell93.Append(paragraph98);

            TableCell tableCell94 = new TableCell();

            TableCellProperties tableCellProperties94 = new TableCellProperties();
            TableCellWidth tableCellWidth94 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };

            tableCellProperties94.Append(tableCellWidth94);

            Paragraph paragraph99 = new Paragraph() { RsidParagraphAddition = "001B715B", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties83 = new ParagraphProperties();
            Justification justification70 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties82 = new ParagraphMarkRunProperties();

            paragraphProperties83.Append(justification70);
            paragraphProperties83.Append(paragraphMarkRunProperties82);

            Run run312 = new Run();

            RunProperties runProperties288 = new RunProperties();
            Text text312 = new Text();
            //Sistole minimo noche
            text312.Text = report.SystolicNightMin.ToString();

            run312.Append(runProperties288);
            run312.Append(text312);

            paragraph99.Append(paragraphProperties83);
            paragraph99.Append(run312);

            tableCell94.Append(tableCellProperties94);
            tableCell94.Append(paragraph99);

            tableRow35.Append(tableCell91);
            tableRow35.Append(tableCell92);
            tableRow35.Append(tableCell93);
            tableRow35.Append(tableCell94);

            TableRow tableRow36 = new TableRow() { RsidTableRowAddition = "001B715B", RsidTableRowProperties = "00653833" };

            TableCell tableCell95 = new TableCell();

            TableCellProperties tableCellProperties95 = new TableCellProperties();
            TableCellWidth tableCellWidth95 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            TableCellBorders tableCellBorders22 = new TableCellBorders();
            BottomBorder bottomBorder20 = new BottomBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };

            tableCellBorders22.Append(bottomBorder20);

            tableCellProperties95.Append(tableCellWidth95);
            tableCellProperties95.Append(tableCellBorders22);

            Paragraph paragraph100 = new Paragraph() { RsidParagraphAddition = "001B715B", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties84 = new ParagraphProperties();
            Justification justification71 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties83 = new ParagraphMarkRunProperties();

            paragraphProperties84.Append(justification71);
            paragraphProperties84.Append(paragraphMarkRunProperties83);

            Run run317 = new Run();

            RunProperties runProperties293 = new RunProperties();
            Text text317 = new Text();
            text317.Text = "Diástole";

            run317.Append(runProperties293);
            run317.Append(text317);

            paragraph100.Append(paragraphProperties84);
            paragraph100.Append(run317);

            tableCell95.Append(tableCellProperties95);
            tableCell95.Append(paragraph100);

            TableCell tableCell96 = new TableCell();

            TableCellProperties tableCellProperties96 = new TableCellProperties();
            TableCellWidth tableCellWidth96 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            TableCellBorders tableCellBorders23 = new TableCellBorders();
            BottomBorder bottomBorder21 = new BottomBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };

            tableCellBorders23.Append(bottomBorder21);

            tableCellProperties96.Append(tableCellWidth96);
            tableCellProperties96.Append(tableCellBorders23);

            Paragraph paragraph101 = new Paragraph() { RsidParagraphAddition = "001B715B", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties85 = new ParagraphProperties();
            Justification justification72 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties84 = new ParagraphMarkRunProperties();

            paragraphProperties85.Append(justification72);
            paragraphProperties85.Append(paragraphMarkRunProperties84);

            Run run318 = new Run();

            RunProperties runProperties294 = new RunProperties();
            Text text318 = new Text();
            //Diastolica minima total
            text318.Text = "";

            run318.Append(runProperties294);
            run318.Append(text318);

            paragraph101.Append(paragraphProperties85);
            paragraph101.Append(run318);

            tableCell96.Append(tableCellProperties96);
            tableCell96.Append(paragraph101);

            TableCell tableCell97 = new TableCell();

            TableCellProperties tableCellProperties97 = new TableCellProperties();
            TableCellWidth tableCellWidth97 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            TableCellBorders tableCellBorders24 = new TableCellBorders();
            BottomBorder bottomBorder22 = new BottomBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };

            tableCellBorders24.Append(bottomBorder22);

            tableCellProperties97.Append(tableCellWidth97);
            tableCellProperties97.Append(tableCellBorders24);

            Paragraph paragraph102 = new Paragraph() { RsidParagraphAddition = "001B715B", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties86 = new ParagraphProperties();
            Justification justification73 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties85 = new ParagraphMarkRunProperties();

            paragraphProperties86.Append(justification73);
            paragraphProperties86.Append(paragraphMarkRunProperties85);

            Run run325 = new Run();

            RunProperties runProperties301 = new RunProperties();
            Text text325 = new Text();
            //Diastolica minima dia
            text325.Text = report.DiastolicDayMin.ToString();

            run325.Append(runProperties301);
            run325.Append(text325);

            paragraph102.Append(paragraphProperties86);
            paragraph102.Append(run325);

            tableCell97.Append(tableCellProperties97);
            tableCell97.Append(paragraph102);

            TableCell tableCell98 = new TableCell();

            TableCellProperties tableCellProperties98 = new TableCellProperties();
            TableCellWidth tableCellWidth98 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };

            TableCellBorders tableCellBorders25 = new TableCellBorders();
            BottomBorder bottomBorder23 = new BottomBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };

            tableCellBorders25.Append(bottomBorder23);

            tableCellProperties98.Append(tableCellWidth98);
            tableCellProperties98.Append(tableCellBorders25);

            Paragraph paragraph103 = new Paragraph() { RsidParagraphAddition = "001B715B", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties87 = new ParagraphProperties();
            Justification justification74 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties86 = new ParagraphMarkRunProperties();

            paragraphProperties87.Append(justification74);
            paragraphProperties87.Append(paragraphMarkRunProperties86);

            Run run332 = new Run();

            RunProperties runProperties308 = new RunProperties();
            Text text332 = new Text();
            //Diastolica minima noche
            text332.Text = report.DiastolicNightMin.ToString();

            run332.Append(runProperties308);
            run332.Append(text332);

            paragraph103.Append(paragraphProperties87);
            paragraph103.Append(run332);

            tableCell98.Append(tableCellProperties98);
            tableCell98.Append(paragraph103);

            tableRow36.Append(tableCell95);
            tableRow36.Append(tableCell96);
            tableRow36.Append(tableCell97);
            tableRow36.Append(tableCell98);

            TableRow tableRow37 = new TableRow() { RsidTableRowAddition = "001B715B", RsidTableRowProperties = "00653833" };

            TableCell tableCell99 = new TableCell();

            TableCellProperties tableCellProperties99 = new TableCellProperties();
            TableCellWidth tableCellWidth99 = new TableCellWidth() { Width = "9345", Type = TableWidthUnitValues.Dxa };
            GridSpan gridSpan11 = new GridSpan() { Val = 4 };

            TableCellBorders tableCellBorders26 = new TableCellBorders();
            LeftBorder leftBorder21 = new LeftBorder() { Val = BorderValues.Nil };
            BottomBorder bottomBorder24 = new BottomBorder() { Val = BorderValues.Nil };
            RightBorder rightBorder20 = new RightBorder() { Val = BorderValues.Nil };

            tableCellBorders26.Append(leftBorder21);
            tableCellBorders26.Append(bottomBorder24);
            tableCellBorders26.Append(rightBorder20);

            tableCellProperties99.Append(tableCellWidth99);
            tableCellProperties99.Append(gridSpan11);
            tableCellProperties99.Append(tableCellBorders26);

            Paragraph paragraph104 = new Paragraph() { RsidParagraphAddition = "001B715B", RsidParagraphProperties = "00653833", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties88 = new ParagraphProperties();

            ParagraphMarkRunProperties paragraphMarkRunProperties87 = new ParagraphMarkRunProperties();

            paragraphProperties88.Append(paragraphMarkRunProperties87);

            Run run337 = new Run();

            RunProperties runProperties313 = new RunProperties();
            Text text337 = new Text();
            text337.Text = "Valores por encima del límite";

            run337.Append(runProperties313);
            run337.Append(text337);

            paragraph104.Append(paragraphProperties88);
            paragraph104.Append(run337);

            tableCell99.Append(tableCellProperties99);
            tableCell99.Append(paragraph104);

            tableRow37.Append(tableCell99);

            TableRow tableRow38 = new TableRow() { RsidTableRowAddition = "001B715B", RsidTableRowProperties = "00653833" };

            TableCell tableCell100 = new TableCell();

            TableCellProperties tableCellProperties100 = new TableCellProperties();
            TableCellWidth tableCellWidth100 = new TableCellWidth() { Width = "9345", Type = TableWidthUnitValues.Dxa };
            GridSpan gridSpan12 = new GridSpan() { Val = 4 };

            TableCellBorders tableCellBorders27 = new TableCellBorders();
            TopBorder topBorder19 = new TopBorder() { Val = BorderValues.Nil };
            LeftBorder leftBorder22 = new LeftBorder() { Val = BorderValues.Nil };
            RightBorder rightBorder21 = new RightBorder() { Val = BorderValues.Nil };

            tableCellBorders27.Append(topBorder19);
            tableCellBorders27.Append(leftBorder22);
            tableCellBorders27.Append(rightBorder21);

            tableCellProperties100.Append(tableCellWidth100);
            tableCellProperties100.Append(gridSpan12);
            tableCellProperties100.Append(tableCellBorders27);

            Paragraph paragraph105 = new Paragraph() { RsidParagraphAddition = "001B715B", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties89 = new ParagraphProperties();
            Justification justification75 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties88 = new ParagraphMarkRunProperties();

            paragraphProperties89.Append(justification75);
            paragraphProperties89.Append(paragraphMarkRunProperties88);

            paragraph105.Append(paragraphProperties89);

            tableCell100.Append(tableCellProperties100);
            tableCell100.Append(paragraph105);

            tableRow38.Append(tableCell100);

            TableRow tableRow39 = new TableRow() { RsidTableRowMarkRevision = "00653833", RsidTableRowAddition = "001B715B", RsidTableRowProperties = "00C31833" };

            TableCell tableCell101 = new TableCell();

            TableCellProperties tableCellProperties101 = new TableCellProperties();
            TableCellWidth tableCellWidth101 = new TableCellWidth() { Width = "9345", Type = TableWidthUnitValues.Dxa };
            GridSpan gridSpan13 = new GridSpan() { Val = 4 };
            Shading shading10 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "BFBFBF", ThemeFill = ThemeColorValues.Background1, ThemeFillShade = "BF" };

            tableCellProperties101.Append(tableCellWidth101);
            tableCellProperties101.Append(gridSpan13);
            tableCellProperties101.Append(shading10);

            Paragraph paragraph106 = new Paragraph() { RsidParagraphMarkRevision = "00653833", RsidParagraphAddition = "001B715B", RsidParagraphProperties = "00653833", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties90 = new ParagraphProperties();

            ParagraphMarkRunProperties paragraphMarkRunProperties89 = new ParagraphMarkRunProperties();
            Bold bold30 = new Bold();

            paragraphMarkRunProperties89.Append(bold30);

            paragraphProperties90.Append(paragraphMarkRunProperties89);

            Run run338 = new Run() { RsidRunProperties = "00653833" };

            RunProperties runProperties314 = new RunProperties();
            Bold bold31 = new Bold();

            runProperties314.Append(bold31);
            Text text338 = new Text();
            text338.Text = "Dipping";

            run338.Append(runProperties314);
            run338.Append(text338);

            paragraph106.Append(paragraphProperties90);
            paragraph106.Append(run338);

            tableCell101.Append(tableCellProperties101);
            tableCell101.Append(paragraph106);

            tableRow39.Append(tableCell101);

            TableRow tableRow40 = new TableRow() { RsidTableRowMarkRevision = "00653833", RsidTableRowAddition = "001B715B", RsidTableRowProperties = "00A6498A" };

            TableCell tableCell102 = new TableCell();

            TableCellProperties tableCellProperties102 = new TableCellProperties();
            TableCellWidth tableCellWidth102 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties102.Append(tableCellWidth102);

            Paragraph paragraph107 = new Paragraph() { RsidParagraphMarkRevision = "00653833", RsidParagraphAddition = "001B715B", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties91 = new ParagraphProperties();
            Justification justification76 = new Justification() { Val = JustificationValues.Center };

            paragraphProperties91.Append(justification76);
            Run run339 = new Run() { RsidRunProperties = "00653833" };
            Text text339 = new Text();
            text339.Text = "Sístole";

            run339.Append(text339);

            paragraph107.Append(paragraphProperties91);
            paragraph107.Append(run339);

            tableCell102.Append(tableCellProperties102);
            tableCell102.Append(paragraph107);

            TableCell tableCell103 = new TableCell();

            TableCellProperties tableCellProperties103 = new TableCellProperties();
            TableCellWidth tableCellWidth103 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties103.Append(tableCellWidth103);

            Paragraph paragraph108 = new Paragraph() { RsidParagraphMarkRevision = "00653833", RsidParagraphAddition = "001B715B", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties92 = new ParagraphProperties();
            Justification justification77 = new Justification() { Val = JustificationValues.Center };

            paragraphProperties92.Append(justification77);

            Run run340 = new Run();
            Text text340 = new Text();
            //Dipping sistolica
            decimal dippingSys = Math.Round(((decimal)(report.SystolicDayAvg.Value - report.SystolicNightAvg.Value) / report.SystolicDayAvg.Value) * 100, 2);
            text340.Text = dippingSys.ToString();

            run340.Append(text340);

            paragraph108.Append(paragraphProperties92);
            paragraph108.Append(run340);

            tableCell103.Append(tableCellProperties103);
            tableCell103.Append(paragraph108);

            TableCell tableCell104 = new TableCell();

            TableCellProperties tableCellProperties104 = new TableCellProperties();
            TableCellWidth tableCellWidth104 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties104.Append(tableCellWidth104);

            Paragraph paragraph109 = new Paragraph() { RsidParagraphMarkRevision = "00653833", RsidParagraphAddition = "001B715B", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties93 = new ParagraphProperties();
            Justification justification78 = new Justification() { Val = JustificationValues.Center };

            paragraphProperties93.Append(justification78);

            Run run341 = new Run();
            Text text341 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            text341.Text = "";

            run341.Append(text341);

            paragraph109.Append(paragraphProperties93);
            paragraph109.Append(run341);

            tableCell104.Append(tableCellProperties104);
            tableCell104.Append(paragraph109);

            TableCell tableCell105 = new TableCell();

            TableCellProperties tableCellProperties105 = new TableCellProperties();
            TableCellWidth tableCellWidth105 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };

            tableCellProperties105.Append(tableCellWidth105);

            Paragraph paragraph110 = new Paragraph() { RsidParagraphMarkRevision = "00653833", RsidParagraphAddition = "001B715B", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties94 = new ParagraphProperties();
            Justification justification79 = new Justification() { Val = JustificationValues.Center };

            paragraphProperties94.Append(justification79);

            Run run344 = new Run();
            Text text344 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            text344.Text = "";

            run344.Append(text344);

            paragraph110.Append(paragraphProperties94);
            paragraph110.Append(run344);

            tableCell105.Append(tableCellProperties105);
            tableCell105.Append(paragraph110);

            tableRow40.Append(tableCell102);
            tableRow40.Append(tableCell103);
            tableRow40.Append(tableCell104);
            tableRow40.Append(tableCell105);

            TableRow tableRow41 = new TableRow() { RsidTableRowMarkRevision = "00653833", RsidTableRowAddition = "001B715B", RsidTableRowProperties = "00A6498A" };

            TableCell tableCell106 = new TableCell();

            TableCellProperties tableCellProperties106 = new TableCellProperties();
            TableCellWidth tableCellWidth106 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties106.Append(tableCellWidth106);

            Paragraph paragraph111 = new Paragraph() { RsidParagraphMarkRevision = "00653833", RsidParagraphAddition = "001B715B", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties95 = new ParagraphProperties();
            Justification justification80 = new Justification() { Val = JustificationValues.Center };

            paragraphProperties95.Append(justification80);

            Run run347 = new Run() { RsidRunProperties = "00653833" };
            LastRenderedPageBreak lastRenderedPageBreak1 = new LastRenderedPageBreak();
            Text text347 = new Text();
            text347.Text = "Diástole";

            run347.Append(lastRenderedPageBreak1);
            run347.Append(text347);

            paragraph111.Append(paragraphProperties95);
            paragraph111.Append(run347);

            tableCell106.Append(tableCellProperties106);
            tableCell106.Append(paragraph111);

            TableCell tableCell107 = new TableCell();

            TableCellProperties tableCellProperties107 = new TableCellProperties();
            TableCellWidth tableCellWidth107 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties107.Append(tableCellWidth107);

            Paragraph paragraph112 = new Paragraph() { RsidParagraphMarkRevision = "00653833", RsidParagraphAddition = "001B715B", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties96 = new ParagraphProperties();
            Justification justification81 = new Justification() { Val = JustificationValues.Center };

            paragraphProperties96.Append(justification81);

            Run run348 = new Run();
            Text text348 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            //Dipping sistolica
            decimal dippingDias = Math.Round(((decimal)(report.DiastolicDayAvg.Value - report.DiastolicNightAvg.Value) / report.DiastolicDayAvg.Value) * 100, 2);
            text348.Text = dippingDias.ToString();

            run348.Append(text348);

            paragraph112.Append(paragraphProperties96);
            paragraph112.Append(run348);

            tableCell107.Append(tableCellProperties107);
            tableCell107.Append(paragraph112);

            TableCell tableCell108 = new TableCell();

            TableCellProperties tableCellProperties108 = new TableCellProperties();
            TableCellWidth tableCellWidth108 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties108.Append(tableCellWidth108);

            Paragraph paragraph113 = new Paragraph() { RsidParagraphMarkRevision = "00653833", RsidParagraphAddition = "001B715B", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties97 = new ParagraphProperties();
            Justification justification82 = new Justification() { Val = JustificationValues.Center };

            paragraphProperties97.Append(justification82);

            Run run351 = new Run();
            Text text351 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            text351.Text = "";

            run351.Append(text351);

            paragraph113.Append(paragraphProperties97);
            paragraph113.Append(run351);

            tableCell108.Append(tableCellProperties108);
            tableCell108.Append(paragraph113);

            TableCell tableCell109 = new TableCell();

            TableCellProperties tableCellProperties109 = new TableCellProperties();
            TableCellWidth tableCellWidth109 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };

            tableCellProperties109.Append(tableCellWidth109);

            Paragraph paragraph114 = new Paragraph() { RsidParagraphMarkRevision = "00653833", RsidParagraphAddition = "001B715B", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties98 = new ParagraphProperties();
            Justification justification83 = new Justification() { Val = JustificationValues.Center };

            paragraphProperties98.Append(justification83);

            Run run356 = new Run();
            Text text356 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            text356.Text = "";

            run356.Append(text356);

            paragraph114.Append(paragraphProperties98);
            paragraph114.Append(run356);

            tableCell109.Append(tableCellProperties109);
            tableCell109.Append(paragraph114);

            tableRow41.Append(tableCell106);
            tableRow41.Append(tableCell107);
            tableRow41.Append(tableCell108);
            tableRow41.Append(tableCell109);

            table2.Append(tableProperties2);
            table2.Append(tableGrid2);
            table2.Append(tableRow13);
            table2.Append(tableRow14);
            table2.Append(tableRow15);
            table2.Append(tableRow16);
            table2.Append(tableRow17);
            table2.Append(tableRow18);
            table2.Append(tableRow19);
            table2.Append(tableRow20);
            table2.Append(tableRow21);
            table2.Append(tableRow22);
            table2.Append(tableRow23);
            table2.Append(tableRow24);
            table2.Append(tableRow25);
            table2.Append(tableRow26);
            table2.Append(tableRow27);
            table2.Append(tableRow28);
            table2.Append(tableRow29);
            table2.Append(tableRow30);
            table2.Append(tableRow31);
            table2.Append(tableRow32);
            table2.Append(tableRow33);
            table2.Append(tableRow34);
            table2.Append(tableRow35);
            table2.Append(tableRow36);
            table2.Append(tableRow37);
            table2.Append(tableRow38);
            table2.Append(tableRow39);
            table2.Append(tableRow40);
            table2.Append(tableRow41);

            Paragraph paragraph115 = new Paragraph() { RsidParagraphMarkRevision = "00653833", RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "009C2A39", RsidRunAdditionDefault = "00653833" };

            Run run361 = new Run() { RsidRunProperties = "00653833" };
            Text text361 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            text361.Text = "Dipping <0% Invertido; <10% Non-Dipper; <20% Normal; >=20% Extremos";

            run361.Append(text361);

            paragraph115.Append(run361);

            tableCell20.Append(tableCellProperties20);
            tableCell20.Append(paragraph25);
            tableCell20.Append(table2);
            tableCell20.Append(paragraph115);

            tableRow12.Append(tableRowProperties3);
            tableRow12.Append(tableCell20);

            return tableRow12;
        }

        private void GenerateCoverHeader(HeaderPart coverHeaderPart)
        {
            Header coverHeader = new Header();

            Paragraph paragraph59 = new Paragraph() { RsidParagraphAddition = "00AA46B2", RsidRunAdditionDefault = "00F4078F" };

            ParagraphProperties paragraphProperties43 = new ParagraphProperties();
            ParagraphStyleId paragraphStyleId3 = new ParagraphStyleId() { Val = "Encabezado" };

            ParagraphMarkRunProperties paragraphMarkRunProperties42 = new ParagraphMarkRunProperties();


            paragraphProperties43.Append(paragraphStyleId3);
            paragraphProperties43.Append(paragraphMarkRunProperties42);

            Run run47 = new Run();

            RunProperties runProperties24 = new RunProperties();
            Text text43 = new Text();
            text43.Text = "Hospital de Clínicas";

            run47.Append(runProperties24);
            run47.Append(text43);

            Run run48 = new Run();

            RunProperties runProperties25 = new RunProperties();

            TabChar tabChar1 = new TabChar();
            Text text44 = new Text();
            text44.Text = "Informe de Hipertensión Arterial";

            run48.Append(runProperties25);
            run48.Append(tabChar1);
            run48.Append(text44);

            Run run49 = new Run();

            RunProperties runProperties26 = new RunProperties();

            TabChar tabChar2 = new TabChar();
            Text text45 = new Text();
            text45.Text = "     " + DateTime.Now.ToString(ConfigurationManager.AppSettings["ShortDateString"]);

            run49.Append(runProperties26);
            run49.Append(tabChar2);
            run49.Append(text45);

            paragraph59.Append(paragraphProperties43);
            paragraph59.Append(run47);
            paragraph59.Append(run48);
            paragraph59.Append(run49);

            coverHeader.Append(paragraph59);

            coverHeaderPart.Header = coverHeader;
        }

        private void GenerateHeader(HeaderPart headerPart1, Patient patient)
        {
            Header header1 = new Header();
            Paragraph paragraph59 = new Paragraph() { RsidParagraphAddition = "00AA46B2", RsidRunAdditionDefault = "00F4078F" };

            ParagraphProperties paragraphProperties43 = new ParagraphProperties();
            ParagraphStyleId paragraphStyleId3 = new ParagraphStyleId() { Val = "Encabezado" };

            ParagraphMarkRunProperties paragraphMarkRunProperties42 = new ParagraphMarkRunProperties();


            paragraphProperties43.Append(paragraphStyleId3);
            paragraphProperties43.Append(paragraphMarkRunProperties42);

            Run run47 = new Run();

            RunProperties runProperties24 = new RunProperties();
            Text text43 = new Text();
            text43.Text = "Hospital de Clínicas";

            run47.Append(runProperties24);
            run47.Append(text43);

            Run run48 = new Run();

            RunProperties runProperties25 = new RunProperties();

            TabChar tabChar1 = new TabChar();
            Text text44 = new Text();
            text44.Text = "Informe de Hipertensión Arterial";

            run48.Append(runProperties25);
            run48.Append(tabChar1);
            run48.Append(text44);

            Run run49 = new Run();

            RunProperties runProperties26 = new RunProperties();

            TabChar tabChar2 = new TabChar();
            Text text45 = new Text();
            text45.Text = "     " + DateTime.Now.ToString(ConfigurationManager.AppSettings["ShortDateString"]);

            run49.Append(runProperties26);
            run49.Append(tabChar2);
            run49.Append(text45);

            paragraph59.Append(paragraphProperties43);
            paragraph59.Append(run47);
            paragraph59.Append(run48);
            paragraph59.Append(run49);

            Paragraph paragraph60 = new Paragraph() { RsidParagraphAddition = "00F4078F", RsidRunAdditionDefault = "00F4078F" };

            ParagraphProperties paragraphProperties44 = new ParagraphProperties();
            ParagraphStyleId paragraphStyleId4 = new ParagraphStyleId() { Val = "Encabezado" };

            ParagraphMarkRunProperties paragraphMarkRunProperties43 = new ParagraphMarkRunProperties();

            paragraphProperties44.Append(paragraphStyleId4);
            paragraphProperties44.Append(paragraphMarkRunProperties43);

            paragraph60.Append(paragraphProperties44);

            Paragraph paragraph61 = new Paragraph() { RsidParagraphMarkRevision = "00CC4E8A", RsidParagraphAddition = "00CC4E8A", RsidRunAdditionDefault = "00CC4E8A" };

            ParagraphProperties paragraphProperties45 = new ParagraphProperties();
            ParagraphStyleId paragraphStyleId5 = new ParagraphStyleId() { Val = "Encabezado" };

            ParagraphMarkRunProperties paragraphMarkRunProperties44 = new ParagraphMarkRunProperties();

            paragraphProperties45.Append(paragraphStyleId5);
            paragraphProperties45.Append(paragraphMarkRunProperties44);

            Run run50 = new Run();

            RunProperties runProperties27 = new RunProperties();

            Picture picture1 = new Picture();

            V.Shapetype shapetype1 = new V.Shapetype() { Id = "_x0000_t32", CoordinateSize = "21600,21600", Oned = true, Filled = false, OptionalNumber = 32, EdgePath = "m,l21600,21600e" };
            V.Path path1 = new V.Path() { AllowFill = false, ShowArrowhead = true, ConnectionPointType = Ovml.ConnectValues.None };
            Ovml.Lock lock1 = new Ovml.Lock() { Extension = V.ExtensionHandlingBehaviorValues.Edit, ShapeType = true };

            shapetype1.Append(path1);
            shapetype1.Append(lock1);
            V.Shape shape1 = new V.Shape() { Id = "_x0000_s3073", Style = "position:absolute;margin-left:-43.45pt;margin-top:13.25pt;width:557.65pt;height:.05pt;z-index:251658240", ConnectorType = Ovml.ConnectorValues.Straight, Type = "#_x0000_t32" };

            picture1.Append(shapetype1);
            picture1.Append(shape1);

            run50.Append(runProperties27);
            run50.Append(picture1);

            Run run51 = new Run();

            RunProperties runProperties28 = new RunProperties();
            Text text46 = new Text();
            text46.Text = "Paciente: ";

            run51.Append(runProperties28);
            run51.Append(text46);

            Run run52 = new Run();

            RunProperties runProperties29 = new RunProperties();
            Text text47 = new Text();
            text47.Text = patient.RegisterNumber;

            run52.Append(runProperties29);
            run52.Append(text47);

            Run run56 = new Run();

            RunProperties runProperties33 = new RunProperties();
            TabChar tabChar3 = new TabChar();
            Text text51 = new Text();
            text51.Text = patient.Names + " " + patient.Surnames;

            run56.Append(runProperties33);
            run56.Append(tabChar3);
            run56.Append(text51);

            Run run57 = new Run();

            RunProperties runProperties34 = new RunProperties();
            TabChar tabChar4 = new TabChar();
            Text text52 = new Text();
            text52.Text = patient.DocumentId;

            run57.Append(runProperties34);
            run57.Append(tabChar4);
            run57.Append(text52);

            paragraph61.Append(paragraphProperties45);
            paragraph61.Append(run50);
            paragraph61.Append(run51);
            paragraph61.Append(run52);
            paragraph61.Append(run56);
            paragraph61.Append(run57);

            header1.Append(paragraph59);
            header1.Append(paragraph60);
            header1.Append(paragraph61);

            headerPart1.Header = header1;

        }

        public void ExportReportPDF(string docxPath, string pdfDestination)
        {
            // Create a new Microsoft Word application object
            Microsoft.Office.Interop.Word.Application word = new Application();

            // C# doesn't have optional arguments so we'll need a dummy value
            object oMissing = System.Reflection.Missing.Value;

            // Get list of Word files in specified directory
            FileInfo wordFile = new FileInfo(docxPath);

            word.Visible = false;
            word.ScreenUpdating = false;

            //foreach (FileInfo wordFile in wordFiles)
            //{
            // Cast as Object for word Open method
            Object filename = wordFile.FullName;

            // Use the dummy value as a placeholder for optional arguments
            Microsoft.Office.Interop.Word.Document doc = word.Documents.Open(ref filename, ref oMissing,
                                               ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                                               ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                                               ref oMissing, ref oMissing, ref oMissing, ref oMissing);
            doc.Activate();

            object outputFileName = pdfDestination;
            object fileFormat = WdSaveFormat.wdFormatPDF;

            // Save document into PDF Format
            doc.SaveAs(ref outputFileName,
                       ref fileFormat, ref oMissing, ref oMissing,
                       ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                       ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                       ref oMissing, ref oMissing, ref oMissing, ref oMissing);

            // Close the Word document, but leave the Word application open.
            // doc has to be cast to type _Document so that it will find the
            // correct Close method.                
            object saveChanges = WdSaveOptions.wdDoNotSaveChanges;
            doc.Close(ref saveChanges, ref oMissing, ref oMissing);
            doc = null;
            //}

            // word has to be cast to type _Application so that it will find
            // the correct Quit method.
            word.Quit(ref oMissing, ref oMissing, ref oMissing);
            word = null;
        }

        #endregion
    }
}
