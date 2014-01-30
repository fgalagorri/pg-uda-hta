using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services.Description;
using Entities;
using IntegrationHC.DatosPacienteCI;
using IntegrationHC.DatosPacienteRegistro;

namespace IntegrationHC
{
    public static class PatientIntegration
    {
        public static Patient GetPatientByDocument(string document)
        {
            int docInt = Int32.Parse(document.Substring(0, document.Length - 1));
            sbyte verification = (sbyte)Char.GetNumericValue(document.ElementAt(document.Length - 1));

            var dpList = new List<DatPersonaCdaDatPersonaCdaItem>().ToArray();
            var service = new WebSrvDatosPersonaCda();
            service.Execute(ref dpList, ref docInt, ref verification);

            if (dpList.Length > 0)
            {
                string name = dpList[0].DatPersonaPrimerNombre + " " +
                              dpList[0].DatPersonaSegundoNombre;

                string surnames = dpList[0].DatPersonaPrimerApellido + " " +
                                  dpList[0].DatPersonaSegundoApellido;

                DateTime birthDate;
                bool bDate = DateTime.TryParse(dpList[0].DatPersonaFchNac, out birthDate);

                return new Patient
                {
                    DocumentId = document,
                    Names = name.Trim(),
                    Surnames = surnames.Trim(),
                    BirthDate = bDate ? (DateTime?)birthDate : null,
                    Sex = dpList[0].DatPersonaSexo == "M" ? SexType.M : SexType.F,
                    Address = dpList[0].DatPersonaDomicilio,
                    City = dpList[0].DatPersonalLocalidad,
                    Department = dpList[0].DatPersonaDepto,
                    Phone = dpList[0].DatPersonaTelefono,
                    RegisterNumber = dpList[0].DatPersonaRegNum
                };
            }

            return null;
        }


        public static Patient GetPatientByRegister(string register)
        {
            int regNo = Int32.Parse(register);
            DatPersona_2DatPersona_2Item[] dpList = new List<DatPersona_2DatPersona_2Item>().ToArray();
            var registro = new RegistrosBuscadosRegistrosBuscadosItem {NumeroRegistroBuscado = regNo};
            var regList = new[] {registro};
            
            using (var service = new WebSrvDarColPer1())
            {
                service.Execute(ref dpList, ref regList);
            }

            if (dpList.Length > 0)
            {
                DateTime birthDate;
                bool bDate = DateTime.TryParse(dpList[0].DatPersonaFchNac2, out birthDate);

                return new Patient
                {
                    DocumentId = dpList[0].DatPersonaCedNum2 + dpList[0].DatPersonaVerNum2,
                    Names = dpList[0].DatPersonaNombres2,
                    Surnames = dpList[0].DatPersonaApellidos2,
                    BirthDate = bDate ? (DateTime?) birthDate : null,
                    Sex = dpList[0].DatPersonaSexo2 == "M" ? SexType.M : SexType.F,
                    Address = dpList[0].DatPersonaUltDomicilio2,
                    Phone = dpList[0].DatPersonaUltTelefono2,
                    RegisterNumber = dpList[0].DatPersonaRegNum2
                };
            }
                

            return null;
        }
    }
}
