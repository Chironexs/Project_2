using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Pharmacy
{
    public static class Menu
    {
        public static void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("-------Apteka-------");
            Console.WriteLine("1 - Baza leków");
            Console.WriteLine("2 - Zlecenie sprzedaży");
            Console.WriteLine("0 - Wyjście z programu");
        }

        public static void ShowPharmacyMenu()
        {
            Console.Clear();
            Console.WriteLine("----Baza leków-----");
            Console.WriteLine("1 - Pokaż wszystkie leki");
            Console.WriteLine("2 - Dodaj lek");
            Console.WriteLine("3 - Edytuj lek");
            Console.WriteLine("4 - Usuń lek");
            Console.WriteLine("9 - Powrót do menu głównego");
            Console.WriteLine("0 - Wyjście z programu");
        }

        public static void ShowMedicinesManagement()
        {
            //Console.Clear();
            Console.WriteLine("----Edycja leku----");
            Console.WriteLine("1 - Edycja nazwy");
            Console.WriteLine("2 - Edycja producenta");
            Console.WriteLine("3 - Edycja ceny");
            Console.WriteLine("4 - Edycja ilości sztuk");
            Console.WriteLine("5 - Edycja wymaganej recepty");
            Console.WriteLine("9 - Powrót do menu głównego");
            Console.WriteLine("0 - Wyjście z programu");
        }



        public static void ShowMedicinesHeader()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Id.".PadRight(4, ' ') + " | ");
            sb.Append("Nazwa leku".PadRight(15, ' ') + " | ");
            sb.Append("Producent".PadRight(15, ' ') + " | ");
            sb.Append("Cena".PadRight(10, ' ') + " | ");
            sb.Append("Ilość".PadRight(10, ' ') + " | ");
            sb.Append("Recepta".PadRight(10, ' ') + " | ").AppendLine();
            sb.Append("--------------------------------------------------------------------");
            Console.WriteLine(sb.ToString());
        }

        public static void GreenText(string text)
        {
            ConsoleColor temp = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n{text}");
            Console.ForegroundColor = temp;
        }

        public static void RedText(string text)
        {
            ConsoleColor temp = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n{text}");
            Console.ForegroundColor = temp;
        }
        

        public static void ShowSaleMenu()
        {
            Console.Clear();
            Console.WriteLine("------Sprzedaż-----");
            Console.WriteLine("1 - Nowe zlecenie");
            Console.WriteLine("2 - Baza recept");
            Console.WriteLine("3 - Baza zamówień");

        }
        public static void ShowPrescriptionMenu()
        {
            Console.Clear();
            Console.WriteLine("---Nowe zlecenie---");
            Console.WriteLine("1 - Lek na receptę");
            Console.WriteLine("2 - Lek bez recepty");
            Console.WriteLine("9 - Powrót do menu głównego");
            Console.WriteLine("0 - Wyjście");
        }
        public static void AddMoreMedicine()
        {
            Console.Clear();
            Console.WriteLine("---Lek na receptę--");
            Console.WriteLine("1 - Dodaj kolejny lek z recepty");
            Console.WriteLine("9 - Powrót do menu głównego");
            Console.WriteLine("0 - Wyjście");
        }
    }
}