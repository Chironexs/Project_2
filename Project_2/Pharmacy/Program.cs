using Pharmacy.Service.Services;
using System;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Channels;

namespace Pharmacy
{
    class Program
    {
        static void Main(string[] args)
        {
            MedicineService medicineService = new MedicineService();
            PrescriptionService prescriptionService = new PrescriptionService();
            OrderService orderService = new OrderService();

            string command = " ";

            do
            {
                Menu.ShowMainMenu();
                command = Console.ReadLine();
                switch (command)
                {
                    case "1": // MEDICINE

                        do
                        {
                            Menu.ShowPharmacyMenu();
                            command = Console.ReadLine();
                            switch (command)
                            {
                                case "1": // SHOW MEDICINE
                                    var list = medicineService.GetMedicines();

                                    Menu.ShowMedicinesHeader();
                                    foreach (var row in list)
                                    {
                                        StringBuilder sb = new StringBuilder();
                                        sb.Append($"{row.Id.ToString().PadRight(4, ' ')} | ");
                                        sb.Append($"{row.Name.PadRight(15, ' ')} | ");
                                        sb.Append($"{row.Manufacturer.PadRight(15, ' ')} | ");
                                        sb.Append($"{row.Price.ToString().PadRight(10, ' ')} | ");
                                        sb.Append($"{row.Amount.ToString().PadRight(10, ' ')} | ");
                                        sb.Append($"{row.WithPrescription.ToString().PadRight(10, ' ')} | ");
                                        Console.WriteLine((sb));
                                    }

                                    Menu.GreenText("\nNaciśnij dowolny klawisz aby powrócić do poprzedniego menu");
                                    Console.ReadKey();
                                    break;

                                case "2": // ADD MEDICINE

                                    Menu.GreenText("Podaj nazwę leku");
                                    var name = Console.ReadLine();
                                    Menu.GreenText("Podaj producenta leku");
                                    var manufacturer = Console.ReadLine();
                                    Menu.GreenText("Podaj cenę leku (000.00)");
                                    var price = DecimalParse(Console.ReadLine());
                                    Menu.GreenText("Podaj ilość (000.00)");
                                    var amount = DecimalParse(Console.ReadLine());
                                    Menu.GreenText("Czy lek jest na receptę (Y/N)");
                                    var withPrescription = TrueOrFalse(Console.ReadLine());
                                    var id = 0;
                                    medicineService.AddMedicine(id, name, manufacturer, price, amount,
                                        withPrescription);
                                    break;

                                case "3": // EDIT MEDICINE
                                    var commandEdit = " ";
                                    do
                                    {
                                        Menu.GreenText("Wpisz ID leku który chcesz edytować");
                                        var medicineId = IntParse(Console.ReadLine());
                                        var existId = Exist(medicineId);
                                        Menu.ShowMedicinesHeader();
                                        var list2 = medicineService.GetMedicinesById(existId);
                                        foreach (var row in list2)
                                        {
                                            StringBuilder sb = new StringBuilder();
                                            sb.Append($"{row.Id.ToString().PadRight(4, ' ')} | ");
                                            sb.Append($"{row.Name.PadRight(15, ' ')} | ");
                                            sb.Append($"{row.Manufacturer.PadRight(15, ' ')} | ");
                                            sb.Append($"{row.Price.ToString().PadRight(10, ' ')} | ");
                                            sb.Append($"{row.Amount.ToString().PadRight(10, ' ')} | ");
                                            sb.Append($"{row.WithPrescription.ToString().PadRight(10, ' ')} | ");
                                            Console.WriteLine((sb));
                                        }

                                        Menu.ShowMedicinesManagement();
                                        commandEdit = Console.ReadLine();
                                        switch (commandEdit)
                                        {
                                            case "1": // EDIT NAME
                                                Menu.GreenText("Wpisz nową nazwę:");
                                                var nameName = Console.ReadLine();
                                                medicineService.EditName(medicineId, nameName);
                                                commandEdit = "9";
                                                break;

                                            case "2": // EDIT MANUFACTURER
                                                Menu.GreenText("Wpisz nowego producenta:");
                                                var nameManuf = Console.ReadLine();
                                                medicineService.EditManufacturer(medicineId, nameManuf);
                                                commandEdit = "9";
                                                break;

                                            case "3": // EDIT PRICE
                                                Menu.GreenText("Wpisz nową cenę:");
                                                var namePrice = DecimalParse(Console.ReadLine());
                                                medicineService.EditPrice(medicineId, namePrice);
                                                commandEdit = "9";
                                                break;

                                            case "4": // EDIT AMOUNT
                                                Menu.GreenText("Wpisz nową ilość:");
                                                var nameAmount = DecimalParse(Console.ReadLine());
                                                medicineService.EditAmount(medicineId, nameAmount);
                                                commandEdit = "9";
                                                break;

                                            case "5": // EDIT PRESCRIPION
                                                Menu.GreenText("Wpisz czy wymagana recepta (Y/N):");
                                                var namePresc = TrueOrFalse(Console.ReadLine());
                                                medicineService.EditWithPrescription(medicineId, namePresc);
                                                commandEdit = "9";
                                                break;
                                        }
                                    } while (commandEdit != "9");


                                    break;

                                case "4": // REMOVE MEDICINE

                                    Menu.GreenText("Wpisz ID leku do usunięcia:");
                                    var removeId = IntParse(Console.ReadLine());
                                    medicineService.RemoveMedicine(removeId);
                                    break;
                            }
                        } while (command != "9");

                        break;

                    case "2": // SALE

                        do
                        {
                            Menu.ShowSaleMenu();
                            command = Console.ReadLine();
                            switch (command)
                            {
                                case "1": // NEW ORDER
                                    do
                                    {
                                        Menu.ShowPrescriptionMenu();
                                        command = Console.ReadLine();
                                        switch (command)
                                        {
                                            case "1": // With Prescription
                                                Console.WriteLine("Podaj PESEL klienta");
                                                var clientPesel = Console.ReadLine();
                                                var clientName = "";
                                                if (!prescriptionService.IsExistPrescriptionUser(clientPesel))
                                                {
                                                    Menu.GreenText("Podaj imię i nazwisko klienta");
                                                    clientName = Console.ReadLine();
                                                }
                                                else
                                                {
                                                    clientName =
                                                        prescriptionService.GetPrescriptionClientName(clientPesel);
                                                }

                                                Menu.GreenText("Podaj numer recepty");
                                                var clientPrescription = Console.ReadLine();
                                                var prescriptionId = prescriptionService.AddPrescription(clientName, clientPesel, clientPrescription); // dodawanie do bazy prescription, (zwraca ID prescriprion)

                                                var cc1 = "";
                                                do
                                                {
                                                    Menu.GreenText("Podaj ID leku");
                                                    var medicineId = IntParse(Console.ReadLine());
                                                    var existMedicineId = Exist(medicineId);

                                                    Menu.GreenText("Podaj ilość");
                                                    var amount = DecimalParse(Console.ReadLine());
                                                    var amountInDB = medicineService.CheckAmount(existMedicineId);
                                                    if (amount > amountInDB)
                                                    {
                                                    }

                                                    amount = amountInDB - amount;
                                                    medicineService.GetMedicinesById(existMedicineId); // dodanie danych do listy
                                                    medicineService.EditAmount(medicineId, amount);

                                                    orderService.AddOrder(prescriptionId, existMedicineId, amount);
                                                    Menu.GreenText("Aby zakończyć realizowanie recepty wciśnij 9");
                                                    Menu.GreenText("Aby dodać kolejny lek wciśnij dowolny klawisz");
                                                    cc1 = Console.ReadLine();
                                                } while (cc1 != "9");

                                                break;
                                            case "2": // Without Prescription

                                                Menu.GreenText("Podaj ID leku");
                                                var medicineIdWithoutPrescription = IntParse(Console.ReadLine());
                                                var existMedicineIdWithoutPrescription = Exist(medicineIdWithoutPrescription);

                                                Menu.GreenText("Podaj ilość");
                                                var amountWithoutPrescription = DecimalParse(Console.ReadLine());
                                                var amountInDBWithoutPrescription = medicineService.CheckAmount(existMedicineIdWithoutPrescription);
                                                if (amountWithoutPrescription > amountInDBWithoutPrescription)
                                                {
                                                }

                                                amountWithoutPrescription = amountInDBWithoutPrescription - amountWithoutPrescription;
                                                medicineService.GetMedicinesById(existMedicineIdWithoutPrescription); // dodanie danych do listy
                                                medicineService.EditAmount(medicineIdWithoutPrescription, amountWithoutPrescription);

                                                int? WithoutPrescription = null;
                                                orderService.AddOrder(WithoutPrescription, existMedicineIdWithoutPrescription, amountWithoutPrescription);
                                                Console.ReadKey();
                                                break;
                                        }
                                    } while (command != "9");

                                    break;
                                    Console.ReadKey();
                                    break;

                                case "2": // Show prescription

                                    var list = orderService.GetOrder();
                                    StringBuilder sb1 = new StringBuilder();
                                    sb1.Append("ID".PadRight(10, ' ') + "| ");
                                    sb1.Append("MedicineId".PadRight(10, ' ') + "| ");
                                    sb1.Append("PrescriptionId".PadRight(10, ' ') + "| ");
                                    sb1.Append("Date".PadRight(10, ' ') + "| ");
                                    sb1.Append("Amount".PadRight(10, ' ') + "| ");
                                    Console.WriteLine((sb1));
                                    foreach (var row in list)
                                    {
                                        StringBuilder sb = new StringBuilder();
                                        sb.Append($"{row.Id.ToString().PadRight(10, ' ')} | ");
                                        sb.Append($"{row.MedicineId.ToString().PadRight(10, ' ')} | ");
                                        sb.Append($"{row.PrescriptionId.ToString().PadRight(10, ' ')} | ");
                                        sb.Append($"{row.Date.ToString().PadRight(10, ' ')} | ");
                                        sb.Append($"{row.Amount.ToString().PadRight(10, ' ')} | ");
                                        Console.WriteLine((sb));
                                    }

                                    Menu.GreenText("\nNaciśnij dowolny klawisz aby powrócić do poprzedniego menu");
                                    Console.ReadKey();
                                    break;

                                case "3": // Show order

                                    var list3 = prescriptionService.GetPrescription();
                                    StringBuilder sb3 = new StringBuilder();
                                    sb3.Append("ID".PadRight(10, ' ') + "| ");
                                    sb3.Append("CustomerName".PadRight(10, ' ') + "| ");
                                    sb3.Append("Pesel".PadRight(10, ' ') + "| ");
                                    sb3.Append("PrescriptionNumber".PadRight(10, ' ') + "| ");
                                    Console.WriteLine((sb3));
                                    foreach (var row in list3)
                                    {
                                        StringBuilder sb = new StringBuilder();
                                        sb.Append($"{row.Id.ToString().PadRight(4, ' ')} | ");
                                        sb.Append($"{row.CustomerName.PadRight(15, ' ')} | ");
                                        sb.Append($"{row.Pesel.PadRight(15, ' ')} | ");
                                        sb.Append($"{row.PrescriptionNumber.PadRight(10, ' ')} | ");
                                        Console.WriteLine((sb));
                                    }

                                    Menu.GreenText("\nNaciśnij dowolny klawisz aby powrócić do poprzedniego menu");
                                    Console.ReadKey();
                                    break;
                            }
                        } while (command != "9");

                        break;
                }
            } while (command != "0");
        }

