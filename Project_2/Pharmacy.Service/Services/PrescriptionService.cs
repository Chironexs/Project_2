using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pharmacy.ActiveRecord.ActiveRecord;

namespace Pharmacy.Service.Services
{
    public class PrescriptionService
    {
        public List<Prescription> listWithCurrentObject;

        public List<Prescription> GetPrescription()
        {
            Prescription prescription = new Prescription();
            return prescription.Reload();
        }

        public List<Prescription> GetPrescriptionsById(int id)
        {
            Prescription prescription = new Prescription();
            listWithCurrentObject = new List<Prescription>();
            var prescriptionById = prescription.GetbyId(id);
            listWithCurrentObject.Add(prescriptionById);
            return listWithCurrentObject;
        }

        public bool IsExistPrescriptionUser(string pesel)
        {
            Prescription prescription = new Prescription();
            var list = prescription.Reload();
            if (list.Exists(prescription => prescription.Pesel == pesel))
            {
                return true;
            }

            return false;
        }
        public string GetPrescriptionClientName(string pesel)
        {
            Prescription prescription = new Prescription();
            var list = prescription.Reload();
            var result = list.Find(prescription => prescription.Pesel == pesel).CustomerName;
            return result;
        }
        public int AddPrescription(string customerName, string pesel, string prescriptionNumber)
        {
            Prescription prescription = new Prescription()
            {
                CustomerName = customerName,
                Pesel = pesel,
                PrescriptionNumber = prescriptionNumber
            };
            var prescriptionId = prescription.Save(prescription);
            return prescriptionId;
        }
//        public List<Prescription> GetId(int id)
//        {
//            Prescription prescription = new Prescription();
//            listWithCurrentObject = new List<Prescription>();
//            var prescriptionById = prescription.ShowId(prescription.CustomerName);
//            
//            return listWithCurrentObject;
//        }

    }
}