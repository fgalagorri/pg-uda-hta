using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using DataAccess;
using DocumentFormat.OpenXml.Packaging;
using Ap = DocumentFormat.OpenXml.ExtendedProperties;
using Vt = DocumentFormat.OpenXml.VariantTypes;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
using A = DocumentFormat.OpenXml.Drawing;



namespace BussinessLogic
{
    public class InvestigationManagement
    {
        public ICollection<Investigation> listInvestigations()
        {
            UdaHtaDataAccess uda = new UdaHtaDataAccess();
            return uda.listInvestigations();
        }

        public int CreateInvestigation(string name, DateTime creationDate)
        {
            UdaHtaDataAccess uda = new UdaHtaDataAccess();
            return uda.insertInvestigation(name, creationDate);
        }

        public void EditInvestigation(string name, DateTime creationDate)
        {

        }

        public void AddReportToInvestigation(Report report, int idInvestigation)
        {
            UdaHtaDataAccess uda = new UdaHtaDataAccess();
            uda.addReportToInvestigation(report.UdaId.Value, report.Patient.UdaId.Value, idInvestigation);
        }

        public void DeleteReportFromInvestigation(Report report, int idInvestigation)
        {
            UdaHtaDataAccess uda = new UdaHtaDataAccess();
            uda.deleteReportFromInvestigation(report.UdaId.Value, report.Patient.UdaId.Value, idInvestigation);
        }


        #region Export Investigation
        public void ExportInvestigation(Investigation investigation, string filePath)
        {
            //Crear una planilla excel
            using (SpreadsheetDocument document = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.MacroEnabledWorkbook))
            {
                //Generar workbook
                WorkbookPart workbookPart1 = document.AddWorkbookPart();
                Workbook workbook1 = new Workbook();
                workbook1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
                WorkbookProperties workbookProperties1 = new WorkbookProperties() { CodeName = "ThisWorkbook", DefaultThemeVersion = (UInt32Value)124226U };

                BookViews bookViews1 = new BookViews();
                WorkbookView workbookView1 = new WorkbookView() { XWindow = 240, YWindow = 45, WindowWidth = (UInt32Value)20115U, WindowHeight = (UInt32Value)7995U, ActiveTab = (UInt32Value)2U };

                bookViews1.Append(workbookView1);

                Sheets sheets1 = new Sheets();
                Sheet sheet1 = new Sheet() { Name = "Perfil", SheetId = (UInt32Value)1U, Id = "rId1" };
                Sheet sheet2 = new Sheet() { Name = "Reportes", SheetId = (UInt32Value)2U, Id = "rId2" };
                Sheet sheet3 = new Sheet() { Name = "Plantilla", SheetId = (UInt32Value)3U, Id = "rId3" };

                sheets1.Append(sheet1);
                sheets1.Append(sheet2);
                sheets1.Append(sheet3);

                int i = 4;
                foreach (var report in investigation.LReports)
                {
                    string id = "rep" + report.UdaId.ToString();
                    string name = "Reporte " + report.UdaId.ToString();
                    UInt32Value sheetId = (uint) i;
                    Sheet sheetReport = new Sheet() { Name = name, SheetId = sheetId, Id = id };

                    sheets1.Append(sheetReport);
                    i++;
                }

                CalculationProperties calculationProperties1 = new CalculationProperties() { CalculationId = (UInt32Value)125725U };

                workbook1.Append(workbookProperties1);
                workbook1.Append(bookViews1);
                workbook1.Append(sheets1);
                workbook1.Append(calculationProperties1);

                workbookPart1.Workbook = workbook1;

                WorksheetPart worksheetPart1 = workbookPart1.AddNewPart<WorksheetPart>("rId3");
                GenerateWorksheetPart1Content(worksheetPart1);

                VbaProjectPart vbaProjectPart1 = workbookPart1.AddNewPart<VbaProjectPart>("rId7");
                GenerateVbaProjectPart1Content(vbaProjectPart1);

                WorksheetPart worksheetPart2 = workbookPart1.AddNewPart<WorksheetPart>("rId2");
                GenerateWorksheetPart2Content(worksheetPart2, investigation.LReports);

                WorksheetPart worksheetPart3 = workbookPart1.AddNewPart<WorksheetPart>("rId1");
                GenerateWorksheetPart3Content(worksheetPart3, investigation);

                foreach (var r in investigation.LReports)
                {
                    string id = "rep" + r.UdaId.ToString();
                    WorksheetPart worksheetPart = workbookPart1.AddNewPart<WorksheetPart>(id);
                    GenerateWorksheetPartContent(worksheetPart, r.Measures, id);
                }

                SpreadsheetPrinterSettingsPart spreadsheetPrinterSettingsPart1 = worksheetPart3.AddNewPart<SpreadsheetPrinterSettingsPart>("rId1");
                GenerateSpreadsheetPrinterSettingsPart1Content(spreadsheetPrinterSettingsPart1);

                WorkbookStylesPart workbookStylesPart1 = workbookPart1.AddNewPart<WorkbookStylesPart>("rId5");
                GenerateWorkbookStylesPart1Content(workbookStylesPart1);

            }
        }

