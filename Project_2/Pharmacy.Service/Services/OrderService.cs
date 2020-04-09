using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;
using Pharmacy.ActiveRecord;
using Pharmacy.ActiveRecord.ActiveRecord;

namespace Pharmacy.Service.Services
{
    public class OrderService
    {
        public List<Order> listWithCurrentObject;

        public List<Order> GetOrder()
        {
            Order order = new Order();
            return order.Reload();
        }

        public List<Order> GetOrderById(int id)
        {
            Order order = new Order();
            listWithCurrentObject = new List<Order>();
            var orderById = order.GetbyId(id);
            listWithCurrentObject.Add(orderById);
            return listWithCurrentObject;
        }

        public bool Exist(int id)
        {
            Order order = new Order();
            var list = order.Reload();
            if (list.Exists(order => order.Id == id))
            {
                return true;
            }

            return false;
        }


        public void AddOrder( int? prescriptionId, int medicineId, decimal amount)
        {
            Order order = new Order()
            {
                PrescriptionId = prescriptionId,
                MedicineId = medicineId,
                Date = DateTime.Now,
                Amount = amount
            };
            order.Save(order);
        }
    }
}