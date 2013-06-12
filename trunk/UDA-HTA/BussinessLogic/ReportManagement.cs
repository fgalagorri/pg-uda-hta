using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Entities;
using DataAccess;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace BussinessLogic
{
    public class ReportManagement
    {

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

        public void ExportReportPdf(Report report, string fileName)
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