        // Generar contenido de worksheet1 / Plantilla
        private void GenerateWorksheetPart1Content(WorksheetPart worksheetPart1)
        {
            Worksheet worksheet1 = new Worksheet();
            worksheet1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            SheetProperties sheetProperties1 = new SheetProperties() { CodeName = "Hoja3" };
            SheetDimension sheetDimension1 = new SheetDimension() { Reference = "A1:F2" };

            SheetViews sheetViews1 = new SheetViews();

            SheetView sheetView1 = new SheetView() { WorkbookViewId = (UInt32Value)0U };
            Selection selection1 = new Selection() { ActiveCell = "A2", SequenceOfReferences = new ListValue<StringValue>() { InnerText = "A2" } };

            sheetView1.Append(selection1);

            sheetViews1.Append(sheetView1);
            SheetFormatProperties sheetFormatProperties1 = new SheetFormatProperties() { BaseColumnWidth = (UInt32Value)10U, DefaultRowHeight = 15D };

            Columns columns1 = new Columns();
            Column column1 = new Column() { Min = (UInt32Value)1U, Max = (UInt32Value)1U, Width = 12D, BestFit = true, CustomWidth = true };

            columns1.Append(column1);

            SheetData sheetData1 = new SheetData();

            Row row1 = new Row() { RowIndex = (UInt32Value)1U, Spans = new ListValue<StringValue>() { InnerText = "1:6" } };

            Cell cell1 = new Cell() { CellReference = "A1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValue1 = new CellValue();
            cellValue1.Text = "Fecha y hora";

            cell1.Append(cellValue1);

            Cell cell2 = new Cell() { CellReference = "B1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValue2 = new CellValue();
            cellValue2.Text = "Sistolica";

            cell2.Append(cellValue2);

            Cell cell3 = new Cell() { CellReference = "C1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValue3 = new CellValue();
            cellValue3.Text = "Diastolica";

            cell3.Append(cellValue3);

            Cell cell4 = new Cell() { CellReference = "D1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValue4 = new CellValue();
            cellValue4.Text = "Media";

            cell4.Append(cellValue4);

            Cell cell5 = new Cell() { CellReference = "E1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValue5 = new CellValue();
            cellValue5.Text = "FC";

            cell5.Append(cellValue5);
            Cell cell6 = new Cell() { CellReference = "F1", StyleIndex = (UInt32Value)1U };

            row1.Append(cell1);
            row1.Append(cell2);
            row1.Append(cell3);
            row1.Append(cell4);
            row1.Append(cell5);
            row1.Append(cell6);

            Row row2 = new Row() { RowIndex = (UInt32Value)2U, Spans = new ListValue<StringValue>() { InnerText = "1:6" } };
            Cell cell7 = new Cell() { CellReference = "B2", StyleIndex = (UInt32Value)3U };

            row2.Append(cell7);

            sheetData1.Append(row1);
            sheetData1.Append(row2);
            PageMargins pageMargins1 = new PageMargins() { Left = 0.7D, Right = 0.7D, Top = 0.75D, Bottom = 0.75D, Header = 0.3D, Footer = 0.3D };

            worksheet1.Append(sheetProperties1);
            worksheet1.Append(sheetDimension1);
            worksheet1.Append(sheetViews1);
            worksheet1.Append(sheetFormatProperties1);
            worksheet1.Append(columns1);
            worksheet1.Append(sheetData1);
            worksheet1.Append(pageMargins1);

            worksheetPart1.Worksheet = worksheet1;
        }

        // Generar contenido de worksheet1
        private void GenerateWorksheetPartContent(WorksheetPart worksheetPart1, ICollection<Measurement> measurements, string rId)
        {
            Worksheet worksheet1 = new Worksheet();
            worksheet1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
                            
            //SheetProperties sheetProperties1 = new SheetProperties() { CodeName = "Hoja3" };
            SheetDimension sheetDimension1 = new SheetDimension() { Reference = "A1:F100" };

            SheetViews sheetViews1 = new SheetViews();

            SheetView sheetView1 = new SheetView() { WorkbookViewId = (UInt32Value)0U };
            Selection selection1 = new Selection() { ActiveCell = "A1", SequenceOfReferences = new ListValue<StringValue>() { InnerText = "A1" } };

            sheetView1.Append(selection1);

            sheetViews1.Append(sheetView1);
            SheetFormatProperties sheetFormatProperties1 = new SheetFormatProperties() { BaseColumnWidth = (UInt32Value)10U, DefaultRowHeight = 15D };

            Columns columns1 = new Columns();
            Column column1 = new Column() { Min = (UInt32Value)1U, Max = (UInt32Value)1U, Width = 12D, BestFit = true, CustomWidth = true };

            columns1.Append(column1);

            SheetData sheetData1 = new SheetData();

            Row row1 = new Row() { RowIndex = (UInt32Value)1U, Spans = new ListValue<StringValue>() { InnerText = "1:6" } };

            Cell cell1 = new Cell() { CellReference = "A1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValue1 = new CellValue();
            cellValue1.Text = "Fecha y hora";

            cell1.Append(cellValue1);

            Cell cell2 = new Cell() { CellReference = "B1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValue2 = new CellValue();
            cellValue2.Text = "Sistolica";

            cell2.Append(cellValue2);

            Cell cell3 = new Cell() { CellReference = "C1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValue3 = new CellValue();
            cellValue3.Text = "Diastolica";

            cell3.Append(cellValue3);

            Cell cell4 = new Cell() { CellReference = "D1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValue4 = new CellValue();
            cellValue4.Text = "Media";

            cell4.Append(cellValue4);

            Cell cell5 = new Cell() { CellReference = "E1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValue5 = new CellValue();
            cellValue5.Text = "FC";

            cell5.Append(cellValue5);

            Cell cell6 = new Cell() { CellReference = "F1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValue6 = new CellValue();
            cellValue5.Text = "Durmiendo";

            cell6.Append(cellValue6);

            row1.Append(cell1);
            row1.Append(cell2);
            row1.Append(cell3);
            row1.Append(cell4);
            row1.Append(cell5);
            row1.Append(cell6);

            sheetData1.Append(row1);

            int i = 2;
            foreach (var measurement in measurements)
            {
                UInt32Value ui = (uint) i;
                Row row2 = new Row() { RowIndex = ui, Spans = new ListValue<StringValue>() { InnerText = "1:6" } };
                
                //Fecha y hora
                string cellRefDate = "A" + i;
                Cell cellDate = new Cell() { CellReference = cellRefDate, StyleIndex = (UInt32Value)1U };
                CellValue cellValueDate = new CellValue();
                cellValueDate.Text = measurement.Time.Value.ToString();
                cellDate.Append(cellValueDate);
                row2.Append(cellDate);

                //Sistolica
                string cellRefSys = "B" + i;
                Cell cellSys = new Cell() { CellReference = cellRefSys, StyleIndex = (UInt32Value)1U };
                CellValue cellValueSys = new CellValue();
                cellValueSys.Text = measurement.Systolic.Value.ToString();
                cellSys.Append(cellValueSys);
                row2.Append(cellSys);

                //Diastolica
                string cellRefDias = "C" + i;
                Cell cellDias = new Cell() { CellReference = cellRefDias, StyleIndex = (UInt32Value)1U };
                CellValue cellValueDias = new CellValue();
                cellValueDias.Text = measurement.Diastolic.Value.ToString();
                cellDias.Append(cellValueDias);
                row2.Append(cellDias);

                //Media
                string cellRefMiddle = "D" + i;
                Cell cellMiddle = new Cell() { CellReference = cellRefMiddle, StyleIndex = (UInt32Value)1U };
                CellValue cellValueMiddle = new CellValue();
                cellValueMiddle.Text = measurement.Middle.Value.ToString();
                cellMiddle.Append(cellValueMiddle);
                row2.Append(cellMiddle);

                //FC
                string cellRefHr = "E" + i;
                Cell cellHr = new Cell() { CellReference = cellRefHr, StyleIndex = (UInt32Value)1U };
                CellValue cellValueHr = new CellValue();
                cellValueHr.Text = measurement.HeartRate.Value.ToString();
                cellHr.Append(cellValueHr);
                row2.Append(cellHr);

                //Durmiendo
                string cellRefAsleep = "F" + i;
                Cell cellAsleep = new Cell() { CellReference = cellRefAsleep, StyleIndex = (UInt32Value)1U };
                CellValue cellValueAsleep = new CellValue();
                cellValueAsleep.Text = measurement.Asleep.Value.ToString();
                cellAsleep.Append(cellValueAsleep);
                row2.Append(cellAsleep);
            
                sheetData1.Append(row2);

                i++;
            }

            PageMargins pageMargins1 = new PageMargins() { Left = 0.7D, Right = 0.7D, Top = 0.75D, Bottom = 0.75D, Header = 0.3D, Footer = 0.3D };
            PageSetup pageSetup = new PageSetup() {Orientation = OrientationValues.Portrait, Id = rId};

            //worksheet1.Append(sheetProperties1);
            worksheet1.Append(sheetDimension1);
            worksheet1.Append(sheetViews1);
            worksheet1.Append(sheetFormatProperties1);
            worksheet1.Append(columns1);
            worksheet1.Append(sheetData1);
            worksheet1.Append(pageMargins1);
            worksheet1.Append(pageSetup);

            worksheetPart1.Worksheet = worksheet1;
        }

        // Genera contenido de worksheetPart2.
        private void GenerateWorksheetPart2Content(WorksheetPart worksheetPart2, ICollection<Report> report)
        {
            Worksheet worksheet2 = new Worksheet();
            worksheet2.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            SheetProperties sheetProperties2 = new SheetProperties() { CodeName = "Hoja2" };
            SheetDimension sheetDimension2 = new SheetDimension() { Reference = "A1:B6" };

            SheetViews sheetViews2 = new SheetViews();

            SheetView sheetView2 = new SheetView() { WorkbookViewId = (UInt32Value)0U };
            Selection selection2 = new Selection() { ActiveCell = "A4", SequenceOfReferences = new ListValue<StringValue>() { InnerText = "A4" } };

            sheetView2.Append(selection2);

            sheetViews2.Append(sheetView2);
            SheetFormatProperties sheetFormatProperties2 = new SheetFormatProperties() { BaseColumnWidth = (UInt32Value)10U, DefaultRowHeight = 15D };

            Columns columns2 = new Columns();
            Column column2 = new Column() { Min = (UInt32Value)1U, Max = (UInt32Value)1U, Width = 11.140625D, CustomWidth = true };
            Column column3 = new Column() { Min = (UInt32Value)2U, Max = (UInt32Value)2U, Width = 11.85546875D, BestFit = true, CustomWidth = true };

            columns2.Append(column2);
            columns2.Append(column3);

            SheetData sheetData2 = new SheetData();

            Row row3 = new Row() { RowIndex = (UInt32Value)1U, Spans = new ListValue<StringValue>() { InnerText = "1:2" }, Height = 18.75D };

            Cell cell8 = new Cell() { CellReference = "A1", StyleIndex = (UInt32Value)2U, DataType = CellValues.SharedString };
            CellValue cellValue6 = new CellValue();
            cellValue6.Text = "Reportes Asociados";

            cell8.Append(cellValue6);

            row3.Append(cell8);

            Row row4 = new Row() { RowIndex = (UInt32Value)3U, Spans = new ListValue<StringValue>() { InnerText = "1:2" } };

            Cell cell9 = new Cell() { CellReference = "A3", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValue7 = new CellValue();
            cellValue7.Text = "Id Reporte";

            cell9.Append(cellValue7);

            Cell cell10 = new Cell() { CellReference = "B3", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValue8 = new CellValue();
            cellValue8.Text = "Fecha";

            cell10.Append(cellValue8);

            row4.Append(cell9);
            row4.Append(cell10);

            sheetData2.Append(row3);
            sheetData2.Append(row4);

            
            int i = 4;
            foreach (var r in report)
            {
                UInt32Value ui = (uint) i;
                Row row5 = new Row() { RowIndex = ui, Spans = new ListValue<StringValue>() { InnerText = "1:2" } };

                string cellRefA = "A" + i.ToString();
                Cell cell11 = new Cell() { CellReference = cellRefA };
                CellValue cellValue9 = new CellValue();
                //Identificador del reporte
                cellValue9.Text = r.UdaId.ToString();

                string cellRefB = "B" + i.ToString();
                Cell cell16 = new Cell() { CellReference = cellRefB };
                CellValue cellValue14 = new CellValue();
                //Fecha del reporte
                cellValue14.Text = r.BeginDate.Value.ToShortDateString();

                cell11.Append(cellValue9);
                cell16.Append(cellValue14);

                row5.Append(cell11);
                row5.Append(cell16);

                sheetData2.Append(row5);
                
                i++;
            }

            PageMargins pageMargins2 = new PageMargins() { Left = 0.7D, Right = 0.7D, Top = 0.75D, Bottom = 0.75D, Header = 0.3D, Footer = 0.3D };

            worksheet2.Append(sheetProperties2);
            worksheet2.Append(sheetDimension2);
            worksheet2.Append(sheetViews2);
            worksheet2.Append(sheetFormatProperties2);
            worksheet2.Append(columns2);
            worksheet2.Append(sheetData2);
            worksheet2.Append(pageMargins2);

            worksheetPart2.Worksheet = worksheet2;
        }

        // Genera contenido de worksheetPart3.
        private void GenerateWorksheetPart3Content(WorksheetPart worksheetPart3, Investigation investigation)
        {
            Worksheet worksheet3 = new Worksheet();
            worksheet3.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            SheetProperties sheetProperties3 = new SheetProperties() { CodeName = "Hoja1" };
            SheetDimension sheetDimension3 = new SheetDimension() { Reference = "A1:B4" };

            SheetViews sheetViews3 = new SheetViews();

            SheetView sheetView3 = new SheetView() { TabSelected = true, WorkbookViewId = (UInt32Value)0U };
            Selection selection3 = new Selection() { ActiveCell = "D6", SequenceOfReferences = new ListValue<StringValue>() { InnerText = "D6" } };

            sheetView3.Append(selection3);

            sheetViews3.Append(sheetView3);
            SheetFormatProperties sheetFormatProperties3 = new SheetFormatProperties() { BaseColumnWidth = (UInt32Value)10U, DefaultRowHeight = 15D };

            Columns columns3 = new Columns();
            Column column4 = new Column() { Min = (UInt32Value)1U, Max = (UInt32Value)1U, Width = 26.85546875D, BestFit = true, CustomWidth = true };

            columns3.Append(column4);

            SheetData sheetData3 = new SheetData();

            Row row8 = new Row() { RowIndex = (UInt32Value)1U, Spans = new ListValue<StringValue>() { InnerText = "1:2" }, Height = 18.75D };

            Cell cell14 = new Cell() { CellReference = "A1", StyleIndex = (UInt32Value)2U, DataType = CellValues.SharedString };
            CellValue cellValue12 = new CellValue();
            cellValue12.Text = "Perfil de Investigacion";

            cell14.Append(cellValue12);

            row8.Append(cell14);

            Row row9 = new Row() { RowIndex = (UInt32Value)3U, Spans = new ListValue<StringValue>() { InnerText = "1:2" } };

            Cell cell15 = new Cell() { CellReference = "A3", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValue13 = new CellValue();
            cellValue13.Text = "Nombre de Investigacion";

            cell15.Append(cellValue13);

            Cell cell16 = new Cell() { CellReference = "B3", DataType = CellValues.SharedString };
            CellValue cellValue14 = new CellValue();
            //Nombre de la investigacion
            cellValue14.Text = investigation.Name;

            cell16.Append(cellValue14);

            row9.Append(cell15);
            row9.Append(cell16);

            Row row10 = new Row() { RowIndex = (UInt32Value)4U, Spans = new ListValue<StringValue>() { InnerText = "1:2" } };

            Cell cell17 = new Cell() { CellReference = "A4", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValue15 = new CellValue();
            cellValue15.Text = "Fecha de Creacion:";

            cell17.Append(cellValue15);

            Cell cell18 = new Cell() { CellReference = "B4", DataType = CellValues.SharedString };
            CellValue cellValue16 = new CellValue();
            //Fecha de creacion de la investigacion
            cellValue16.Text = investigation.CreationDate.ToShortDateString();

            cell18.Append(cellValue16);

            row10.Append(cell17);
            row10.Append(cell18);

            sheetData3.Append(row8);
            sheetData3.Append(row9);
            sheetData3.Append(row10);
            PageMargins pageMargins3 = new PageMargins() { Left = 0.7D, Right = 0.7D, Top = 0.75D, Bottom = 0.75D, Header = 0.3D, Footer = 0.3D };
            PageSetup pageSetup1 = new PageSetup() { Orientation = OrientationValues.Portrait, Id = "rId1" };

            worksheet3.Append(sheetProperties3);
            worksheet3.Append(sheetDimension3);
            worksheet3.Append(sheetViews3);
            worksheet3.Append(sheetFormatProperties3);
            worksheet3.Append(columns3);
            worksheet3.Append(sheetData3);
            worksheet3.Append(pageMargins3);
            worksheet3.Append(pageSetup1);

            worksheetPart3.Worksheet = worksheet3;
        }

                // Generates content of spreadsheetPrinterSettingsPart1.
        private void GenerateSpreadsheetPrinterSettingsPart1Content(SpreadsheetPrinterSettingsPart spreadsheetPrinterSettingsPart1)
        {
            System.IO.Stream data = GetBinaryDataStream(spreadsheetPrinterSettingsPart1Data);
            spreadsheetPrinterSettingsPart1.FeedData(data);
            data.Close();
        }

        // Generates content of workbookStylesPart1.
        private void GenerateWorkbookStylesPart1Content(WorkbookStylesPart workbookStylesPart1)
        {
            Stylesheet stylesheet1 = new Stylesheet();

            Fonts fonts1 = new Fonts() { Count = (UInt32Value)3U };

            Font font1 = new Font();
            FontSize fontSize1 = new FontSize() { Val = 11D };
            Color color1 = new Color() { Theme = (UInt32Value)1U };
            FontName fontName1 = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering1 = new FontFamilyNumbering() { Val = 2 };
            FontScheme fontScheme1 = new FontScheme() { Val = FontSchemeValues.Minor };

            font1.Append(fontSize1);
            font1.Append(color1);
            font1.Append(fontName1);
            font1.Append(fontFamilyNumbering1);
            font1.Append(fontScheme1);

            Font font2 = new Font();
            Bold bold1 = new Bold();
            FontSize fontSize2 = new FontSize() { Val = 11D };
            Color color2 = new Color() { Theme = (UInt32Value)1U };
            FontName fontName2 = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering2 = new FontFamilyNumbering() { Val = 2 };
            FontScheme fontScheme2 = new FontScheme() { Val = FontSchemeValues.Minor };

            font2.Append(bold1);
            font2.Append(fontSize2);
            font2.Append(color2);
            font2.Append(fontName2);
            font2.Append(fontFamilyNumbering2);
            font2.Append(fontScheme2);

            Font font3 = new Font();
            Bold bold2 = new Bold();
            FontSize fontSize3 = new FontSize() { Val = 14D };
            Color color3 = new Color() { Theme = (UInt32Value)1U };
            FontName fontName3 = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering3 = new FontFamilyNumbering() { Val = 2 };
            FontScheme fontScheme3 = new FontScheme() { Val = FontSchemeValues.Minor };

            font3.Append(bold2);
            font3.Append(fontSize3);
            font3.Append(color3);
            font3.Append(fontName3);
            font3.Append(fontFamilyNumbering3);
            font3.Append(fontScheme3);

            fonts1.Append(font1);
            fonts1.Append(font2);
            fonts1.Append(font3);

            Fills fills1 = new Fills() { Count = (UInt32Value)2U };

            Fill fill1 = new Fill();
            PatternFill patternFill1 = new PatternFill() { PatternType = PatternValues.None };

            fill1.Append(patternFill1);

            Fill fill2 = new Fill();
            PatternFill patternFill2 = new PatternFill() { PatternType = PatternValues.Gray125 };

            fill2.Append(patternFill2);

            fills1.Append(fill1);
            fills1.Append(fill2);

            Borders borders1 = new Borders() { Count = (UInt32Value)1U };

            Border border1 = new Border();
            LeftBorder leftBorder1 = new LeftBorder();
            RightBorder rightBorder1 = new RightBorder();
            TopBorder topBorder1 = new TopBorder();
            BottomBorder bottomBorder1 = new BottomBorder();
            DiagonalBorder diagonalBorder1 = new DiagonalBorder();

            border1.Append(leftBorder1);
            border1.Append(rightBorder1);
            border1.Append(topBorder1);
            border1.Append(bottomBorder1);
            border1.Append(diagonalBorder1);

            borders1.Append(border1);

            CellStyleFormats cellStyleFormats1 = new CellStyleFormats() { Count = (UInt32Value)1U };
            CellFormat cellFormat1 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U };

            cellStyleFormats1.Append(cellFormat1);

            CellFormats cellFormats1 = new CellFormats() { Count = (UInt32Value)4U };
            CellFormat cellFormat2 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U };
            CellFormat cellFormat3 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)1U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true };
            CellFormat cellFormat4 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)2U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true };
            CellFormat cellFormat5 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true };

            cellFormats1.Append(cellFormat2);
            cellFormats1.Append(cellFormat3);
            cellFormats1.Append(cellFormat4);
            cellFormats1.Append(cellFormat5);

            CellStyles cellStyles1 = new CellStyles() { Count = (UInt32Value)1U };
            CellStyle cellStyle1 = new CellStyle() { Name = "Normal", FormatId = (UInt32Value)0U, BuiltinId = (UInt32Value)0U };

            cellStyles1.Append(cellStyle1);
            DifferentialFormats differentialFormats1 = new DifferentialFormats() { Count = (UInt32Value)0U };
            TableStyles tableStyles1 = new TableStyles() { Count = (UInt32Value)0U, DefaultTableStyle = "TableStyleMedium9", DefaultPivotStyle = "PivotStyleLight16" };

            stylesheet1.Append(fonts1);
            stylesheet1.Append(fills1);
            stylesheet1.Append(borders1);
            stylesheet1.Append(cellStyleFormats1);
            stylesheet1.Append(cellFormats1);
            stylesheet1.Append(cellStyles1);
            stylesheet1.Append(differentialFormats1);
            stylesheet1.Append(tableStyles1);

            workbookStylesPart1.Stylesheet = stylesheet1;
        }


        // Genera contenido de vbaProjectPart1.
        private void GenerateVbaProjectPart1Content(VbaProjectPart vbaProjectPart1)
        {
            System.IO.Stream data = GetBinaryDataStream(vbaProjectPart1Data);
            vbaProjectPart1.FeedData(data);
            data.Close();
        }

        #region Binary Data
        private string vbaProjectPart1Data = "0M8R4KGxGuEAAAAAAAAAAAAAAAAAAAAAPgADAP7/CQAGAAAAAAAAAAAAAAABAAAAAQAAAAAAAAAAEAAAAgAAAAIAAAD+////AAAAAAAAAAD////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////9////CAAAACgAAAAEAAAABQAAAAYAAAAHAAAAGwAAAB8AAAAKAAAACwAAAAwAAAANAAAADgAAAA8AAAAZAAAAEQAAABIAAAATAAAAFAAAABUAAAAWAAAAFwAAABgAAAAJAAAAGgAAAP7///8cAAAAHQAAAB4AAAAgAAAAIwAAACEAAAAiAAAAJAAAADMAAAAlAAAAJgAAACcAAAApAAAA/v///yoAAAArAAAALAAAAC0AAAAuAAAALwAAADAAAAAxAAAAMgAAADQAAAD+/////v///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////1IAbwBvAHQAIABFAG4AdAByAHkAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAWAAUA//////////8RAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAKB0VyUMec4BAwAAAIA1AAAAAAAAVgBCAEEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAgAAQD//////////wcAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAGDYViUMec4BkE1XJQx5zgEAAAAAAAAAAAAAAABUAGgAaQBzAFcAbwByAGsAYgBvAG8AawAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAGgACAQUAAAAMAAAA/////wAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAADhAwAAAAAAAEgAbwBqAGEAMQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAMAAIBDQAAAP//////////AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAH0EAAAAAAAAAQAAAAIAAAADAAAABAAAAAUAAAAGAAAABwAAAAgAAAAJAAAACgAAAAsAAAAMAAAADQAAAA4AAAAPAAAA/v///xEAAAASAAAAEwAAABQAAAAVAAAAFgAAABcAAAAYAAAAGQAAABoAAAAbAAAAHAAAAB0AAAAeAAAAHwAAACAAAAAhAAAA/v///yMAAAAkAAAAJQAAAP7///8nAAAA/v///ykAAAAqAAAAKwAAACwAAAAtAAAALgAAAC8AAAAwAAAAMQAAADIAAAAzAAAANAAAADUAAAA2AAAANwAAADgAAAA5AAAAOgAAADsAAAA8AAAAPQAAAD4AAAA/AAAAQAAAAEEAAABCAAAAQwAAAP7///9FAAAA/v///0cAAABIAAAASQAAAEoAAABLAAAATAAAAE0AAABOAAAATwAAAFAAAABRAAAAUgAAAFMAAABUAAAAVQAAAFYAAABXAAAA/v///1kAAABaAAAAWwAAAP7///9dAAAA/v///18AAABgAAAAYQAAAGIAAABjAAAAZAAAAGUAAABmAAAAZwAAAGgAAABpAAAAagAAAGsAAABsAAAAbQAAAG4AAABvAAAAcAAAAHEAAAByAAAAcwAAAHQAAAB1AAAAdgAAAHcAAAB4AAAAeQAAAHoAAAB7AAAAfAAAAH0AAAB+AAAAfwAAAIAAAAABFgEAAfAAAADMAgAA1AAAAAACAAD/////0wIAACcDAAAAAAAAAQAAAE2/F3kAAP//IwEAAIgAAAC2AP//AQEAAAAA/////wAAAAD///////8AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQAAAAAwAAAAUAAAAHAAAA//////////8BAQgAAAD/////eAAAAAgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAP//AAAAAE1FAAD///////8AAAAA//8AAAAA//8BAQAAAADfAP//AAAAAAwA//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////8oAAAAAgBTTP////8AAAEAUxD/////AAABAFOU/////wAAAAACPP////8AAP//AQEAAAAAAQBOADAAewAwADAAMAAyADAAOAAxADkALQAwADAAMAAwAC0AMAAwADAAMAAtAEMAMAAwADAALQAwADAAMAAwADAAMAAwADAAMAAwADQANgB9AAYAAAD/////AQFAAAAAAoD+//////8QAP//KAAAAAIB//8AAAAAAAAAAP//////////AAAAAB0AAAAlAAAA/////0gAAAD/////QAAAAAAAAAAAAAEAAAAAAAAAAAD///////////////8AAAAA//////////////////////////8AAAAA//////////////////////////8AAAAAAAAAAP//////////AAAAAP///////////////////////////////wEAMAAAAN/xhFMNAN8AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAD+ygEAAAD/////AQEIAAAA/////3gAAAD/////AAABsLAAQXR0cmlidXQAZSBWQl9OYW0AZSA9ICJUaGkAc1dvcmtib28QayINCgqMQmFzAQKMMHswMDAyMFA4MTktABAwAwhDBwAUAhIBJDAwNDZ9gQ18R2xvYmFsAdAQU3BhYwGSRmFsBHNlDGRDcmVhdAhhYmwVH1ByZWSQZWNsYQAGSWQAsQhUcnUNQkV4cG8Ec2UUHFRlbXBsAGF0ZURlcml2AwISkkJ1c3RvbWkGegREAzIAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAARYBAAYAAQAAPAMAAOQAAAAQAgAAagMAAHgDAADMAwAAAAAAAAEAAABNv5WuAAD//yMBAACIAAAAtgD//wEBAAAAAP////8AAAAA//88AP//AACe0EE937qoQqmnwNGGUMjAIAgCAAAAAADAAAAAAAAARgAAAAAAAAAAAAAAAAAAAAABAAAAC4KwTqIdaU2yv4mqflUFdxAAAAADAAAABQAAAAcAAAD//////////wEBCAAAAP////94AAAACAuCsE6iHWlNsr+Jqn5VBXee0EE937qoQqmnwNGGUMjA//8AAAAATUUAAP///////wAAAAD//wAAAAD//wEBAAAAAN8A//8AAAAA/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////ygAAAACAFNM/////wAAAQBTEP////8AAAEAU5T/////AAAAADYi/////wAA//8BAQAAAAABAE4AMAB7ADAAMAAwADIAMAA4ADIAMAAtADAAMAAwADAALQAwADAAMAAwAC0AQwAwADAAMAAtADAAMAAwADAAMAAwADAAMAAwADAANAA2AH0ABwAAAP////8BAaAAAAACgP7//////xAA//8oAAAAAgH//wAAAAAAAAAA//////////8AAAAAHQAAACUAAAD/////SAAAAAKD/v//////CAD//2AAAAAAAP///////wAAAAD//////////wAAAAAdAAwAJQAAAIKgGAL//////v///5AAAAACAP///v///wAAAAD//////////wAAAAAdAAwAJQAAAP////9gAAAAAAAAAAAAAQAAAAAAAAAAAP///////////////wAAAAD//////////////////////////wAAAAD///////////////9oAAAAOAAAAAAAAAAAAAAAQAAEAAAAAABcAlwC/////////////////////////////wgAAQAwAAAA3/GEUw0AASQAKgBcAFIAZgBmAGYAZgAqADAAZwA1ADMAOAA0AGUAOQBkADcA3wEAAAAAAP////80AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA/soBAAAA/////wEBCAAAAP////94AAAA/////wAAAaewAEF0dHJpYnV0AGUgVkJfTmFtAGUgPSAiSG9qIGExIg0KCuBCYQJzAnAwezAwMDJgMDgyMC0AIAQQQwcAFAIcASQwMDQ2fYENfEdsb2JhbAHCEFNwYWMBkkZhbARzZQzIQ3JlYXQIYWJsFR9QcmVkkGVjbGEABklkAKoIVHJ1DUJFeHBvBHNlFBxUZW1wbABhdGVEZXJpdgMCJJJCdXN0b21pBnoERAMyAAAAclWAAAAAAAAAAIAAAACAAAAAAAAAAB4AAAAJAAAAAAAAAAkAAAAAAAsAgAEAAAAAAAAAAAAAAAAAAAEAAQAAAAEAwQsAAAAAAAD5BQAAAAAAAAkMAAAAAAAA/////6kFAAAAAAAACAAQADQAAABJBgAAAAAAAGEAAAAAAAEAcQYAAAAAAAD///////////////8AAP//////////////////////////////////////////////////////////////////////////////////////////AACQAGAAAH8AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAHJVgAAAAAAAAACAAAAAgAAAAAAA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//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////+gAAAAAgBbTP////8AAAEAUxD/////AAABAFOU/////wAAAQA4EP//JgIAAAAAPiL/////AAAAABpM/////wAAAAAalP////8AAAAAGkz/////AAAAABpM/////wAAAAAaCP////8AAAAAmiL/////AAAAABpM/////wAAAAAaTP////8AAAAAGkz/////AAAAABoI/////wAAAAACPP////8AAP//AQEAAAAAAQBOADAAewAwADAAMAAyADAAOAAyADAALQAwADAAMAAwAC0AMAAwADAAMAAtAEMAMAAwADAALQAwADAAMAAwADAAMAAwADAAMAAwADQANgB9AAcAAABwCAAAAQFQCgAAAoD+//////8QAP//KAAAAAIB//8AAAAAAAAAAP//////////AAAAAB0AAAAlAAAADBEiAv////8AAANgAAAAAP//////////AAAAAAAAAAAAAQAAePxvAHAAAAD/////XAIAAEUARQAAAJQBAAEAACmDJAL/////DAD//5AAAAAAAAAA/////4QAAAAAAAAAHQAMACUAAAD/////MAAAAAKD/v//////CAD//8gAAAAAAP///////wAAAAD//////////wAAAAAdABAAJQAAAIKgGgL//////v////gAAAACAP///v///wAAAAD//////////wAAAAAdABAAJQAAAP///////////////////////////////+gJAAD/////WAUAAP///////////////8gEAAD/////////////////////AAoAABAFAAAQCAAA/////ygFAAD/////GAoAAP//////////////////////////////////////////////////////////////////////////MAoAAOgEAAAghCQC/////3j///+4AQAAgAAAAAMAQTodAAwAJQAAACCEJAL/////eP///9gBAACAAAAAKAYAAB0ADAAlAAAAIIQkAv////94////+AEAAIAAAAAIBgAAHQAMACUAAAAghCQC/////3j///8YAgAAgAAAAOgFAAAdAAwAJQAAACCEJAL/////eP///zgCAACAAAAAyAUAAB0ADAAlAAAAIIQkAv////94////WAIAAIAAAACoBQAAHQAMACUAAAAghCQC/////3j///94AgAAgAAAAIgFAAAdAAwAJQAAACCEJAL/////eP///5gCAACAAAAAaA0AAB0ADAAlAAAA/////1gNAAAghCQC/////3j////AAgAAgAAAAAAAAAAdAAwAJQAAACCEJAL/////eP///+ACAACAAAAAwAcAAB0ADAAlAAAAIIQkAv////94////AAMAAIAAAACgBwAAHQAMACUAAAAghCQC/////3j///8gAwAAgAAAAIAHAAAdAAwAJQAAACCEJAL/////eP///0ADAACAAAAAOAUAAB0ADAAlAAAAIIQkAv////94////YAMAAIAAAAAYBQAAHQAMACUAAAAghCQC/////3j///+AAwAAgAAAAPgEAAAdAAwAJQAAACCEJAL/////eP///6ADAACAAAAA2AQAAB0ADAAlAAAAIIQkAv////94////wAMAAIAAAAC4BAAAHQAMACUAAAAghCQC/////3j////gAwAAgAAAAJgEAAAdAAwAJQAAACCEJAL/////eP///wAEAACAAAAAeAQAAB0ADAAlAAAAIIQkAv////94////IAQAAIAAAABYBAAAHQAMACUAAAAghCQC/////3j///9ABAAAgAAAADgEAAAdAAwAJQAAACCEJAL/////eP///2AEAACAAAAAGAQAAB0ADAAlAAAAIIQkAv////94////gAQAAIAAAAD4AwAAHQAMACUAAAAghCQC/////3j///+gBAAAgAAAANgDAAAdAAwAJQAAACCEJAL/////eP///8AEAACAAAAAuAMAAB0ADAAlAAAAIIQkAv////94////4AQAAIAAAACYAwAAHQAMACUAAAD/////yAQAAAAA/////////////ygCJgIuAjICOAI6AjwCHAIIAP//QAT+/3AFAAB0////CQD//yAAAAD/////QAT+/0AFAABk////DAD//yAAAAD/////QAT+/4gFAABU////DAD//yAAAAD/////QAQsAv////9E////DAD//wAAAAD/////QAT+//gHAABA////CQD//yAAAAAQAAAAQAT+/6AFAAAw////DAD//yAAAAAQAAAAQAT+/7gFAAAg////DAD//yAAAAAQAAAAQAT+/9AFAAAQ////DAD//yAAAAAQAAAAQAT+/+gFAAAA////DAD//yAAAAAQAAAAQAT+/wAGAADw/v//DAD//yAAAAAQAAAAQAT+/xgGAADg/v//DAD//yAAAAAQAAAAQAT+/zAGAADQ/v//DAD//yAAAAAQAAAAQAT+/0gGAADA/v//DAD//yAAAAAQAAAAQAT+/2AGAACw/v//DAD//yAAAAAQAAAAQAT+/3gGAACg/v//DAD//yAAAAAQAAAAQAT+/5AGAACQ/v//DAD//yAAAAAQAAAAQAT+/6gGAACA/v//DAD//yAAAAAQAAAAQAT+/8AGAABw/v//DAD//yAAAAAQAAAAQAT+/9gGAABg/v//DAD//yAAAAAQAAAAQAT+//AGAABQ/v//DAD//yAAAAAQAAAAQAT+/wgHAABA/v//DAD//yAAAAAQAAAAQAT+/yAHAAAw/v//DAD//yAAAAAQAAAAQAT+/zgHAAAg/v//DAD//yAAAAAQAAAAQAT+/1AHAAAQ/v//DAD//yAAAAAQAAAAQAT+/2gHAAAA/v//DAD//yAAAAAQAAAAQAT+/4AHAADw/f//DAD//yAAAACIAAAAQAT+/5gHAADg/f//DAD//yAAAAAQAAAAQAT+/7AHAADQ/f//DAD//yAAAAAQAAAAQAT+/8gHAADA/f//DAD//yAAAAAQAAAAQAT+/+AHAACw/f//DAD//yAAAAAQAAAAQAT+/5gIAACg/f//DAD//yAAAAAQAAAAQAT+/4AIAACc/f//CQD//yAAAAAQAAAAQAT+/ygIAACM/f//DAD//yAAAABQAAAAQAT+/0AIAAB8/f//DAD//yAAAAAQAAAAQAT+/1gIAABs/f//DAD//yAAAAAQAAAAQAT+/7AIAABc/f//DAD//yAAAAAIAAAA/////wAAAAD/////IAIAAEAE/v/4CAAAWP3//wkA//8gAAAA/////0AE/v8QCQAASP3//wwA//8gAAAAAQD//0AE/v/ICAAAOP3//wwA//8gAAAAMAgAAEAE/v/gCAAAKP3//wwA//8gAAAAAQAAAEAE/v8oCQAAGP3//wwA//8gAAAA/////0AE/v9wCQAAFP3//wkA//8gAAAAAQD//0AE/v+ICQAABP3//wwA//8gAAAA/////0AE/v9ACQAA9Pz//wwA//8gAAAA/////0AE/v9YCQAA5Pz//wwA//8gAAAAyAgAAEAE/v+gCQAA1Pz//wwA//8gAAAAAQAAAEAE/v//////0Pz//wkA//8gAAAAAQD//0AE/v//////wPz//wwA//8gAAAAAQD//0AE/v+4CQAAsPz//wwA//8gAAAA/////0AE/v/QCQAAoPz//wwA//8gAAAAQAkAAEAE/v//////kPz//wwA//8gAAAAWAkAAEAENAL/////gPz//wwA//8AAAAA/////0AE/v//////fPz//wgA//8gAAAA/////0AE/v//////ePz//wMA//8gAAAA0AYAAPgEAADcAAAAAAAAAAAAAAAAAAAAAQAAAP////+wBgAAAQABAAAAAQAAAAAAAAAAADAAAAD//////////wAAAAD//////////zAAAAD//////////wAAAAD////////////////QAAAAoAAAAAAAAAAAAAAAQAAEAAAAAABgAlwC/////////////////////////////wgAAgCYAAAA3/GEUw0AARIAKgBcAFIAMQAqACMAMgAyADIAASQAKgBcAFIAZgBmAGYAZgAqADAAYAA1ADMAOAA0AGUAOQA5ADMAARAAKgBcAFIAMQAqACMAZgBjAAESACoAXABSADEAKgAjADMAMwBiAAESACoAXABSADEAKgAjADEAMABkAAESACoAXABSADEAKgAjADEAMAAxAAEOACoAXABSADAAKgAjAGYAASQAKgBcAFIAZgBmAGYAZgAqADEANwA1ADMAOAA0AGUAYgA4ADUAARAAKgBcAFIAMQAqACMAZgBjAAESACoAXABSADEAKgAjADEAMABkAAESACoAXABSADEAKgAjADEAMAAxAAEOACoAXABSADAAKgAjAGYA3wEAAAAAAP////80AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA/soBAEUAIoEMAAYACQAAAAAAAIEIAAQABQAIAAAAAIAJACoAAAAQAAAAAIAJACYAAABAAAAAAIEIAAoAHQBoAAAAAIAJAC4AAAB4AAAAAIAJADQAAACoAAAAAIAJADIAAADgAAAAAIAJADgAAAAY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AAAZBwAAAAAAAEEHAAAAAAAA0QYAAAAAAABZCQAAAAAAALkHAAAAAAAAkQcAAAAAAADhBwAAAAAAAAkIAAAAAAAAIQgAAAAAAABJCAAAAAAAAHEIAAAAAAAAkQgAAAAAAACRCQAAAAAAAKkIAAAAAAAAwQgAAAAAAADZCAAAAAAAAPEIAAAAAAAACQkAAAAAAAAhCQAAAAAAAEAAAAAAAAAACQAAAAAAAgABCgAAAAAAADkJAAAAAAAAmQwAAAAAAADxDQAAAAAAAFEOAAAAAAAAsAIAAAAAAAAJAAAAAAAEADEPAAAAAAAAgRAAAAAAAAABAAEAAAABANEFAAAAAAAA+QUAAAAAAAAhBgAAAAAAAP////+pBQAAAAAAAAgAEAA0AAAASQYAAAAAAABhAAAAAAABAHEGAAAAAAAA////////////////AQAHBgAAAAADYAkBAAAAAAIA/////////////////////////////////////////////////////////////////////////////////////wEAkAAXAQAAAAACAPgEAABoBAAAAAlsDAD8+Hj/AAVL//8AHfQA9QkEAAAEdP8hDRwAAAAIdP8N0AMBABp0/wAcBHT/J1T/JTpk/xgAJSENkAEAAFF0//z8RP8CAASc/fUJBAAAJ6D9JSew/SUnwP0lJ9D9JSfg/SUn8P0lJwD+JScQ/iUnIP4lJzD+JSdA/iUnUP4lJ2D+JSdw/iUngP4lJ5D+JSeg/iUnsP4lJ8D+JSfQ/iUn4P4lJ/D+JScA/yUnEP8lJyD/JScw/yUnVP8lJ2T/JQRE//3zAwBbQP9seP/9nHT/BQQAJAUADcgABgAInP1hjP3sAAAAYEZ8/V0ERP//QWz9BwD7L1z9BFj9BQQAJAUADSgABgAIWP1hOP0AAAAAOkj9CABd+zwo/fsnGP0EFP0FBAAkBQANKAAGAAgU/WH0/AAAAAA6BP0ZAF37POT8+yfU/ATQ/AUEACQFAA0oAAYACND8YbD8AAAAADrA/BoAXfs8oPz7J5D8/xspDAB0/0D/nP1Y/RT90Pw2DACM/Xz9bP04/fT8sPwcPQQALTpk/x0ABHT/BQQAJAUADSgABgAIdP9hjP0GAAAA++98/fz2gPwadP81jP0ALwRA//ztgPwEdP8FBAAkBQANuAAGAAh0/w1kAAkACED//pgKAAAAKQQAdP9A/wAmBHT/BQQAJAUADUAABgAIdP9XjP0LAASA/PtAGnT/NYz9HCwEAC31AAAAAPX/////9QEAAAAbCAAbDAAEgPz9/nz8Cw0AGABGjP389oD8L3z8AC31AAAAAPX/////9QEAAAAbCAAbDgAEgPz9/nz8Cw0AGABGjP389oD8L3z8AC31AAAAAPX/////9QEAAAAbCAAbDwAEgPz9/nz8Cw0AGABGjP389oD8L3z8AC31AAAAAPX/////9QEAAAAbCAAbEAAEgPz9/nz8Cw0AGABGjP389oD8L3z8AC31AAAAAPX/////9QEAAAAbCAAbEQAEgPz9/nz8Cw0AGABGjP389oD8L3z8AC31AAAAAPX/////9QEAAAAbCAAbEgAEgPz9/nz8Cw0AGABGjP389oD8L3z8AC31AAAAAPX/////9QEAAAAbCAAbEwAEgPz9/nz8Cw0AGABGjP389oD8L3z8ABAEgPw6ZP8IAF37QBwnBAAW9QkEAAAnZP8lBRsAJBwADZQAAAAAYvUJBAAABJz9BHj8BHT/BQQAJAUADbgABgAIdP8NMAAJAGx4/P1pZP8lBED/BQQAJAUADbgABgAIQP8NZAAJAFGc/f1vjP0lJ1T/JQUbACQcAA0sAAAAKQQAdP9A/zWM/QAx9R8AAAAEgPwEjP0KFgAMAPztjP0EdP8FBAAkBQANQAAGAAh0//6bCwAadP81jP0AIwR0/ydU/yU6ZP8XACUhDZABAAAIdP/+oOsAAAAAABp0/wACHjsEAAIADQSA/P2f/pgKAAAAAAIAAgAI/GP8+0T/AB30//UJBAAABHT/IQ0cAAAACHT/DdADAQAadP8AABMAAAAAAAgABANoBDAACAAAAIwAKAAAAAAAGAAAAAAAAwAAAAAAgPwCAET/AgB4/wMAXAAAAAAAFAAAAAAAfPwBAHT/AwBA/wMAnP0DAFj9AwAU/QMA0PwDAIz9AgB8/QIAbP0CAFz9AgA4/QIAKP0CABj9AgD0/AIA5PwCANT8AgCw/AIAoPwCAJD8AgABAEcBBwAAEwAANABYAAB/AAAAAAAAAAAAAAAAclWAAAAAAAAAAIAAAACAAAAAAAAAABAAAAAJAAAAAAACAP//////////AAAAAEAAAAAEACwAAQEAAAAAAgAAAANgBABcAhwA//////////8AAAAAoQAAAAAAAQAAAAAAHh2BAAAAAAABAAAAAAAAAG4AAH8AAAAAAAAAAAAAAAABFgEABgABAAA8AwAA5AAAABACAABqAwAAeAMAAMwDAAAAAAAAAQAAAE2/g3MAAP//IwEAAIgAAAC2AP//AQEAAAAA/////wAAAAD//zwA//8AAOwqv/EfWsROuP58e/LZhaEgCAIAAAAAAMAAAAAAAABGAAAAAAAAAAAAAAAAAF8AXwBTAFIAUABfADMAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQAAIA////////////////AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAARAAAAHgAAAAAAAAASABvAGoAYQAzAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAwAAgD///////////////8AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABGAAAAfQQAAAAAAABfAF8AUwBSAFAAXwA0AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAACAQgAAAALAAAA/////wAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAFgAAADkAAAAAAAAAF8AXwBTAFIAUABfADUAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQAAIA////////////////AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAXAAAAEIAAAAAAAAAAAAAAQAAAEZcL7Tf051CoRCovR10ALUQAAAAAwAAAAUAAAAHAAAA//////////8BAQgAAAD/////eAAAAAhGXC+039OdQqEQqL0ddAC17Cq/8R9axE64/nx78tmFof//AAAAAE1FAAD///////8AAAAA//8AAAAA//8BAQAAAADfAP//AAAAAP////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////8oAAAAAgBTTP////8AAAEAUxD/////AAABAFOU/////wAAAAA2Iv////8AAP//AQEAAAAAAQBOADAAewAwADAAMAAyADAAOAAyADAALQAwADAAMAAwAC0AMAAwADAAMAAtAEMAMAAwADAALQAwADAAMAAwADAAMAAwADAAMAAwADQANgB9AAcAAAD/////AQGgAAAAAoD+//////8QAP//KAAAAAIB//8AAAAAAAAAAP//////////AAAAAB0AAAAlAAAA/////0gAAAACg/7//////wgA//9gAAAAAAD///////8AAAAA//////////8AAAAAHQAMACUAAACCoBwC//////7///+QAAAAAgD///7///8AAAAA//////////8AAAAAHQAMACUAAAD/////YAAAAAAAAAAAAAEAAAAAAAAAAAD///////////////8AAAAA//////////////////////////8AAAAA////////////////aAAAADgAAAAAAAAAAAAAAEAABAAAAAAAXAJcAv////////////////////////////8IAAEAMAAAAN/xhFMNAAEkACoAXABSAGYAZgBmAGYAKgAxADcANQAzADgANABlAGIAOAA1AN8BAAAAAAD/////NAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAP7KAQAAAP////8BAQgAAAD/////eAAAAP////8AAAGnsABBdHRyaWJ1dABlIFZCX05hbQBlID0gIkhvaiBhMyINCgrgQmECcwJwMHswMDAyYDA4MjAtACAEEEMHABQCHAEkMDA0Nn2BDXxHbG9iYWwBwhBTcGFjAZJGYWwEc2UMyENyZWF0CGFibBUfUHJlZJBlY2xhAAZJZACqCFRydQ1CRXhwbwRzZRQcVGVtcGwAYXRlRGVyaXYDAiSSQnVzdG9taQZ6BEQDMgAAAHJVgAAAAAAAAACAAAAAgAAAAAAAAAAeAAAACQAAAAAAAAAJAAAAAAAFAKACAAAAAAAAAAAAAAAAAAABAAEAAAABAOEOAAAAAAAA+QUAAAAAAAAJDwAAAAAAAP////+pBQAAAAAAAAgAEAA0AAAASQYAAAAAAABhAAAAAAABAHEGAAAAAAAA////////////////AAD//////////////////////////////////////////////////////////////////////////////////////////wAAkABgAAB/AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAByVYAAAAAAAAAAgAAAAIAAAAAAAAAAEAAAAAkAAAAAAAQA//////////8AAAAAQAAAAAQAAAAAAAAAbgAAfwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAMxhiAAAAQD/CgwAAAkEAADkBAEAAAAAAAAAAAABAAQAAgAoASoAXABHAHsAMAAwADAAMgAwADQARQBGAC0AMAAwADAAMAAtADAAMAAwADAALQBDADAAMAAwAC0AMAAwADAAMAAwADAAMAAwADAAMAA0ADYAfQAjADQALgAwACMAXwBWAEIAQQBfAFAAUgBPAEoARQBDAFQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABoAAgD///////////////8AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABeAAAAAA0AAAAAAABkAGkAcgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACAACAP///////////////wAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAJIAAAAyAgAAAAAAAF8AXwBTAFIAUABfADAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQAAIBCQAAAA8AAAD/////AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAmwAAAAULAAAAAAAAXwBfAFMAUgBQAF8AMQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABAAAgD///////////////8AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAADIAAAAqAAAAAAAAAA5ACMAQwA6AFwAUAByAG8AZwByAGEAbQAgAEYAaQBsAGUAcwAgACgAeAA4ADYAKQBcAEMAbwBtAG0AbwBuACAARgBpAGwAZQBzAFwATQBpAGMAcgBvAHMAbwBmAHQAIABTAGgAYQByAGUAZABcAFYAQgBBAFwAVgBCAEEANgBcAFYAQgBFADYALgBEAEwATAAjAFYAaQBzAHUAYQBsACAAQgBhAHMAaQBjACAARgBvAHIAIABBAHAAcABsAGkAYwBhAHQAaQBvAG4AcwAAAAAAAAAAAAAAAAAcASoAXABHAHsAMAAwADAAMgAwADgAMQAzAC0AMAAwADAAMAAtADAAMAAwADAALQBDADAAMAAwAC0AMAAwADAAMAAwADAAMAAwADAAMAA0ADYAfQAjADEALgA2ACMAMAAjAEMAOgBcAFAAcgBvAGcAcgBhAG0AIABGAGkAbABlAHMAIAAoAHgAOAA2ACkAXABNAGkAYwByAG8AcwBvAGYAdAAgAE8AZgBmAGkAYwBlAFwATwBmAGYAaQBjAGUAMQAyAFwARQBYAEMARQBMAC4ARQBYAEUAIwBNAGkAYwByAG8AcwBvAGYAdAAgAEUAeABjAGUAbAAgADEAMgAuADAAIABPAGIAagBlAGMAdAAgAEwAaQBiAHIAYQByAHkAAAAAAAAAAAAAAAAAvAAqAFwARwB7ADAAMAAwADIAMAA0ADMAMAAtADAAMAAwADAALQAwADAAMAAwAC0AQwAwADAAMAAtADAAMAAwADAAMAAwADAAMAAwADAANAA2AH0AIwAyAC4AMAAjADAAIwBDADoAXABXAGkAbgBkAG8AdwBzAFwAUwB5AHMAVwBPAFcANgA0AFwAcwB0AGQAbwBsAGUAMgAuAHQAbABiACMATwBMAEUAIABBAHUAdABvAG0AYQB0AGkAbwBuAAAAAAAAAAAAAAAAADQBKgBcAEcAewAyAEQARgA4AEQAMAA0AEMALQA1AEIARgBBAC0AMQAwADEAQgAtAEIARABFADUALQAwADAAQQBBADAAMAA0ADQARABFADUAMgB9ACMAMgAuADQAIwAwACMAQwA6AFwAUAByAG8AZwByAGEAbQAgAEYAaQBsAGUAcwAgACgAeAA4ADYAKQBcAEMAbwBtAG0AbwBuACAARgBpAGwAZQBzAFwATQBpAGMAcgBvAHMAbwBmAHQAIABTAGgAYQByAGUAZABcAE8ARgBGAEkAQwBFADEAMgBcAE0AUwBPAC4ARABMAEwAIwBNAGkAYwByAG8AcwBvAGYAdAAgAE8AZgBmAGkAYwBlACAAMQAyAC4AMAAgAE8AYgBqAGUAYwB0ACAATABpAGIAcgBhAHIAeQAAAAAAAAAAAAAAAAAEAAIAAgACAAIABAAEAgAABgIBAAgCAAAKAgEAEAL///////8AAAAA//8AAN/xhFMNAP//////////////////////////////////AwD//wIA////////////////////////AQD///////////////8BAAAAAAAAAAAAAAAAAAAAAAAAAE2/BAAYAFQAaABpAHMAVwBvAHIAawBiAG8AbwBrABQAMABTADUAMwA4ADQAZQA4ADgAYgD//xUCGABUAGgAaQBzAFcAbwByAGsAYgBvAG8AawD//xd5AAAAAAAAAAIAAAAtAwAA//8KAEgAbwBqAGEAMQAUADAAZwA1ADMAOAA0AGUAOQBkADcA//8ZAgoASABvAGoAYQAxAP//la4AAAAAAAAYAgAAANIDAAD//woASABvAGoAYQAyABQAMABgADUAMwA4ADQAZQA5ADkAMwD//xsCCgBIAG8AagBhADIA//8kQQAAAAABAEgCAAD/////MAIAAADjGwAAAAAKAEgAbwBqAGEAMwAUADEANwA1ADMAOAA0AGUAYgA4ADUA//8dAgoASABvAGoAYQAzAP//g3MAAAAAAABIAgAAANIDAAD///////8BAXACAAD/////////////////////////////////////AAIAAP////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////8wAgAA/////xgCAAD/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////SAIAAP///////////////////////////////////////////////////////////////2zm5MUR409CoYCHqH49CY7/////AQAAANo49c+KtoEAAACCAAAAgwAAAIQAAACFAAAAhgAAAIcAAACIAAAAiQAAAIoAAACLAAAAjAAAAI0AAACOAAAAjwAAAJAAAACRAAAA/v///5MAAACUAAAAlQAAAJYAAACXAAAAmAAAAJkAAACaAAAA/v///5wAAACdAAAAngAAAJ8AAACgAAAAoQAAAKIAAACjAAAApAAAAKUAAACmAAAApwAAAKgAAACpAAAAqgAAAKsAAACsAAAArQAAAK4AAACvAAAAsAAAALEAAACyAAAAswAAALQAAAC1AAAAtgAAALcAAAC4AAAAuQAAALoAAAC7AAAAvAAAAL0AAAC+AAAAvwAAAMAAAADBAAAAwgAAAMMAAADEAAAAxQAAAMYAAADHAAAA/v///8kAAADKAAAA/v///8wAAAD+////zgAAAM8AAADQAAAA0QAAANIAAADTAAAA1AAAANUAAAD+////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////u0qRGrmfYLoA//////8BAAAAF9/VAteCKECZZ9aSQ0Flyv////8BAAAAckVoCiRJ+UKZSaf4namXBP////8BAAAA/////5gCAAD/////YAIAAIAAAAAAADYBNwD/APwr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";

        private string spreadsheetPrinterSettingsPart1Data = "TQBpAGMAcgBvAHMAbwBmAHQAIABYAFAAUwAgAEQAbwBjAHUAbQBlAG4AdAAgAFcAcgBpAHQAZQByAAAAAAAAAAEEAAbcAFgDA/8AAAEAAQDqCm8IZAABAA8AWAICAAEAWAICAAAATABlAHQAdABlAHIAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQAAAAAAAAABAAAAAgAAAAEAAAD/////AAAAAAAAAAAAAAAAAAAAAERJTlUiABABTAMMAMrS9nIAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACQAAAAAAAAAAAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQAAAAAAAAAAAAAAEAEAAFNNVEoAAAAAEAAAAU0AaQBjAHIAbwBzAG8AZgB0ACAAWABQAFMAIABEAG8AYwB1AG0AZQBuAHQAIABXAHIAaQB0AGUAcgAAAElucHV0QmluAEZPUk1TT1VSQ0UAUkVTRExMAFVuaXJlc0RMTABJbnRlcmxlYXZpbmcAT0ZGAEltYWdlVHlwZQBKUEVHTWVkAE9yaWVudGF0aW9uAFBPUlRSQUlUAENvbGxhdGUAT0ZGAFJlc29sdXRpb24AT3B0aW9uMQBQYXBlclNpemUATEVUVEVSAENvbG9yTW9kZQAyNGJwcAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAMAAAATVhEVwEBAAA=";

        private System.IO.Stream GetBinaryDataStream(string base64String)
        {
            return new System.IO.MemoryStream(System.Convert.FromBase64String(base64String));
        }

        #endregion

        #endregion
    }
}
