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
            Console.Write("Enter Vessel ID: ");
            if (!int.TryParse(Console.ReadLine(), out int vesselId))
            {
                Console.WriteLine("Invalid Vassel ID! Please enter a valid ID.");
                return;
            }
            using (SqlConnection connection = _databaseService.Getconnection())
            {
                connection.Open();
                // Add Validation
                string checkVesselQuery = "SELECT COUNT(*) FROM PowerCalculations WHERE Id = @VesselId";
                using (SqlCommand checkCommand = new SqlCommand(checkVesselQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@vesselId", vesselId);
                    int count = (int)checkCommand.ExecuteScalar();

                    if (count == 0)
                    {
                        Console.WriteLine("Error: This Vassel already has a power calculation record.");
                        return;
                    }
                }
                Console.Write("Enter Engine Power (KW): ");
                decimal enginePower = decimal.Parse(Console.ReadLine());

                Console.Write("Enter Fuel Consumption (tons/hour): ");
                decimal fuelConsumption = decimal.Parse(Console.ReadLine());

                Console.Write("Enter Speed (Knots): ");
                decimal speed = decimal.Parse(Console.ReadLine());

                string insertQuery = "INSERT INTO PowerCalculations (VesselId, EnginePower, FuelConsumption, Speed) VALUES (@VesselId, @EnginePower, @FuelConsumption, @Speed)";
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@VesselId", vesselId);
                    command.Parameters.AddWithValue("@EnginePower", enginePower);
                    command.Parameters.AddWithValue("@FuelConsumption", fuelConsumption);
                    command.Parameters.AddWithValue("@Speed", speed);
                    command.ExecuteNonQuery();
                }
            }
            Console.WriteLine("Power calculations added successfuly!");
        }
    }
}