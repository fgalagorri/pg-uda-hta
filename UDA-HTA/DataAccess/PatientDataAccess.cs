using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using MySql.Data.MySqlClient;
using Entities;

namespace DataAccess
{
    public class PatientDataAccess
    {
        public PatientDataAccess()
        {
        }

        public long InsertPatient(Patient p)
        {
            using (var udaContext = new patient_info_dbEntities())
            {
                ObjectParameter lastIdPatient = new ObjectParameter("id", typeof (int));
                ObjectParameter lastIdDevRef = new ObjectParameter("id", typeof (int));

                var sex = SexType.M.ToString();
                if (p.Sex != null && ((p.Sex.Value == SexType.F) || (p.Sex.Value == SexType.M)))
                    sex = p.Sex.Value.ToString();

                udaContext.insertPatient(lastIdPatient, p.Names, p.Surnames, p.Address, p.DocumentId,
                                         p.BirthDate, sex, p.Neighbour, p.City, p.Department,
                                         p.Phone, p.CellPhone, p.Phone2, p.Email, p.RegisterNumer);

                foreach (var devRef in p.DeviceReferences)
                    udaContext.insertDeviceReference(lastIdDevRef, devRef.deviceType, 
                                                     devRef.deviceReferenceId,
                                                     (long) lastIdPatient.Value);

                return (long) lastIdPatient.Value;
            }
        }


        public void InsertEmergencyContact(long patientId, ICollection<EmergencyContact> contacts)
        {
            using (var patientContext = new patient_info_dbEntities())
            {
                ObjectParameter lastId = new ObjectParameter("id", typeof (long));

                // TODO: ver si usando AddToemergency_contact(ec) no mejora
                foreach (var ec in contacts)
                    patientContext.insertEmergencyContact(lastId, ec.Name, ec.Surname, ec.Phone, patientId);
            }
        }


        /*
         * Lista todos los pacientes existentes en la base.
         */
        public ICollection<PatientSearch> ListPatients(string documentId, string names, string surnames,
                                                       DateTime? birthDate, string registerNo)
        {
            using (var patientContext = new patient_info_dbEntities())
            {
                var list = patientContext.patient.AsQueryable();

                if (!string.IsNullOrWhiteSpace(documentId))
                    list = list.Where(p => p.document.Contains(documentId));
                if (!string.IsNullOrWhiteSpace(names))
                    list = list.Where(p => p.name.Contains(names));
                if (!string.IsNullOrWhiteSpace(surnames))
                    list = list.Where(p => p.surname.Contains(surnames));
                if (birthDate.HasValue)
                    list = list.Where(p => p.birthday.Value.Date == birthDate.Value.Date);
                if (!String.IsNullOrWhiteSpace(registerNo))
                    list = list.Where(p => p.register_number.Contains(registerNo));

                return list.Select(p => new PatientSearch
                    {
                        UdaId = p.idPatient,
                        DocumentId = p.document,
                        Names = p.name,
                        Surnames = p.surname,
                        BirthDate = p.birthday,
                        RegisterNumer = p.register_number
                    }).ToList();
            }
        }


        public Patient GetPatient(long patientId)
        {
            using (var patientContext = new patient_info_dbEntities())
            {
                var pat = patientContext.patient.Where(p => p.idPatient == patientId)
                                        .Select(p => new
                                            {
                                                p.idPatient,
                                                p.address,
                                                p.birthday,
                                                p.cell_phone,
                                                p.city,
                                                p.document,
                                                p.e_mail,
                                                p.emergency_contact,
                                                p.gender,
                                                p.name,
                                                p.neighborhood,
                                                p.department,
                                                p.surname,
                                                p.telephone,
                                                p.telephone_alt
                                            }).ToList().First();

                Patient patient = new Patient
                    {
                        UdaId = pat.idPatient,
                        Address = pat.address,
                        BirthDate = pat.birthday,
                        CellPhone = pat.cell_phone,
                        City = pat.city,
                        DocumentId = pat.document,
                        Email = pat.e_mail,
                        Names = pat.name,
                        Neighbour = pat.neighborhood,
                        Department = pat.department,
                        Phone = pat.telephone,
                        Phone2 = pat.telephone_alt,
                        Sex = pat.gender == "M" ? SexType.M : SexType.F,
                        Surnames = pat.surname
                    };

                foreach (var ec in pat.emergency_contact)
                {
                    patient.EmergencyContactList.Add(new EmergencyContact
                        {
                            EmergencyContactId = ec.idemergency_contact,
                            Name = ec.name,
                            Phone = ec.phone,
                            Surname = ec.surname
                        });
                }

                return patient;
            }
        }


        public bool ExistPatientReference(string patientRef, int devType)
        {
            using (var patientContext = new patient_info_dbEntities())
            {
                return patientContext.device_reference.Any(p => p.device_ref == patientRef && p.device_type == devType);
            }
        }


        public long? GetPatientId(string patientRef, int dev)
        {
            using (var patientContext = new patient_info_dbEntities())
            {
                var pat =
                    patientContext.device_reference.Where(p => (p.device_ref == patientRef && p.device_type == dev))
                                  .Select(p => new {p.patient_idPatient})
                                  .FirstOrDefault();

                return (pat != null) ? (long?) pat.patient_idPatient : null;
            }
        }
    }
}
