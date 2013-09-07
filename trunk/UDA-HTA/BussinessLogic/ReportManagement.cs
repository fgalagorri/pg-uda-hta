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
            return uhda.InsertReport(report);
        }

        public void UpdateDiagnosis(long reportId, string diagnosis, DateTime diagnosisDate, string doctor)
        {
            UdaHtaDataAccess uda = new UdaHtaDataAccess();
            uda.UpdateDiagnosis(reportId, diagnosis, diagnosisDate, doctor);
        }

        public void UpdateMeasureInformation(long measureId, bool enabled, string comment)
        {
            UdaHtaDataAccess uda = new UdaHtaDataAccess();
            uda.UpdateMeasureInformation(measureId,enabled,comment);
        }

        public ICollection<Report> ListPatientReports(long idPatient)
        {
            UdaHtaDataAccess uda = new UdaHtaDataAccess();
            return uda.GetReportsByPatientId(idPatient);
        }

        public ICollection<Report> ListFilteredReports(int? patientLowerAge, int? patientUpperAge, DateTime? reportSinceDate, 
            DateTime? reportUntilDate, bool? isSmoker, bool? isDiabetic, bool? isHypertense, bool? isDysplidemic)
        {
            UdaHtaDataAccess uda = new UdaHtaDataAccess();
            var reports = uda.ListFilteredReport(patientLowerAge, patientUpperAge, reportSinceDate, reportUntilDate, isSmoker,
                                          isDiabetic, isHypertense, isDysplidemic);

            foreach (var report in reports)
            {   
                PatientDataAccess pda = new PatientDataAccess();
                report.Patient = pda.GetPatient(report.Patient.UdaId.Value);
            }

            return reports;
        } 

        public Limits GetLimits()
        {
            UdaHtaDataAccess uda = new UdaHtaDataAccess();
            return uda.GetLimits();
        }

        #region Drugs Region
        public void AddDrug(string type, string active, string name)
        {
            UdaHtaDataAccess uhda = new UdaHtaDataAccess();
            uhda.InsertDrug(name, active, type);
        }

        public void DeleteDrug(string name)
        {
            // TODO
        }

        public void EditDrug(int id, string type, string name, string active)
        {
            UdaHtaDataAccess uda = new UdaHtaDataAccess();
            uda.EditDrug(id, name, type, active);
        }

        public ICollection<string> GetDrugTypes()
        {
            UdaHtaDataAccess uda = new UdaHtaDataAccess();
            return uda.GetDrugTypes();
        }

        public ICollection<Drug> GetDrugs(string type, string active, string name)
        {
            UdaHtaDataAccess udaHta = new UdaHtaDataAccess();
            return udaHta.GetDrugs(type, active, name);
        }


        #endregion


        #region Export Region

        public void PrintReport(int idReport)
        {
        }

        public void ExportReportDocx(Report report, bool includePatientData, bool includeDiagnostic, bool includeProfile, bool includeGraphic, bool includeMeasures, string filePath)
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

                tableProperties1.AppendChild(tableStyle1);
                tableProperties1.AppendChild(tableWidth1);
                tableProperties1.AppendChild(tableLook1);

                table1.AppendChild(tableProperties1);

                TableGrid tableGrid1 = new TableGrid();
                GridColumn gridColumn1 = new GridColumn() {Width = "4788"};
                GridColumn gridColumn2 = new GridColumn() {Width = "4788"};

                tableGrid1.AppendChild(gridColumn1);
                tableGrid1.AppendChild(gridColumn2);

                table1.AppendChild(tableGrid1);

                TableRow tableRow1 = new TableRow()
                    {
                        RsidTableRowAddition = "004D2B75",
                        RsidTableRowProperties = "00EC01A4"
                    };

                TableRowProperties tableRowProperties1 = new TableRowProperties();
                TableRowHeight tableRowHeight1 = new TableRowHeight() {Val = (UInt32Value) 547U};

                tableRowProperties1.AppendChild(tableRowHeight1);

                TableCell tableCell1 = new TableCell();

                TableCellProperties tableCellProperties1 = new TableCellProperties();
                TableCellWidth tableCellWidth1 = new TableCellWidth()
                    {
                        Width = "9576",
                        Type = TableWidthUnitValues.Dxa
                    };
                GridSpan gridSpan1 = new GridSpan() {Val = 2};

                TableCellBorders tableCellBorders1 = new TableCellBorders();

                tableCellBorders1.AppendChild(new TopBorder() {Val = BorderValues.Nil});
                tableCellBorders1.AppendChild(new LeftBorder() {Val = BorderValues.Nil});
                tableCellBorders1.AppendChild(new BottomBorder() {Val = BorderValues.Nil});
                tableCellBorders1.AppendChild(new RightBorder() {Val = BorderValues.Nil});
                TableCellVerticalAlignment tableCellVerticalAlignment1 = new TableCellVerticalAlignment()
                    {
                        Val = TableVerticalAlignmentValues.Center
                    };

                tableCellProperties1.AppendChild(tableCellWidth1);
                tableCellProperties1.AppendChild(gridSpan1);
                tableCellProperties1.AppendChild(tableCellBorders1);
                tableCellProperties1.AppendChild(tableCellVerticalAlignment1);

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
                /*
                paragraphMarkRunProperties1.AppendChild(new Bold());
                paragraphMarkRunProperties1.AppendChild(new FontSize() {Val = "32"});
                paragraphMarkRunProperties1.AppendChild(new FontSizeComplexScript() {Val = "32"});
                */
                paragraphProperties1.AppendChild(justification1);
                paragraphProperties1.AppendChild(paragraphMarkRunProperties1);

                Run run1 = new Run() {RsidRunProperties = "004D2B75"};

                RunProperties runProperties1 = new RunProperties();

                runProperties1.AppendChild(new Bold());
                runProperties1.AppendChild(new FontSize() {Val = "32"});
                runProperties1.AppendChild(new FontSizeComplexScript() {Val = "32"});
                Text text1 = new Text();
                text1.Text = "Informe de Hipertension Arterial";

                run1.AppendChild(runProperties1);
                run1.AppendChild(text1);

                paragraph1.AppendChild(paragraphProperties1);
                paragraph1.AppendChild(run1);

                tableCell1.AppendChild(tableCellProperties1);
                tableCell1.AppendChild(paragraph1);

                tableRow1.AppendChild(tableRowProperties1);
                tableRow1.AppendChild(tableCell1);

                table1.AppendChild(tableRow1);

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

                tableCellBorders2.AppendChild(new TopBorder() {Val = BorderValues.Nil});
                tableCellBorders2.AppendChild(new LeftBorder() {Val = BorderValues.Nil});
                tableCellBorders2.AppendChild(bottomBorder2);
                tableCellBorders2.AppendChild(new RightBorder() {Val = BorderValues.Nil});

                tableCellProperties2.AppendChild(new TableCellWidth() {Width = "4788", Type = TableWidthUnitValues.Dxa});
                tableCellProperties2.AppendChild(tableCellBorders2);
                Paragraph paragraph2 = new Paragraph()
                    {
                        RsidParagraphAddition = "004D2B75",
                        RsidParagraphProperties = "00DF0ACA",
                        RsidRunAdditionDefault = "004D2B75"
                    };

                tableCell2.AppendChild(tableCellProperties2);
                tableCell2.AppendChild(paragraph2);

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

                tableCellBorders3.AppendChild(new TopBorder() {Val = BorderValues.Nil});
                tableCellBorders3.AppendChild(new LeftBorder() {Val = BorderValues.Nil});
                tableCellBorders3.AppendChild(bottomBorder3);
                tableCellBorders3.AppendChild(new RightBorder() {Val = BorderValues.Nil});

                tableCellProperties3.AppendChild(new TableCellWidth() {Width = "4788", Type = TableWidthUnitValues.Dxa});
                tableCellProperties3.AppendChild(tableCellBorders3);
                Paragraph paragraph3 = new Paragraph()
                    {
                        RsidParagraphAddition = "004D2B75",
                        RsidParagraphProperties = "00DF0ACA",
                        RsidRunAdditionDefault = "004D2B75"
                    };

                tableCell3.AppendChild(tableCellProperties3);
                tableCell3.AppendChild(paragraph3);

                tableRow2.AppendChild(tableCell2);
                tableRow2.AppendChild(tableCell3);

                table1.AppendChild(tableRow2);

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

                tableCellBorders4.AppendChild(topBorder4);
                tableCellBorders4.AppendChild(new LeftBorder() {Val = BorderValues.Nil});
                tableCellBorders4.AppendChild(new BottomBorder() {Val = BorderValues.Nil});
                tableCellBorders4.AppendChild(new RightBorder() {Val = BorderValues.Nil});

                tableCellProperties4.AppendChild(new TableCellWidth() {Width = "4788", Type = TableWidthUnitValues.Dxa});
                tableCellProperties4.AppendChild(tableCellBorders4);

                /*
                 * INCLUIR DATOS PACIENTE
                 */
                if (includePatientData)
                {
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

                    run5.AppendChild(text5);
                    paragraph4.AppendChild(run5);
                    tableCell4.AppendChild(tableCellProperties4);
                    tableCell4.AppendChild(paragraph4);

                    TableCell tableCell5 = new TableCell();

                    TableCellProperties tableCellProperties5 = new TableCellProperties();

                    TableCellBorders tableCellBorders5 = new TableCellBorders();
                    TopBorder topBorder5 = new TopBorder()
                    {
                        Val = BorderValues.Single,
                        Color = "auto",
                        Size = (UInt32Value)4U,
                        Space = (UInt32Value)0U
                    };

                    tableCellBorders5.AppendChild(topBorder5);
                    tableCellBorders5.AppendChild(new LeftBorder() { Val = BorderValues.Nil });
                    tableCellBorders5.AppendChild(new BottomBorder() { Val = BorderValues.Nil });
                    tableCellBorders5.AppendChild(new RightBorder() { Val = BorderValues.Nil });

                    tableCellProperties5.AppendChild(new TableCellWidth() { Width = "4788", Type = TableWidthUnitValues.Dxa });
                    tableCellProperties5.AppendChild(tableCellBorders5);
                    Paragraph paragraph5 = new Paragraph()
                    {
                        RsidParagraphAddition = "00AA46B2",
                        RsidParagraphProperties = "00DF0ACA",
                        RsidRunAdditionDefault = "00AA46B2"
                    };

                    tableCell5.AppendChild(tableCellProperties5);
                    tableCell5.AppendChild(paragraph5);

                    tableRow3.AppendChild(tableCell4);
                    tableRow3.AppendChild(tableCell5);

                    table1.AppendChild(tableRow3);

                    TableRow tableRow4 = new TableRow()
                    {
                        RsidTableRowAddition = "004D2B75",
                        RsidTableRowProperties = "00AA46B2"
                    };

                    TableCell tableCell6 = new TableCell();

                    TableCellProperties tableCellProperties6 = new TableCellProperties();

                    TableCellBorders tableCellBorders6 = new TableCellBorders();

                    tableCellBorders6.AppendChild(new TopBorder() { Val = BorderValues.Nil });
                    tableCellBorders6.AppendChild(new LeftBorder() { Val = BorderValues.Nil });
                    tableCellBorders6.AppendChild(new BottomBorder() { Val = BorderValues.Nil });
                    tableCellBorders6.AppendChild(new RightBorder() { Val = BorderValues.Nil });

                    tableCellProperties6.AppendChild(new TableCellWidth() { Width = "4788", Type = TableWidthUnitValues.Dxa });
                    tableCellProperties6.AppendChild(tableCellBorders6);

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

                    run9.AppendChild(text9);
                    paragraph6.AppendChild(run9);
                    tableCell6.AppendChild(tableCellProperties6);
                    tableCell6.AppendChild(paragraph6);

                    TableCell tableCell7 = new TableCell();

                    TableCellProperties tableCellProperties7 = new TableCellProperties();

                    TableCellBorders tableCellBorders7 = new TableCellBorders();

                    tableCellBorders7.AppendChild(new TopBorder() { Val = BorderValues.Nil });
                    tableCellBorders7.AppendChild(new LeftBorder() { Val = BorderValues.Nil });
                    tableCellBorders7.AppendChild(new BottomBorder() { Val = BorderValues.Nil });
                    tableCellBorders7.AppendChild(new RightBorder() { Val = BorderValues.Nil });

                    tableCellProperties7.AppendChild(new TableCellWidth() { Width = "4788", Type = TableWidthUnitValues.Dxa });
                    tableCellProperties7.AppendChild(tableCellBorders7);

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

                    run11.AppendChild(text11);

                    paragraph7.AppendChild(run11);
                    tableCell7.AppendChild(tableCellProperties7);
                    tableCell7.AppendChild(paragraph7);

                    tableRow4.AppendChild(tableCell6);
                    tableRow4.AppendChild(tableCell7);

                    TableRow tableRow5 = new TableRow()
                    {
                        RsidTableRowAddition = "004D2B75",
                        RsidTableRowProperties = "00EC01A4"
                    };

                    TableCell tableCell8 = new TableCell();

                    TableCellProperties tableCellProperties8 = new TableCellProperties();

                    TableCellBorders tableCellBorders8 = new TableCellBorders();

                    tableCellBorders8.AppendChild(new TopBorder() { Val = BorderValues.Nil });
                    tableCellBorders8.AppendChild(new LeftBorder() { Val = BorderValues.Nil });
                    tableCellBorders8.AppendChild(new BottomBorder() { Val = BorderValues.Nil });
                    tableCellBorders8.AppendChild(new RightBorder() { Val = BorderValues.Nil });

                    tableCellProperties8.AppendChild(new TableCellWidth() { Width = "4788", Type = TableWidthUnitValues.Dxa });
                    tableCellProperties8.AppendChild(tableCellBorders8);

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


                    run15.AppendChild(text15);
                    paragraph8.AppendChild(run15);

                    tableCell8.AppendChild(tableCellProperties8);
                    tableCell8.AppendChild(paragraph8);

                    TableCell tableCell9 = new TableCell();

                    TableCellProperties tableCellProperties9 = new TableCellProperties();

                    TableCellBorders tableCellBorders9 = new TableCellBorders();

                    tableCellBorders9.AppendChild(new TopBorder() { Val = BorderValues.Nil });
                    tableCellBorders9.AppendChild(new LeftBorder() { Val = BorderValues.Nil });
                    tableCellBorders9.AppendChild(new BottomBorder() { Val = BorderValues.Nil });
                    tableCellBorders9.AppendChild(new RightBorder() { Val = BorderValues.Nil });

                    tableCellProperties9.AppendChild(new TableCellWidth() { Width = "4788", Type = TableWidthUnitValues.Dxa });
                    tableCellProperties9.AppendChild(tableCellBorders9);

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

                    run17.AppendChild(text17);

                    paragraph9.AppendChild(run17);

                    tableCell9.AppendChild(tableCellProperties9);
                    tableCell9.AppendChild(paragraph9);

                    tableRow5.AppendChild(tableCell8);
                    tableRow5.AppendChild(tableCell9);

                    TableRow tableRow6 = new TableRow()
                    {
                        RsidTableRowAddition = "004D2B75",
                        RsidTableRowProperties = "00EC01A4"
                    };

                    TableCell tableCell10 = new TableCell();

                    TableCellProperties tableCellProperties10 = new TableCellProperties();

                    TableCellBorders tableCellBorders10 = new TableCellBorders();

                    tableCellBorders10.AppendChild(new TopBorder() { Val = BorderValues.Nil });
                    tableCellBorders10.AppendChild(new LeftBorder() { Val = BorderValues.Nil });
                    tableCellBorders10.AppendChild(new BottomBorder() { Val = BorderValues.Nil });
                    tableCellBorders10.AppendChild(new RightBorder() { Val = BorderValues.Nil });

                    tableCellProperties10.AppendChild(new TableCellWidth() { Width = "4788", Type = TableWidthUnitValues.Dxa });
                    tableCellProperties10.AppendChild(tableCellBorders10);

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

                    run18.AppendChild(text18);

                    paragraph10.AppendChild(run18);

                    tableCell10.AppendChild(tableCellProperties10);
                    tableCell10.AppendChild(paragraph10);

                    TableCell tableCell11 = new TableCell();

                    TableCellProperties tableCellProperties11 = new TableCellProperties();
                    TableCellWidth tableCellWidth11 = new TableCellWidth()
                    {
                        Width = "4788",
                        Type = TableWidthUnitValues.Dxa
                    };

                    TableCellBorders tableCellBorders11 = new TableCellBorders();
                    TopBorder topBorder11 = new TopBorder() { Val = BorderValues.Nil };
                    LeftBorder leftBorder11 = new LeftBorder() { Val = BorderValues.Nil };
                    BottomBorder bottomBorder11 = new BottomBorder() { Val = BorderValues.Nil };
                    RightBorder rightBorder11 = new RightBorder() { Val = BorderValues.Nil };

                    tableCellBorders11.AppendChild(topBorder11);
                    tableCellBorders11.AppendChild(leftBorder11);
                    tableCellBorders11.AppendChild(bottomBorder11);
                    tableCellBorders11.AppendChild(rightBorder11);

                    tableCellProperties11.AppendChild(tableCellWidth11);
                    tableCellProperties11.AppendChild(tableCellBorders11);

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

                    run20.AppendChild(text20);
                    paragraph11.AppendChild(run20);

                    tableCell11.AppendChild(tableCellProperties11);
                    tableCell11.AppendChild(paragraph11);

                    tableRow6.AppendChild(tableCell10);
                    tableRow6.AppendChild(tableCell11);

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
                    TopBorder topBorder12 = new TopBorder() { Val = BorderValues.Nil };
                    LeftBorder leftBorder12 = new LeftBorder() { Val = BorderValues.Nil };
                    BottomBorder bottomBorder12 = new BottomBorder() { Val = BorderValues.Nil };
                    RightBorder rightBorder12 = new RightBorder() { Val = BorderValues.Nil };

                    tableCellBorders12.AppendChild(topBorder12);
                    tableCellBorders12.AppendChild(leftBorder12);
                    tableCellBorders12.AppendChild(bottomBorder12);
                    tableCellBorders12.AppendChild(rightBorder12);

                    tableCellProperties12.AppendChild(tableCellWidth12);
                    tableCellProperties12.AppendChild(tableCellBorders12);

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

                    run22.AppendChild(text22);
                    paragraph12.AppendChild(run22);

                    tableCell12.AppendChild(tableCellProperties12);
                    tableCell12.AppendChild(paragraph12);

                    TableCell tableCell13 = new TableCell();

                    TableCellProperties tableCellProperties13 = new TableCellProperties();
                    TableCellWidth tableCellWidth13 = new TableCellWidth()
                    {
                        Width = "4788",
                        Type = TableWidthUnitValues.Dxa
                    };

                    TableCellBorders tableCellBorders13 = new TableCellBorders();
                    TopBorder topBorder13 = new TopBorder() { Val = BorderValues.Nil };
                    LeftBorder leftBorder13 = new LeftBorder() { Val = BorderValues.Nil };
                    BottomBorder bottomBorder13 = new BottomBorder() { Val = BorderValues.Nil };
                    RightBorder rightBorder13 = new RightBorder() { Val = BorderValues.Nil };

                    tableCellBorders13.AppendChild(topBorder13);
                    tableCellBorders13.AppendChild(leftBorder13);
                    tableCellBorders13.AppendChild(bottomBorder13);
                    tableCellBorders13.AppendChild(rightBorder13);

                    tableCellProperties13.AppendChild(tableCellWidth13);
                    tableCellProperties13.AppendChild(tableCellBorders13);

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

                    run24.AppendChild(text24);
                    paragraph13.AppendChild(run24);

                    tableCell13.AppendChild(tableCellProperties13);
                    tableCell13.AppendChild(paragraph13);

                    tableRow7.AppendChild(tableCell12);
                    tableRow7.AppendChild(tableCell13);

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
                    TopBorder topBorder14 = new TopBorder() { Val = BorderValues.Nil };
                    LeftBorder leftBorder14 = new LeftBorder() { Val = BorderValues.Nil };
                    BottomBorder bottomBorder14 = new BottomBorder() { Val = BorderValues.Nil };
                    RightBorder rightBorder14 = new RightBorder() { Val = BorderValues.Nil };

                    tableCellBorders14.AppendChild(topBorder14);
                    tableCellBorders14.AppendChild(leftBorder14);
                    tableCellBorders14.AppendChild(bottomBorder14);
                    tableCellBorders14.AppendChild(rightBorder14);

                    tableCellProperties14.AppendChild(tableCellWidth14);
                    tableCellProperties14.AppendChild(tableCellBorders14);

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

                    run26.AppendChild(text26);
                    paragraph14.AppendChild(run26);

                    tableCell14.AppendChild(tableCellProperties14);
                    tableCell14.AppendChild(paragraph14);

                    TableCell tableCell15 = new TableCell();

                    TableCellProperties tableCellProperties15 = new TableCellProperties();
                    TableCellWidth tableCellWidth15 = new TableCellWidth()
                    {
                        Width = "4788",
                        Type = TableWidthUnitValues.Dxa
                    };

                    TableCellBorders tableCellBorders15 = new TableCellBorders();
                    TopBorder topBorder15 = new TopBorder() { Val = BorderValues.Nil };
                    LeftBorder leftBorder15 = new LeftBorder() { Val = BorderValues.Nil };
                    BottomBorder bottomBorder15 = new BottomBorder() { Val = BorderValues.Nil };
                    RightBorder rightBorder15 = new RightBorder() { Val = BorderValues.Nil };

                    tableCellBorders15.AppendChild(topBorder15);
                    tableCellBorders15.AppendChild(leftBorder15);
                    tableCellBorders15.AppendChild(bottomBorder15);
                    tableCellBorders15.AppendChild(rightBorder15);

                    tableCellProperties15.AppendChild(tableCellWidth15);
                    tableCellProperties15.AppendChild(tableCellBorders15);

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

                    run28.AppendChild(text28);

                    paragraph15.AppendChild(run28);

                    tableCell15.AppendChild(tableCellProperties15);
                    tableCell15.AppendChild(paragraph15);

                    tableRow8.AppendChild(tableCell14);
                    tableRow8.AppendChild(tableCell15);

                    table1.AppendChild(tableRow4);
                    table1.AppendChild(tableRow5);
                    table1.AppendChild(tableRow6);
                    table1.AppendChild(tableRow7);
                    table1.AppendChild(tableRow8);

                }


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

                tableCellBorders16.AppendChild(topBorder16);
                tableCellBorders16.AppendChild(leftBorder16);
                tableCellBorders16.AppendChild(rightBorder16);

                tableCellProperties16.AppendChild(tableCellWidth16);
                tableCellProperties16.AppendChild(gridSpan2);
                tableCellProperties16.AppendChild(tableCellBorders16);

                Paragraph paragraph16 = new Paragraph()
                    {
                        RsidParagraphAddition = "004D2B75",
                        RsidParagraphProperties = "00DF0ACA",
                        RsidRunAdditionDefault = "004D2B75"
                    };
                BookmarkStart bookmarkStart1 = new BookmarkStart() {Name = "_GoBack", Id = "0"};
                BookmarkEnd bookmarkEnd1 = new BookmarkEnd() {Id = "0"};

                paragraph16.AppendChild(bookmarkStart1);
                paragraph16.AppendChild(bookmarkEnd1);

                tableCell16.AppendChild(tableCellProperties16);
                tableCell16.AppendChild(paragraph16);

                tableRow9.AppendChild(tableCell16);

                table1.AppendChild(tableRow9);

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

                tableCellBorders17.AppendChild(topBorder16_1);
                tableCellBorders17.AppendChild(leftBorder17);
                tableCellBorders17.AppendChild(bottomBorder16);

                tableCellProperties17.AppendChild(tableCellWidth17);
                tableCellProperties17.AppendChild(tableCellBorders17);
                Paragraph paragraph17 = new Paragraph()
                    {
                        RsidParagraphAddition = "004D2B75",
                        RsidParagraphProperties = "00DF0ACA",
                        RsidRunAdditionDefault = "004D2B75"
                    };

                tableCell17.AppendChild(tableCellProperties17);
                tableCell17.AppendChild(paragraph17);

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

                tableCellBorders18.AppendChild(topBorder17_1);
                tableCellBorders18.AppendChild(bottomBorder17);
                tableCellBorders18.AppendChild(rightBorder17);

                tableCellProperties18.AppendChild(tableCellWidth18);
                tableCellProperties18.AppendChild(tableCellBorders18);

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

                paragraphMarkRunProperties2.AppendChild(bold6);

                paragraphProperties2.AppendChild(paragraphMarkRunProperties2);

                Run run29 = new Run() {RsidRunProperties = "004D2B75"};

                RunProperties runProperties5 = new RunProperties();
                Bold bold7 = new Bold();

                runProperties5.AppendChild(bold7);
                runProperties5.AppendChild(new FontSize() { Val = "28" });
                runProperties5.AppendChild(new FontSizeComplexScript() { Val = "28" });
                Text text29 = new Text();
                text29.Text = "24h ABDM";

                run29.AppendChild(runProperties5);
                run29.AppendChild(text29);

                paragraph18.AppendChild(paragraphProperties2);
                paragraph18.AppendChild(run29);

                Paragraph paragraph19 = new Paragraph()
                    {
                        RsidParagraphAddition = "004D2B75",
                        RsidParagraphProperties = "004D2B75",
                        RsidRunAdditionDefault = "004D2B75"
                    };

                ParagraphProperties paragraphProperties3 = new ParagraphProperties();

                ParagraphMarkRunProperties paragraphMarkRunProperties3 = new ParagraphMarkRunProperties();


                paragraphProperties3.AppendChild(paragraphMarkRunProperties3);

                Run run30 = new Run() {RsidRunProperties = "004D2B75"};

                RunProperties runProperties6 = new RunProperties();

                Text text30 = new Text() {Space = SpaceProcessingModeValues.Preserve};
                text30.Text = "Fecha y hora de inicio: " + report.BeginDate.ToString();

                run30.AppendChild(runProperties6);
                run30.AppendChild(text30);

                paragraph19.AppendChild(paragraphProperties3);
                paragraph19.AppendChild(run30);

                Paragraph paragraph20 = new Paragraph()
                    {
                        RsidParagraphMarkRevision = "004D2B75",
                        RsidParagraphAddition = "004D2B75",
                        RsidParagraphProperties = "004D2B75",
                        RsidRunAdditionDefault = "004D2B75"
                    };

                ParagraphProperties paragraphProperties4 = new ParagraphProperties();

                ParagraphMarkRunProperties paragraphMarkRunProperties4 = new ParagraphMarkRunProperties();

                paragraphProperties4.AppendChild(paragraphMarkRunProperties4);

                Run run31 = new Run();

                RunProperties runProperties7 = new RunProperties();

                Text text31 = new Text();
                text31.Text = "Fecha y hora de fin: " + report.EndDate;

                run31.AppendChild(runProperties7);
                run31.AppendChild(text31);

                paragraph20.AppendChild(paragraphProperties4);
                paragraph20.AppendChild(run31);

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

                paragraphProperties3_1.AppendChild(paragraphMarkRunProperties3_1);

                Run run3_1 = new Run();

                RunProperties runProperties3_1 = new RunProperties();

                Text text3_1 = new Text();
                text3_1.Text = "\n Hora inicio noche: " + report.Carnet.SleepTimeStart.ToString();

                run3_1.AppendChild(runProperties3_1);
                run3_1.AppendChild(text3_1);

                paragraph3_1.AppendChild(paragraphProperties3_1);
                paragraph3_1.AppendChild(run3_1);

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

                paragraphProperties3_2.AppendChild(paragraphMarkRunProperties3_2);

                Run run3_2 = new Run();

                RunProperties runProperties3_2 = new RunProperties();

                Text text3_2 = new Text();
                text3_2.Text = "Hora fin noche: " + report.Carnet.SleepTimeEnd.ToString();

                run3_2.AppendChild(runProperties3_2);
                run3_2.AppendChild(text3_2);

                paragraph3_2.AppendChild(paragraphProperties3_2);
                paragraph3_2.AppendChild(run3_2);

                //-----
                tableCell18.AppendChild(tableCellProperties18);
                tableCell18.AppendChild(paragraph18);
                tableCell18.AppendChild(paragraph19);
                tableCell18.AppendChild(paragraph20);
                tableCell18.AppendChild(paragraph3_1);
                tableCell18.AppendChild(paragraph3_2);

                tableRow10.AppendChild(tableCell17);
                tableRow10.AppendChild(tableCell18);

                table1.AppendChild(tableRow10);


                /*
                 * ICLUIR RESUMEN DE MEDIDAS
                 */
                if (includeProfile)
                {
                    TableRow tableRow11 = new TableRow()
                    {
                        RsidTableRowMarkRevision = "004D2B75",
                        RsidTableRowAddition = "00EC01A4",
                        RsidTableRowProperties = "00EC01A4"
                    };

                    TableRowProperties tableRowProperties2 = new TableRowProperties();
                    TableRowHeight tableRowHeight2 = new TableRowHeight() { Val = (UInt32Value)242U };

                    tableRowProperties2.AppendChild(tableRowHeight2);

                    TableCell tableCell19 = new TableCell();

                    TableCellProperties tableCellProperties19 = new TableCellProperties();
                    TableCellWidth tableCellWidth19 = new TableCellWidth()
                    {
                        Width = "9576",
                        Type = TableWidthUnitValues.Dxa
                    };
                    GridSpan gridSpan3 = new GridSpan() { Val = 2 };

                    TableCellBorders tableCellBorders19 = new TableCellBorders();
                    LeftBorder leftBorder18 = new LeftBorder() { Val = BorderValues.Nil };
                    BottomBorder bottomBorder18 = new BottomBorder() { Val = BorderValues.Nil };
                    RightBorder rightBorder18 = new RightBorder() { Val = BorderValues.Nil };

                    tableCellBorders19.AppendChild(leftBorder18);
                    tableCellBorders19.AppendChild(bottomBorder18);
                    tableCellBorders19.AppendChild(rightBorder18);
                    TableCellVerticalAlignment tableCellVerticalAlignment2 = new TableCellVerticalAlignment()
                    {
                        Val = TableVerticalAlignmentValues.Center
                    };

                    tableCellProperties19.AppendChild(tableCellWidth19);
                    tableCellProperties19.AppendChild(gridSpan3);
                    tableCellProperties19.AppendChild(tableCellBorders19);
                    tableCellProperties19.AppendChild(tableCellVerticalAlignment2);

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

                    paragraphMarkRunProperties5.AppendChild(bold8);

                    paragraphProperties5.AppendChild(paragraphMarkRunProperties5);

                    Run run32 = new Run() {RsidRunProperties = "00EC01A4"};

                    RunProperties runProperties8 = new RunProperties();
                    Bold bold9 = new Bold();

                    runProperties8.AppendChild(bold9);
                    runProperties8.AppendChild(new FontSize() { Val = "28" });
                    runProperties8.AppendChild(new FontSizeComplexScript() { Val = "28" });
                    Text text32 = new Text();
                    text32.Text = "Resumen de Medidas";

                    run32.AppendChild(runProperties8);
                    run32.AppendChild(text32);

                    paragraph21.AppendChild(paragraphProperties5);
                    paragraph21.AppendChild(run32);

                    tableCell19.AppendChild(tableCellProperties19);
                    tableCell19.AppendChild(paragraph21);

                    tableRow11.AppendChild(tableRowProperties2);
                    tableRow11.AppendChild(tableCell19);

                    table1.AppendChild(tableRow11);

                    //Resumen de medidas
                    var tableRow12 = MeasuresSummary(report);
                    table1.AppendChild(tableRow12);
                }

                /*
                 * INCLUIR DIAGNOSTICO
                 */
                if (includeDiagnostic)
                {
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

                    GridSpan gridSpan5 = new GridSpan() { Val = 2 };

                    TableCellBorders tableCellBorders21 = new TableCellBorders();
                    LeftBorder leftBorder20 = new LeftBorder() { Val = BorderValues.Nil };
                    BottomBorder bottomBorder20 = new BottomBorder() { Val = BorderValues.Nil };
                    RightBorder rightBorder20 = new RightBorder() { Val = BorderValues.Nil };

                    tableCellBorders21.AppendChild(leftBorder20);
                    tableCellBorders21.AppendChild(bottomBorder20);
                    tableCellBorders21.AppendChild(rightBorder20);

                    tableCellProperties21.AppendChild(tableCellWidth21);
                    tableCellProperties21.AppendChild(gridSpan5);
                    tableCellProperties21.AppendChild(tableCellBorders21);


                    Paragraph paragraph23 = new Paragraph()
                    {
                        RsidParagraphMarkRevision = "004D2B75",
                        RsidParagraphAddition = "00EC01A4",
                        RsidRunAdditionDefault = "00EC01A4"
                    };

                    ParagraphProperties paragraphProperties7 = new ParagraphProperties();

                    ParagraphMarkRunProperties paragraphMarkRunProperties7 = new ParagraphMarkRunProperties();

                    paragraphProperties7.AppendChild(paragraphMarkRunProperties7);

                    Run run34 = new Run() { RsidRunProperties = "00EC01A4" };

                    RunProperties runProperties10 = new RunProperties();
                    Bold bold10 = new Bold();

                    runProperties10.AppendChild(bold10);
                    runProperties10.AppendChild(new FontSize() { Val = "28" });
                    runProperties10.AppendChild(new FontSizeComplexScript() { Val = "28" });
                    Text text34 = new Text();
                    text34.Text = "Diagnóstico";

                    run34.AppendChild(runProperties10);
                    run34.AppendChild(text34);

                    paragraph23.AppendChild(paragraphProperties7);
                    paragraph23.AppendChild(run34);

                    tableCell21.AppendChild(tableCellProperties21);
                    tableCell21.AppendChild(paragraph23);

                    tableRow13.AppendChild(tableCell21);

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
                    GridSpan gridSpan6 = new GridSpan() { Val = 2 };

                    TableCellBorders tableCellBorders22 = new TableCellBorders();
                    TopBorder topBorder18 = new TopBorder() { Val = BorderValues.Nil };
                    LeftBorder leftBorder21 = new LeftBorder() { Val = BorderValues.Nil };
                    BottomBorder bottomBorder21 = new BottomBorder() { Val = BorderValues.Nil };
                    RightBorder rightBorder21 = new RightBorder() { Val = BorderValues.Nil };

                    tableCellBorders22.AppendChild(topBorder18);
                    tableCellBorders22.AppendChild(leftBorder21);
                    tableCellBorders22.AppendChild(bottomBorder21);
                    tableCellBorders22.AppendChild(rightBorder21);

                    tableCellProperties22.AppendChild(tableCellWidth22);
                    tableCellProperties22.AppendChild(gridSpan6);
                    tableCellProperties22.AppendChild(tableCellBorders22);

                    Paragraph paragraph24 = new Paragraph()
                    {
                        RsidParagraphMarkRevision = "004D2B75",
                        RsidParagraphAddition = "00EC01A4",
                        RsidRunAdditionDefault = "00EC01A4"
                    };

                    ParagraphProperties paragraphProperties8 = new ParagraphProperties();

                    ParagraphMarkRunProperties paragraphMarkRunProperties8 = new ParagraphMarkRunProperties();

                    paragraphProperties8.AppendChild(paragraphMarkRunProperties8);

                    Run run35 = new Run();


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
                        run35.AppendChild(text35);
                    }


                    paragraph24.AppendChild(paragraphProperties8);
                    paragraph24.AppendChild(run35);

                    tableCell22.AppendChild(tableCellProperties22);
                    tableCell22.AppendChild(paragraph24);

                    tableRow14.AppendChild(tableCell22);

                    table1.AppendChild(tableRow13);
                    table1.AppendChild(tableRow14);
                }


                /*
                 * INLCUIR GRAFICA
                 */
                if (includeGraphic)
                {
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
                    GridSpan gridSpan7 = new GridSpan() { Val = 2 };

                    TableCellBorders tableCellBorders23 = new TableCellBorders();
                    LeftBorder leftBorder22 = new LeftBorder() { Val = BorderValues.Nil };
                    BottomBorder bottomBorder22 = new BottomBorder() { Val = BorderValues.Nil };
                    RightBorder rightBorder22 = new RightBorder() { Val = BorderValues.Nil };

                    tableCellBorders23.AppendChild(leftBorder22);
                    tableCellBorders23.AppendChild(bottomBorder22);
                    tableCellBorders23.AppendChild(rightBorder22);

                    tableCellProperties23.AppendChild(tableCellWidth23);
                    tableCellProperties23.AppendChild(gridSpan7);
                    tableCellProperties23.AppendChild(tableCellBorders23);

                    Paragraph paragraph25 = new Paragraph()
                    {
                        RsidParagraphMarkRevision = "00EC01A4",
                        RsidParagraphAddition = "00EC01A4",
                        RsidRunAdditionDefault = "00EC01A4"
                    };

                    ParagraphProperties paragraphProperties9 = new ParagraphProperties();

                    ParagraphMarkRunProperties paragraphMarkRunProperties9 = new ParagraphMarkRunProperties();
                    Bold bold11 = new Bold();

                    paragraphMarkRunProperties9.AppendChild(bold11);

                    paragraphProperties9.AppendChild(paragraphMarkRunProperties9);

                    Run run36 = new Run() { RsidRunProperties = "00EC01A4" };

                    RunProperties runProperties12 = new RunProperties();
                    Bold bold12 = new Bold();

                    runProperties12.AppendChild(bold12);
                    runProperties12.AppendChild(new FontSize() { Val = "28" });
                    runProperties12.AppendChild(new FontSizeComplexScript() { Val = "28" });
                    Text text36 = new Text();
                    text36.Text = "Perfil de la Tensión Arterial";

                    run36.AppendChild(runProperties12);
                    run36.AppendChild(text36);

                    paragraph25.AppendChild(paragraphProperties9);
                    paragraph25.AppendChild(run36);

                    tableCell23.AppendChild(tableCellProperties23);
                    tableCell23.AppendChild(paragraph25);

                    tableRow15.AppendChild(tableCell23);

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
                    GridSpan gridSpan8 = new GridSpan() { Val = 2 };

                    TableCellBorders tableCellBorders24 = new TableCellBorders();
                    TopBorder topBorder19 = new TopBorder() { Val = BorderValues.Nil };
                    LeftBorder leftBorder23 = new LeftBorder() { Val = BorderValues.Nil };
                    BottomBorder bottomBorder23 = new BottomBorder()
                    {
                        Val = BorderValues.Single,
                        Color = "auto",
                        Size = (UInt32Value)4U,
                        Space = (UInt32Value)0U
                    };
                    RightBorder rightBorder23 = new RightBorder() { Val = BorderValues.Nil };

                    tableCellBorders24.AppendChild(topBorder19);
                    tableCellBorders24.AppendChild(leftBorder23);
                    tableCellBorders24.AppendChild(bottomBorder23);
                    tableCellBorders24.AppendChild(rightBorder23);

                    tableCellProperties24.AppendChild(tableCellWidth24);
                    tableCellProperties24.AppendChild(gridSpan8);
                    tableCellProperties24.AppendChild(tableCellBorders24);

                    Paragraph paragraph26 = new Paragraph()
                    {
                        RsidParagraphMarkRevision = "004D2B75",
                        RsidParagraphAddition = "00EC01A4",
                        RsidRunAdditionDefault = "00EC01A4"
                    };

                    ParagraphProperties paragraphProperties10 = new ParagraphProperties();

                    ParagraphMarkRunProperties paragraphMarkRunProperties10 = new ParagraphMarkRunProperties();

                    paragraphProperties10.AppendChild(paragraphMarkRunProperties10);

                    Run run37 = new Run();

                    RunProperties runProperties13 = new RunProperties();

                    Text text37 = new Text();
                    text37.Text = "<Gráfica tensión arterial/tiempo>";

                    run37.AppendChild(runProperties13);
                    run37.AppendChild(text37);

                    paragraph26.AppendChild(paragraphProperties10);
                    paragraph26.AppendChild(run37);

                    tableCell24.AppendChild(tableCellProperties24);
                    tableCell24.AppendChild(paragraph26);

                    tableRow16.AppendChild(tableCell24);

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
                    GridSpan gridSpan9 = new GridSpan() { Val = 2 };

                    TableCellBorders tableCellBorders25 = new TableCellBorders();
                    LeftBorder leftBorder24 = new LeftBorder() { Val = BorderValues.Nil };
                    BottomBorder bottomBorder24 = new BottomBorder() { Val = BorderValues.Nil };
                    RightBorder rightBorder24 = new RightBorder() { Val = BorderValues.Nil };

                    tableCellBorders25.AppendChild(leftBorder24);
                    tableCellBorders25.AppendChild(bottomBorder24);
                    tableCellBorders25.AppendChild(rightBorder24);

                    tableCellProperties25.AppendChild(tableCellWidth25);
                    tableCellProperties25.AppendChild(gridSpan9);
                    tableCellProperties25.AppendChild(tableCellBorders25);

                    Paragraph paragraph27 = new Paragraph()
                    {
                        RsidParagraphMarkRevision = "00EC01A4",
                        RsidParagraphAddition = "00EC01A4",
                        RsidRunAdditionDefault = "00EC01A4"
                    };

                    ParagraphProperties paragraphProperties11 = new ParagraphProperties();

                    ParagraphMarkRunProperties paragraphMarkRunProperties11 = new ParagraphMarkRunProperties();
                    Bold bold13 = new Bold();

                    paragraphMarkRunProperties11.AppendChild(bold13);

                    paragraphProperties11.AppendChild(paragraphMarkRunProperties11);

                    Run run38 = new Run() { RsidRunProperties = "00EC01A4" };

                    RunProperties runProperties14 = new RunProperties();
                    Bold bold14 = new Bold();

                    runProperties14.AppendChild(bold14);
                    runProperties14.AppendChild(new FontSize() { Val = "28" });
                    runProperties14.AppendChild(new FontSizeComplexScript() { Val = "28" });
                    Text text38 = new Text();
                    text38.Text = "Valores por encima del límite";

                    run38.AppendChild(runProperties14);
                    run38.AppendChild(text38);

                    paragraph27.AppendChild(paragraphProperties11);
                    paragraph27.AppendChild(run38);

                    tableCell25.AppendChild(tableCellProperties25);
                    tableCell25.AppendChild(paragraph27);

                    tableRow17.AppendChild(tableCell25);

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
                    GridSpan gridSpan10 = new GridSpan() { Val = 2 };

                    TableCellBorders tableCellBorders26 = new TableCellBorders();
                    TopBorder topBorder20 = new TopBorder() { Val = BorderValues.Nil };
                    LeftBorder leftBorder25 = new LeftBorder() { Val = BorderValues.Nil };
                    BottomBorder bottomBorder25 = new BottomBorder() { Val = BorderValues.Nil };
                    RightBorder rightBorder25 = new RightBorder() { Val = BorderValues.Nil };

                    tableCellBorders26.AppendChild(topBorder20);
                    tableCellBorders26.AppendChild(leftBorder25);
                    tableCellBorders26.AppendChild(bottomBorder25);
                    tableCellBorders26.AppendChild(rightBorder25);

                    tableCellProperties26.AppendChild(tableCellWidth26);
                    tableCellProperties26.AppendChild(gridSpan10);
                    tableCellProperties26.AppendChild(tableCellBorders26);

                    Paragraph paragraph28 = new Paragraph()
                    {
                        RsidParagraphMarkRevision = "004D2B75",
                        RsidParagraphAddition = "00EC01A4",
                        RsidRunAdditionDefault = "00EC01A4"
                    };

                    ParagraphProperties paragraphProperties12 = new ParagraphProperties();

                    ParagraphMarkRunProperties paragraphMarkRunProperties12 = new ParagraphMarkRunProperties();

                    paragraphProperties12.AppendChild(paragraphMarkRunProperties12);

                    Run run39 = new Run();

                    RunProperties runProperties15 = new RunProperties();

                    Text text39 = new Text();
                    text39.Text = "<Gráfico de torta>";

                    run39.AppendChild(runProperties15);
                    run39.AppendChild(text39);

                    paragraph28.AppendChild(paragraphProperties12);
                    paragraph28.AppendChild(run39);

                    tableCell26.AppendChild(tableCellProperties26);
                    tableCell26.AppendChild(paragraph28);

                    tableRow18.AppendChild(tableCell26);

                    table1.AppendChild(tableRow15);
                    table1.AppendChild(tableRow16);
                    table1.AppendChild(tableRow17);
                    table1.AppendChild(tableRow18);
                }

                /*
                 * TABLA DE MEDIDAS
                 */
                if (includeMeasures)
                {
                    TableRow tableRow30 = new TableRow()
                    {
                        RsidTableRowMarkRevision = "004D2B75",
                        RsidTableRowAddition = "00EC01A4",
                        RsidTableRowProperties = "00EC01A4"
                    };

                    TableRowProperties tableRowProperties30 = new TableRowProperties();
                    TableRowHeight tableRowHeight30 = new TableRowHeight() { Val = (UInt32Value)242U };

                    tableRowProperties30.AppendChild(tableRowHeight30);

                    TableCell tableCell30 = new TableCell();

                    TableCellProperties tableCellProperties30 = new TableCellProperties();
                    TableCellWidth tableCellWidth30 = new TableCellWidth()
                    {
                        Width = "9576",
                        Type = TableWidthUnitValues.Dxa
                    };
                    GridSpan gridSpan30 = new GridSpan() { Val = 2 };

                    TableCellBorders tableCellBorders30 = new TableCellBorders();
                    TopBorder topBorder30 = new TopBorder() { Val = BorderValues.Nil };
                    LeftBorder leftBorder30 = new LeftBorder() { Val = BorderValues.Nil };
                    BottomBorder bottomBorder30 = new BottomBorder() { Val = BorderValues.Nil };
                    RightBorder rightBorder30 = new RightBorder() { Val = BorderValues.Nil };

                    tableCellBorders30.AppendChild(topBorder30);
                    tableCellBorders30.AppendChild(leftBorder30);
                    tableCellBorders30.AppendChild(bottomBorder30);
                    tableCellBorders30.AppendChild(rightBorder30);
                    TableCellVerticalAlignment tableCellVerticalAlignment30 = new TableCellVerticalAlignment()
                    {
                        Val = TableVerticalAlignmentValues.Center
                    };

                    tableCellProperties30.AppendChild(tableCellWidth30);
                    tableCellProperties30.AppendChild(gridSpan30);
                    tableCellProperties30.AppendChild(tableCellBorders30);
                    tableCellProperties30.AppendChild(tableCellVerticalAlignment30);

                    Paragraph paragraph30 = new Paragraph()
                    {
                        RsidParagraphMarkRevision = "00EC01A4",
                        RsidParagraphAddition = "00EC01A4",
                        RsidParagraphProperties = "00EC01A4",
                        RsidRunAdditionDefault = "00EC01A4"
                    };

                    ParagraphProperties paragraphProperties30 = new ParagraphProperties();

                    ParagraphMarkRunProperties paragraphMarkRunProperties30 = new ParagraphMarkRunProperties();
                    Bold bold8 = new Bold();

                    paragraphMarkRunProperties30.AppendChild(bold8);

                    paragraphProperties30.AppendChild(paragraphMarkRunProperties30);

                    Run runTabla = new Run() { RsidRunProperties = "00EC01A4" };

                    RunProperties runPropertiesTabla = new RunProperties();
                    Bold bold = new Bold();

                    runPropertiesTabla.AppendChild(bold);
                    runPropertiesTabla.AppendChild(new FontSize() { Val = "28" });
                    runPropertiesTabla.AppendChild(new FontSizeComplexScript() { Val = "28" });
                    Text textTabla = new Text();
                    textTabla.Text = "Tabla Completa de Medidas";

                    runTabla.AppendChild(runPropertiesTabla);
                    runTabla.AppendChild(textTabla);

                    paragraph30.AppendChild(paragraphProperties30);
                    paragraph30.AppendChild(runTabla);

                    tableCell30.AppendChild(tableCellProperties30);
                    tableCell30.AppendChild(paragraph30);

                    tableRow30.AppendChild(tableRowProperties30);
                    tableRow30.AppendChild(tableCell30);

                    table1.AppendChild(tableRow30);

                    //tabla de medidas
                    var tableRow31 = CompleteMeasuresTable(report);
                    table1.AppendChild(tableRow31);

                }


                Paragraph paragraph29 = new Paragraph()
                    {
                        RsidParagraphMarkRevision = "004D2B75",
                        RsidParagraphAddition = "00E11155",
                        RsidRunAdditionDefault = "00B86685"
                    };

                ParagraphProperties paragraphProperties13 = new ParagraphProperties();

                ParagraphMarkRunProperties paragraphMarkRunProperties13 = new ParagraphMarkRunProperties();

                paragraphProperties13.AppendChild(paragraphMarkRunProperties13);

                paragraph29.AppendChild(paragraphProperties13);

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

                sectionProperties1.AppendChild(coverHeaderReference);
                sectionProperties1.AppendChild(headerReference1);
                //sectionProperties1.AppendChild(footerReference1);
                sectionProperties1.AppendChild(pageSize1);
                sectionProperties1.AppendChild(pageMargin1);
                sectionProperties1.AppendChild(columns1);
                sectionProperties1.AppendChild(titlepage1);
                sectionProperties1.AppendChild(docGrid1);

                body1.AppendChild(table1);
                body1.AppendChild(paragraph29);
                body1.AppendChild(sectionProperties1);

                document1.AppendChild(body1);


                mainDocumentPart1.Document = document1;
                HeaderPart coverHeader = mainDocumentPart1.AddNewPart<HeaderPart>("rId8");
                HeaderPart headerPart1 = mainDocumentPart1.AddNewPart<HeaderPart>("rId7");


                GenerateCoverHeader(coverHeader);
                GenerateHeader(headerPart1, report.Patient);


                mainDocumentPart1.Document.Save();
            }
        }

        private TableRow CompleteMeasuresTable(Report report)
        {

            TableRow tableRow12 = new TableRow() { RsidTableRowMarkRevision = "00653833", RsidTableRowAddition = "004D2B75", RsidTableRowProperties = "00EC01A4" };

            TableRowProperties tableRowProperties3 = new TableRowProperties();
            TableRowHeight tableRowHeight3 = new TableRowHeight() { Val = (UInt32Value)1383U };

            tableRowProperties3.AppendChild(tableRowHeight3);

            TableCell tableCell20 = new TableCell();

            TableCellProperties tableCellProperties20 = new TableCellProperties();
            TableCellWidth tableCellWidth20 = new TableCellWidth() { Width = "9576", Type = TableWidthUnitValues.Dxa };
            GridSpan gridSpan4 = new GridSpan() { Val = 2 };

            TableCellBorders tableCellBorders20 = new TableCellBorders();
            TopBorder topBorder19 = new TopBorder() { Val = BorderValues.Nil };
            LeftBorder leftBorder19 = new LeftBorder() { Val = BorderValues.Nil };
            BottomBorder bottomBorder19 = new BottomBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };
            RightBorder rightBorder19 = new RightBorder() { Val = BorderValues.Nil };

            tableCellBorders20.AppendChild(topBorder19);
            tableCellBorders20.AppendChild(leftBorder19);
            tableCellBorders20.AppendChild(bottomBorder19);
            tableCellBorders20.AppendChild(rightBorder19);
            TableCellVerticalAlignment tableCellVerticalAlignment3 = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };

            tableCellProperties20.AppendChild(tableCellWidth20);
            tableCellProperties20.AppendChild(gridSpan4);
            tableCellProperties20.AppendChild(tableCellBorders20);
            tableCellProperties20.AppendChild(tableCellVerticalAlignment3);

            Paragraph paragraph25 = new Paragraph() { RsidParagraphAddition = "004D2B75", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "004D2B75" };

            ParagraphProperties paragraphProperties9 = new ParagraphProperties();
            Justification justification2 = new Justification() { Val = JustificationValues.Center };
            
            ParagraphMarkRunProperties paragraphMarkRunProperties9 = new ParagraphMarkRunProperties();

            paragraphProperties9.AppendChild(justification2);
            paragraphProperties9.AppendChild(paragraphMarkRunProperties9);

            paragraph25.AppendChild(paragraphProperties9);

            Table table2 = new Table();

            TableProperties tableProperties2 = new TableProperties();
            TableStyle tableStyle2 = new TableStyle() { Val = "Tablaconcuadrcula" };
            TableWidth tableWidth2 = new TableWidth() { Width = "0", Type = TableWidthUnitValues.Auto };
            TableLook tableLook2 = new TableLook() { Val = "04A0" };

            tableProperties2.AppendChild(tableStyle2);
            tableProperties2.AppendChild(tableWidth2);
            tableProperties2.AppendChild(tableLook2);

            TableGrid tableGrid2 = new TableGrid();
            GridColumn gridColumn1 = new GridColumn() { Width = "2336" };
            GridColumn gridColumn2 = new GridColumn() { Width = "2336" };
            GridColumn gridColumn3 = new GridColumn() { Width = "2336" };
            GridColumn gridColumn4 = new GridColumn() { Width = "2337" };
            GridColumn gridColumn5 = new GridColumn() { Width = "2337" };
            GridColumn gridColumn6 = new GridColumn() { Width = "2337" };

            tableGrid2.AppendChild(gridColumn1);
            tableGrid2.AppendChild(gridColumn2);
            tableGrid2.AppendChild(gridColumn3);
            tableGrid2.AppendChild(gridColumn4);
            tableGrid2.AppendChild(gridColumn5);
            tableGrid2.AppendChild(gridColumn6);

            TableRow tableRow13 = new TableRow() { RsidTableRowAddition = "00A6498A", RsidTableRowProperties = "009C2A39" };

            //----------
            TableCell tableCell21 = new TableCell();

            TableCellProperties tableCellProperties21 = new TableCellProperties();
            TableCellWidth tableCellWidth21 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };
            Shading shading0 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "BFBFBF", ThemeFill = ThemeColorValues.Background1, ThemeFillShade = "BF" };

            tableCellProperties21.AppendChild(tableCellWidth21);
            tableCellProperties21.AppendChild(shading0);

            Paragraph paragraph26 = new Paragraph() { RsidParagraphMarkRevision = "00A6498A", RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "00A6498A" };

            ParagraphProperties paragraphProperties10 = new ParagraphProperties();
            
            ParagraphMarkRunProperties paragraphMarkRunProperties10 = new ParagraphMarkRunProperties();

            paragraphMarkRunProperties10.AppendChild(new Bold());
            paragraphProperties10.AppendChild(new Justification() { Val = JustificationValues.Center });
            paragraphProperties10.AppendChild(paragraphMarkRunProperties10);

            Run run34 = new Run() { RsidRunProperties = "00A6498A" };

            RunProperties runProperties10 = new RunProperties();

            runProperties10.AppendChild(new Bold());
            Text text34 = new Text();
            text34.Text = "Fecha";

            run34.AppendChild(runProperties10);
            run34.AppendChild(text34);

            paragraph26.AppendChild(paragraphProperties10);
            paragraph26.AppendChild(run34);

            tableCell21.AppendChild(tableCellProperties21);
            tableCell21.AppendChild(paragraph26);

            //----------

            TableCell tableCell22 = new TableCell();

            TableCellProperties tableCellProperties22 = new TableCellProperties();
            TableCellWidth tableCellWidth22 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };
            Shading shading1 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "BFBFBF", ThemeFill = ThemeColorValues.Background1, ThemeFillShade = "BF" };

            tableCellProperties22.AppendChild(tableCellWidth22);
            tableCellProperties22.AppendChild(shading1);

            Paragraph paragraph27 = new Paragraph() { RsidParagraphMarkRevision = "00A6498A", RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "00A6498A" };

            ParagraphProperties paragraphProperties11 = new ParagraphProperties();

            ParagraphMarkRunProperties paragraphMarkRunProperties11 = new ParagraphMarkRunProperties();

            paragraphMarkRunProperties11.AppendChild(new Bold());
            paragraphProperties11.AppendChild(new Justification() { Val = JustificationValues.Center });
            paragraphProperties11.AppendChild(paragraphMarkRunProperties11);

            Run run35 = new Run() { RsidRunProperties = "00A6498A" };

            RunProperties runProperties11 = new RunProperties();

            runProperties11.AppendChild(new Bold());
            Text text35 = new Text();
            text35.Text = "Hora";

            run35.AppendChild(runProperties11);
            run35.AppendChild(text35);

            paragraph27.AppendChild(paragraphProperties11);
            paragraph27.AppendChild(run35);

            tableCell22.AppendChild(tableCellProperties22);
            tableCell22.AppendChild(paragraph27);

            //----------

            TableCell tableCell23 = new TableCell();

            TableCellProperties tableCellProperties23 = new TableCellProperties();
            TableCellWidth tableCellWidth23 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };
            Shading shading2 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "BFBFBF", ThemeFill = ThemeColorValues.Background1, ThemeFillShade = "BF" };

            tableCellProperties23.AppendChild(tableCellWidth23);
            tableCellProperties23.AppendChild(shading2);

            Paragraph paragraph28 = new Paragraph() { RsidParagraphMarkRevision = "00A6498A", RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "00A6498A" };

            ParagraphProperties paragraphProperties12 = new ParagraphProperties();

            ParagraphMarkRunProperties paragraphMarkRunProperties12 = new ParagraphMarkRunProperties();

            paragraphMarkRunProperties12.AppendChild(new Bold());

            paragraphProperties12.AppendChild(new Justification() { Val = JustificationValues.Center });
            paragraphProperties12.AppendChild(paragraphMarkRunProperties12);

            Run run36 = new Run() { RsidRunProperties = "00A6498A" };

            RunProperties runProperties12 = new RunProperties();

            runProperties12.AppendChild(new Bold());
            Text text36 = new Text();
            text36.Text = "Presion arterial sistolica";

            run36.AppendChild(runProperties12);
            run36.AppendChild(text36);

            paragraph28.AppendChild(paragraphProperties12);
            paragraph28.AppendChild(run36);

            tableCell23.AppendChild(tableCellProperties23);
            tableCell23.AppendChild(paragraph28);

            //----------

            TableCell tableCell24 = new TableCell();

            TableCellProperties tableCellProperties24 = new TableCellProperties();
            TableCellWidth tableCellWidth24 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };
            Shading shading3 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "BFBFBF", ThemeFill = ThemeColorValues.Background1, ThemeFillShade = "BF" };

            tableCellProperties24.AppendChild(tableCellWidth24);
            tableCellProperties24.AppendChild(shading3);

            Paragraph paragraph29 = new Paragraph() { RsidParagraphMarkRevision = "00A6498A", RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "00A6498A" };

            ParagraphProperties paragraphProperties13 = new ParagraphProperties();

            ParagraphMarkRunProperties paragraphMarkRunProperties13 = new ParagraphMarkRunProperties();

            paragraphMarkRunProperties13.AppendChild(new Bold());

            paragraphProperties13.AppendChild(new Justification() { Val = JustificationValues.Center });
            paragraphProperties13.AppendChild(paragraphMarkRunProperties13);

            Run run37 = new Run() { RsidRunProperties = "00A6498A" };

            RunProperties runProperties13 = new RunProperties();

            runProperties13.AppendChild(new Bold());
            Text text37 = new Text();
            text37.Text = "Presion arterial diastolica";

            run37.AppendChild(runProperties13);
            run37.AppendChild(text37);

            paragraph29.AppendChild(paragraphProperties13);
            paragraph29.AppendChild(run37);

            tableCell24.AppendChild(tableCellProperties24);
            tableCell24.AppendChild(paragraph29);

            //----------

            TableCell tableCell25 = new TableCell();

            TableCellProperties tableCellProperties25 = new TableCellProperties();
            TableCellWidth tableCellWidth25 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };
            Shading shading4 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "BFBFBF", ThemeFill = ThemeColorValues.Background1, ThemeFillShade = "BF" };

            tableCellProperties25.AppendChild(tableCellWidth25);
            tableCellProperties25.AppendChild(shading4);

            Paragraph paragraph30 = new Paragraph() { RsidParagraphMarkRevision = "00A6498A", RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "00A6498A" };

            ParagraphProperties paragraphProperties14 = new ParagraphProperties();

            ParagraphMarkRunProperties paragraphMarkRunProperties14 = new ParagraphMarkRunProperties();

            paragraphMarkRunProperties14.AppendChild(new Bold());

            paragraphProperties14.AppendChild(new Justification() { Val = JustificationValues.Center });
            paragraphProperties14.AppendChild(paragraphMarkRunProperties14);

            Run run38 = new Run() { RsidRunProperties = "00A6498A" };

            RunProperties runProperties14 = new RunProperties();

            runProperties14.AppendChild(new Bold());
            Text text38 = new Text();
            text38.Text = "Presion arterial media";

            run38.AppendChild(runProperties14);
            run38.AppendChild(text38);

            paragraph30.AppendChild(paragraphProperties14);
            paragraph30.AppendChild(run38);

            tableCell25.AppendChild(tableCellProperties25);
            tableCell25.AppendChild(paragraph30);

            //----------

            TableCell tableCell26 = new TableCell();

            TableCellProperties tableCellProperties26 = new TableCellProperties();
            TableCellWidth tableCellWidth26 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };
            Shading shading5 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "BFBFBF", ThemeFill = ThemeColorValues.Background1, ThemeFillShade = "BF" };

            tableCellProperties26.AppendChild(tableCellWidth26);
            tableCellProperties26.AppendChild(shading5);

            Paragraph paragraph31 = new Paragraph() { RsidParagraphMarkRevision = "00A6498A", RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "00A6498A" };

            ParagraphProperties paragraphProperties15 = new ParagraphProperties();

            ParagraphMarkRunProperties paragraphMarkRunProperties15 = new ParagraphMarkRunProperties();

            paragraphMarkRunProperties15.AppendChild(new Bold());

            paragraphProperties15.AppendChild(new Justification() { Val = JustificationValues.Center });
            paragraphProperties15.AppendChild(paragraphMarkRunProperties15);

            Run run39 = new Run() { RsidRunProperties = "00A6498A" };

            RunProperties runProperties15 = new RunProperties();

            runProperties15.AppendChild(new Bold());
            Text text39 = new Text();
            text39.Text = "Frecuencia cardiaca";

            run39.AppendChild(runProperties15);
            run39.AppendChild(text39);

            paragraph31.AppendChild(paragraphProperties15);
            paragraph31.AppendChild(run39);

            tableCell26.AppendChild(tableCellProperties26);
            tableCell26.AppendChild(paragraph31);

            //----------
            
            tableRow13.AppendChild(tableCell21);
            tableRow13.AppendChild(tableCell22);
            tableRow13.AppendChild(tableCell23);
            tableRow13.AppendChild(tableCell24);
            tableRow13.AppendChild(tableCell25);
            tableRow13.AppendChild(tableCell26);

            table2.AppendChild(tableProperties2);
            table2.AppendChild(tableGrid2);
            table2.AppendChild(tableRow13);

            foreach (Measurement measure in report.Measures)
            {
                TableRow tableRow14 = new TableRow() { RsidTableRowAddition = "00A6498A", RsidTableRowProperties = "009C2A39" };

                //----------
                TableCell tableCell = new TableCell();

                TableCellProperties tableCellProperties = new TableCellProperties();
                TableCellWidth tableCellWidth = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

                tableCellProperties21.AppendChild(tableCellWidth);

                Paragraph paragraph = new Paragraph() { RsidParagraphMarkRevision = "00A6498A", RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "00A6498A" };

                ParagraphProperties paragraphProperties = new ParagraphProperties();

                ParagraphMarkRunProperties paragraphMarkRunProperties = new ParagraphMarkRunProperties();

                paragraphProperties.AppendChild(new Justification() { Val = JustificationValues.Center });
                paragraphProperties.AppendChild(paragraphMarkRunProperties);

                Run run = new Run() { RsidRunProperties = "00A6498A" };

                RunProperties runProperties = new RunProperties();

                //FECHA
                Text text = new Text();
                text.Text = measure.Time.Value.ToString(ConfigurationManager.AppSettings["ShortDateString"]);

                run.AppendChild(runProperties);
                run.AppendChild(text);

                paragraph.AppendChild(paragraphProperties);
                paragraph.AppendChild(run);

                tableCell.AppendChild(tableCellProperties);
                tableCell.AppendChild(paragraph);

                //----------

                TableCell tableCellTime = new TableCell();

                TableCellProperties tableCellPropertiesTime = new TableCellProperties();
                TableCellWidth tableCellWidthTime = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

                tableCellPropertiesTime.AppendChild(tableCellWidthTime);

                Paragraph paragraphTime = new Paragraph() { RsidParagraphMarkRevision = "00A6498A", RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "00A6498A" };

                ParagraphProperties paragraphPropertiesTime = new ParagraphProperties();

                ParagraphMarkRunProperties paragraphMarkRunPropertiesTime = new ParagraphMarkRunProperties();

                paragraphPropertiesTime.AppendChild(new Justification() { Val = JustificationValues.Center });
                paragraphPropertiesTime.AppendChild(paragraphMarkRunPropertiesTime);

                Run runTime = new Run() { RsidRunProperties = "00A6498A" };

                RunProperties runPropertiesTime = new RunProperties();

                //HORA
                Text textTime = new Text();
                textTime.Text = measure.Time.Value.ToString(ConfigurationManager.AppSettings["ShortTimeString"]);

                runTime.AppendChild(runPropertiesTime);
                runTime.AppendChild(textTime);

                paragraphTime.AppendChild(paragraphPropertiesTime);
                paragraphTime.AppendChild(runTime);

                tableCellTime.AppendChild(tableCellPropertiesTime);
                tableCellTime.AppendChild(paragraphTime);

                //----------

                TableCell tableCellSys = new TableCell();

                TableCellProperties tableCellPropertiesSys = new TableCellProperties();
                TableCellWidth tableCellWidthSys = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

                tableCellPropertiesSys.AppendChild(tableCellWidthSys);

                Paragraph paragraphSys = new Paragraph() { RsidParagraphMarkRevision = "00A6498A", RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "00A6498A" };

                ParagraphProperties paragraphPropertiesSys = new ParagraphProperties();

                ParagraphMarkRunProperties paragraphMarkRunPropertiesSys = new ParagraphMarkRunProperties();

                paragraphPropertiesSys.AppendChild(new Justification() { Val = JustificationValues.Center });
                paragraphPropertiesSys.AppendChild(paragraphMarkRunPropertiesSys);

                Run runSys = new Run() { RsidRunProperties = "00A6498A" };

                RunProperties runPropertiesSys = new RunProperties();

                //"Presion arterial sistolica"
                Text textSys = new Text();
                textSys.Text = measure.Systolic.Value.ToString();

                runSys.AppendChild(runPropertiesSys);
                runSys.AppendChild(textSys);

                paragraphSys.AppendChild(paragraphPropertiesSys);
                paragraphSys.AppendChild(runSys);

                tableCellSys.AppendChild(tableCellPropertiesSys);
                tableCellSys.AppendChild(paragraphSys);

                //----------

                TableCell tableCellDias = new TableCell();

                TableCellProperties tableCellPropertiesDias = new TableCellProperties();
                TableCellWidth tableCellWidthDias = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };

                tableCellPropertiesDias.AppendChild(tableCellWidthDias);

                Paragraph paragraphDias = new Paragraph() { RsidParagraphMarkRevision = "00A6498A", RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "00A6498A" };

                ParagraphProperties paragraphPropertiesDias = new ParagraphProperties();

                ParagraphMarkRunProperties paragraphMarkRunPropertiesDias = new ParagraphMarkRunProperties();

                paragraphPropertiesDias.AppendChild(new Justification() { Val = JustificationValues.Center });
                paragraphPropertiesDias.AppendChild(paragraphMarkRunPropertiesDias);

                Run runDias = new Run() { RsidRunProperties = "00A6498A" };

                RunProperties runPropertiesDias = new RunProperties();

                //"Presion arterial diastolica"
                Text textDias = new Text();
                textDias.Text = measure.Diastolic.Value.ToString();

                runDias.AppendChild(runPropertiesDias);
                runDias.AppendChild(textDias);

                paragraphDias.AppendChild(paragraphPropertiesDias);
                paragraphDias.AppendChild(runDias);

                tableCellDias.AppendChild(tableCellPropertiesDias);
                tableCellDias.AppendChild(paragraphDias);

                //----------

                TableCell tableCellMid = new TableCell();

                TableCellProperties tableCellPropertiesMid = new TableCellProperties();
                TableCellWidth tableCellWidthMid = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };

                tableCellPropertiesMid.AppendChild(tableCellWidthMid);

                Paragraph paragraphMid = new Paragraph() { RsidParagraphMarkRevision = "00A6498A", RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "00A6498A" };

                ParagraphProperties paragraphPropertiesMid = new ParagraphProperties();

                ParagraphMarkRunProperties paragraphMarkRunPropertiesMid = new ParagraphMarkRunProperties();

                paragraphPropertiesMid.AppendChild(new Justification() { Val = JustificationValues.Center });
                paragraphPropertiesMid.AppendChild(paragraphMarkRunPropertiesMid);

                Run runMid = new Run() { RsidRunProperties = "00A6498A" };

                RunProperties runPropertiesMid = new RunProperties();

                //"Presion arterial media"
                Text textMid = new Text();
                textMid.Text = measure.Middle.Value.ToString();

                runMid.AppendChild(runPropertiesMid);
                runMid.AppendChild(textMid);

                paragraphMid.AppendChild(paragraphPropertiesMid);
                paragraphMid.AppendChild(runMid);

                tableCellMid.AppendChild(tableCellPropertiesMid);
                tableCellMid.AppendChild(paragraphMid);

                //----------

                TableCell tableCellFc = new TableCell();

                TableCellProperties tableCellPropertiesFc = new TableCellProperties();
                TableCellWidth tableCellWidthFc = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };

                tableCellPropertiesFc.AppendChild(tableCellWidthFc);

                Paragraph paragraphFc = new Paragraph() { RsidParagraphMarkRevision = "00A6498A", RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "00A6498A" };

                ParagraphProperties paragraphPropertiesFc = new ParagraphProperties();

                ParagraphMarkRunProperties paragraphMarkRunPropertiesFc = new ParagraphMarkRunProperties();

                paragraphPropertiesFc.AppendChild(new Justification() { Val = JustificationValues.Center });
                paragraphPropertiesFc.AppendChild(paragraphMarkRunPropertiesFc);

                Run runFc = new Run() { RsidRunProperties = "00A6498A" };

                RunProperties runPropertiesFc = new RunProperties();

                //"Frecuencia cardiaca"
                Text textFc = new Text();
                textFc.Text = measure.HeartRate.Value.ToString();

                runFc.AppendChild(runPropertiesFc);
                runFc.AppendChild(textFc);

                paragraphFc.AppendChild(paragraphPropertiesFc);
                paragraphFc.AppendChild(runFc);

                tableCellFc.AppendChild(tableCellPropertiesFc);
                tableCellFc.AppendChild(paragraphFc);

                tableRow14.AppendChild(tableCell);
                tableRow14.AppendChild(tableCellTime);
                tableRow14.AppendChild(tableCellSys);
                tableRow14.AppendChild(tableCellDias);
                tableRow14.AppendChild(tableCellMid);
                tableRow14.AppendChild(tableCellFc);

                table2.AppendChild(tableRow14);
                
            }


            Paragraph paragraph115 = new Paragraph() { RsidParagraphMarkRevision = "00653833", RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "009C2A39", RsidRunAdditionDefault = "00653833" };

            Run run361 = new Run() { RsidRunProperties = "00653833" };
            Text text361 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            run361.AppendChild(text361);
            paragraph115.AppendChild(run361);

            tableCell20.AppendChild(tableCellProperties20);
            tableCell20.AppendChild(paragraph25);
            tableCell20.AppendChild(table2);
            tableCell20.AppendChild(paragraph115);

            tableRow12.AppendChild(tableRowProperties3);
            tableRow12.AppendChild(tableCell20);

            return tableRow12;
        }

        private TableRow MeasuresSummary(Report report)
        {
            TableRow tableRow12 = new TableRow() { RsidTableRowMarkRevision = "00653833", RsidTableRowAddition = "004D2B75", RsidTableRowProperties = "00EC01A4" };

            TableRowProperties tableRowProperties3 = new TableRowProperties();
            TableRowHeight tableRowHeight3 = new TableRowHeight() { Val = (UInt32Value)1383U };

            tableRowProperties3.AppendChild(tableRowHeight3);

            TableCell tableCell20 = new TableCell();

            TableCellProperties tableCellProperties20 = new TableCellProperties();
            TableCellWidth tableCellWidth20 = new TableCellWidth() { Width = "9576", Type = TableWidthUnitValues.Dxa };
            GridSpan gridSpan4 = new GridSpan() { Val = 2 };

            TableCellBorders tableCellBorders20 = new TableCellBorders();
            TopBorder topBorder17 = new TopBorder() { Val = BorderValues.Nil };
            LeftBorder leftBorder19 = new LeftBorder() { Val = BorderValues.Nil };
            BottomBorder bottomBorder19 = new BottomBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };
            RightBorder rightBorder19 = new RightBorder() { Val = BorderValues.Nil };

            tableCellBorders20.AppendChild(topBorder17);
            tableCellBorders20.AppendChild(leftBorder19);
            tableCellBorders20.AppendChild(bottomBorder19);
            tableCellBorders20.AppendChild(rightBorder19);
            TableCellVerticalAlignment tableCellVerticalAlignment3 = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };

            tableCellProperties20.AppendChild(tableCellWidth20);
            tableCellProperties20.AppendChild(gridSpan4);
            tableCellProperties20.AppendChild(tableCellBorders20);
            tableCellProperties20.AppendChild(tableCellVerticalAlignment3);

            Paragraph paragraph25 = new Paragraph() { RsidParagraphAddition = "004D2B75", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "004D2B75" };

            ParagraphProperties paragraphProperties9 = new ParagraphProperties();
            Justification justification2 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties9 = new ParagraphMarkRunProperties();

            paragraphProperties9.AppendChild(justification2);
            paragraphProperties9.AppendChild(paragraphMarkRunProperties9);

            paragraph25.AppendChild(paragraphProperties9);

            Table table2 = new Table();

            TableProperties tableProperties2 = new TableProperties();
            TableStyle tableStyle2 = new TableStyle() { Val = "Tablaconcuadrcula" };
            TableWidth tableWidth2 = new TableWidth() { Width = "0", Type = TableWidthUnitValues.Auto };
            TableLook tableLook2 = new TableLook() { Val = "04A0" };

            tableProperties2.AppendChild(tableStyle2);
            tableProperties2.AppendChild(tableWidth2);
            tableProperties2.AppendChild(tableLook2);

            TableGrid tableGrid2 = new TableGrid();
            GridColumn gridColumn3 = new GridColumn() { Width = "2336" };
            GridColumn gridColumn4 = new GridColumn() { Width = "2336" };
            GridColumn gridColumn5 = new GridColumn() { Width = "2336" };
            GridColumn gridColumn6 = new GridColumn() { Width = "2337" };

            tableGrid2.AppendChild(gridColumn3);
            tableGrid2.AppendChild(gridColumn4);
            tableGrid2.AppendChild(gridColumn5);
            tableGrid2.AppendChild(gridColumn6);

            TableRow tableRow13 = new TableRow() { RsidTableRowAddition = "00A6498A", RsidTableRowProperties = "009C2A39" };

            TableCell tableCell21 = new TableCell();

            TableCellProperties tableCellProperties21 = new TableCellProperties();
            TableCellWidth tableCellWidth21 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            TableCellBorders tableCellBorders21 = new TableCellBorders();
            TopBorder topBorder18 = new TopBorder() { Val = BorderValues.Nil };
            LeftBorder leftBorder20 = new LeftBorder() { Val = BorderValues.Nil };

            tableCellBorders21.AppendChild(topBorder18);
            tableCellBorders21.AppendChild(leftBorder20);

            tableCellProperties21.AppendChild(tableCellWidth21);
            tableCellProperties21.AppendChild(tableCellBorders21);

            Paragraph paragraph26 = new Paragraph() { RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "00A6498A" };

            ParagraphProperties paragraphProperties10 = new ParagraphProperties();
            Justification justification3 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties10 = new ParagraphMarkRunProperties();

            paragraphProperties10.AppendChild(justification3);
            paragraphProperties10.AppendChild(paragraphMarkRunProperties10);

            paragraph26.AppendChild(paragraphProperties10);

            tableCell21.AppendChild(tableCellProperties21);
            tableCell21.AppendChild(paragraph26);

            TableCell tableCell22 = new TableCell();

            TableCellProperties tableCellProperties22 = new TableCellProperties();
            TableCellWidth tableCellWidth22 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };
            Shading shading1 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "BFBFBF", ThemeFill = ThemeColorValues.Background1, ThemeFillShade = "BF" };

            tableCellProperties22.AppendChild(tableCellWidth22);
            tableCellProperties22.AppendChild(shading1);

            Paragraph paragraph27 = new Paragraph() { RsidParagraphMarkRevision = "00A6498A", RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "00A6498A" };

            ParagraphProperties paragraphProperties11 = new ParagraphProperties();
            Justification justification4 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties11 = new ParagraphMarkRunProperties();
            Bold bold10 = new Bold();

            paragraphMarkRunProperties11.AppendChild(bold10);
            paragraphProperties11.AppendChild(justification4);
            paragraphProperties11.AppendChild(paragraphMarkRunProperties11);

            Run run35 = new Run() { RsidRunProperties = "00A6498A" };

            RunProperties runProperties11 = new RunProperties();
            Bold bold11 = new Bold();

            runProperties11.AppendChild(bold11);
            Text text35 = new Text();
            text35.Text = "24 horas";

            run35.AppendChild(runProperties11);
            run35.AppendChild(text35);

            paragraph27.AppendChild(paragraphProperties11);
            paragraph27.AppendChild(run35);

            tableCell22.AppendChild(tableCellProperties22);
            tableCell22.AppendChild(paragraph27);

            TableCell tableCell23 = new TableCell();

            TableCellProperties tableCellProperties23 = new TableCellProperties();
            TableCellWidth tableCellWidth23 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };
            Shading shading2 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "BFBFBF", ThemeFill = ThemeColorValues.Background1, ThemeFillShade = "BF" };

            tableCellProperties23.AppendChild(tableCellWidth23);
            tableCellProperties23.AppendChild(shading2);

            Paragraph paragraph28 = new Paragraph() { RsidParagraphMarkRevision = "00A6498A", RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "00A6498A" };

            ParagraphProperties paragraphProperties12 = new ParagraphProperties();
            Justification justification5 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties12 = new ParagraphMarkRunProperties();
            Bold bold12 = new Bold();

            paragraphMarkRunProperties12.AppendChild(bold12);

            paragraphProperties12.AppendChild(justification5);
            paragraphProperties12.AppendChild(paragraphMarkRunProperties12);

            Run run36 = new Run() { RsidRunProperties = "00A6498A" };

            RunProperties runProperties12 = new RunProperties();
            Bold bold13 = new Bold();

            runProperties12.AppendChild(bold13);
            Text text36 = new Text();
            text36.Text = "Día";

            run36.AppendChild(runProperties12);
            run36.AppendChild(text36);

            paragraph28.AppendChild(paragraphProperties12);
            paragraph28.AppendChild(run36);

            tableCell23.AppendChild(tableCellProperties23);
            tableCell23.AppendChild(paragraph28);

            TableCell tableCell24 = new TableCell();

            TableCellProperties tableCellProperties24 = new TableCellProperties();
            TableCellWidth tableCellWidth24 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };
            Shading shading3 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "BFBFBF", ThemeFill = ThemeColorValues.Background1, ThemeFillShade = "BF" };

            tableCellProperties24.AppendChild(tableCellWidth24);
            tableCellProperties24.AppendChild(shading3);

            Paragraph paragraph29 = new Paragraph() { RsidParagraphMarkRevision = "00A6498A", RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "00A6498A" };

            ParagraphProperties paragraphProperties13 = new ParagraphProperties();
            Justification justification6 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties13 = new ParagraphMarkRunProperties();
            Bold bold14 = new Bold();

            paragraphMarkRunProperties13.AppendChild(bold14);

            paragraphProperties13.AppendChild(justification6);
            paragraphProperties13.AppendChild(paragraphMarkRunProperties13);

            Run run37 = new Run() { RsidRunProperties = "00A6498A" };

            RunProperties runProperties13 = new RunProperties();
            Bold bold15 = new Bold();

            runProperties13.AppendChild(bold15);
            Text text37 = new Text();
            text37.Text = "Noche";

            run37.AppendChild(runProperties13);
            run37.AppendChild(text37);

            paragraph29.AppendChild(paragraphProperties13);
            paragraph29.AppendChild(run37);

            tableCell24.AppendChild(tableCellProperties24);
            tableCell24.AppendChild(paragraph29);

            tableRow13.AppendChild(tableCell21);
            tableRow13.AppendChild(tableCell22);
            tableRow13.AppendChild(tableCell23);
            tableRow13.AppendChild(tableCell24);

            TableRow tableRow14 = new TableRow() { RsidTableRowAddition = "00A6498A", RsidTableRowProperties = "00A6498A" };

            TableCell tableCell25 = new TableCell();

            TableCellProperties tableCellProperties25 = new TableCellProperties();
            TableCellWidth tableCellWidth25 = new TableCellWidth() { Width = "9345", Type = TableWidthUnitValues.Dxa };
            GridSpan gridSpan5 = new GridSpan() { Val = 4 };
            Shading shading4 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "BFBFBF", ThemeFill = ThemeColorValues.Background1, ThemeFillShade = "BF" };

            tableCellProperties25.AppendChild(tableCellWidth25);
            tableCellProperties25.AppendChild(gridSpan5);
            tableCellProperties25.AppendChild(shading4);

            Paragraph paragraph30 = new Paragraph() { RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "00A6498A", RsidRunAdditionDefault = "00A6498A" };

            ParagraphProperties paragraphProperties14 = new ParagraphProperties();

            ParagraphMarkRunProperties paragraphMarkRunProperties14 = new ParagraphMarkRunProperties();

            paragraphProperties14.AppendChild(paragraphMarkRunProperties14);

            Run run38 = new Run() { RsidRunProperties = "00A6498A" };

            RunProperties runProperties14 = new RunProperties();
            Bold bold16 = new Bold();

            runProperties14.AppendChild(bold16);
            Text text38 = new Text();
            text38.Text = "Mediciones";

            run38.AppendChild(runProperties14);
            run38.AppendChild(text38);

            paragraph30.AppendChild(paragraphProperties14);
            paragraph30.AppendChild(run38);

            tableCell25.AppendChild(tableCellProperties25);
            tableCell25.AppendChild(paragraph30);

            tableRow14.AppendChild(tableCell25);

            TableRow tableRow15 = new TableRow() { RsidTableRowAddition = "00A6498A", RsidTableRowProperties = "00A6498A" };

            TableCell tableCell26 = new TableCell();

            TableCellProperties tableCellProperties26 = new TableCellProperties();
            TableCellWidth tableCellWidth26 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties26.AppendChild(tableCellWidth26);

            Paragraph paragraph31 = new Paragraph() { RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "00A6498A" };

            ParagraphProperties paragraphProperties15 = new ParagraphProperties();
            Justification justification7 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties15 = new ParagraphMarkRunProperties();

            paragraphProperties15.AppendChild(justification7);
            paragraphProperties15.AppendChild(paragraphMarkRunProperties15);

            Run run39 = new Run();

            RunProperties runProperties15 = new RunProperties();
            Text text39 = new Text();
            text39.Text = "Total";

            run39.AppendChild(runProperties15);
            run39.AppendChild(text39);

            paragraph31.AppendChild(paragraphProperties15);
            paragraph31.AppendChild(run39);

            tableCell26.AppendChild(tableCellProperties26);
            tableCell26.AppendChild(paragraph31);

            TableCell tableCell27 = new TableCell();

            TableCellProperties tableCellProperties27 = new TableCellProperties();
            TableCellWidth tableCellWidth27 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties27.AppendChild(tableCellWidth27);

            Paragraph paragraph32 = new Paragraph() { RsidParagraphMarkRevision = "00A6498A", RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "00A6498A" };

            ParagraphProperties paragraphProperties16 = new ParagraphProperties();
            Justification justification8 = new Justification() { Val = JustificationValues.Center };

            paragraphProperties16.AppendChild(justification8);

            Run run40 = new Run();

            RunProperties runProperties16 = new RunProperties();
            Text text40 = new Text();
            // Cantidad total de medidas
            var cantMeasureTotal = report.Measures.Count;
            text40.Text = cantMeasureTotal.ToString();

            run40.AppendChild(runProperties16);
            run40.AppendChild(text40);

            paragraph32.AppendChild(paragraphProperties16);
            paragraph32.AppendChild(run40);

            tableCell27.AppendChild(tableCellProperties27);
            tableCell27.AppendChild(paragraph32);

            TableCell tableCell28 = new TableCell();

            TableCellProperties tableCellProperties28 = new TableCellProperties();
            TableCellWidth tableCellWidth28 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties28.AppendChild(tableCellWidth28);

            Paragraph paragraph33 = new Paragraph() { RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "00A6498A" };

            ParagraphProperties paragraphProperties17 = new ParagraphProperties();
            Justification justification9 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties16 = new ParagraphMarkRunProperties();

            paragraphProperties17.AppendChild(justification9);
            paragraphProperties17.AppendChild(paragraphMarkRunProperties16);

            Run run45 = new Run();

            RunProperties runProperties21 = new RunProperties();
            Text text45 = new Text();
            //Cantidad de medidas durante el dia
            var cantMeasureDay = report.Measures.Count(m => !m.Asleep.Value);
            text45.Text = cantMeasureDay.ToString();

            run45.AppendChild(runProperties21);
            run45.AppendChild(text45);

            paragraph33.AppendChild(paragraphProperties17);
            paragraph33.AppendChild(run45);

            tableCell28.AppendChild(tableCellProperties28);
            tableCell28.AppendChild(paragraph33);

            TableCell tableCell29 = new TableCell();

            TableCellProperties tableCellProperties29 = new TableCellProperties();
            TableCellWidth tableCellWidth29 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };

            tableCellProperties29.AppendChild(tableCellWidth29);

            Paragraph paragraph34 = new Paragraph() { RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "00A6498A" };

            ParagraphProperties paragraphProperties18 = new ParagraphProperties();
            Justification justification10 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties17 = new ParagraphMarkRunProperties();

            paragraphProperties18.AppendChild(justification10);
            paragraphProperties18.AppendChild(paragraphMarkRunProperties17);

            Run run50 = new Run();

            RunProperties runProperties26 = new RunProperties();
            Text text50 = new Text();
            //Cantidad medidas durante la noche
            var cantMeasureNight = report.Measures.Count(m => m.Asleep.Value);
            text50.Text = cantMeasureNight.ToString();

            run50.AppendChild(runProperties26);
            run50.AppendChild(text50);

            paragraph34.AppendChild(paragraphProperties18);
            paragraph34.AppendChild(run50);

            tableCell29.AppendChild(tableCellProperties29);
            tableCell29.AppendChild(paragraph34);

            tableRow15.AppendChild(tableCell26);
            tableRow15.AppendChild(tableCell27);
            tableRow15.AppendChild(tableCell28);
            tableRow15.AppendChild(tableCell29);

            TableRow tableRow16 = new TableRow() { RsidTableRowAddition = "00A6498A", RsidTableRowProperties = "00A6498A" };

            TableCell tableCell30 = new TableCell();

            TableCellProperties tableCellProperties30 = new TableCellProperties();
            TableCellWidth tableCellWidth30 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties30.AppendChild(tableCellWidth30);

            Paragraph paragraph35 = new Paragraph() { RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "00A6498A" };

            ParagraphProperties paragraphProperties19 = new ParagraphProperties();
            Justification justification11 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties18 = new ParagraphMarkRunProperties();

            paragraphProperties19.AppendChild(justification11);
            paragraphProperties19.AppendChild(paragraphMarkRunProperties18);

            Run run53 = new Run();

            RunProperties runProperties29 = new RunProperties();
            Text text53 = new Text();
            text53.Text = "Válido";

            run53.AppendChild(runProperties29);
            run53.AppendChild(text53);

            paragraph35.AppendChild(paragraphProperties19);
            paragraph35.AppendChild(run53);

            tableCell30.AppendChild(tableCellProperties30);
            tableCell30.AppendChild(paragraph35);

            TableCell tableCell31 = new TableCell();

            TableCellProperties tableCellProperties31 = new TableCellProperties();
            TableCellWidth tableCellWidth31 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties31.AppendChild(tableCellWidth31);

            Paragraph paragraph36 = new Paragraph() { RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "00A6498A" };

            ParagraphProperties paragraphProperties20 = new ParagraphProperties();
            Justification justification12 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties19 = new ParagraphMarkRunProperties();

            paragraphProperties20.AppendChild(justification12);
            paragraphProperties20.AppendChild(paragraphMarkRunProperties19);

            Run run54 = new Run();

            RunProperties runProperties30 = new RunProperties();
            Text text54 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            //Cantidad total de medidas validas
            var cantValidTot = report.Measures.Count(m => m.Valid);
            text54.Text = cantValidTot.ToString();

            run54.AppendChild(runProperties30);
            run54.AppendChild(text54);

            paragraph36.AppendChild(paragraphProperties20);
            paragraph36.AppendChild(run54);

            tableCell31.AppendChild(tableCellProperties31);
            tableCell31.AppendChild(paragraph36);

            TableCell tableCell32 = new TableCell();

            TableCellProperties tableCellProperties32 = new TableCellProperties();
            TableCellWidth tableCellWidth32 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties32.AppendChild(tableCellWidth32);

            Paragraph paragraph37 = new Paragraph() { RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "00A6498A" };

            ParagraphProperties paragraphProperties21 = new ParagraphProperties();
            Justification justification13 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties20 = new ParagraphMarkRunProperties();

            paragraphProperties21.AppendChild(justification13);
            paragraphProperties21.AppendChild(paragraphMarkRunProperties20);

            Run run57 = new Run();

            RunProperties runProperties33 = new RunProperties();
            Text text57 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            //Cantidad de medidas validas tomadas durante el dia
            var cantValidDay = report.Measures.Count(m => (m.Valid && (bool)(!m.Asleep)));
            text57.Text = cantValidDay.ToString();

            run57.AppendChild(runProperties33);
            run57.AppendChild(text57);

            paragraph37.AppendChild(paragraphProperties21);
            paragraph37.AppendChild(run57);

            tableCell32.AppendChild(tableCellProperties32);
            tableCell32.AppendChild(paragraph37);

            TableCell tableCell33 = new TableCell();

            TableCellProperties tableCellProperties33 = new TableCellProperties();
            TableCellWidth tableCellWidth33 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };

            tableCellProperties33.AppendChild(tableCellWidth33);

            Paragraph paragraph38 = new Paragraph() { RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "00A6498A" };

            ParagraphProperties paragraphProperties22 = new ParagraphProperties();
            Justification justification14 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties21 = new ParagraphMarkRunProperties();

            paragraphProperties22.AppendChild(justification14);
            paragraphProperties22.AppendChild(paragraphMarkRunProperties21);

            Run run60 = new Run();

            RunProperties runProperties36 = new RunProperties();
            Text text60 = new Text();
            //Cantidad de medidas validas tomadas durante la noche
            var cantValidNight = report.Measures.Count(m => m.Valid && m.Asleep.Value);
            text60.Text = cantValidNight.ToString();

            run60.AppendChild(runProperties36);
            run60.AppendChild(text60);

            paragraph38.AppendChild(paragraphProperties22);
            paragraph38.AppendChild(run60);

            tableCell33.AppendChild(tableCellProperties33);
            tableCell33.AppendChild(paragraph38);

            tableRow16.AppendChild(tableCell30);
            tableRow16.AppendChild(tableCell31);
            tableRow16.AppendChild(tableCell32);
            tableRow16.AppendChild(tableCell33);

            TableRow tableRow17 = new TableRow() { RsidTableRowAddition = "00A6498A", RsidTableRowProperties = "00A6498A" };

            TableCell tableCell34 = new TableCell();

            TableCellProperties tableCellProperties34 = new TableCellProperties();
            TableCellWidth tableCellWidth34 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties34.AppendChild(tableCellWidth34);

            Paragraph paragraph39 = new Paragraph() { RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "00A6498A" };

            ParagraphProperties paragraphProperties23 = new ParagraphProperties();
            Justification justification15 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties22 = new ParagraphMarkRunProperties();

            paragraphProperties23.AppendChild(justification15);
            paragraphProperties23.AppendChild(paragraphMarkRunProperties22);

            Run run61 = new Run();

            RunProperties runProperties37 = new RunProperties();
            Text text61 = new Text();
            text61.Text = "% Válido";

            run61.AppendChild(runProperties37);
            run61.AppendChild(text61);

            paragraph39.AppendChild(paragraphProperties23);
            paragraph39.AppendChild(run61);

            tableCell34.AppendChild(tableCellProperties34);
            tableCell34.AppendChild(paragraph39);

            TableCell tableCell35 = new TableCell();

            TableCellProperties tableCellProperties35 = new TableCellProperties();
            TableCellWidth tableCellWidth35 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties35.AppendChild(tableCellWidth35);

            Paragraph paragraph40 = new Paragraph() { RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "00A6498A" };

            ParagraphProperties paragraphProperties24 = new ParagraphProperties();
            Justification justification16 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties23 = new ParagraphMarkRunProperties();

            paragraphProperties24.AppendChild(justification16);
            paragraphProperties24.AppendChild(paragraphMarkRunProperties23);

            Run run62 = new Run();

            RunProperties runProperties38 = new RunProperties();
            Text text62 = new Text();
            //Porcentaje de medidas validas totales
            var percentageTotal = (cantValidTot * 100) / cantMeasureTotal;
            text62.Text = percentageTotal.ToString();

            run62.AppendChild(runProperties38);
            run62.AppendChild(text62);

            paragraph40.AppendChild(paragraphProperties24);
            paragraph40.AppendChild(run62);

            tableCell35.AppendChild(tableCellProperties35);
            tableCell35.AppendChild(paragraph40);

            TableCell tableCell36 = new TableCell();

            TableCellProperties tableCellProperties36 = new TableCellProperties();
            TableCellWidth tableCellWidth36 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties36.AppendChild(tableCellWidth36);

            Paragraph paragraph41 = new Paragraph() { RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "00A6498A" };

            ParagraphProperties paragraphProperties25 = new ParagraphProperties();
            Justification justification17 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties24 = new ParagraphMarkRunProperties();

            paragraphProperties25.AppendChild(justification17);
            paragraphProperties25.AppendChild(paragraphMarkRunProperties24);

            Run run63 = new Run();

            RunProperties runProperties39 = new RunProperties();
            Text text63 = new Text();
            //Porcentage de medidas validas durante el dia
            var percentageDay = (cantValidDay * 100) / cantMeasureDay;
            text63.Text = percentageDay.ToString();

            run63.AppendChild(runProperties39);
            run63.AppendChild(text63);

            paragraph41.AppendChild(paragraphProperties25);
            paragraph41.AppendChild(run63);

            tableCell36.AppendChild(tableCellProperties36);
            tableCell36.AppendChild(paragraph41);

            TableCell tableCell37 = new TableCell();

            TableCellProperties tableCellProperties37 = new TableCellProperties();
            TableCellWidth tableCellWidth37 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };

            tableCellProperties37.AppendChild(tableCellWidth37);

            Paragraph paragraph42 = new Paragraph() { RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "00C204AF" };

            ParagraphProperties paragraphProperties26 = new ParagraphProperties();
            Justification justification18 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties25 = new ParagraphMarkRunProperties();

            paragraphProperties26.AppendChild(justification18);
            paragraphProperties26.AppendChild(paragraphMarkRunProperties25);

            Run run67 = new Run();

            RunProperties runProperties43 = new RunProperties();
            Text text67 = new Text();
            //Porcentaje de medidas validas durante la noche
            var percentageNight = (cantValidNight * 100) / cantMeasureNight;
            text67.Text = percentageNight.ToString();

            run67.AppendChild(runProperties43);
            run67.AppendChild(text67);

            paragraph42.AppendChild(paragraphProperties26);
            paragraph42.AppendChild(run67);

            tableCell37.AppendChild(tableCellProperties37);
            tableCell37.AppendChild(paragraph42);

            tableRow17.AppendChild(tableCell34);
            tableRow17.AppendChild(tableCell35);
            tableRow17.AppendChild(tableCell36);
            tableRow17.AppendChild(tableCell37);

            TableRow tableRow18 = new TableRow() { RsidTableRowAddition = "00C204AF", RsidTableRowProperties = "00C204AF" };

            TableCell tableCell38 = new TableCell();

            TableCellProperties tableCellProperties38 = new TableCellProperties();
            TableCellWidth tableCellWidth38 = new TableCellWidth() { Width = "9345", Type = TableWidthUnitValues.Dxa };
            GridSpan gridSpan6 = new GridSpan() { Val = 4 };
            Shading shading5 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "BFBFBF", ThemeFill = ThemeColorValues.Background1, ThemeFillShade = "BF" };

            tableCellProperties38.AppendChild(tableCellWidth38);
            tableCellProperties38.AppendChild(gridSpan6);
            tableCellProperties38.AppendChild(shading5);

            Paragraph paragraph43 = new Paragraph() { RsidParagraphMarkRevision = "00C204AF", RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "00C204AF", RsidRunAdditionDefault = "00C204AF" };

            ParagraphProperties paragraphProperties27 = new ParagraphProperties();

            ParagraphMarkRunProperties paragraphMarkRunProperties26 = new ParagraphMarkRunProperties();
            Bold bold17 = new Bold();
            paragraphMarkRunProperties26.AppendChild(bold17);

            paragraphProperties27.AppendChild(paragraphMarkRunProperties26);

            Run run68 = new Run() { RsidRunProperties = "00C204AF" };

            RunProperties runProperties44 = new RunProperties();
            Bold bold18 = new Bold();
            runProperties44.AppendChild(bold18);
            Text text68 = new Text();
            text68.Text = "Promedio";

            run68.AppendChild(runProperties44);
            run68.AppendChild(text68);

            paragraph43.AppendChild(paragraphProperties27);
            paragraph43.AppendChild(run68);

            tableCell38.AppendChild(tableCellProperties38);
            tableCell38.AppendChild(paragraph43);

            tableRow18.AppendChild(tableCell38);

            TableRow tableRow19 = new TableRow() { RsidTableRowAddition = "00C204AF", RsidTableRowProperties = "0075392D" };

            TableCell tableCell39 = new TableCell();

            TableCellProperties tableCellProperties39 = new TableCellProperties();
            TableCellWidth tableCellWidth39 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties39.AppendChild(tableCellWidth39);

            Paragraph paragraph44 = new Paragraph() { RsidParagraphMarkRevision = "00C204AF", RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "00C204AF" };

            ParagraphProperties paragraphProperties28 = new ParagraphProperties();
            Justification justification19 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties27 = new ParagraphMarkRunProperties();

            paragraphProperties28.AppendChild(justification19);
            paragraphProperties28.AppendChild(paragraphMarkRunProperties27);

            Run run69 = new Run();

            RunProperties runProperties45 = new RunProperties();
            Text text69 = new Text();
            text69.Text = "Presión arterial sistólica";

            run69.AppendChild(runProperties45);
            run69.AppendChild(text69);

            paragraph44.AppendChild(paragraphProperties28);
            paragraph44.AppendChild(run69);

            tableCell39.AppendChild(tableCellProperties39);
            tableCell39.AppendChild(paragraph44);

            TableCell tableCell40 = new TableCell();

            TableCellProperties tableCellProperties40 = new TableCellProperties();
            TableCellWidth tableCellWidth40 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties40.AppendChild(tableCellWidth40);

            Paragraph paragraph45 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties29 = new ParagraphProperties();
            Justification justification20 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties28 = new ParagraphMarkRunProperties();

            paragraphProperties29.AppendChild(justification20);
            paragraphProperties29.AppendChild(paragraphMarkRunProperties28);

            Run run71 = new Run();

            RunProperties runProperties47 = new RunProperties();
            Text text71 = new Text();
            //Promedio total de sistolica
            text71.Text = report.SystolicTotalAvg.ToString();

            run71.AppendChild(runProperties47);
            run71.AppendChild(text71);

            paragraph45.AppendChild(paragraphProperties29);
            paragraph45.AppendChild(run71);

            tableCell40.AppendChild(tableCellProperties40);
            tableCell40.AppendChild(paragraph45);

            TableCell tableCell41 = new TableCell();

            TableCellProperties tableCellProperties41 = new TableCellProperties();
            TableCellWidth tableCellWidth41 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties41.AppendChild(tableCellWidth41);

            Paragraph paragraph46 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties30 = new ParagraphProperties();
            Justification justification21 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties29 = new ParagraphMarkRunProperties();

            paragraphProperties30.AppendChild(justification21);
            paragraphProperties30.AppendChild(paragraphMarkRunProperties29);

            Run run78 = new Run();

            RunProperties runProperties54 = new RunProperties();
            Text text78 = new Text();
            //Promedio dia sistolica
            text78.Text = report.SystolicDayAvg.ToString();

            run78.AppendChild(runProperties54);
            run78.AppendChild(text78);

            paragraph46.AppendChild(paragraphProperties30);
            paragraph46.AppendChild(run78);

            tableCell41.AppendChild(tableCellProperties41);
            tableCell41.AppendChild(paragraph46);

            TableCell tableCell42 = new TableCell();

            TableCellProperties tableCellProperties42 = new TableCellProperties();
            TableCellWidth tableCellWidth42 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };

            tableCellProperties42.AppendChild(tableCellWidth42);

            Paragraph paragraph47 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties31 = new ParagraphProperties();
            Justification justification22 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties30 = new ParagraphMarkRunProperties();

            paragraphProperties31.AppendChild(justification22);
            paragraphProperties31.AppendChild(paragraphMarkRunProperties30);

            Run run85 = new Run();

            RunProperties runProperties61 = new RunProperties();
            Text text85 = new Text();
            //Promedio noche sistolica
            text85.Text = report.SystolicNightAvg.ToString();

            run85.AppendChild(runProperties61);
            run85.AppendChild(text85);

            paragraph47.AppendChild(paragraphProperties31);
            paragraph47.AppendChild(run85);

            tableCell42.AppendChild(tableCellProperties42);
            tableCell42.AppendChild(paragraph47);

            tableRow19.AppendChild(tableCell39);
            tableRow19.AppendChild(tableCell40);
            tableRow19.AppendChild(tableCell41);
            tableRow19.AppendChild(tableCell42);

            TableRow tableRow20 = new TableRow() { RsidTableRowAddition = "00C204AF", RsidTableRowProperties = "0075392D" };

            TableCell tableCell43 = new TableCell();

            TableCellProperties tableCellProperties43 = new TableCellProperties();
            TableCellWidth tableCellWidth43 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties43.AppendChild(tableCellWidth43);

            Paragraph paragraph48 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "00C204AF" };

            ParagraphProperties paragraphProperties32 = new ParagraphProperties();
            Justification justification23 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties31 = new ParagraphMarkRunProperties();

            paragraphProperties32.AppendChild(justification23);
            paragraphProperties32.AppendChild(paragraphMarkRunProperties31);

            Run run90 = new Run();

            RunProperties runProperties66 = new RunProperties();
            Text text90 = new Text();
            text90.Text = "Presión arterial diastólica";

            run90.AppendChild(runProperties66);
            run90.AppendChild(text90);

            paragraph48.AppendChild(paragraphProperties32);
            paragraph48.AppendChild(run90);

            tableCell43.AppendChild(tableCellProperties43);
            tableCell43.AppendChild(paragraph48);

            TableCell tableCell44 = new TableCell();

            TableCellProperties tableCellProperties44 = new TableCellProperties();
            TableCellWidth tableCellWidth44 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties44.AppendChild(tableCellWidth44);

            Paragraph paragraph49 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties33 = new ParagraphProperties();
            Justification justification24 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties32 = new ParagraphMarkRunProperties();

            paragraphProperties33.AppendChild(justification24);
            paragraphProperties33.AppendChild(paragraphMarkRunProperties32);

            Run run91 = new Run();

            RunProperties runProperties67 = new RunProperties();
            Text text91 = new Text();
            //Promedio del total de medidas diastolicas
            text91.Text = report.DiastolicTotalAvg.ToString();

            run91.AppendChild(runProperties67);
            run91.AppendChild(text91);

            paragraph49.AppendChild(paragraphProperties33);
            paragraph49.AppendChild(run91);

            tableCell44.AppendChild(tableCellProperties44);
            tableCell44.AppendChild(paragraph49);

            TableCell tableCell45 = new TableCell();

            TableCellProperties tableCellProperties45 = new TableCellProperties();
            TableCellWidth tableCellWidth45 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties45.AppendChild(tableCellWidth45);

            Paragraph paragraph50 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties34 = new ParagraphProperties();
            Justification justification25 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties33 = new ParagraphMarkRunProperties();

            paragraphProperties34.AppendChild(justification25);
            paragraphProperties34.AppendChild(paragraphMarkRunProperties33);

            Run run98 = new Run();

            RunProperties runProperties74 = new RunProperties();
            Text text98 = new Text();
            //Promedio dia diastolica
            text98.Text = report.DiastolicDayAvg.ToString();

            run98.AppendChild(runProperties74);
            run98.AppendChild(text98);

            paragraph50.AppendChild(paragraphProperties34);
            paragraph50.AppendChild(run98);

            tableCell45.AppendChild(tableCellProperties45);
            tableCell45.AppendChild(paragraph50);

            TableCell tableCell46 = new TableCell();

            TableCellProperties tableCellProperties46 = new TableCellProperties();
            TableCellWidth tableCellWidth46 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };

            tableCellProperties46.AppendChild(tableCellWidth46);

            Paragraph paragraph51 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties35 = new ParagraphProperties();
            Justification justification26 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties34 = new ParagraphMarkRunProperties();

            paragraphProperties35.AppendChild(justification26);
            paragraphProperties35.AppendChild(paragraphMarkRunProperties34);

            Run run105 = new Run();

            RunProperties runProperties81 = new RunProperties();
            Text text105 = new Text();
            //Promedio noche diastolica
            text105.Text = report.DiastolicNightAvg.ToString();

            run105.AppendChild(runProperties81);
            run105.AppendChild(text105);

            paragraph51.AppendChild(paragraphProperties35);
            paragraph51.AppendChild(run105);

            tableCell46.AppendChild(tableCellProperties46);
            tableCell46.AppendChild(paragraph51);

            tableRow20.AppendChild(tableCell43);
            tableRow20.AppendChild(tableCell44);
            tableRow20.AppendChild(tableCell45);
            tableRow20.AppendChild(tableCell46);

            TableRow tableRow21 = new TableRow() { RsidTableRowAddition = "00C204AF", RsidTableRowProperties = "0075392D" };

            TableCell tableCell47 = new TableCell();

            TableCellProperties tableCellProperties47 = new TableCellProperties();
            TableCellWidth tableCellWidth47 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties47.AppendChild(tableCellWidth47);

            Paragraph paragraph52 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "00C204AF" };

            ParagraphProperties paragraphProperties36 = new ParagraphProperties();
            Justification justification27 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties35 = new ParagraphMarkRunProperties();

            paragraphProperties36.AppendChild(justification27);
            paragraphProperties36.AppendChild(paragraphMarkRunProperties35);

            Run run110 = new Run();

            RunProperties runProperties86 = new RunProperties();
            Text text110 = new Text();
            text110.Text = "TAM";

            run110.AppendChild(runProperties86);
            run110.AppendChild(text110);

            paragraph52.AppendChild(paragraphProperties36);
            paragraph52.AppendChild(run110);

            tableCell47.AppendChild(tableCellProperties47);
            tableCell47.AppendChild(paragraph52);

            TableCell tableCell48 = new TableCell();

            TableCellProperties tableCellProperties48 = new TableCellProperties();
            TableCellWidth tableCellWidth48 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties48.AppendChild(tableCellWidth48);

            Paragraph paragraph53 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties37 = new ParagraphProperties();
            Justification justification28 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties36 = new ParagraphMarkRunProperties();

            paragraphProperties37.AppendChild(justification28);
            paragraphProperties37.AppendChild(paragraphMarkRunProperties36);

            Run run111 = new Run();

            RunProperties runProperties87 = new RunProperties();
            Text text111 = new Text();
            //Promedio total tam
            text111.Text = report.MiddleTotalAvg.ToString();

            run111.AppendChild(runProperties87);
            run111.AppendChild(text111);

            paragraph53.AppendChild(paragraphProperties37);
            paragraph53.AppendChild(run111);

            tableCell48.AppendChild(tableCellProperties48);
            tableCell48.AppendChild(paragraph53);

            TableCell tableCell49 = new TableCell();

            TableCellProperties tableCellProperties49 = new TableCellProperties();
            TableCellWidth tableCellWidth49 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties49.AppendChild(tableCellWidth49);

            Paragraph paragraph54 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties38 = new ParagraphProperties();
            Justification justification29 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties37 = new ParagraphMarkRunProperties();

            paragraphProperties38.AppendChild(justification29);
            paragraphProperties38.AppendChild(paragraphMarkRunProperties37);

            Run run118 = new Run();

            RunProperties runProperties94 = new RunProperties();
            Text text118 = new Text();
            //Pormedio dia TAM
            text118.Text = report.MiddleDayAvg.ToString();

            run118.AppendChild(runProperties94);
            run118.AppendChild(text118);

            paragraph54.AppendChild(paragraphProperties38);
            paragraph54.AppendChild(run118);

            tableCell49.AppendChild(tableCellProperties49);
            tableCell49.AppendChild(paragraph54);

            TableCell tableCell50 = new TableCell();

            TableCellProperties tableCellProperties50 = new TableCellProperties();
            TableCellWidth tableCellWidth50 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };

            tableCellProperties50.AppendChild(tableCellWidth50);

            Paragraph paragraph55 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties39 = new ParagraphProperties();
            Justification justification30 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties38 = new ParagraphMarkRunProperties();

            paragraphProperties39.AppendChild(justification30);
            paragraphProperties39.AppendChild(paragraphMarkRunProperties38);

            Run run125 = new Run();

            RunProperties runProperties101 = new RunProperties();
            Text text125 = new Text();
            //Promedio noche TAM
            text125.Text = report.MiddleNightAvg.ToString();

            run125.AppendChild(runProperties101);
            run125.AppendChild(text125);

            paragraph55.AppendChild(paragraphProperties39);
            paragraph55.AppendChild(run125);

            tableCell50.AppendChild(tableCellProperties50);
            tableCell50.AppendChild(paragraph55);

            tableRow21.AppendChild(tableCell47);
            tableRow21.AppendChild(tableCell48);
            tableRow21.AppendChild(tableCell49);
            tableRow21.AppendChild(tableCell50);

            TableRow tableRow22 = new TableRow() { RsidTableRowAddition = "00C204AF", RsidTableRowProperties = "0075392D" };

            TableCell tableCell51 = new TableCell();

            TableCellProperties tableCellProperties51 = new TableCellProperties();
            TableCellWidth tableCellWidth51 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties51.AppendChild(tableCellWidth51);

            Paragraph paragraph56 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "00C204AF" };

            ParagraphProperties paragraphProperties40 = new ParagraphProperties();
            Justification justification31 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties39 = new ParagraphMarkRunProperties();

            paragraphProperties40.AppendChild(justification31);
            paragraphProperties40.AppendChild(paragraphMarkRunProperties39);

            Run run130 = new Run();

            RunProperties runProperties106 = new RunProperties();
            Text text130 = new Text();
            text130.Text = "FC";

            run130.AppendChild(runProperties106);
            run130.AppendChild(text130);

            paragraph56.AppendChild(paragraphProperties40);
            paragraph56.AppendChild(run130);

            tableCell51.AppendChild(tableCellProperties51);
            tableCell51.AppendChild(paragraph56);

            TableCell tableCell52 = new TableCell();

            TableCellProperties tableCellProperties52 = new TableCellProperties();
            TableCellWidth tableCellWidth52 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties52.AppendChild(tableCellWidth52);

            Paragraph paragraph57 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties41 = new ParagraphProperties();
            Justification justification32 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties40 = new ParagraphMarkRunProperties();

            paragraphProperties41.AppendChild(justification32);
            paragraphProperties41.AppendChild(paragraphMarkRunProperties40);

            Run run131 = new Run();

            RunProperties runProperties107 = new RunProperties();
            Text text131 = new Text();
            //Pormedio total frecuencia cardiaca
            text131.Text = report.HeartRateTotalAvg.ToString();

            run131.AppendChild(runProperties107);
            run131.AppendChild(text131);

            paragraph57.AppendChild(paragraphProperties41);
            paragraph57.AppendChild(run131);

            tableCell52.AppendChild(tableCellProperties52);
            tableCell52.AppendChild(paragraph57);

            TableCell tableCell53 = new TableCell();

            TableCellProperties tableCellProperties53 = new TableCellProperties();
            TableCellWidth tableCellWidth53 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties53.AppendChild(tableCellWidth53);

            Paragraph paragraph58 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties42 = new ParagraphProperties();
            Justification justification33 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties41 = new ParagraphMarkRunProperties();
            paragraphProperties42.AppendChild(justification33);
            paragraphProperties42.AppendChild(paragraphMarkRunProperties41);

            Run run138 = new Run();

            RunProperties runProperties114 = new RunProperties();
            Text text138 = new Text();
            //Promedio dia frecuencia cardiaca
            text138.Text = report.HeartRateDayAvg.ToString();

            run138.AppendChild(runProperties114);
            run138.AppendChild(text138);

            paragraph58.AppendChild(paragraphProperties42);
            paragraph58.AppendChild(run138);

            tableCell53.AppendChild(tableCellProperties53);
            tableCell53.AppendChild(paragraph58);

            TableCell tableCell54 = new TableCell();

            TableCellProperties tableCellProperties54 = new TableCellProperties();
            TableCellWidth tableCellWidth54 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };

            tableCellProperties54.AppendChild(tableCellWidth54);

            Paragraph paragraph59 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties43 = new ParagraphProperties();
            Justification justification34 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties42 = new ParagraphMarkRunProperties();

            paragraphProperties43.AppendChild(justification34);
            paragraphProperties43.AppendChild(paragraphMarkRunProperties42);

            Run run145 = new Run();

            RunProperties runProperties121 = new RunProperties();
            Text text145 = new Text();
            //Promedio noche frecuencia cardiaca
            text145.Text = report.HeartRateNightAvg.ToString();

            run145.AppendChild(runProperties121);
            run145.AppendChild(text145);

            paragraph59.AppendChild(paragraphProperties43);
            paragraph59.AppendChild(run145);

            tableCell54.AppendChild(tableCellProperties54);
            tableCell54.AppendChild(paragraph59);

            tableRow22.AppendChild(tableCell51);
            tableRow22.AppendChild(tableCell52);
            tableRow22.AppendChild(tableCell53);
            tableRow22.AppendChild(tableCell54);

            TableRow tableRow23 = new TableRow() { RsidTableRowAddition = "00C204AF", RsidTableRowProperties = "00C204AF" };

            TableCell tableCell55 = new TableCell();

            TableCellProperties tableCellProperties55 = new TableCellProperties();
            TableCellWidth tableCellWidth55 = new TableCellWidth() { Width = "9345", Type = TableWidthUnitValues.Dxa };
            GridSpan gridSpan7 = new GridSpan() { Val = 4 };
            Shading shading6 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "BFBFBF", ThemeFill = ThemeColorValues.Background1, ThemeFillShade = "BF" };

            tableCellProperties55.AppendChild(tableCellWidth55);
            tableCellProperties55.AppendChild(gridSpan7);
            tableCellProperties55.AppendChild(shading6);

            Paragraph paragraph60 = new Paragraph() { RsidParagraphMarkRevision = "00C204AF", RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "00C204AF", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties44 = new ParagraphProperties();

            ParagraphMarkRunProperties paragraphMarkRunProperties43 = new ParagraphMarkRunProperties();
            Bold bold19 = new Bold();
            paragraphMarkRunProperties43.AppendChild(bold19);

            paragraphProperties44.AppendChild(paragraphMarkRunProperties43);

            Run run150 = new Run();

            RunProperties runProperties126 = new RunProperties();
            Bold bold20 = new Bold();
            runProperties126.AppendChild(bold20);
            Text text150 = new Text();
            text150.Text = "Desviación Estandar";

            run150.AppendChild(runProperties126);
            run150.AppendChild(text150);

            paragraph60.AppendChild(paragraphProperties44);
            paragraph60.AppendChild(run150);

            tableCell55.AppendChild(tableCellProperties55);
            tableCell55.AppendChild(paragraph60);

            tableRow23.AppendChild(tableCell55);

            TableRow tableRow24 = new TableRow() { RsidTableRowAddition = "00C204AF", RsidTableRowProperties = "0075392D" };

            TableCell tableCell56 = new TableCell();

            TableCellProperties tableCellProperties56 = new TableCellProperties();
            TableCellWidth tableCellWidth56 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties56.AppendChild(tableCellWidth56);

            Paragraph paragraph61 = new Paragraph() { RsidParagraphMarkRevision = "00C204AF", RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "00C204AF" };

            ParagraphProperties paragraphProperties45 = new ParagraphProperties();
            Justification justification35 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties44 = new ParagraphMarkRunProperties();

            paragraphProperties45.AppendChild(justification35);
            paragraphProperties45.AppendChild(paragraphMarkRunProperties44);

            Run run154 = new Run();

            RunProperties runProperties130 = new RunProperties();
            Text text154 = new Text();
            text154.Text = "Presión arterial sistólica";

            run154.AppendChild(runProperties130);
            run154.AppendChild(text154);

            paragraph61.AppendChild(paragraphProperties45);
            paragraph61.AppendChild(run154);

            tableCell56.AppendChild(tableCellProperties56);
            tableCell56.AppendChild(paragraph61);

            TableCell tableCell57 = new TableCell();

            TableCellProperties tableCellProperties57 = new TableCellProperties();
            TableCellWidth tableCellWidth57 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties57.AppendChild(tableCellWidth57);

            Paragraph paragraph62 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties46 = new ParagraphProperties();
            Justification justification36 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties45 = new ParagraphMarkRunProperties();

            paragraphProperties46.AppendChild(justification36);
            paragraphProperties46.AppendChild(paragraphMarkRunProperties45);

            Run run156 = new Run();

            RunProperties runProperties132 = new RunProperties();
            Text text156 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            //Desviacion estandar total sistolica 
            text156.Text = report.StandardDeviationSysTotal.ToString();

            run156.AppendChild(runProperties132);
            run156.AppendChild(text156);

            paragraph62.AppendChild(paragraphProperties46);
            paragraph62.AppendChild(run156);

            tableCell57.AppendChild(tableCellProperties57);
            tableCell57.AppendChild(paragraph62);

            TableCell tableCell58 = new TableCell();

            TableCellProperties tableCellProperties58 = new TableCellProperties();
            TableCellWidth tableCellWidth58 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties58.AppendChild(tableCellWidth58);

            Paragraph paragraph63 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties47 = new ParagraphProperties();
            Justification justification37 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties46 = new ParagraphMarkRunProperties();

            paragraphProperties47.AppendChild(justification37);
            paragraphProperties47.AppendChild(paragraphMarkRunProperties46);

            Run run161 = new Run();

            RunProperties runProperties137 = new RunProperties();
            Text text161 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            //Desviacion estandar sistolica del dia
            text161.Text = report.StandardDeviationSysDay.ToString();

            run161.AppendChild(runProperties137);
            run161.AppendChild(text161);

            paragraph63.AppendChild(paragraphProperties47);
            paragraph63.AppendChild(run161);

            tableCell58.AppendChild(tableCellProperties58);
            tableCell58.AppendChild(paragraph63);

            TableCell tableCell59 = new TableCell();

            TableCellProperties tableCellProperties59 = new TableCellProperties();
            TableCellWidth tableCellWidth59 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };

            tableCellProperties59.AppendChild(tableCellWidth59);

            Paragraph paragraph64 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties48 = new ParagraphProperties();
            Justification justification38 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties47 = new ParagraphMarkRunProperties();

            paragraphProperties48.AppendChild(justification38);
            paragraphProperties48.AppendChild(paragraphMarkRunProperties47);

            Run run166 = new Run();

            RunProperties runProperties142 = new RunProperties();
            Text text166 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            //Desvacion estandar noche sistolica
            text166.Text = report.StandardDeviationSysNight.ToString();

            run166.AppendChild(runProperties142);
            run166.AppendChild(text166);

            paragraph64.AppendChild(paragraphProperties48);
            paragraph64.AppendChild(run166);

            tableCell59.AppendChild(tableCellProperties59);
            tableCell59.AppendChild(paragraph64);

            tableRow24.AppendChild(tableCell56);
            tableRow24.AppendChild(tableCell57);
            tableRow24.AppendChild(tableCell58);
            tableRow24.AppendChild(tableCell59);

            TableRow tableRow25 = new TableRow() { RsidTableRowAddition = "00C204AF", RsidTableRowProperties = "0075392D" };

            TableCell tableCell60 = new TableCell();

            TableCellProperties tableCellProperties60 = new TableCellProperties();
            TableCellWidth tableCellWidth60 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties60.AppendChild(tableCellWidth60);

            Paragraph paragraph65 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "00C204AF" };

            ParagraphProperties paragraphProperties49 = new ParagraphProperties();
            Justification justification39 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties48 = new ParagraphMarkRunProperties();

            paragraphProperties49.AppendChild(justification39);
            paragraphProperties49.AppendChild(paragraphMarkRunProperties48);

            Run run169 = new Run();

            RunProperties runProperties145 = new RunProperties();
            Text text169 = new Text();
            text169.Text = "Presión arterial diastólica";

            run169.AppendChild(runProperties145);
            run169.AppendChild(text169);

            paragraph65.AppendChild(paragraphProperties49);
            paragraph65.AppendChild(run169);

            tableCell60.AppendChild(tableCellProperties60);
            tableCell60.AppendChild(paragraph65);

            TableCell tableCell61 = new TableCell();

            TableCellProperties tableCellProperties61 = new TableCellProperties();
            TableCellWidth tableCellWidth61 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties61.AppendChild(tableCellWidth61);

            Paragraph paragraph66 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties50 = new ParagraphProperties();
            Justification justification40 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties49 = new ParagraphMarkRunProperties();

            paragraphProperties50.AppendChild(justification40);
            paragraphProperties50.AppendChild(paragraphMarkRunProperties49);

            Run run170 = new Run();

            RunProperties runProperties146 = new RunProperties();
            Text text170 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            //desviacion estandar diastolica total
            text170.Text = report.StandardDeviationDiasTotal.ToString();

            run170.AppendChild(runProperties146);
            run170.AppendChild(text170);

            paragraph66.AppendChild(paragraphProperties50);
            paragraph66.AppendChild(run170);

            tableCell61.AppendChild(tableCellProperties61);
            tableCell61.AppendChild(paragraph66);

            TableCell tableCell62 = new TableCell();

            TableCellProperties tableCellProperties62 = new TableCellProperties();
            TableCellWidth tableCellWidth62 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties62.AppendChild(tableCellWidth62);

            Paragraph paragraph67 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties51 = new ParagraphProperties();
            Justification justification41 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties50 = new ParagraphMarkRunProperties();

            paragraphProperties51.AppendChild(justification41);
            paragraphProperties51.AppendChild(paragraphMarkRunProperties50);

            Run run175 = new Run();

            RunProperties runProperties151 = new RunProperties();
            Text text175 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            //Desviacion estandar dia diastolica
            text175.Text = report.StandardDeviationDiasDay.ToString();

            run175.AppendChild(runProperties151);
            run175.AppendChild(text175);

            paragraph67.AppendChild(paragraphProperties51);
            paragraph67.AppendChild(run175);

            tableCell62.AppendChild(tableCellProperties62);
            tableCell62.AppendChild(paragraph67);

            TableCell tableCell63 = new TableCell();

            TableCellProperties tableCellProperties63 = new TableCellProperties();
            TableCellWidth tableCellWidth63 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };

            tableCellProperties63.AppendChild(tableCellWidth63);

            Paragraph paragraph68 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties52 = new ParagraphProperties();
            Justification justification42 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties51 = new ParagraphMarkRunProperties();

            paragraphProperties52.AppendChild(justification42);
            paragraphProperties52.AppendChild(paragraphMarkRunProperties51);

            Run run180 = new Run();

            RunProperties runProperties156 = new RunProperties();
            Text text180 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            //Desviacion estandar noche diastolica
            text180.Text = report.StandardDeviationDiasNight.ToString();

            run180.AppendChild(runProperties156);
            run180.AppendChild(text180);

            paragraph68.AppendChild(paragraphProperties52);
            paragraph68.AppendChild(run180);

            tableCell63.AppendChild(tableCellProperties63);
            tableCell63.AppendChild(paragraph68);

            tableRow25.AppendChild(tableCell60);
            tableRow25.AppendChild(tableCell61);
            tableRow25.AppendChild(tableCell62);
            tableRow25.AppendChild(tableCell63);

            TableRow tableRow26 = new TableRow() { RsidTableRowAddition = "00C204AF", RsidTableRowProperties = "0075392D" };

            TableCell tableCell64 = new TableCell();

            TableCellProperties tableCellProperties64 = new TableCellProperties();
            TableCellWidth tableCellWidth64 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties64.AppendChild(tableCellWidth64);

            Paragraph paragraph69 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "00C204AF" };

            ParagraphProperties paragraphProperties53 = new ParagraphProperties();
            Justification justification43 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties52 = new ParagraphMarkRunProperties();

            paragraphProperties53.AppendChild(justification43);
            paragraphProperties53.AppendChild(paragraphMarkRunProperties52);

            Run run183 = new Run();

            RunProperties runProperties159 = new RunProperties();
            Text text183 = new Text();
            text183.Text = "TAM";

            run183.AppendChild(runProperties159);
            run183.AppendChild(text183);

            paragraph69.AppendChild(paragraphProperties53);
            paragraph69.AppendChild(run183);

            tableCell64.AppendChild(tableCellProperties64);
            tableCell64.AppendChild(paragraph69);

            TableCell tableCell65 = new TableCell();

            TableCellProperties tableCellProperties65 = new TableCellProperties();
            TableCellWidth tableCellWidth65 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties65.AppendChild(tableCellWidth65);

            Paragraph paragraph70 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties54 = new ParagraphProperties();
            Justification justification44 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties53 = new ParagraphMarkRunProperties();

            paragraphProperties54.AppendChild(justification44);
            paragraphProperties54.AppendChild(paragraphMarkRunProperties53);

            Run run184 = new Run();

            RunProperties runProperties160 = new RunProperties();
            Text text184 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            //Desviacion estandar total TAM
            text184.Text = report.StandardDeviationTamTotal.ToString();

            run184.AppendChild(runProperties160);
            run184.AppendChild(text184);

            paragraph70.AppendChild(paragraphProperties54);
            paragraph70.AppendChild(run184);

            tableCell65.AppendChild(tableCellProperties65);
            tableCell65.AppendChild(paragraph70);

            TableCell tableCell66 = new TableCell();

            TableCellProperties tableCellProperties66 = new TableCellProperties();
            TableCellWidth tableCellWidth66 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties66.AppendChild(tableCellWidth66);

            Paragraph paragraph71 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties55 = new ParagraphProperties();
            Justification justification45 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties54 = new ParagraphMarkRunProperties();

            paragraphProperties55.AppendChild(justification45);
            paragraphProperties55.AppendChild(paragraphMarkRunProperties54);

            Run run189 = new Run();

            RunProperties runProperties165 = new RunProperties();
            Text text189 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            //Desviacion estandar dia TAM
            text189.Text = report.StandardDeviationTamDay.ToString();

            run189.AppendChild(runProperties165);
            run189.AppendChild(text189);

            paragraph71.AppendChild(paragraphProperties55);
            paragraph71.AppendChild(run189);

            tableCell66.AppendChild(tableCellProperties66);
            tableCell66.AppendChild(paragraph71);

            TableCell tableCell67 = new TableCell();

            TableCellProperties tableCellProperties67 = new TableCellProperties();
            TableCellWidth tableCellWidth67 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };

            tableCellProperties67.AppendChild(tableCellWidth67);

            Paragraph paragraph72 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties56 = new ParagraphProperties();
            Justification justification46 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties55 = new ParagraphMarkRunProperties();

            paragraphProperties56.AppendChild(justification46);
            paragraphProperties56.AppendChild(paragraphMarkRunProperties55);

            Run run194 = new Run();

            RunProperties runProperties170 = new RunProperties();
            Text text194 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            //Desviacion estandar noche TAM
            text194.Text = report.StandarDeviationTamNight.ToString();

            run194.AppendChild(runProperties170);
            run194.AppendChild(text194);

            paragraph72.AppendChild(paragraphProperties56);
            paragraph72.AppendChild(run194);

            tableCell67.AppendChild(tableCellProperties67);
            tableCell67.AppendChild(paragraph72);

            tableRow26.AppendChild(tableCell64);
            tableRow26.AppendChild(tableCell65);
            tableRow26.AppendChild(tableCell66);
            tableRow26.AppendChild(tableCell67);

            TableRow tableRow27 = new TableRow() { RsidTableRowAddition = "00C204AF", RsidTableRowProperties = "0075392D" };

            TableCell tableCell68 = new TableCell();

            TableCellProperties tableCellProperties68 = new TableCellProperties();
            TableCellWidth tableCellWidth68 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties68.AppendChild(tableCellWidth68);

            Paragraph paragraph73 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "00C204AF" };

            ParagraphProperties paragraphProperties57 = new ParagraphProperties();
            Justification justification47 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties56 = new ParagraphMarkRunProperties();

            paragraphProperties57.AppendChild(justification47);
            paragraphProperties57.AppendChild(paragraphMarkRunProperties56);

            Run run197 = new Run();

            RunProperties runProperties173 = new RunProperties();
            Text text197 = new Text();
            text197.Text = "FC";

            run197.AppendChild(runProperties173);
            run197.AppendChild(text197);

            paragraph73.AppendChild(paragraphProperties57);
            paragraph73.AppendChild(run197);

            tableCell68.AppendChild(tableCellProperties68);
            tableCell68.AppendChild(paragraph73);

            TableCell tableCell69 = new TableCell();

            TableCellProperties tableCellProperties69 = new TableCellProperties();
            TableCellWidth tableCellWidth69 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties69.AppendChild(tableCellWidth69);

            Paragraph paragraph74 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties58 = new ParagraphProperties();
            Justification justification48 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties57 = new ParagraphMarkRunProperties();

            paragraphProperties58.AppendChild(justification48);
            paragraphProperties58.AppendChild(paragraphMarkRunProperties57);

            Run run198 = new Run();

            RunProperties runProperties174 = new RunProperties();
            Text text198 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            //Desviacion estandar total frecuencia cardiaca
            text198.Text = report.StandardDeviationHeartRateTotal.ToString();

            run198.AppendChild(runProperties174);
            run198.AppendChild(text198);

            paragraph74.AppendChild(paragraphProperties58);
            paragraph74.AppendChild(run198);

            tableCell69.AppendChild(tableCellProperties69);
            tableCell69.AppendChild(paragraph74);

            TableCell tableCell70 = new TableCell();

            TableCellProperties tableCellProperties70 = new TableCellProperties();
            TableCellWidth tableCellWidth70 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties70.AppendChild(tableCellWidth70);

            Paragraph paragraph75 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties59 = new ParagraphProperties();
            Justification justification49 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties58 = new ParagraphMarkRunProperties();

            paragraphProperties59.AppendChild(justification49);
            paragraphProperties59.AppendChild(paragraphMarkRunProperties58);

            Run run203 = new Run();

            RunProperties runProperties179 = new RunProperties();
            Text text203 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            //Desviacion estandar frecuencia cardiaca dia
            text203.Text = report.StandardDeviationHeartRateDay.ToString();

            run203.AppendChild(runProperties179);
            run203.AppendChild(text203);

            paragraph75.AppendChild(paragraphProperties59);
            paragraph75.AppendChild(run203);

            tableCell70.AppendChild(tableCellProperties70);
            tableCell70.AppendChild(paragraph75);

            TableCell tableCell71 = new TableCell();

            TableCellProperties tableCellProperties71 = new TableCellProperties();
            TableCellWidth tableCellWidth71 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };

            tableCellProperties71.AppendChild(tableCellWidth71);

            Paragraph paragraph76 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties60 = new ParagraphProperties();
            Justification justification50 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties59 = new ParagraphMarkRunProperties();

            paragraphProperties60.AppendChild(justification50);
            paragraphProperties60.AppendChild(paragraphMarkRunProperties59);

            Run run208 = new Run();

            RunProperties runProperties184 = new RunProperties();
            Text text208 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            //Desviacion estandar frecuencia cardiaca noche
            text208.Text = report.StandardDeviationHeartRateNight.ToString();

            run208.AppendChild(runProperties184);
            run208.AppendChild(text208);

            paragraph76.AppendChild(paragraphProperties60);
            paragraph76.AppendChild(run208);

            tableCell71.AppendChild(tableCellProperties71);
            tableCell71.AppendChild(paragraph76);

            tableRow27.AppendChild(tableCell68);
            tableRow27.AppendChild(tableCell69);
            tableRow27.AppendChild(tableCell70);
            tableRow27.AppendChild(tableCell71);

            TableRow tableRow28 = new TableRow() { RsidTableRowAddition = "00C204AF", RsidTableRowProperties = "00C204AF" };

            TableCell tableCell72 = new TableCell();

            TableCellProperties tableCellProperties72 = new TableCellProperties();
            TableCellWidth tableCellWidth72 = new TableCellWidth() { Width = "9345", Type = TableWidthUnitValues.Dxa };
            GridSpan gridSpan8 = new GridSpan() { Val = 4 };
            Shading shading7 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "BFBFBF", ThemeFill = ThemeColorValues.Background1, ThemeFillShade = "BF" };

            tableCellProperties72.AppendChild(tableCellWidth72);
            tableCellProperties72.AppendChild(gridSpan8);
            tableCellProperties72.AppendChild(shading7);

            Paragraph paragraph77 = new Paragraph() { RsidParagraphMarkRevision = "00C204AF", RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "00C204AF", RsidRunAdditionDefault = "00C204AF" };

            ParagraphProperties paragraphProperties61 = new ParagraphProperties();

            ParagraphMarkRunProperties paragraphMarkRunProperties60 = new ParagraphMarkRunProperties();
            Bold bold24 = new Bold();

            paragraphMarkRunProperties60.AppendChild(bold24);

            paragraphProperties61.AppendChild(paragraphMarkRunProperties60);

            Run run211 = new Run() { RsidRunProperties = "00C204AF" };

            RunProperties runProperties187 = new RunProperties();
            Bold bold25 = new Bold();
            runProperties187.AppendChild(bold25);
            Text text211 = new Text();
            text211.Text = "Valores por encima del límite";

            var uda = new UdaHtaDataAccess();
            var limits = uda.GetLimits();

            run211.AppendChild(runProperties187);
            run211.AppendChild(text211);

            paragraph77.AppendChild(paragraphProperties61);
            paragraph77.AppendChild(run211);

            tableCell72.AppendChild(tableCellProperties72);
            tableCell72.AppendChild(paragraph77);

            tableRow28.AppendChild(tableCell72);

            TableRow tableRow29 = new TableRow() { RsidTableRowAddition = "00C204AF", RsidTableRowProperties = "0075392D" };

            TableCell tableCell73 = new TableCell();

            TableCellProperties tableCellProperties73 = new TableCellProperties();
            TableCellWidth tableCellWidth73 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties73.AppendChild(tableCellWidth73);

            Paragraph paragraph78 = new Paragraph() { RsidParagraphMarkRevision = "00C204AF", RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "00C204AF" };

            ParagraphProperties paragraphProperties62 = new ParagraphProperties();
            Justification justification51 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties61 = new ParagraphMarkRunProperties();

            paragraphProperties62.AppendChild(justification51);
            paragraphProperties62.AppendChild(paragraphMarkRunProperties61);

            Run run212 = new Run();

            RunProperties runProperties188 = new RunProperties();
            Text text212 = new Text();
            text212.Text = "Presión arterial sistólica";

            run212.AppendChild(runProperties188);
            run212.AppendChild(text212);

            paragraph78.AppendChild(paragraphProperties62);
            paragraph78.AppendChild(run212);

            tableCell73.AppendChild(tableCellProperties73);
            tableCell73.AppendChild(paragraph78);

            TableCell tableCell74 = new TableCell();

            TableCellProperties tableCellProperties74 = new TableCellProperties();
            TableCellWidth tableCellWidth74 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties74.AppendChild(tableCellWidth74);

            Paragraph paragraph79 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties63 = new ParagraphProperties();
            Justification justification52 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties62 = new ParagraphMarkRunProperties();

            paragraphProperties63.AppendChild(justification52);
            paragraphProperties63.AppendChild(paragraphMarkRunProperties62);

            Run run214 = new Run();

            RunProperties runProperties190 = new RunProperties();
            Text text214 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            //Porcentaje de valores por encima del limite sistolica
            text214.Text = "";

            run214.AppendChild(runProperties190);
            run214.AppendChild(text214);

            paragraph79.AppendChild(paragraphProperties63);
            paragraph79.AppendChild(run214);

            tableCell74.AppendChild(tableCellProperties74);
            tableCell74.AppendChild(paragraph79);

            TableCell tableCell75 = new TableCell();

            TableCellProperties tableCellProperties75 = new TableCellProperties();
            TableCellWidth tableCellWidth75 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties75.AppendChild(tableCellWidth75);

            Paragraph paragraph80 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties64 = new ParagraphProperties();
            Justification justification53 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties63 = new ParagraphMarkRunProperties();

            paragraphProperties64.AppendChild(justification53);
            paragraphProperties64.AppendChild(paragraphMarkRunProperties63);

            Run run221 = new Run();

            RunProperties runProperties197 = new RunProperties();
            Text text221 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            //Porcentaje de valores por encima del limite sistolica
            // val lim sis
            text221.Text = (report.Measures.Count(m => m.Asleep.Value && m.Systolic > limits.MaxSysDay)*100/cantValidDay).ToString();

            run221.AppendChild(runProperties197);
            run221.AppendChild(text221);

            paragraph80.AppendChild(paragraphProperties64);
            paragraph80.AppendChild(run221);

            tableCell75.AppendChild(tableCellProperties75);
            tableCell75.AppendChild(paragraph80);

            TableCell tableCell76 = new TableCell();

            TableCellProperties tableCellProperties76 = new TableCellProperties();
            TableCellWidth tableCellWidth76 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };

            tableCellProperties76.AppendChild(tableCellWidth76);

            Paragraph paragraph81 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "001B715B", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties65 = new ParagraphProperties();
            Justification justification54 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties64 = new ParagraphMarkRunProperties();

            paragraphProperties65.AppendChild(justification54);
            paragraphProperties65.AppendChild(paragraphMarkRunProperties64);

            Run run228 = new Run();

            RunProperties runProperties204 = new RunProperties();
            Text text228 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            //Porcentaje de valores por encima del limite sistolica
            //val lim noche sis
            text228.Text = (report.Measures.Count(m => m.Asleep.Value && m.Systolic > limits.MaxSysNight)*100/cantValidNight).ToString();

            run228.AppendChild(runProperties204);
            run228.AppendChild(text228);

            paragraph81.AppendChild(paragraphProperties65);
            paragraph81.AppendChild(run228);

            tableCell76.AppendChild(tableCellProperties76);
            tableCell76.AppendChild(paragraph81);

            tableRow29.AppendChild(tableCell73);
            tableRow29.AppendChild(tableCell74);
            tableRow29.AppendChild(tableCell75);
            tableRow29.AppendChild(tableCell76);

            TableRow tableRow30 = new TableRow() { RsidTableRowAddition = "00C204AF", RsidTableRowProperties = "0075392D" };

            TableCell tableCell77 = new TableCell();

            TableCellProperties tableCellProperties77 = new TableCellProperties();
            TableCellWidth tableCellWidth77 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties77.AppendChild(tableCellWidth77);

            Paragraph paragraph82 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "00C204AF" };

            ParagraphProperties paragraphProperties66 = new ParagraphProperties();
            Justification justification55 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties65 = new ParagraphMarkRunProperties();

            paragraphProperties66.AppendChild(justification55);
            paragraphProperties66.AppendChild(paragraphMarkRunProperties65);

            Run run233 = new Run();

            RunProperties runProperties209 = new RunProperties();
            Text text233 = new Text();
            text233.Text = "Presión arterial diastólica";

            run233.AppendChild(runProperties209);
            run233.AppendChild(text233);

            paragraph82.AppendChild(paragraphProperties66);
            paragraph82.AppendChild(run233);

            tableCell77.AppendChild(tableCellProperties77);
            tableCell77.AppendChild(paragraph82);

            TableCell tableCell78 = new TableCell();

            TableCellProperties tableCellProperties78 = new TableCellProperties();
            TableCellWidth tableCellWidth78 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties78.AppendChild(tableCellWidth78);

            Paragraph paragraph83 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties67 = new ParagraphProperties();
            Justification justification56 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties66 = new ParagraphMarkRunProperties();

            paragraphProperties67.AppendChild(justification56);
            paragraphProperties67.AppendChild(paragraphMarkRunProperties66);

            Run run234 = new Run();

            RunProperties runProperties210 = new RunProperties();
            Text text234 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            //Porcentaje de valores por encima del limite diastolica
            text234.Text = "";

            run234.AppendChild(runProperties210);
            run234.AppendChild(text234);

            paragraph83.AppendChild(paragraphProperties67);
            paragraph83.AppendChild(run234);

            tableCell78.AppendChild(tableCellProperties78);
            tableCell78.AppendChild(paragraph83);

            TableCell tableCell79 = new TableCell();

            TableCellProperties tableCellProperties79 = new TableCellProperties();
            TableCellWidth tableCellWidth79 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties79.AppendChild(tableCellWidth79);

            Paragraph paragraph84 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties68 = new ParagraphProperties();
            Justification justification57 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties67 = new ParagraphMarkRunProperties();

            paragraphProperties68.AppendChild(justification57);
            paragraphProperties68.AppendChild(paragraphMarkRunProperties67);

            Run run241 = new Run();

            RunProperties runProperties217 = new RunProperties();
            Text text241 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            //Porcentaje de valores por encima del limite diastolica
            // val lim dia dias
            text241.Text = (report.Measures.Count(m => m.Asleep.Value && m.Diastolic > limits.MaxDiasDay)*100/cantValidDay).ToString();

            run241.AppendChild(runProperties217);
            run241.AppendChild(text241);

            paragraph84.AppendChild(paragraphProperties68);
            paragraph84.AppendChild(run241);

            tableCell79.AppendChild(tableCellProperties79);
            tableCell79.AppendChild(paragraph84);

            TableCell tableCell80 = new TableCell();

            TableCellProperties tableCellProperties80 = new TableCellProperties();
            TableCellWidth tableCellWidth80 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };

            tableCellProperties80.AppendChild(tableCellWidth80);

            Paragraph paragraph85 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties69 = new ParagraphProperties();
            Justification justification58 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties68 = new ParagraphMarkRunProperties();

            paragraphProperties69.AppendChild(justification58);
            paragraphProperties69.AppendChild(paragraphMarkRunProperties68);

            Run run248 = new Run();

            RunProperties runProperties224 = new RunProperties();
            Text text248 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            //Porcentaje de valores por encima del limite sistolica
            // val lim noche dias
            text248.Text = (report.Measures.Count(m => m.Asleep.Value && m.Diastolic > limits.MaxDiasNight)*100/cantValidNight).ToString();

            run248.AppendChild(runProperties224);
            run248.AppendChild(text248);

            paragraph85.AppendChild(paragraphProperties69);
            paragraph85.AppendChild(run248);

            tableCell80.AppendChild(tableCellProperties80);
            tableCell80.AppendChild(paragraph85);

            tableRow30.AppendChild(tableCell77);
            tableRow30.AppendChild(tableCell78);
            tableRow30.AppendChild(tableCell79);
            tableRow30.AppendChild(tableCell80);

            TableRow tableRow31 = new TableRow() { RsidTableRowMarkRevision = "00C204AF", RsidTableRowAddition = "00C204AF", RsidTableRowProperties = "003A3FD4" };

            TableCell tableCell81 = new TableCell();

            TableCellProperties tableCellProperties81 = new TableCellProperties();
            TableCellWidth tableCellWidth81 = new TableCellWidth() { Width = "9345", Type = TableWidthUnitValues.Dxa };
            GridSpan gridSpan9 = new GridSpan() { Val = 4 };
            Shading shading8 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "BFBFBF", ThemeFill = ThemeColorValues.Background1, ThemeFillShade = "BF" };

            tableCellProperties81.AppendChild(tableCellWidth81);
            tableCellProperties81.AppendChild(gridSpan9);
            tableCellProperties81.AppendChild(shading8);

            Paragraph paragraph86 = new Paragraph() { RsidParagraphMarkRevision = "00C204AF", RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "00C204AF", RsidRunAdditionDefault = "00C204AF" };

            ParagraphProperties paragraphProperties70 = new ParagraphProperties();

            ParagraphMarkRunProperties paragraphMarkRunProperties69 = new ParagraphMarkRunProperties();
            Bold bold26 = new Bold();
            paragraphMarkRunProperties69.AppendChild(bold26);

            paragraphProperties70.AppendChild(paragraphMarkRunProperties69);

            Run run253 = new Run() { RsidRunProperties = "00C204AF" };

            RunProperties runProperties229 = new RunProperties();
            Bold bold27 = new Bold();
            runProperties229.AppendChild(bold27);
            Text text253 = new Text();
            text253.Text = "Máximo";

            run253.AppendChild(runProperties229);
            run253.AppendChild(text253);

            paragraph86.AppendChild(paragraphProperties70);
            paragraph86.AppendChild(run253);

            tableCell81.AppendChild(tableCellProperties81);
            tableCell81.AppendChild(paragraph86);

            tableRow31.AppendChild(tableCell81);

            TableRow tableRow32 = new TableRow() { RsidTableRowAddition = "00C204AF", RsidTableRowProperties = "0075392D" };

            TableCell tableCell82 = new TableCell();

            TableCellProperties tableCellProperties82 = new TableCellProperties();
            TableCellWidth tableCellWidth82 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties82.AppendChild(tableCellWidth82);

            Paragraph paragraph87 = new Paragraph() { RsidParagraphMarkRevision = "00C204AF", RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "00C204AF" };

            ParagraphProperties paragraphProperties71 = new ParagraphProperties();
            Justification justification59 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties70 = new ParagraphMarkRunProperties();

            paragraphProperties71.AppendChild(justification59);
            paragraphProperties71.AppendChild(paragraphMarkRunProperties70);

            Run run254 = new Run();

            RunProperties runProperties230 = new RunProperties();
            Text text254 = new Text();
            text254.Text = "Presión arterial sistólica";

            run254.AppendChild(runProperties230);
            run254.AppendChild(text254);

            paragraph87.AppendChild(paragraphProperties71);
            paragraph87.AppendChild(run254);

            tableCell82.AppendChild(tableCellProperties82);
            tableCell82.AppendChild(paragraph87);

            TableCell tableCell83 = new TableCell();

            TableCellProperties tableCellProperties83 = new TableCellProperties();
            TableCellWidth tableCellWidth83 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties83.AppendChild(tableCellWidth83);

            Paragraph paragraph88 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties72 = new ParagraphProperties();
            Justification justification60 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties71 = new ParagraphMarkRunProperties();

            paragraphProperties72.AppendChild(justification60);
            paragraphProperties72.AppendChild(paragraphMarkRunProperties71);

            Run run256 = new Run();

            RunProperties runProperties232 = new RunProperties();
            Text text256 = new Text();
            //Sistolica maxima total
            text256.Text = "";

            run256.AppendChild(runProperties232);
            run256.AppendChild(text256);

            paragraph88.AppendChild(paragraphProperties72);
            paragraph88.AppendChild(run256);

            tableCell83.AppendChild(tableCellProperties83);
            tableCell83.AppendChild(paragraph88);

            TableCell tableCell84 = new TableCell();

            TableCellProperties tableCellProperties84 = new TableCellProperties();
            TableCellWidth tableCellWidth84 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties84.AppendChild(tableCellWidth84);

            Paragraph paragraph89 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties73 = new ParagraphProperties();
            Justification justification61 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties72 = new ParagraphMarkRunProperties();

            paragraphProperties73.AppendChild(justification61);
            paragraphProperties73.AppendChild(paragraphMarkRunProperties72);

            Run run263 = new Run();

            RunProperties runProperties239 = new RunProperties();
            Text text263 = new Text();
            //Sistolica maxima del dia
            text263.Text = report.SystolicDayMax.ToString();

            run263.AppendChild(runProperties239);
            run263.AppendChild(text263);

            paragraph89.AppendChild(paragraphProperties73);
            paragraph89.AppendChild(run263);

            tableCell84.AppendChild(tableCellProperties84);
            tableCell84.AppendChild(paragraph89);

            TableCell tableCell85 = new TableCell();

            TableCellProperties tableCellProperties85 = new TableCellProperties();
            TableCellWidth tableCellWidth85 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };

            tableCellProperties85.AppendChild(tableCellWidth85);

            Paragraph paragraph90 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties74 = new ParagraphProperties();
            Justification justification62 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties73 = new ParagraphMarkRunProperties();

            paragraphProperties74.AppendChild(justification62);
            paragraphProperties74.AppendChild(paragraphMarkRunProperties73);

            Run run270 = new Run();

            RunProperties runProperties246 = new RunProperties();
            Text text270 = new Text();
            //Sistolica maxima de la noche
            text270.Text = report.SystolicNightMax.ToString();

            run270.AppendChild(runProperties246);
            run270.AppendChild(text270);

            paragraph90.AppendChild(paragraphProperties74);
            paragraph90.AppendChild(run270);

            tableCell85.AppendChild(tableCellProperties85);
            tableCell85.AppendChild(paragraph90);

            tableRow32.AppendChild(tableCell82);
            tableRow32.AppendChild(tableCell83);
            tableRow32.AppendChild(tableCell84);
            tableRow32.AppendChild(tableCell85);

            TableRow tableRow33 = new TableRow() { RsidTableRowAddition = "00C204AF", RsidTableRowProperties = "0075392D" };

            TableCell tableCell86 = new TableCell();

            TableCellProperties tableCellProperties86 = new TableCellProperties();
            TableCellWidth tableCellWidth86 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties86.AppendChild(tableCellWidth86);

            Paragraph paragraph91 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "00C204AF" };

            ParagraphProperties paragraphProperties75 = new ParagraphProperties();
            Justification justification63 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties74 = new ParagraphMarkRunProperties();

            paragraphProperties75.AppendChild(justification63);
            paragraphProperties75.AppendChild(paragraphMarkRunProperties74);

            Run run275 = new Run();

            RunProperties runProperties251 = new RunProperties();
            Text text275 = new Text();
            text275.Text = "Presión arterial diastólica";

            run275.AppendChild(runProperties251);
            run275.AppendChild(text275);

            paragraph91.AppendChild(paragraphProperties75);
            paragraph91.AppendChild(run275);

            tableCell86.AppendChild(tableCellProperties86);
            tableCell86.AppendChild(paragraph91);

            TableCell tableCell87 = new TableCell();

            TableCellProperties tableCellProperties87 = new TableCellProperties();
            TableCellWidth tableCellWidth87 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties87.AppendChild(tableCellWidth87);

            Paragraph paragraph92 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties76 = new ParagraphProperties();
            Justification justification64 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties75 = new ParagraphMarkRunProperties();

            paragraphProperties76.AppendChild(justification64);
            paragraphProperties76.AppendChild(paragraphMarkRunProperties75);

            Run run276 = new Run();

            RunProperties runProperties252 = new RunProperties();
            Text text276 = new Text();
            //Diastolica maxima total
            text276.Text = report.DiastolicTotalMax.ToString();

            run276.AppendChild(runProperties252);
            run276.AppendChild(text276);

            paragraph92.AppendChild(paragraphProperties76);
            paragraph92.AppendChild(run276);

            tableCell87.AppendChild(tableCellProperties87);
            tableCell87.AppendChild(paragraph92);

            TableCell tableCell88 = new TableCell();

            TableCellProperties tableCellProperties88 = new TableCellProperties();
            TableCellWidth tableCellWidth88 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties88.AppendChild(tableCellWidth88);

            Paragraph paragraph93 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties77 = new ParagraphProperties();
            Justification justification65 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties76 = new ParagraphMarkRunProperties();

            paragraphProperties77.AppendChild(justification65);
            paragraphProperties77.AppendChild(paragraphMarkRunProperties76);

            Run run283 = new Run();

            RunProperties runProperties259 = new RunProperties();
            Text text283 = new Text();
            //Diastolica maxima del dia
            text283.Text = report.DiastolicDayMax.ToString();

            run283.AppendChild(runProperties259);
            run283.AppendChild(text283);

            paragraph93.AppendChild(paragraphProperties77);
            paragraph93.AppendChild(run283);

            tableCell88.AppendChild(tableCellProperties88);
            tableCell88.AppendChild(paragraph93);

            TableCell tableCell89 = new TableCell();

            TableCellProperties tableCellProperties89 = new TableCellProperties();
            TableCellWidth tableCellWidth89 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };

            tableCellProperties89.AppendChild(tableCellWidth89);

            Paragraph paragraph94 = new Paragraph() { RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties78 = new ParagraphProperties();
            Justification justification66 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties77 = new ParagraphMarkRunProperties();

            paragraphProperties78.AppendChild(justification66);
            paragraphProperties78.AppendChild(paragraphMarkRunProperties77);

            Run run290 = new Run();

            RunProperties runProperties266 = new RunProperties();
            Text text290 = new Text();
            //Diastolica maxima de la noche
            text290.Text = report.DiastolicNightMax.ToString();

            run290.AppendChild(runProperties266);
            run290.AppendChild(text290);

            paragraph94.AppendChild(paragraphProperties78);
            paragraph94.AppendChild(run290);

            tableCell89.AppendChild(tableCellProperties89);
            tableCell89.AppendChild(paragraph94);

            tableRow33.AppendChild(tableCell86);
            tableRow33.AppendChild(tableCell87);
            tableRow33.AppendChild(tableCell88);
            tableRow33.AppendChild(tableCell89);

            TableRow tableRow34 = new TableRow() { RsidTableRowMarkRevision = "00C204AF", RsidTableRowAddition = "00C204AF", RsidTableRowProperties = "009E0479" };

            TableCell tableCell90 = new TableCell();

            TableCellProperties tableCellProperties90 = new TableCellProperties();
            TableCellWidth tableCellWidth90 = new TableCellWidth() { Width = "9345", Type = TableWidthUnitValues.Dxa };
            GridSpan gridSpan10 = new GridSpan() { Val = 4 };
            Shading shading9 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "BFBFBF", ThemeFill = ThemeColorValues.Background1, ThemeFillShade = "BF" };

            tableCellProperties90.AppendChild(tableCellWidth90);
            tableCellProperties90.AppendChild(gridSpan10);
            tableCellProperties90.AppendChild(shading9);

            Paragraph paragraph95 = new Paragraph() { RsidParagraphMarkRevision = "00C204AF", RsidParagraphAddition = "00C204AF", RsidParagraphProperties = "00C204AF", RsidRunAdditionDefault = "00C204AF" };

            ParagraphProperties paragraphProperties79 = new ParagraphProperties();

            ParagraphMarkRunProperties paragraphMarkRunProperties78 = new ParagraphMarkRunProperties();
            Bold bold28 = new Bold();
            paragraphMarkRunProperties78.AppendChild(bold28);

            paragraphProperties79.AppendChild(paragraphMarkRunProperties78);

            Run run295 = new Run() { RsidRunProperties = "00C204AF" };

            RunProperties runProperties271 = new RunProperties();
            Bold bold29 = new Bold();

            runProperties271.AppendChild(bold29);
            Text text295 = new Text();
            text295.Text = "Mínimo";

            run295.AppendChild(runProperties271);
            run295.AppendChild(text295);

            paragraph95.AppendChild(paragraphProperties79);
            paragraph95.AppendChild(run295);

            tableCell90.AppendChild(tableCellProperties90);
            tableCell90.AppendChild(paragraph95);

            tableRow34.AppendChild(tableCell90);

            TableRow tableRow35 = new TableRow() { RsidTableRowAddition = "001B715B", RsidTableRowProperties = "0075392D" };

            TableCell tableCell91 = new TableCell();

            TableCellProperties tableCellProperties91 = new TableCellProperties();
            TableCellWidth tableCellWidth91 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties91.AppendChild(tableCellWidth91);

            Paragraph paragraph96 = new Paragraph() { RsidParagraphMarkRevision = "00C204AF", RsidParagraphAddition = "001B715B", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties80 = new ParagraphProperties();
            Justification justification67 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties79 = new ParagraphMarkRunProperties();

            paragraphProperties80.AppendChild(justification67);
            paragraphProperties80.AppendChild(paragraphMarkRunProperties79);

            Run run296 = new Run();

            RunProperties runProperties272 = new RunProperties();
            Text text296 = new Text();
            text296.Text = "Presión arterial sistólica";

            run296.AppendChild(runProperties272);
            run296.AppendChild(text296);

            paragraph96.AppendChild(paragraphProperties80);
            paragraph96.AppendChild(run296);

            tableCell91.AppendChild(tableCellProperties91);
            tableCell91.AppendChild(paragraph96);

            TableCell tableCell92 = new TableCell();

            TableCellProperties tableCellProperties92 = new TableCellProperties();
            TableCellWidth tableCellWidth92 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties92.AppendChild(tableCellWidth92);

            Paragraph paragraph97 = new Paragraph() { RsidParagraphAddition = "001B715B", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties81 = new ParagraphProperties();
            Justification justification68 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties80 = new ParagraphMarkRunProperties();

            paragraphProperties81.AppendChild(justification68);
            paragraphProperties81.AppendChild(paragraphMarkRunProperties80);

            Run run298 = new Run();

            RunProperties runProperties274 = new RunProperties();
            Text text298 = new Text();
            //Sistole total minimo
            text298.Text = "";

            run298.AppendChild(runProperties274);
            run298.AppendChild(text298);

            paragraph97.AppendChild(paragraphProperties81);
            paragraph97.AppendChild(run298);

            tableCell92.AppendChild(tableCellProperties92);
            tableCell92.AppendChild(paragraph97);

            TableCell tableCell93 = new TableCell();

            TableCellProperties tableCellProperties93 = new TableCellProperties();
            TableCellWidth tableCellWidth93 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties93.AppendChild(tableCellWidth93);

            Paragraph paragraph98 = new Paragraph() { RsidParagraphAddition = "001B715B", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties82 = new ParagraphProperties();
            Justification justification69 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties81 = new ParagraphMarkRunProperties();

            paragraphProperties82.AppendChild(justification69);
            paragraphProperties82.AppendChild(paragraphMarkRunProperties81);

            Run run305 = new Run();

            RunProperties runProperties281 = new RunProperties();
            Text text305 = new Text();
            //Sistole minimo dia
            text305.Text = report.SystolicDayMin.ToString();

            run305.AppendChild(runProperties281);
            run305.AppendChild(text305);

            paragraph98.AppendChild(paragraphProperties82);
            paragraph98.AppendChild(run305);

            tableCell93.AppendChild(tableCellProperties93);
            tableCell93.AppendChild(paragraph98);

            TableCell tableCell94 = new TableCell();

            TableCellProperties tableCellProperties94 = new TableCellProperties();
            TableCellWidth tableCellWidth94 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };

            tableCellProperties94.AppendChild(tableCellWidth94);

            Paragraph paragraph99 = new Paragraph() { RsidParagraphAddition = "001B715B", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties83 = new ParagraphProperties();
            Justification justification70 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties82 = new ParagraphMarkRunProperties();

            paragraphProperties83.AppendChild(justification70);
            paragraphProperties83.AppendChild(paragraphMarkRunProperties82);

            Run run312 = new Run();

            RunProperties runProperties288 = new RunProperties();
            Text text312 = new Text();
            //Sistole minimo noche
            text312.Text = report.SystolicNightMin.ToString();

            run312.AppendChild(runProperties288);
            run312.AppendChild(text312);

            paragraph99.AppendChild(paragraphProperties83);
            paragraph99.AppendChild(run312);

            tableCell94.AppendChild(tableCellProperties94);
            tableCell94.AppendChild(paragraph99);

            tableRow35.AppendChild(tableCell91);
            tableRow35.AppendChild(tableCell92);
            tableRow35.AppendChild(tableCell93);
            tableRow35.AppendChild(tableCell94);

            TableRow tableRow36 = new TableRow() { RsidTableRowAddition = "001B715B", RsidTableRowProperties = "00653833" };

            TableCell tableCell95 = new TableCell();

            TableCellProperties tableCellProperties95 = new TableCellProperties();
            TableCellWidth tableCellWidth95 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            TableCellBorders tableCellBorders22 = new TableCellBorders();
            BottomBorder bottomBorder20 = new BottomBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };

            tableCellBorders22.AppendChild(bottomBorder20);

            tableCellProperties95.AppendChild(tableCellWidth95);
            tableCellProperties95.AppendChild(tableCellBorders22);

            Paragraph paragraph100 = new Paragraph() { RsidParagraphAddition = "001B715B", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties84 = new ParagraphProperties();
            Justification justification71 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties83 = new ParagraphMarkRunProperties();

            paragraphProperties84.AppendChild(justification71);
            paragraphProperties84.AppendChild(paragraphMarkRunProperties83);

            Run run317 = new Run();

            RunProperties runProperties293 = new RunProperties();
            Text text317 = new Text();
            text317.Text = "Presión arterial diastólica";

            run317.AppendChild(runProperties293);
            run317.AppendChild(text317);

            paragraph100.AppendChild(paragraphProperties84);
            paragraph100.AppendChild(run317);

            tableCell95.AppendChild(tableCellProperties95);
            tableCell95.AppendChild(paragraph100);

            TableCell tableCell96 = new TableCell();

            TableCellProperties tableCellProperties96 = new TableCellProperties();
            TableCellWidth tableCellWidth96 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            TableCellBorders tableCellBorders23 = new TableCellBorders();
            BottomBorder bottomBorder21 = new BottomBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };

            tableCellBorders23.AppendChild(bottomBorder21);

            tableCellProperties96.AppendChild(tableCellWidth96);
            tableCellProperties96.AppendChild(tableCellBorders23);

            Paragraph paragraph101 = new Paragraph() { RsidParagraphAddition = "001B715B", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties85 = new ParagraphProperties();
            Justification justification72 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties84 = new ParagraphMarkRunProperties();

            paragraphProperties85.AppendChild(justification72);
            paragraphProperties85.AppendChild(paragraphMarkRunProperties84);

            Run run318 = new Run();

            RunProperties runProperties294 = new RunProperties();
            Text text318 = new Text();
            //Diastolica minima total
            text318.Text = "";

            run318.AppendChild(runProperties294);
            run318.AppendChild(text318);

            paragraph101.AppendChild(paragraphProperties85);
            paragraph101.AppendChild(run318);

            tableCell96.AppendChild(tableCellProperties96);
            tableCell96.AppendChild(paragraph101);

            TableCell tableCell97 = new TableCell();

            TableCellProperties tableCellProperties97 = new TableCellProperties();
            TableCellWidth tableCellWidth97 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            TableCellBorders tableCellBorders24 = new TableCellBorders();
            BottomBorder bottomBorder22 = new BottomBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };

            tableCellBorders24.AppendChild(bottomBorder22);

            tableCellProperties97.AppendChild(tableCellWidth97);
            tableCellProperties97.AppendChild(tableCellBorders24);

            Paragraph paragraph102 = new Paragraph() { RsidParagraphAddition = "001B715B", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties86 = new ParagraphProperties();
            Justification justification73 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties85 = new ParagraphMarkRunProperties();

            paragraphProperties86.AppendChild(justification73);
            paragraphProperties86.AppendChild(paragraphMarkRunProperties85);

            Run run325 = new Run();

            RunProperties runProperties301 = new RunProperties();
            Text text325 = new Text();
            //Diastolica minima dia
            text325.Text = report.DiastolicDayMin.ToString();

            run325.AppendChild(runProperties301);
            run325.AppendChild(text325);

            paragraph102.AppendChild(paragraphProperties86);
            paragraph102.AppendChild(run325);

            tableCell97.AppendChild(tableCellProperties97);
            tableCell97.AppendChild(paragraph102);

            TableCell tableCell98 = new TableCell();

            TableCellProperties tableCellProperties98 = new TableCellProperties();
            TableCellWidth tableCellWidth98 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };

            TableCellBorders tableCellBorders25 = new TableCellBorders();
            BottomBorder bottomBorder23 = new BottomBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };

            tableCellBorders25.AppendChild(bottomBorder23);

            tableCellProperties98.AppendChild(tableCellWidth98);
            tableCellProperties98.AppendChild(tableCellBorders25);

            Paragraph paragraph103 = new Paragraph() { RsidParagraphAddition = "001B715B", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties87 = new ParagraphProperties();
            Justification justification74 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties86 = new ParagraphMarkRunProperties();

            paragraphProperties87.AppendChild(justification74);
            paragraphProperties87.AppendChild(paragraphMarkRunProperties86);

            Run run332 = new Run();

            RunProperties runProperties308 = new RunProperties();
            Text text332 = new Text();
            //Diastolica minima noche
            text332.Text = report.DiastolicNightMin.ToString();

            run332.AppendChild(runProperties308);
            run332.AppendChild(text332);

            paragraph103.AppendChild(paragraphProperties87);
            paragraph103.AppendChild(run332);

            tableCell98.AppendChild(tableCellProperties98);
            tableCell98.AppendChild(paragraph103);

            tableRow36.AppendChild(tableCell95);
            tableRow36.AppendChild(tableCell96);
            tableRow36.AppendChild(tableCell97);
            tableRow36.AppendChild(tableCell98);

            TableRow tableRow37 = new TableRow() { RsidTableRowAddition = "001B715B", RsidTableRowProperties = "00653833" };

            TableCell tableCell99 = new TableCell();

            TableCellProperties tableCellProperties99 = new TableCellProperties();
            TableCellWidth tableCellWidth99 = new TableCellWidth() { Width = "9345", Type = TableWidthUnitValues.Dxa };
            GridSpan gridSpan11 = new GridSpan() { Val = 4 };

            TableCellBorders tableCellBorders26 = new TableCellBorders();
            LeftBorder leftBorder21 = new LeftBorder() { Val = BorderValues.Nil };
            BottomBorder bottomBorder24 = new BottomBorder() { Val = BorderValues.Nil };
            RightBorder rightBorder20 = new RightBorder() { Val = BorderValues.Nil };

            tableCellBorders26.AppendChild(leftBorder21);
            tableCellBorders26.AppendChild(bottomBorder24);
            tableCellBorders26.AppendChild(rightBorder20);

            tableCellProperties99.AppendChild(tableCellWidth99);
            tableCellProperties99.AppendChild(gridSpan11);
            tableCellProperties99.AppendChild(tableCellBorders26);

            Paragraph paragraph104 = new Paragraph() { RsidParagraphAddition = "001B715B", RsidParagraphProperties = "00653833", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties88 = new ParagraphProperties();

            ParagraphMarkRunProperties paragraphMarkRunProperties87 = new ParagraphMarkRunProperties();

            paragraphProperties88.AppendChild(paragraphMarkRunProperties87);

            Run run337 = new Run();

            RunProperties runProperties313 = new RunProperties();
            Text text337 = new Text();
            text337.Text = "Valores por encima del límite";

            run337.AppendChild(runProperties313);
            run337.AppendChild(text337);

            paragraph104.AppendChild(paragraphProperties88);
            paragraph104.AppendChild(run337);

            tableCell99.AppendChild(tableCellProperties99);
            tableCell99.AppendChild(paragraph104);

            tableRow37.AppendChild(tableCell99);

            TableRow tableRow38 = new TableRow() { RsidTableRowAddition = "001B715B", RsidTableRowProperties = "00653833" };

            TableCell tableCell100 = new TableCell();

            TableCellProperties tableCellProperties100 = new TableCellProperties();
            TableCellWidth tableCellWidth100 = new TableCellWidth() { Width = "9345", Type = TableWidthUnitValues.Dxa };
            GridSpan gridSpan12 = new GridSpan() { Val = 4 };

            TableCellBorders tableCellBorders27 = new TableCellBorders();
            TopBorder topBorder19 = new TopBorder() { Val = BorderValues.Nil };
            LeftBorder leftBorder22 = new LeftBorder() { Val = BorderValues.Nil };
            RightBorder rightBorder21 = new RightBorder() { Val = BorderValues.Nil };

            tableCellBorders27.AppendChild(topBorder19);
            tableCellBorders27.AppendChild(leftBorder22);
            tableCellBorders27.AppendChild(rightBorder21);

            tableCellProperties100.AppendChild(tableCellWidth100);
            tableCellProperties100.AppendChild(gridSpan12);
            tableCellProperties100.AppendChild(tableCellBorders27);

            Paragraph paragraph105 = new Paragraph() { RsidParagraphAddition = "001B715B", RsidParagraphProperties = "0075392D", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties89 = new ParagraphProperties();
            Justification justification75 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties88 = new ParagraphMarkRunProperties();

            paragraphProperties89.AppendChild(justification75);
            paragraphProperties89.AppendChild(paragraphMarkRunProperties88);

            paragraph105.AppendChild(paragraphProperties89);

            tableCell100.AppendChild(tableCellProperties100);
            tableCell100.AppendChild(paragraph105);

            tableRow38.AppendChild(tableCell100);

            TableRow tableRow39 = new TableRow() { RsidTableRowMarkRevision = "00653833", RsidTableRowAddition = "001B715B", RsidTableRowProperties = "00C31833" };

            TableCell tableCell101 = new TableCell();

            TableCellProperties tableCellProperties101 = new TableCellProperties();
            TableCellWidth tableCellWidth101 = new TableCellWidth() { Width = "9345", Type = TableWidthUnitValues.Dxa };
            GridSpan gridSpan13 = new GridSpan() { Val = 4 };
            Shading shading10 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "BFBFBF", ThemeFill = ThemeColorValues.Background1, ThemeFillShade = "BF" };

            tableCellProperties101.AppendChild(tableCellWidth101);
            tableCellProperties101.AppendChild(gridSpan13);
            tableCellProperties101.AppendChild(shading10);

            Paragraph paragraph106 = new Paragraph() { RsidParagraphMarkRevision = "00653833", RsidParagraphAddition = "001B715B", RsidParagraphProperties = "00653833", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties90 = new ParagraphProperties();

            ParagraphMarkRunProperties paragraphMarkRunProperties89 = new ParagraphMarkRunProperties();
            Bold bold30 = new Bold();

            paragraphMarkRunProperties89.AppendChild(bold30);

            paragraphProperties90.AppendChild(paragraphMarkRunProperties89);

            Run run338 = new Run() { RsidRunProperties = "00653833" };

            RunProperties runProperties314 = new RunProperties();
            Bold bold31 = new Bold();

            runProperties314.AppendChild(bold31);
            Text text338 = new Text();
            text338.Text = "Dipping";

            run338.AppendChild(runProperties314);
            run338.AppendChild(text338);

            paragraph106.AppendChild(paragraphProperties90);
            paragraph106.AppendChild(run338);

            tableCell101.AppendChild(tableCellProperties101);
            tableCell101.AppendChild(paragraph106);

            tableRow39.AppendChild(tableCell101);

            TableRow tableRow40 = new TableRow() { RsidTableRowMarkRevision = "00653833", RsidTableRowAddition = "001B715B", RsidTableRowProperties = "00A6498A" };

            TableCell tableCell102 = new TableCell();

            TableCellProperties tableCellProperties102 = new TableCellProperties();
            TableCellWidth tableCellWidth102 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties102.AppendChild(tableCellWidth102);

            Paragraph paragraph107 = new Paragraph() { RsidParagraphMarkRevision = "00653833", RsidParagraphAddition = "001B715B", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties91 = new ParagraphProperties();
            Justification justification76 = new Justification() { Val = JustificationValues.Center };

            paragraphProperties91.AppendChild(justification76);
            Run run339 = new Run() { RsidRunProperties = "00653833" };
            Text text339 = new Text();
            text339.Text = "Presión arterial Sistólica";

            run339.AppendChild(text339);

            paragraph107.AppendChild(paragraphProperties91);
            paragraph107.AppendChild(run339);

            tableCell102.AppendChild(tableCellProperties102);
            tableCell102.AppendChild(paragraph107);

            TableCell tableCell103 = new TableCell();

            TableCellProperties tableCellProperties103 = new TableCellProperties();
            TableCellWidth tableCellWidth103 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties103.AppendChild(tableCellWidth103);

            Paragraph paragraph108 = new Paragraph() { RsidParagraphMarkRevision = "00653833", RsidParagraphAddition = "001B715B", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties92 = new ParagraphProperties();
            Justification justification77 = new Justification() { Val = JustificationValues.Center };

            paragraphProperties92.AppendChild(justification77);

            Run run340 = new Run();
            Text text340 = new Text();
            //Dipping sistolica
            decimal dippingSys = Math.Round(((decimal)(report.SystolicDayAvg.Value - report.SystolicNightAvg.Value) / report.SystolicDayAvg.Value) * 100, 2);
            text340.Text = dippingSys.ToString();

            run340.AppendChild(text340);

            paragraph108.AppendChild(paragraphProperties92);
            paragraph108.AppendChild(run340);

            tableCell103.AppendChild(tableCellProperties103);
            tableCell103.AppendChild(paragraph108);

            TableCell tableCell104 = new TableCell();

            TableCellProperties tableCellProperties104 = new TableCellProperties();
            TableCellWidth tableCellWidth104 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties104.AppendChild(tableCellWidth104);

            Paragraph paragraph109 = new Paragraph() { RsidParagraphMarkRevision = "00653833", RsidParagraphAddition = "001B715B", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties93 = new ParagraphProperties();
            Justification justification78 = new Justification() { Val = JustificationValues.Center };

            paragraphProperties93.AppendChild(justification78);

            Run run341 = new Run();
            Text text341 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            text341.Text = "";

            run341.AppendChild(text341);

            paragraph109.AppendChild(paragraphProperties93);
            paragraph109.AppendChild(run341);

            tableCell104.AppendChild(tableCellProperties104);
            tableCell104.AppendChild(paragraph109);

            TableCell tableCell105 = new TableCell();

            TableCellProperties tableCellProperties105 = new TableCellProperties();
            TableCellWidth tableCellWidth105 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };

            tableCellProperties105.AppendChild(tableCellWidth105);

            Paragraph paragraph110 = new Paragraph() { RsidParagraphMarkRevision = "00653833", RsidParagraphAddition = "001B715B", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties94 = new ParagraphProperties();
            Justification justification79 = new Justification() { Val = JustificationValues.Center };

            paragraphProperties94.AppendChild(justification79);

            Run run344 = new Run();
            Text text344 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            text344.Text = "";

            run344.AppendChild(text344);

            paragraph110.AppendChild(paragraphProperties94);
            paragraph110.AppendChild(run344);

            tableCell105.AppendChild(tableCellProperties105);
            tableCell105.AppendChild(paragraph110);

            tableRow40.AppendChild(tableCell102);
            tableRow40.AppendChild(tableCell103);
            tableRow40.AppendChild(tableCell104);
            tableRow40.AppendChild(tableCell105);

            TableRow tableRow41 = new TableRow() { RsidTableRowMarkRevision = "00653833", RsidTableRowAddition = "001B715B", RsidTableRowProperties = "00A6498A" };

            TableCell tableCell106 = new TableCell();

            TableCellProperties tableCellProperties106 = new TableCellProperties();
            TableCellWidth tableCellWidth106 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties106.AppendChild(tableCellWidth106);

            Paragraph paragraph111 = new Paragraph() { RsidParagraphMarkRevision = "00653833", RsidParagraphAddition = "001B715B", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties95 = new ParagraphProperties();
            Justification justification80 = new Justification() { Val = JustificationValues.Center };

            paragraphProperties95.AppendChild(justification80);

            Run run347 = new Run() { RsidRunProperties = "00653833" };
            LastRenderedPageBreak lastRenderedPageBreak1 = new LastRenderedPageBreak();
            Text text347 = new Text();
            text347.Text = "Presión arterial diastólica";

            run347.AppendChild(lastRenderedPageBreak1);
            run347.AppendChild(text347);

            paragraph111.AppendChild(paragraphProperties95);
            paragraph111.AppendChild(run347);

            tableCell106.AppendChild(tableCellProperties106);
            tableCell106.AppendChild(paragraph111);

            TableCell tableCell107 = new TableCell();

            TableCellProperties tableCellProperties107 = new TableCellProperties();
            TableCellWidth tableCellWidth107 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties107.AppendChild(tableCellWidth107);

            Paragraph paragraph112 = new Paragraph() { RsidParagraphMarkRevision = "00653833", RsidParagraphAddition = "001B715B", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties96 = new ParagraphProperties();
            Justification justification81 = new Justification() { Val = JustificationValues.Center };

            paragraphProperties96.AppendChild(justification81);

            Run run348 = new Run();
            Text text348 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            //Dipping sistolica
            decimal dippingDias = Math.Round(((decimal)(report.DiastolicDayAvg.Value - report.DiastolicNightAvg.Value) / report.DiastolicDayAvg.Value) * 100, 2);
            text348.Text = dippingDias.ToString();

            run348.AppendChild(text348);

            paragraph112.AppendChild(paragraphProperties96);
            paragraph112.AppendChild(run348);

            tableCell107.AppendChild(tableCellProperties107);
            tableCell107.AppendChild(paragraph112);

            TableCell tableCell108 = new TableCell();

            TableCellProperties tableCellProperties108 = new TableCellProperties();
            TableCellWidth tableCellWidth108 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };

            tableCellProperties108.AppendChild(tableCellWidth108);

            Paragraph paragraph113 = new Paragraph() { RsidParagraphMarkRevision = "00653833", RsidParagraphAddition = "001B715B", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties97 = new ParagraphProperties();
            Justification justification82 = new Justification() { Val = JustificationValues.Center };

            paragraphProperties97.AppendChild(justification82);

            Run run351 = new Run();
            Text text351 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            text351.Text = "";

            run351.AppendChild(text351);

            paragraph113.AppendChild(paragraphProperties97);
            paragraph113.AppendChild(run351);

            tableCell108.AppendChild(tableCellProperties108);
            tableCell108.AppendChild(paragraph113);

            TableCell tableCell109 = new TableCell();

            TableCellProperties tableCellProperties109 = new TableCellProperties();
            TableCellWidth tableCellWidth109 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };

            tableCellProperties109.AppendChild(tableCellWidth109);

            Paragraph paragraph114 = new Paragraph() { RsidParagraphMarkRevision = "00653833", RsidParagraphAddition = "001B715B", RsidParagraphProperties = "004D2B75", RsidRunAdditionDefault = "001B715B" };

            ParagraphProperties paragraphProperties98 = new ParagraphProperties();
            Justification justification83 = new Justification() { Val = JustificationValues.Center };

            paragraphProperties98.AppendChild(justification83);

            Run run356 = new Run();
            Text text356 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            text356.Text = "";

            run356.AppendChild(text356);

            paragraph114.AppendChild(paragraphProperties98);
            paragraph114.AppendChild(run356);

            tableCell109.AppendChild(tableCellProperties109);
            tableCell109.AppendChild(paragraph114);

            tableRow41.AppendChild(tableCell106);
            tableRow41.AppendChild(tableCell107);
            tableRow41.AppendChild(tableCell108);
            tableRow41.AppendChild(tableCell109);

            table2.AppendChild(tableProperties2);
            table2.AppendChild(tableGrid2);
            table2.AppendChild(tableRow13);
            table2.AppendChild(tableRow14);
            table2.AppendChild(tableRow15);
            table2.AppendChild(tableRow16);
            table2.AppendChild(tableRow17);
            table2.AppendChild(tableRow18);
            table2.AppendChild(tableRow19);
            table2.AppendChild(tableRow20);
            table2.AppendChild(tableRow21);
            table2.AppendChild(tableRow22);
            table2.AppendChild(tableRow23);
            table2.AppendChild(tableRow24);
            table2.AppendChild(tableRow25);
            table2.AppendChild(tableRow26);
            table2.AppendChild(tableRow27);
            table2.AppendChild(tableRow28);
            table2.AppendChild(tableRow29);
            table2.AppendChild(tableRow30);
            table2.AppendChild(tableRow31);
            table2.AppendChild(tableRow32);
            table2.AppendChild(tableRow33);
            table2.AppendChild(tableRow34);
            table2.AppendChild(tableRow35);
            table2.AppendChild(tableRow36);
            table2.AppendChild(tableRow37);
            table2.AppendChild(tableRow38);
            table2.AppendChild(tableRow39);
            table2.AppendChild(tableRow40);
            table2.AppendChild(tableRow41);

            Paragraph paragraph115 = new Paragraph() { RsidParagraphMarkRevision = "00653833", RsidParagraphAddition = "00A6498A", RsidParagraphProperties = "009C2A39", RsidRunAdditionDefault = "00653833" };

            Run run361 = new Run() { RsidRunProperties = "00653833" };
            Text text361 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            text361.Text = "Dipping <0% Invertido; <10% Non-Dipper; <20% Normal; >=20% Extremos";

            run361.AppendChild(text361);

            paragraph115.AppendChild(run361);

            tableCell20.AppendChild(tableCellProperties20);
            tableCell20.AppendChild(paragraph25);
            tableCell20.AppendChild(table2);
            tableCell20.AppendChild(paragraph115);

            tableRow12.AppendChild(tableRowProperties3);
            tableRow12.AppendChild(tableCell20);

            return tableRow12;
        }

        private void GenerateCoverHeader(HeaderPart coverHeaderPart)
        {
            Header coverHeader = new Header();

            Paragraph paragraph59 = new Paragraph() { RsidParagraphAddition = "00AA46B2", RsidRunAdditionDefault = "00F4078F" };

            ParagraphProperties paragraphProperties43 = new ParagraphProperties();
            ParagraphStyleId paragraphStyleId3 = new ParagraphStyleId() { Val = "Encabezado" };

            ParagraphMarkRunProperties paragraphMarkRunProperties42 = new ParagraphMarkRunProperties();


            paragraphProperties43.AppendChild(paragraphStyleId3);
            paragraphProperties43.AppendChild(paragraphMarkRunProperties42);

            Run run47 = new Run();

            RunProperties runProperties24 = new RunProperties();
            Text text43 = new Text();
            text43.Text = "Hospital de Clínicas";

            run47.AppendChild(runProperties24);
            run47.AppendChild(text43);

            Run run48 = new Run();

            RunProperties runProperties25 = new RunProperties();

            TabChar tabChar1 = new TabChar();
            Text text44 = new Text();
            text44.Text = "Informe de Hipertensión Arterial";

            run48.AppendChild(runProperties25);
            run48.AppendChild(tabChar1);
            run48.AppendChild(text44);

            Run run49 = new Run();

            RunProperties runProperties26 = new RunProperties();

            TabChar tabChar2 = new TabChar();
            Text text45 = new Text();
            text45.Text = "     " + DateTime.Now.ToString(ConfigurationManager.AppSettings["ShortDateString"]);

            run49.AppendChild(runProperties26);
            run49.AppendChild(tabChar2);
            run49.AppendChild(text45);

            paragraph59.AppendChild(paragraphProperties43);
            paragraph59.AppendChild(run47);
            paragraph59.AppendChild(run48);
            paragraph59.AppendChild(run49);

            coverHeader.AppendChild(paragraph59);

            coverHeaderPart.Header = coverHeader;
        }

        private void GenerateHeader(HeaderPart headerPart1, Patient patient)
        {
            Header header1 = new Header();
            Paragraph paragraph59 = new Paragraph() { RsidParagraphAddition = "00AA46B2", RsidRunAdditionDefault = "00F4078F" };

            ParagraphProperties paragraphProperties43 = new ParagraphProperties();
            ParagraphStyleId paragraphStyleId3 = new ParagraphStyleId() { Val = "Encabezado" };

            ParagraphMarkRunProperties paragraphMarkRunProperties42 = new ParagraphMarkRunProperties();


            paragraphProperties43.AppendChild(paragraphStyleId3);
            paragraphProperties43.AppendChild(paragraphMarkRunProperties42);

            Run run47 = new Run();

            RunProperties runProperties24 = new RunProperties();
            Text text43 = new Text();
            text43.Text = "Hospital de Clínicas";

            run47.AppendChild(runProperties24);
            run47.AppendChild(text43);

            Run run48 = new Run();

            RunProperties runProperties25 = new RunProperties();

            TabChar tabChar1 = new TabChar();
            Text text44 = new Text();
            text44.Text = "Informe de Hipertensión Arterial";

            run48.AppendChild(runProperties25);
            run48.AppendChild(tabChar1);
            run48.AppendChild(text44);

            Run run49 = new Run();

            RunProperties runProperties26 = new RunProperties();

            TabChar tabChar2 = new TabChar();
            Text text45 = new Text();
            text45.Text = "     " + DateTime.Now.ToString(ConfigurationManager.AppSettings["ShortDateString"]);

            run49.AppendChild(runProperties26);
            run49.AppendChild(tabChar2);
            run49.AppendChild(text45);

            paragraph59.AppendChild(paragraphProperties43);
            paragraph59.AppendChild(run47);
            paragraph59.AppendChild(run48);
            paragraph59.AppendChild(run49);

            Paragraph paragraph60 = new Paragraph() { RsidParagraphAddition = "00F4078F", RsidRunAdditionDefault = "00F4078F" };

            ParagraphProperties paragraphProperties44 = new ParagraphProperties();
            ParagraphStyleId paragraphStyleId4 = new ParagraphStyleId() { Val = "Encabezado" };

            ParagraphMarkRunProperties paragraphMarkRunProperties43 = new ParagraphMarkRunProperties();

            paragraphProperties44.AppendChild(paragraphStyleId4);
            paragraphProperties44.AppendChild(paragraphMarkRunProperties43);

            paragraph60.AppendChild(paragraphProperties44);

            Paragraph paragraph61 = new Paragraph() { RsidParagraphMarkRevision = "00CC4E8A", RsidParagraphAddition = "00CC4E8A", RsidRunAdditionDefault = "00CC4E8A" };

            ParagraphProperties paragraphProperties45 = new ParagraphProperties();
            ParagraphStyleId paragraphStyleId5 = new ParagraphStyleId() { Val = "Encabezado" };

            ParagraphMarkRunProperties paragraphMarkRunProperties44 = new ParagraphMarkRunProperties();

            paragraphProperties45.AppendChild(paragraphStyleId5);
            paragraphProperties45.AppendChild(paragraphMarkRunProperties44);

            Run run50 = new Run();

            RunProperties runProperties27 = new RunProperties();

            Picture picture1 = new Picture();

            V.Shapetype shapetype1 = new V.Shapetype() { Id = "_x0000_t32", CoordinateSize = "21600,21600", Oned = true, Filled = false, OptionalNumber = 32, EdgePath = "m,l21600,21600e" };
            V.Path path1 = new V.Path() { AllowFill = false, ShowArrowhead = true, ConnectionPointType = Ovml.ConnectValues.None };
            Ovml.Lock lock1 = new Ovml.Lock() { Extension = V.ExtensionHandlingBehaviorValues.Edit, ShapeType = true };

            shapetype1.AppendChild(path1);
            shapetype1.AppendChild(lock1);
            V.Shape shape1 = new V.Shape() { Id = "_x0000_s3073", Style = "position:absolute;margin-left:-43.45pt;margin-top:13.25pt;width:557.65pt;height:.05pt;z-index:251658240", ConnectorType = Ovml.ConnectorValues.Straight, Type = "#_x0000_t32" };

            picture1.AppendChild(shapetype1);
            picture1.AppendChild(shape1);

            run50.AppendChild(runProperties27);
            run50.AppendChild(picture1);

            Run run51 = new Run();

            RunProperties runProperties28 = new RunProperties();
            Text text46 = new Text();
            text46.Text = "Paciente: ";

            run51.AppendChild(runProperties28);
            run51.AppendChild(text46);

            Run run52 = new Run();

            RunProperties runProperties29 = new RunProperties();
            Text text47 = new Text();
            text47.Text = patient.RegisterNumber;

            run52.AppendChild(runProperties29);
            run52.AppendChild(text47);

            Run run56 = new Run();

            RunProperties runProperties33 = new RunProperties();
            TabChar tabChar3 = new TabChar();
            Text text51 = new Text();
            text51.Text = patient.Names + " " + patient.Surnames;

            run56.AppendChild(runProperties33);
            run56.AppendChild(tabChar3);
            run56.AppendChild(text51);

            Run run57 = new Run();

            RunProperties runProperties34 = new RunProperties();
            TabChar tabChar4 = new TabChar();
            Text text52 = new Text();
            text52.Text = patient.DocumentId;

            run57.AppendChild(runProperties34);
            run57.AppendChild(tabChar4);
            run57.AppendChild(text52);

            paragraph61.AppendChild(paragraphProperties45);
            paragraph61.AppendChild(run50);
            paragraph61.AppendChild(run51);
            paragraph61.AppendChild(run52);
            paragraph61.AppendChild(run56);
            paragraph61.AppendChild(run57);

            header1.AppendChild(paragraph59);
            header1.AppendChild(paragraph60);
            header1.AppendChild(paragraph61);

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
