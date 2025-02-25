using System;
using System.Data.SqlClient;
using System.Net.Http.Headers;

namespace VesselPowerManagement.Services
{
    public class PowerCalculationService
    {
        private readonly DatabaseService _databaseService;

        public PowerCalculationService(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public void AddPowerCalculation()
        {
            int vesselId;
            decimal enginePower, fuelConsumption, speed;

            Console.Write("Enter Vessel ID: ");
            while (!int.TryParse(Console.ReadLine(), out vesselId))
            {
                Console.Write("Invalid input! Please enter a valid Vessel ID: ");
            }

            Console.Write("Enter Engine Power(KW): ");
            while (!decimal.TryParse(Console.ReadLine(), out enginePower))
            {
                Console.Write("Invalid input. Enter a valid number: ");
            }

            Console.Write("Enter Fuel Consumption (tons/hour): ");
            while (!decimal.TryParse(Console.ReadLine(), out fuelConsumption))
            {
                Console.Write("Invalid input. Enter a valid number: ");
            }

            Console.Write("Enter speed (Knots): ");
            while (!decimal.TryParse(Console.ReadLine(), out speed))
            {
                Console.Write("Invalid input. Enter a valid number: ");
            }

            try
            {
                using (SqlConnection connection = _databaseService.GetConnection())
                {
                    connection.Open();
                    string query = @"
                    INSERT INTO PowerCalculations (VesselId, EnginePower, FuelConsumption, Speed)
                    VALUES (@VesselId, @EnginePower, @FuelConsumption, @Speed)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@VesselId", vesselId);
                        command.Parameters.AddWithValue("@EnginePower", enginePower);
                        command.Parameters.AddWithValue("@FuelConsumption", fuelConsumption);
                        command.Parameters.AddWithValue("@Speed", speed);
                        command.ExecuteNonQuery();
                    }
                }

                Console.WriteLine("Power calculations added successfully!");
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}