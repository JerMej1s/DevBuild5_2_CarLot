using System;
using System.Collections.Generic;

namespace Lab5_2_CarLot
{
    enum CarMake{ Ford, Chevrolet, Chrysler, Honda, Toyota }
    class Car
    {
        protected CarMake Make;
        protected string Model;
        protected int Year;
        protected decimal Price;
        public Car(CarMake _Make, string _Model, int _Year, decimal _Price)
        {
            Make = _Make;
            Model = _Model;
            Year = _Year;
            Price = _Price;
        }
        public static void ListCars(List<Car> myList)
        {
            foreach (Car mycar in myList) Console.WriteLine(mycar);
        }
        public static void AddCar(List<Car> myList)
        {
            Console.WriteLine("\nYou are adding a car to inventory. Please provide the following information:");
            Console.Write("\nMake: ");
            CarMake newCarMake = (CarMake)Enum.Parse(typeof(CarMake), Console.ReadLine());
            Console.Write("\nModel: ");
            string newModel = Console.ReadLine();
            Console.Write("\nYear: ");
            int newYear = int.Parse(Console.ReadLine());
            Console.Write("\nPrice: ");
            decimal newPrice = decimal.Parse(Console.ReadLine());
            Console.Write("\nNumber of previous owners: ");
            int newNumberOfOwners = int.Parse(Console.ReadLine());

            if (newNumberOfOwners <= 0)
            {
                Console.Write("\nExtended Warranty (true/false): ");
                bool newExtendedWarranty = bool.Parse(Console.ReadLine());

                Car myCar = new NewCar(newCarMake, newModel, newYear, newPrice, newExtendedWarranty);
                myList.Add(myCar);
            }
            else
            {
                Console.Write("\nMileage: ");
                int newMileage = int.Parse(Console.ReadLine());

                Car myCar = new UsedCar(newCarMake, newModel, newYear, newPrice, newMileage, newNumberOfOwners);
                myList.Add(myCar);
            }
        }
        public static Car FindOne(List<Car> myList, string userInput)
        {
            foreach (Car myCar in myList)
            {
                if (myCar.Model.StartsWith(userInput)) return myCar;
            }

            return null;
        }
        /*public static List<Car> FindAll(List<Car> myList, string userInput)
        {
            List<Car> result = new List<Car>();

            foreach (Car mycar in myList)
            {
                if (mycar.Model.StartsWith(userInput)) result.Add(mycar);
            }

            return result;
        }*/
        public static void Purchase(List<Car> myList, Car foundCar)
        {
            Console.WriteLine($"{foundCar}");
            Console.Write("\nDo you want to purchase this car? (y/n): ");
            string userPurchaseInput = Console.ReadLine();
            if (userPurchaseInput == "y" || userPurchaseInput == "Y") myList.Remove(foundCar);
        }
    }
    class NewCar : Car
    {
        public bool ExtendedWarranty;
        public NewCar(CarMake _Make, string _Model, int _Year, decimal _Price, bool _ExtendedWarranty) : base(_Make, _Model, _Year, _Price)
        {
            ExtendedWarranty = _ExtendedWarranty;
        }
        public override string ToString() => $"\nMake: {Make}" +
            $"\nModel: {Model}" +
            $"\nYear: {Year}" +
            $"\nPrice: ${Price}" +
            $"\nExtended Warranty: {ExtendedWarranty}";
    }
    class UsedCar : Car
    {
        public int NumberOfOwners;
        public int Mileage;
        public UsedCar( CarMake _Make, string _Model, int _Year, decimal _Price, int _Mileage, int _NumberOfOwners) : base(_Make, _Model, _Year, _Price)
        {
            NumberOfOwners = _NumberOfOwners;
            Mileage = _Mileage;
        }
        public override string ToString() => $"\nMake: {Make}" +
            $"\nModel: {Model}" +
            $"\nYear: {Year}" +
            $"\nPrice: ${Price}" +
            $"\nMileage: {Mileage}" +
            $"\nNumber of Owners: {NumberOfOwners}";
    }
    class Program
    {
        static void PrintMenu()
        {
            Console.WriteLine("\n\t\tWELCOME TO THE CAR LOT!" +
                "\n\n\tMAIN MENU" +
                "\n=========================" +
                "\n\nEnter A to add a car to inventory." +
                "\nEnter a model to search and optionally purchase." +
                "\nEnter Q to quit.\n");
        }
        static void Main(string[] args)
        {
            List<Car> myList = new List<Car>();

            Car myCar = new NewCar(CarMake.Ford, "EcoSport", 2021, 23960, false);
            myList.Add(myCar);

            myCar = new NewCar(CarMake.Chevrolet, "Equinox", 2022, 15490, true);
            myList.Add(myCar);

            myCar = new NewCar(CarMake.Toyota, "Highlander", 2021, 35085, false);
            myList.Add(myCar);

            myCar = new UsedCar(CarMake.Chrysler, "Pacifica", 2018, 31990, 42315, 2);
            myList.Add(myCar);

            myCar = new UsedCar(CarMake.Honda, "Civic", 2010, 9000, 129064, 4);
            myList.Add(myCar);

            myCar = new UsedCar(CarMake.Ford, "Bronco", 2021, 39990, 6341, 1);
            myList.Add(myCar);

            bool quit = false;
            while (!quit)
            {
                Car.ListCars(myList);
                PrintMenu();
                string userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "A":  // Add a new car to inventory
                        Car.AddCar(myList);
                        break;

                    case "Q":  // Quit and exit program
                        quit = true;
                        break;

                    default:  // Search for and optionally purchase a car
                        Car foundCar = Car.FindOne(myList, userInput);
                        if (foundCar != null) Car.Purchase(myList, foundCar);
                        break;
                }
            }

            Console.WriteLine("\nGoodbye");
        }
    }
}