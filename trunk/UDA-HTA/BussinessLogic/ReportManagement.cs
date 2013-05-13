using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfaceBussinessLogic;
using Entities;
using DataAccess;
using PdfSharp;
using PdfSharp.Pdf;

namespace BussinessLogic
{
    public class ReportManagement : IReportManagement
    {

        public void addDrug(int type, string name)
        {
            UdaHtaDataAccess uhda = new UdaHtaDataAccess();
            uhda.insertDrug(name, type);
        }

        public void deleteDrug(string name)
        {
        }

        public void editDrug(string type, string name)
        {
        }


        public ICollection<Report> listPatientReports(int idPatient)
        {
            return null;
        }

        public void printReport(int idReport)
        {
        }

        public void exportReportPDF(Report report, string fileName)
        {
            //Crear documento PDF
            PdfDocument doc = new PdfDocument();
            doc.Info.Title = "Informe de Hipertensión Arterial";

            //Crear pagina vacia
            PdfPage page = doc.AddPage();

            // . . . . . 

            //Guardar el documento
            doc.Save(fileName);
        }

        public void addReport(Report report, string idPatient, DailyCarnet dailyCarnet, TemporaryData temporaryData)
        {
            var uhda = new UdaHtaDataAccess();
            uhda.InsertReport(int.Parse(idPatient),report, dailyCarnet,temporaryData);
        }
    }
}
