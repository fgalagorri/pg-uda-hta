using System;
using System.Collections.Generic;
using System.Configuration;
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
        //Obtiene la investigacion 'id'
        public Investigation GetInvestigation(long id)
        {
            UdaHtaDataAccess uda = new UdaHtaDataAccess();
            var investigation = uda.GetInvestigation(id);

            foreach (var report in investigation.LReports)
            {
                PatientDataAccess pda = new PatientDataAccess();
                if (report.Patient.UdaId != null) report.Patient = pda.GetPatient(report.Patient.UdaId.Value);
            }

            return investigation;
        }

        //Obtiene una lista de investigaciones, filtrando por id, nombre y/o fecha de creacion
        public ICollection<InvestigationSearch> ListInvestigations(int? id, string name, DateTime? creationDate)
        {
            UdaHtaDataAccess uda = new UdaHtaDataAccess();
            return uda.ListInvestigations(id, name, creationDate);
        }

        public Investigation CreateInvestigation(string name, DateTime creationDate, string comment)
        {
            UdaHtaDataAccess uda = new UdaHtaDataAccess();
            var id = uda.insertInvestigation(name, creationDate, comment);
            return new Investigation(id,name,creationDate,comment);
        }

        public void EditInvestigation(Investigation investigation)
        {
            UdaHtaDataAccess uda = new UdaHtaDataAccess();
            uda.editInvestigation(investigation);
        }

        public void AddReportToInvestigation(long idReport, long idPatient, long idInvestigation)
        {
            UdaHtaDataAccess uda = new UdaHtaDataAccess();
            uda.addReportToInvestigation(idReport, idPatient, idInvestigation);
        }

        public void DeleteReportFromInvestigation(Report report, long idInvestigation)
        {
            UdaHtaDataAccess uda = new UdaHtaDataAccess();
            uda.deleteReportFromInvestigation(report.Patient.UdaId.Value, report.UdaId.Value, idInvestigation);
        }


        #region Export Investigation
        public void ExportInvestigation(Investigation investigation, string filePath)
        {
            //Crear una planilla excel
            using (SpreadsheetDocument document = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
            {
                ExtendedFilePropertiesPart extendedFilePropertiesPart1 = document.AddNewPart<ExtendedFilePropertiesPart>("rId4");
                GenerateExtendedFilePropertiesPart1Content(extendedFilePropertiesPart1);

                //Generar workbook
                WorkbookPart workbookPart1 = document.AddWorkbookPart();
                Workbook workbook1 = new Workbook();
                workbook1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
                WorkbookProperties workbookProperties1 = new WorkbookProperties() { CodeName = "ThisWorkbook", DefaultThemeVersion = (UInt32Value)124226U };

                BookViews bookViews1 = new BookViews();
                WorkbookView workbookView1 = new WorkbookView() { XWindow = 240, YWindow = 45, WindowWidth = (UInt32Value)20115U, WindowHeight = (UInt32Value)7995U };

                bookViews1.Append(workbookView1);

                Sheets sheets1 = new Sheets();
                //Sheet sheet1 = new Sheet() { Name = "Perfil", SheetId = (UInt32Value)1U, Id = "rId1" };
                Sheet sheet2 = new Sheet() { Name = "Reportes", SheetId = (UInt32Value)2U, Id = "rId2" };
                Sheet sheet3 = new Sheet() { Name = "Plantilla", SheetId = (UInt32Value)3U, Id = "rId3", State = SheetStateValues.Hidden};

                //sheets1.Append(sheet1);
                sheets1.Append(sheet2);
                sheets1.Append(sheet3);

                CalculationProperties calculationProperties1 = new CalculationProperties() { CalculationId = (UInt32Value)125725U };

                workbook1.Append(workbookProperties1);
                workbook1.Append(bookViews1);
                workbook1.Append(sheets1);
                workbook1.Append(calculationProperties1);

                workbookPart1.Workbook = workbook1;

                WorksheetPart worksheetPart1 = workbookPart1.AddNewPart<WorksheetPart>("rId3");
                GenerateWorksheetPart1Content(worksheetPart1);

                WorksheetPart worksheetPart2 = workbookPart1.AddNewPart<WorksheetPart>("rId2");
                GenerateWorksheetPart2Content(worksheetPart2, investigation.LReports);

                //WorksheetPart worksheetPart3 = workbookPart1.AddNewPart<WorksheetPart>("rId1");
                //GenerateWorksheetPart3Content(worksheetPart3, investigation);
                /*
                SpreadsheetPrinterSettingsPart spreadsheetPrinterSettingsPart1 = worksheetPart3.AddNewPart<SpreadsheetPrinterSettingsPart>("rId4");
                GenerateSpreadsheetPrinterSettingsPart1Content(spreadsheetPrinterSettingsPart1);
                */
                WorkbookStylesPart workbookStylesPart1 = workbookPart1.AddNewPart<WorkbookStylesPart>("rId5");
                GenerateWorkbookStylesPart1Content(workbookStylesPart1);

                

            }
        }

        // Generates content of extendedFilePropertiesPart1.
        private void GenerateExtendedFilePropertiesPart1Content(ExtendedFilePropertiesPart extendedFilePropertiesPart1)
        {
            Ap.Properties properties1 = new Ap.Properties();
            properties1.AddNamespaceDeclaration("vt", "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes");
            Ap.Application application1 = new Ap.Application();
            application1.Text = "Microsoft Excel";
            Ap.DocumentSecurity documentSecurity1 = new Ap.DocumentSecurity();
            documentSecurity1.Text = "0";
            Ap.ScaleCrop scaleCrop1 = new Ap.ScaleCrop();
            scaleCrop1.Text = "false";

            Ap.HeadingPairs headingPairs1 = new Ap.HeadingPairs();

            Vt.VTVector vTVector1 = new Vt.VTVector() { BaseType = Vt.VectorBaseValues.Variant, Size = (UInt32Value)2U };

            Vt.Variant variant1 = new Vt.Variant();
            Vt.VTLPSTR vTLPSTR1 = new Vt.VTLPSTR();
            vTLPSTR1.Text = "Hojas de cálculo";

            variant1.Append(vTLPSTR1);

            Vt.Variant variant2 = new Vt.Variant();
            Vt.VTInt32 vTInt321 = new Vt.VTInt32();
            vTInt321.Text = "3";

            variant2.Append(vTInt321);

            vTVector1.Append(variant1);
            vTVector1.Append(variant2);

            headingPairs1.Append(vTVector1);

            Ap.TitlesOfParts titlesOfParts1 = new Ap.TitlesOfParts();

            Vt.VTVector vTVector2 = new Vt.VTVector() { BaseType = Vt.VectorBaseValues.Lpstr, Size = (UInt32Value)4U };
            Vt.VTLPSTR vTLPSTR2 = new Vt.VTLPSTR();
            vTLPSTR2.Text = "Perfil";
            Vt.VTLPSTR vTLPSTR3 = new Vt.VTLPSTR();
            vTLPSTR3.Text = "Reportes";
            Vt.VTLPSTR vTLPSTR4 = new Vt.VTLPSTR();
            vTLPSTR4.Text = "Plantilla";

            vTVector2.Append(vTLPSTR2);
            vTVector2.Append(vTLPSTR3);
            vTVector2.Append(vTLPSTR4);

            titlesOfParts1.Append(vTVector2);
            Ap.LinksUpToDate linksUpToDate1 = new Ap.LinksUpToDate();
            linksUpToDate1.Text = "false";
            Ap.SharedDocument sharedDocument1 = new Ap.SharedDocument();
            sharedDocument1.Text = "false";
            Ap.HyperlinksChanged hyperlinksChanged1 = new Ap.HyperlinksChanged();
            hyperlinksChanged1.Text = "false";
            Ap.ApplicationVersion applicationVersion1 = new Ap.ApplicationVersion();
            applicationVersion1.Text = "12.0000";

            properties1.Append(application1);
            properties1.Append(documentSecurity1);
            properties1.Append(scaleCrop1);
            properties1.Append(headingPairs1);
            properties1.Append(titlesOfParts1);
            properties1.Append(linksUpToDate1);
            properties1.Append(sharedDocument1);
            properties1.Append(hyperlinksChanged1);
            properties1.Append(applicationVersion1);

            extendedFilePropertiesPart1.Properties = properties1;
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
            cellValue1.Text = "Fecha";

            cell1.Append(cellValue1);

            Cell cellHora = new Cell() { CellReference = "B1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValueHora = new CellValue();
            cellValueHora.Text = "Hora";

            cellHora.Append(cellValueHora);

            Cell cell2 = new Cell() { CellReference = "C1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValue2 = new CellValue();
            cellValue2.Text = "Sistolica";

            cell2.Append(cellValue2);

            Cell cell3 = new Cell() { CellReference = "D1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValue3 = new CellValue();
            cellValue3.Text = "Diastolica";

            cell3.Append(cellValue3);

            Cell cell4 = new Cell() { CellReference = "E1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValue4 = new CellValue();
            cellValue4.Text = "Media";

            cell4.Append(cellValue4);

            Cell cell5 = new Cell() { CellReference = "F1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValue5 = new CellValue();
            cellValue5.Text = "FC";

            cell5.Append(cellValue5);

            Cell cell6 = new Cell() { CellReference = "G1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValue6 = new CellValue();
            cellValue6.Text = "Durmiendo";


            row1.Append(cell1);
            row1.Append(cellHora);
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
            PageMargins pageMargins1 = new PageMargins() { Left = 0.7D, Right = 0.7D, Top = 0.7D, Bottom = 0.7D, Header = 0.3D, Footer = 0.3D };

            worksheet1.Append(sheetProperties1);
            worksheet1.Append(sheetDimension1);
            worksheet1.Append(sheetViews1);
            worksheet1.Append(sheetFormatProperties1);
            worksheet1.Append(columns1);
            worksheet1.Append(sheetData1);
            worksheet1.Append(pageMargins1);

            worksheetPart1.Worksheet = worksheet1;
        }

        /*
         * Genera contenido de la hoja Reportes
         * Contiene una lista de informacion de los reportes asociados a la investigacion
         */
        private void GenerateWorksheetPart2Content(WorksheetPart worksheetPart2, ICollection<Report> report)
        {
            Worksheet worksheet2 = new Worksheet();
            worksheet2.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            SheetProperties sheetProperties2 = new SheetProperties() { CodeName = "Hoja2" };
            SheetDimension sheetDimension2 = new SheetDimension() { Reference = "A1:B6" };

            SheetViews sheetViews2 = new SheetViews();

            SheetView sheetView2 = new SheetView() { WorkbookViewId = (UInt32Value)0U };
            Selection selection2 = new Selection() { ActiveCell = "B4", SequenceOfReferences = new ListValue<StringValue>() { InnerText = "B4" } };

            sheetView2.Append(selection2);

            sheetViews2.Append(sheetView2);
            SheetFormatProperties sheetFormatProperties2 = new SheetFormatProperties() { BaseColumnWidth = (UInt32Value)10U, DefaultRowHeight = 15D };

            Columns columns2 = new Columns();
            Column column2 = new Column() { Min = (UInt32Value)1U, Max = (UInt32Value)1U, Width = 12D, CustomWidth = true };
            Column column3 = new Column() { Min = (UInt32Value)2U, Max = (UInt32Value)2U, Width = 12D, BestFit = true, CustomWidth = true };
            
            columns2.Append(column2);
            columns2.Append(column3);

            SheetData sheetData2 = new SheetData();
            /*
            Row row3 = new Row() { RowIndex = (UInt32Value)1U, Spans = new ListValue<StringValue>() { InnerText = "1:2" }, Height = 18.75D };

            Cell cell8 = new Cell() { CellReference = "A1", StyleIndex = (UInt32Value)2U, DataType = CellValues.SharedString };
            CellValue cellValue6 = new CellValue();
            cellValue6.Text = "Reportes Asociados";

            cell8.Append(cellValue6);

            row3.Append(cell8);
            */
            Row row4 = new Row() { RowIndex = (UInt32Value)1U, Spans = new ListValue<StringValue>() { InnerText = "1:2" } };

            Cell cell9 = new Cell() { CellReference = "A1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValue7 = new CellValue();
            cellValue7.Text = "Id Reporte";

            cell9.Append(cellValue7);

            Cell cellCI = new Cell() { CellReference = "B1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValueCI = new CellValue();
            cellValueCI.Text = "CI";

            cellCI.Append(cellValueCI);

            Cell cell10 = new Cell() { CellReference = "C1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValue8 = new CellValue();
            cellValue8.Text = "Fecha";

            cell10.Append(cellValue8);

            Cell cellTime = new Cell() { CellReference = "D1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValueTime = new CellValue();
            cellValueTime.Text = "Hora";

            cellTime.Append(cellValueTime);

            Cell cellDev = new Cell() { CellReference = "E1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValueDev = new CellValue();
            cellValueDev.Text = "Dispositivo";

            cellDev.Append(cellValueDev);

            Cell cellSex = new Cell() { CellReference = "F1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValueSex = new CellValue();
            cellValueSex.Text = "Sexo";

            cellSex.Append(cellValueSex);

            Cell cellWeight = new Cell() { CellReference = "G1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValueWeight = new CellValue();
            cellValueWeight.Text = "Peso";

            cellWeight.Append(cellValueWeight);

            Cell cellHeight = new Cell() { CellReference = "H1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValueHeight = new CellValue();
            cellValueHeight.Text = "Altura";

            cellHeight.Append(cellValueHeight);

            Cell cellAge = new Cell() { CellReference = "I1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValueAge = new CellValue();
            cellValueAge.Text = "Edad";

            cellAge.Append(cellValueAge);

            Cell cellSmoker = new Cell() { CellReference = "J1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValueSmoker = new CellValue();
            cellValueSmoker.Text = "Fumador";

            cellSmoker.Append(cellValueSmoker);

            Cell cellDysli = new Cell() { CellReference = "K1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValueDysli = new CellValue();
            cellValueDysli.Text = "Dislipidémico";

            cellDysli.Append(cellValueDysli);

            Cell cellDiabetic = new Cell() { CellReference = "L1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValueDiabetic = new CellValue();
            cellValueDiabetic.Text = "Diabetico";

            cellDiabetic.Append(cellValueDiabetic);

            Cell cellHypertense = new Cell() { CellReference = "M1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValueHypertense = new CellValue();
            cellValueHypertense.Text = "Hipertenso";

            cellHypertense.Append(cellValueHypertense);

            Cell cellMassIndex = new Cell() { CellReference = "N1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValueMassIndex = new CellValue();
            cellValueMassIndex.Text = "Masa corporal";

            cellMassIndex.Append(cellValueMassIndex);

            Cell cellFatper = new Cell() { CellReference = "O1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValueFatper = new CellValue();
            cellValueFatper.Text = "Porcentaje de grasa";

            cellFatper.Append(cellValueFatper);

            Cell cellMuscle = new Cell() { CellReference = "P1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValueMuscle = new CellValue();
            cellValueMuscle.Text = "Porcentaje muscular";

            cellMuscle.Append(cellValueMuscle);

            Cell cell2 = new Cell() { CellReference = "Q1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValue2 = new CellValue();
            cellValue2.Text = "Sistolica";

            cell2.Append(cellValue2);

            Cell cell3 = new Cell() { CellReference = "R1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValue3 = new CellValue();
            cellValue3.Text = "Diastolica";

            cell3.Append(cellValue3);

            Cell cell4 = new Cell() { CellReference = "S1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValue4 = new CellValue();
            cellValue4.Text = "Media";

            cell4.Append(cellValue4);

            Cell cell5 = new Cell() { CellReference = "T1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValue5 = new CellValue();
            cellValue5.Text = "FC";

            cell5.Append(cellValue5);

            Cell cellSleep = new Cell() { CellReference = "U1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValueSleep = new CellValue();
            cellValueSleep.Text = "Durmiendo";

            cellSleep.Append(cellValueSleep);

            //Titulos para Medicamentos
            Cell cellCat1 = new Cell() { CellReference = "V1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValueCat1 = new CellValue();
            cellValueCat1.Text = "Droga Cat.1";
            cellCat1.Append(cellValueCat1);

            Cell cellActive1 = new Cell() { CellReference = "W1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValueActive1 = new CellValue();
            cellValueActive1.Text = "Droga Activo 1";
            cellActive1.Append(cellValueActive1);

            Cell cellCat2 = new Cell() { CellReference = "X1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValueCat2 = new CellValue();
            cellValueCat2.Text = "Droga Cat.2";
            cellCat2.Append(cellValueCat2);

            Cell cellActive2 = new Cell() { CellReference = "Y1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValueActive2 = new CellValue();
            cellValueActive2.Text = "Droga Activo 2";
            cellActive2.Append(cellValueActive2);

            Cell cellCat3 = new Cell() { CellReference = "Z1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValueCat3 = new CellValue();
            cellValueCat3.Text = "Droga Cat.3";
            cellCat3.Append(cellValueCat3);

            Cell cellActive3 = new Cell() { CellReference = "AA1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValueActive3 = new CellValue();
            cellValueActive3.Text = "Droga Activo 3";
            cellActive3.Append(cellValueActive3);

            Cell cellCat4 = new Cell() { CellReference = "AB1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValueCat4 = new CellValue();
            cellValueCat4.Text = "Droga Cat.4";
            cellCat4.Append(cellValueCat4);

            Cell cellActive4 = new Cell() { CellReference = "AC1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValueActive4 = new CellValue();
            cellValueActive4.Text = "Droga Activo 4";
            cellActive4.Append(cellValueActive4);

            Cell cellCat5 = new Cell() { CellReference = "AD1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValueCat5 = new CellValue();
            cellValueCat5.Text = "Droga Cat.5";
            cellCat5.Append(cellValueCat5);

            Cell cellActive5 = new Cell() { CellReference = "AE1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValueActive5 = new CellValue();
            cellValueActive5.Text = "Droga Activo 5";
            cellActive5.Append(cellValueActive5);

            row4.Append(cell9);
            row4.Append(cellCI);
            row4.Append(cell10);
            row4.Append(cellTime);
            row4.Append(cellDev);
            row4.Append(cellSex);
            row4.Append(cellWeight);
            row4.Append(cellHeight);
            row4.Append(cellAge);
            row4.Append(cellSmoker);
            row4.Append(cellDysli);
            row4.Append(cellDiabetic);
            row4.Append(cellHypertense);
            row4.Append(cellMassIndex);
            row4.Append(cellFatper);
            row4.Append(cellMuscle);
            row4.Append(cell2);
            row4.Append(cell3);
            row4.Append(cell4);
            row4.Append(cell5);
            row4.Append(cellSleep);
            row4.Append(cellCat1);
            row4.Append(cellActive1);
            row4.Append(cellCat2);
            row4.Append(cellActive2);
            row4.Append(cellCat3);
            row4.Append(cellActive3);
            row4.Append(cellCat4);
            row4.Append(cellActive4);
            row4.Append(cellCat5);
            row4.Append(cellActive5);

            //sheetData2.Append(row3);
            sheetData2.Append(row4);

            
            int i = 4;
            foreach (var r in report)
            {
                foreach (var m in r.Measures)
                {
                    UInt32Value ui = (uint)i;
                    Row row5 = new Row() { RowIndex = ui, Spans = new ListValue<StringValue>() { InnerText = "1:2" } };

                    string cellRefA = "A" + i.ToString();
                    Cell cell11 = new Cell() { CellReference = cellRefA };
                    CellValue cellValue9 = new CellValue();
                    //Identificador del reporte
                    cellValue9.Text = r.UdaId.HasValue ? r.UdaId.Value.ToString() : "";

                    //Documento de Identidad
                    string cellRefB = "B" + i;
                    Cell cellDNI = new Cell() { CellReference = cellRefB };
                    CellValue cellValueDNI = new CellValue();

                    if(r.Patient.DocumentId != null)
                    {
                        cellValueDNI.Text = r.Patient.DocumentId;
                    }

                    string cellRefC = "C" + i.ToString();
                    Cell cell16 = new Cell() { CellReference = cellRefC };
                    CellValue cellValue14 = new CellValue();
                    //Fecha del reporte
                    cellValue14.Text = r.BeginDate != null ? r.BeginDate.Value.ToShortDateString() : "";

                    string cellRefD = "D" + i.ToString();
                    Cell cell17 = new Cell() { CellReference = cellRefD };
                    CellValue cellValue17 = new CellValue();
                    //Hora del reporte
                    cellValue17.Text = r.BeginDate != null ? r.BeginDate.Value.ToShortTimeString() : "";

                    string cellRefE = "E" + i.ToString();
                    Cell cell18 = new Cell() { CellReference = cellRefE };
                    CellValue cellValue18 = new CellValue();
                    //Dispositivo
                    switch (r.DeviceId)
                    {
                        case 0:
                            cellValue18.Text = "HMS";
                            break;
                        case 1:
                            cellValue18.Text = "Spacelabs";
                            break;
                    }

                    string cellRefF = "F" + i.ToString();
                    Cell cellSexo = new Cell() { CellReference = cellRefF };
                    CellValue cellValueSexo = new CellValue();
                    //Sexo
                    if (r.Patient.Sex.HasValue)
                    {
                        cellValueSexo.Text = r.Patient.Sex.Value.ToString();
                    }

                    string cellRefG = "G" + i.ToString();
                    Cell cell19 = new Cell() { CellReference = cellRefG, StyleIndex = (UInt32Value)3U };
                    CellValue cellValue19 = new CellValue();
                    //Peso del paciente
                    if (r.TemporaryData.Weight != null)
                    {
                        cellValue19.Text = ((double)r.TemporaryData.Weight.Value).ToString();
                    }

                    string cellRefH = "H" + i.ToString();
                    Cell cell20 = new Cell() { CellReference = cellRefH, StyleIndex = (UInt32Value)3U };
                    CellValue cellValue20 = new CellValue();
                    //Altura del paciente
                    if (r.TemporaryData.Height != null)
                    {
                        double height = (double)r.TemporaryData.Height.Value * 100;
                        cellValue20.Text = height.ToString();
                    }

                    string cellRefI = "I" + i.ToString();
                    Cell cell21 = new Cell() { CellReference = cellRefI };
                    CellValue cellValue21 = new CellValue();
                    //Edad del paciente
                    if (r.TemporaryData.Age != null)
                    {
                        cellValue21.Text = r.TemporaryData.Age.Value.ToString();
                    }

                    string cellRefJ = "J" + i.ToString();
                    Cell cell22 = new Cell() { CellReference = cellRefJ };
                    CellValue cellValue22 = new CellValue();
                    //Fumador
                    if (r.TemporaryData.Smoker.HasValue)
                    {
                        cellValue22.Text = r.TemporaryData.Smoker.Value ? "SI" : "NO";
                    }

                    string cellRefK = "K" + i.ToString();
                    Cell cell23 = new Cell() { CellReference = cellRefK };
                    CellValue cellValue23 = new CellValue();
                    //Dilipidemico
                    if (r.TemporaryData.Dyslipidemia.HasValue)
                    {
                        cellValue23.Text = r.TemporaryData.Dyslipidemia.Value ? "SI" : "NO";
                    }

                    string cellRefL = "L" + i.ToString();
                    Cell cell24 = new Cell() { CellReference = cellRefL };
                    CellValue cellValue24 = new CellValue();
                    //Diabetico
                    if (r.TemporaryData.Diabetic.HasValue)
                    {
                        cellValue24.Text = r.TemporaryData.Diabetic.Value ? "SI" : "NO";
                    }

                    string cellRefM = "M" + i.ToString();
                    Cell cell25 = new Cell() { CellReference = cellRefM };
                    CellValue cellValue25 = new CellValue();
                    //Hipertenso
                    if (r.TemporaryData.Hypertensive.HasValue)
                    {
                        cellValue25.Text = r.TemporaryData.Hypertensive.Value ? "SI" : "NO";
                    }

                    string cellRefN = "N" + i.ToString();
                    Cell cell26 = new Cell() { CellReference = cellRefN, StyleIndex = (UInt32Value)3U };
                    CellValue cellValue26 = new CellValue();
                    //Masa corporal
                    if (r.TemporaryData.BodyMassIndex.HasValue)
                    {
                        cellValue26.Text = r.TemporaryData.BodyMassIndex.Value.ToString();
                    }

                    string cellRefO = "O" + i.ToString();
                    Cell cell27 = new Cell() { CellReference = cellRefO, StyleIndex = (UInt32Value)3U };
                    CellValue cellValue27 = new CellValue();
                    //Porcentaje de grasa
                    if (r.TemporaryData.FatPercentage.HasValue)
                    {
                        cellValue27.Text = r.TemporaryData.FatPercentage.Value.ToString();
                    }

                    string cellRefP = "P" + i.ToString();
                    Cell cell28 = new Cell() { CellReference = cellRefP, StyleIndex = (UInt32Value)3U };
                    CellValue cellValue28 = new CellValue();
                    //Porcentaje muscular
                    if (r.TemporaryData.MusclePercentage.HasValue)
                    {
                        cellValue28.Text = r.TemporaryData.MusclePercentage.Value.ToString();
                    }

                    //Sistolica
                    string cellRefSys = "Q" + i;
                    Cell cellSys = new Cell() { CellReference = cellRefSys };
                    CellValue cellValueSys = new CellValue();
                    if (m.Systolic != null)
                    {
                        cellValueSys.Text = m.Systolic.Value.ToString();
                    }

                    //Diastolica
                    string cellRefDias = "R" + i;
                    Cell cellDias = new Cell() { CellReference = cellRefDias };
                    CellValue cellValueDias = new CellValue();
                    if (m.Diastolic != null)
                    {
                        cellValueDias.Text = m.Diastolic.Value.ToString();
                    }

                    //Media
                    string cellRefMiddle = "S" + i;
                    Cell cellMiddle = new Cell() { CellReference = cellRefMiddle };
                    CellValue cellValueMiddle = new CellValue();
                    if (m.Middle != null)
                    {
                        cellValueMiddle.Text = m.Middle.Value.ToString();
                    }

                    //FC
                    string cellRefHr = "T" + i;
                    Cell cellHr = new Cell() { CellReference = cellRefHr };
                    CellValue cellValueHr = new CellValue();
                    if (m.HeartRate != null)
                    {
                        cellValueHr.Text = m.HeartRate.Value.ToString();
                    }

                    //Durmiendo
                    string cellRefAsleep = "U" + i;
                    Cell cellAsleep = new Cell() { CellReference = cellRefAsleep };
                    CellValue cellValueAsleep = new CellValue();
                    if (m.Asleep != null)
                    {
                        cellValueAsleep.Text = m.Asleep.Value.ToString();
                    }
                    
                    cell11.Append(cellValue9);
                    cellDNI.Append(cellValueDNI);
                    cell16.Append(cellValue14);
                    cell17.Append(cellValue17);
                    cell18.Append(cellValue18);
                    cellSexo.Append(cellValueSexo);
                    cell19.Append(cellValue19);
                    cell20.Append(cellValue20);
                    cell21.Append(cellValue21);
                    cell22.Append(cellValue22);
                    cell23.Append(cellValue23);
                    cell24.Append(cellValue24);
                    cell25.Append(cellValue25);
                    cell26.Append(cellValue26);
                    cell27.Append(cellValue27);
                    cell28.Append(cellValue28);
                    cellSys.Append(cellValueSys);
                    cellDias.Append(cellValueDias);
                    cellMiddle.Append(cellValueMiddle);
                    cellHr.Append(cellValueHr);
                    cellAsleep.Append(cellValueAsleep);

                    row5.Append(cell11);
                    row5.Append(cellDNI);
                    row5.Append(cell16);
                    row5.Append(cell17);
                    row5.Append(cell18);
                    row5.Append(cellSexo);
                    row5.Append(cell19);
                    row5.Append(cell20);
                    row5.Append(cell21);
                    row5.Append(cell22);
                    row5.Append(cell23);
                    row5.Append(cell24);
                    row5.Append(cell25);
                    row5.Append(cell26);
                    row5.Append(cell27);
                    row5.Append(cell28);
                    row5.Append(cellSys);
                    row5.Append(cellDias);
                    row5.Append(cellMiddle);
                    row5.Append(cellHr);
                    row5.Append(cellAsleep);

                    string[] colRef = new string[10];
                    colRef[0] = "V"; colRef[1] = "W";
                    colRef[2] = "X"; colRef[3] = "Y";
                    colRef[4] = "Z"; colRef[5] = "AA";
                    colRef[6] = "AB"; colRef[7] = "AC";
                    colRef[8] = "AD"; colRef[9] = "AE";

                    int j = 0;

                    //Medicamentos
                    foreach (var med in r.TemporaryData.Medication)
                    {
                        if (j < 10)
                        {
                            string cellRefCategory = colRef[j] + i;
                            Cell cellCategory = new Cell() { CellReference = cellRefCategory };
                            CellValue cellValueCategory = new CellValue();
                            cellValueCategory.Text = med.Drug.Category;

                            cellCategory.Append(cellValueCategory);
                            row5.Append(cellCategory);

                            j++;
                            string cellRefActive = colRef[j] + i;
                            Cell cellActive = new Cell() { CellReference = cellRefActive };
                            CellValue cellValueActive = new CellValue();
                            cellValueActive.Text = med.Drug.Active;

                            cellActive.Append(cellValueActive);
                            row5.Append(cellActive);

                            j++;                            
                        }
                        else
                        {
                            break;
                        }
                    }

                    sheetData2.Append(row5);

                    i++;
                    
                }
            }

            PageMargins pageMargins2 = new PageMargins() { Left = 0.7D, Right = 0.7D, Top = 0.7D, Bottom = 0.7D, Header = 0.3D, Footer = 0.3D };

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
            Column column4 = new Column() { Min = (UInt32Value)1U, Max = (UInt32Value)1U, Width = 27D, BestFit = true, CustomWidth = true };

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
            cellValue16.Text = investigation.CreationDate.Date.ToString(ConfigurationManager.AppSettings["ShortDateString"]);

            cell18.Append(cellValue16);

            row10.Append(cell17);
            row10.Append(cell18);

            sheetData3.Append(row8);
            sheetData3.Append(row9);
            sheetData3.Append(row10);
            PageMargins pageMargins3 = new PageMargins() { Left = 0.7D, Right = 0.7D, Top = 0.7D, Bottom = 0.7D, Header = 0.3D, Footer = 0.3D };
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
            FontSize fontSize2 = new FontSize() { Val = 11D };
            Color color2 = new Color() { Theme = (UInt32Value)1U };
            FontName fontName2 = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering2 = new FontFamilyNumbering() { Val = 2 };
            FontScheme fontScheme2 = new FontScheme() { Val = FontSchemeValues.Minor };

            font2.Append(fontSize2);
            font2.Append(color2);
            font2.Append(fontName2);
            font2.Append(fontFamilyNumbering2);
            font2.Append(fontScheme2);

            Font font3 = new Font();
            FontSize fontSize3 = new FontSize() { Val = 14D };
            Color color3 = new Color() { Theme = (UInt32Value)1U };
            FontName fontName3 = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering3 = new FontFamilyNumbering() { Val = 2 };
            FontScheme fontScheme3 = new FontScheme() { Val = FontSchemeValues.Minor };

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
            CellFormat cellFormat5 = new CellFormat() { NumberFormatId = (UInt32Value)4U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyFont = true };

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

        #endregion
    }
}
