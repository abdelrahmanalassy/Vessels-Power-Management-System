using System;
using VesselPowerManagement.Services;

class Program
{
    static void Main()
    {
        string connectionString = "Server=DESKTOP-AUM63T7\\SQLEXPRESS;Database=VesselDB;Integrated Security=True;";

        DatabaseService databaseService = new DatabaseService(connectionString);
        VesselService vesselService = new VesselService(databaseService);
        PowerCalculationService powerCalculationService = new PowerCalculationService(databaseService);

        // Ask the user to choose an option
        while (true)
        {
            Console.WriteLine("\n--- Vessel Power Management System ---");
            Console.WriteLine("1. Add a new Vessel");
            Console.WriteLine("2. Add power calculations to a Vessel");
            Console.WriteLine("3. Display all Vessels with average power efficiency");
            Console.WriteLine("4. Find a Vessel by ID");
            Console.WriteLine("5. Search for a Vessel");
            Console.WriteLine("6. Update vessel details");
            Console.WriteLine("7. Delete Vessel");
            Console.WriteLine("8. Exit the program");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();

            // Results that selected by user
            switch (choice)
            {
                case "1":
                    vesselService.AddVessel();
                    break;
                case "2":
                    powerCalculationService.AddPowerCalculation();
                    break;
                case "3":
                    vesselService.DisplayAllVessels();
                    break;
                case "4":
                    vesselService.FindVesselById();
                    break;
                case "5":
                    vesselService.SearchVessel();
                    break;
                case "6":
                    vesselService.UpdateVeseel();
                    break;
                case "7":
                    vesselService.DeleteVessel();
                    break;
                case "8":
                    Console.WriteLine("Exiting program...");
                    return;
                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    break;
            }
        }
    }
}