        private static bool TrueOrFalse(string text)
        {
            while (true)
            {
                if (text.ToLower() == "y")
                {
                    return true;
                }

                if (text.ToLower() == "n")
                {
                    return false;
                }

                Menu.RedText("Wprowadzono nieporawną wartość, spróbuj ponowanie");
                text = Console.ReadLine();
            }
        }


        private static decimal DecimalParse(string text)
        {
            while (true)
            {
                decimal patternDate = 00.00M;
                if (decimal.TryParse(text, out patternDate))
                {
                    return decimal.Parse(text);
                }

                Menu.RedText("Wprowadzono nieporawną wartość, spróbuj ponowanie");
                text = Console.ReadLine();
            }
        }

        private static int IntParse(string text)
        {
            while (true)
            {
                int patternDate = 123;
                if (int.TryParse(text, out patternDate))
                {
                    return int.Parse(text);
                }

                Menu.RedText("Wprowadzono nieporawną wartość, spróbuj ponowanie");
                text = Console.ReadLine();
            }
        }

        private static int Exist(int id)
        {
            while (true)
            {
                MedicineService medicine = new MedicineService();
                if (medicine.Exist(id))
                {
                    return id;
                    //break;
                }

                Menu.RedText("Nie ma takiego leku w bazie, spróbój ponowanie");
                id = int.Parse(Console.ReadLine());
            }
        }
    }
}