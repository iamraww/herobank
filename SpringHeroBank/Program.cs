using System;
using System.Text;
using SpringHeroBank.view;

namespace SpringHeroBank
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Menu menu = new Menu();
            menu.ApplicationMenu();
        }
    }
}