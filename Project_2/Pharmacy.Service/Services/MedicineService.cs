using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using Pharmacy.ActiveRecord.ActiveRecord;

namespace Pharmacy.Service.Services
{
    public class MedicineService
    {
        public List<Medicine> listWithCurrentObject;

        public List<Medicine> GetMedicines()
        {
            Medicine medicine = new Medicine();
            return medicine.Reload();
        }

        public List<Medicine> GetMedicinesById(int id)
        {
            Medicine medicine = new Medicine();
            listWithCurrentObject = new List<Medicine>();
            var medicineById = medicine.GetbyId(id);
            listWithCurrentObject.Add(medicineById);
            return listWithCurrentObject;
        }

        public bool Exist(int id)
        {
            Medicine medicine = new Medicine();
            var list = medicine.Reload();
            if (list.Exists(medicine => medicine.Id == id))
            {
                return true;
            }

            return false;
        }


        public void AddMedicine(int id, string name, string manufacturer, decimal price, decimal amount,
            bool withPrescription)
        {
            Medicine medicine = new Medicine()
            {
                Id = id,
                Name = name,
                Manufacturer = manufacturer,
                Price = price,
                Amount = amount,
                WithPrescription = withPrescription,
            };
            medicine.Save(medicine);
        }

        public void EditName(int id, string name)
        {
            Medicine medicine = new Medicine();
            {
                medicine.Id = id;
                medicine.Name = name;
                medicine.Manufacturer = listWithCurrentObject[0].Manufacturer;
                medicine.Price = (decimal) listWithCurrentObject[0].Price;
                medicine.Amount = (decimal) listWithCurrentObject[0].Amount;
                medicine.WithPrescription = (bool) listWithCurrentObject[0].WithPrescription;
            }
            ;
            medicine.Save(medicine);
        }

        public void EditManufacturer(int id, string manufacturer)
        {
            Medicine medicine = new Medicine();
            {
                medicine.Id = id;
                medicine.Name = listWithCurrentObject[0].Name;
                medicine.Manufacturer = manufacturer;
                medicine.Price = (decimal) listWithCurrentObject[0].Price;
                medicine.Amount = (decimal) listWithCurrentObject[0].Amount;
                medicine.WithPrescription = (bool) listWithCurrentObject[0].WithPrescription;
            }
            ;
            medicine.Save(medicine);
        }

        public void EditPrice(int id, decimal price)
        {
            Medicine medicine = new Medicine();
            {
                medicine.Id = id;
                medicine.Name = listWithCurrentObject[0].Name;
                medicine.Manufacturer = listWithCurrentObject[0].Manufacturer;
                medicine.Price = price;
                medicine.Amount = (decimal) listWithCurrentObject[0].Amount;
                medicine.WithPrescription = (bool) listWithCurrentObject[0].WithPrescription;
            }
            ;
            medicine.Save(medicine);
        }

        public void EditAmount(int id, decimal amount)
        {
            Medicine medicine = new Medicine();
            {
                medicine.Id = id;
                medicine.Name = listWithCurrentObject[0].Name;
                medicine.Manufacturer = listWithCurrentObject[0].Manufacturer;
                medicine.Price = (decimal) listWithCurrentObject[0].Price;
                medicine.Amount = amount;
                medicine.WithPrescription = (bool) listWithCurrentObject[0].WithPrescription;
            }
            ;
            medicine.Save(medicine);
        }

        public void EditWithPrescription(int id, bool withPrescription)
        {
            Medicine medicine = new Medicine();
            {
                medicine.Id = id;
                medicine.Name = listWithCurrentObject[0].Name;
                medicine.Manufacturer = listWithCurrentObject[0].Manufacturer;
                medicine.Price = (decimal) listWithCurrentObject[0].Price;
                medicine.Amount = (decimal) listWithCurrentObject[0].Amount;
                medicine.WithPrescription = withPrescription;
            }
            ;
            medicine.Save(medicine);
        }

        public void RemoveMedicine(int id)
        {
            Medicine medicine = new Medicine();
            medicine.Remove(id);
        }

        public decimal CheckAmount(int id)
        {
            Medicine medicine = new Medicine();
            listWithCurrentObject = new List<Medicine>();
            var medicineById = medicine.GetbyId(id);
            return decimal.Parse(medicineById.Amount.ToString());
        }
    }
}