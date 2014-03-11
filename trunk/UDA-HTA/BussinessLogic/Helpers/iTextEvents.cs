using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using sd = System.Drawing;
using Entities;

namespace BussinessLogic
{
    public class iTextEvents : PdfPageEventHelper
    {
        // This is the contentbyte object of the writer
        PdfContentByte cb;

        // we will put the final number of pages in a template
        PdfTemplate headerTemplate;

        // this is the BaseFont we are going to use for the header / footer
        BaseFont bf = null;

        // This keeps track of the creation time
        DateTime PrintTime = DateTime.Now;

        #region Fields
        private string _header;

        private Patient _patient;
        #endregion

        #region Properties
        public string Header
        {
            get { return _header; }
            set { _header = value; }
        }

        public void SetPatient(Patient p)
        {
            _patient = p;
        }
        #endregion


        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            try
            {
                PrintTime = DateTime.Now;
                bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb = writer.DirectContent;
                headerTemplate = cb.CreateTemplate(100, 100);
            }
            catch (DocumentException de)
            {

            }
            catch (System.IO.IOException ioe)
            {

            }
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);

            Font baseFontNormal = new Font(Font.FontFamily.HELVETICA, 9f, Font.NORMAL, BaseColor.GRAY);

            if (document.PageNumber == 1)
            {
                Phrase p1Header = new Phrase("Hospital de Clínicas - Unidad de Hipertensión Arterial", baseFontNormal);

                //Header primera pagina
                //Create PdfTable object
                PdfPTable pdfTab = new PdfPTable(4);

                //We will have to create separate cells to include image logo and 2 separate strings
                //Row 1
                using (FileStream fs = new FileStream(ConfigurationManager.AppSettings["HCLogo"],FileMode.Open))
                {
                    Image png = Image.GetInstance(sd.Image.FromStream(fs), ImageFormat.Png);
                    png.ScalePercent(80);
                    PdfPCell pdfCell1 = new PdfPCell();
                    pdfCell1.AddElement(png);

                    PdfPCell pdfCell2 = new PdfPCell(p1Header);
                    PdfPCell pdfCell3 = new PdfPCell(new Phrase(PrintTime.ToShortDateString(), baseFontNormal));


                    //set the alignment of all three cells and set border to 0
                    pdfCell1.HorizontalAlignment = Element.ALIGN_CENTER;
                    pdfCell2.HorizontalAlignment = Element.ALIGN_CENTER;
                    pdfCell3.HorizontalAlignment = Element.ALIGN_CENTER;

                    pdfCell1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    pdfCell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                    pdfCell3.VerticalAlignment = Element.ALIGN_MIDDLE;

                    pdfCell1.Border = 0;
                    pdfCell2.Border = 0;
                    pdfCell3.Border = 0;

                    pdfCell2.Colspan = 2;

                    //add all three cells into PdfTable
                    pdfTab.AddCell(pdfCell1);
                    pdfTab.AddCell(pdfCell2);
                    pdfTab.AddCell(pdfCell3);

                    pdfTab.TotalWidth = document.PageSize.Width - 80f;
                    pdfTab.WidthPercentage = 70;


                    //call WriteSelectedRows of PdfTable. This writes rows from PdfWriter in PdfTable
                    //first param is start row. -1 indicates there is no end row and all the rows to be included to write
                    //Third and fourth param is x and y position to start writing
                    pdfTab.WriteSelectedRows(0, -1, 40, document.PageSize.Height - 30, writer.DirectContent);
                    //set pdfContent value

                    //Move the pointer and draw line to separate header section from rest of page
                    cb.MoveTo(40, document.PageSize.Height - 100);
                    cb.LineTo(document.PageSize.Width - 40, document.PageSize.Height - 100);
                    cb.Stroke();

                    fs.Close();
                }

            }
            else
            {
                //Header del resto de las paginas
                Phrase c1Header = new Phrase("Hospital de Clínicas", baseFontNormal);
                Phrase c2Header = new Phrase("Informe de Monitoreo de Presión Arterial", baseFontNormal);                

                //Header primera pagina
                //Create PdfTable object
                PdfPTable pdfTab = new PdfPTable(3);

                //We will have to create separate cells to include image logo and 2 separate strings
                //Row 1
                PdfPCell pdfCell1 = new PdfPCell(c1Header);
                PdfPCell pdfCell2 = new PdfPCell(c2Header);
                PdfPCell pdfCell3 = new PdfPCell(new Phrase(PrintTime.ToShortDateString(), baseFontNormal));
                 
                //Row 2
                PdfPCell pdfCell4 = new PdfPCell(new Phrase("Paciente: " + _patient.RegisterNumber, baseFontNormal));
                PdfPCell pdfCell5 = new PdfPCell(new Phrase(_patient.Names + _patient.Surnames, baseFontNormal));
                PdfPCell pdfCell6 = new PdfPCell(new Phrase("CI: " + _patient.DocumentId,baseFontNormal));

                //set the alignment of all three cells and set border to 0
                pdfCell1.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfCell2.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfCell3.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfCell4.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfCell5.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfCell6.HorizontalAlignment = Element.ALIGN_CENTER;

                pdfCell1.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell4.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell5.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell6.VerticalAlignment = Element.ALIGN_MIDDLE;
                
                pdfCell1.Border = 0;
                pdfCell2.Border = 0;
                pdfCell3.Border = 0;
                pdfCell4.Border = 0;
                pdfCell5.Border = 0;
                pdfCell6.Border = 0;

                //add all three cells into PdfTable
                pdfTab.AddCell(pdfCell1);
                pdfTab.AddCell(pdfCell2);
                pdfTab.AddCell(pdfCell3);
                pdfTab.AddCell(pdfCell4);
                pdfTab.AddCell(pdfCell5);
                pdfTab.AddCell(pdfCell6);

                pdfTab.TotalWidth = document.PageSize.Width - 80f;
                pdfTab.WidthPercentage = 70;

                //call WriteSelectedRows of PdfTable. This writes rows from PdfWriter in PdfTable
                //first param is start row. -1 indicates there is no end row and all the rows to be included to write
                //Third and fourth param is x and y position to start writing
                pdfTab.WriteSelectedRows(0, -1, 40, document.PageSize.Height - 30, writer.DirectContent);
                //set pdfContent value

                //Move the pointer and draw line to separate header section from rest of page
                cb.MoveTo(40, document.PageSize.Height - 80);
                cb.LineTo(document.PageSize.Width - 40, document.PageSize.Height - 80);
                cb.Stroke();

            }

        }
        /*
        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);

            headerTemplate.BeginText();
            headerTemplate.SetFontAndSize(bf, 12);
            headerTemplate.SetTextMatrix(0, 0);
            headerTemplate.ShowText((writer.PageNumber - 1).ToString());
            headerTemplate.EndText();

            footerTemplate.BeginText();
            footerTemplate.SetFontAndSize(bf, 12);
            footerTemplate.SetTextMatrix(0, 0);
            footerTemplate.ShowText((writer.PageNumber - 1).ToString());
            footerTemplate.EndText();

        }*/
  
    }
}
