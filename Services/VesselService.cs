using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using VesselPowerManagement.Models;

namespace VesselPowerManagement.Services
{
    public class VesselService
    {
        private readonly DatabaseService _databaseService;

        public VesselService(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        // Add a new Vessel 
        public void AddVessel()
        {
            Console.Write("Enter Vessel Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Vessel Type: ");
            string type = Console.ReadLine();

            try
            {
                using (SqlConnection connection = _databaseService.GetConnection())
                {
                    // First, Check is the Vessel name that the user enterd is exist or not?
                    connection.Open();
                    string checkQuery = "SELECT COUNT(*) FROM Vessels WHERE Name = @Name";

                    using (SqlCommand checkcommand = new SqlCommand(checkQuery, connection))
                    {
                        checkcommand.Parameters.AddWithValue("@Name", name);
                        int existingCount = (int)checkcommand.ExecuteScalar();

                        if (existingCount > 0)
                        {
                            Console.WriteLine("Error: A Vessel with this name already exists.");
                            return;
                        }
                    }

                    // Add a new Vessel if the vessel enterd not exists
                    string insertQuery = @"
                    INSERT INTO Vessels (Name, Type)
                    VALUES (@Name, @Type)";

                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Type", type);
                        command.ExecuteNonQuery();
                    }
                }

                Console.WriteLine("Vessel added successfully!");
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        //Display all Vessels with efficiency calculations "By Using LINQ"
        public void DisplayAllVessels()
        {
            try
            {
                List<Vessel> vessels = new List<Vessel>();
                List<PowerCalculation> calculations = new List<PowerCalculation>();

                using (SqlConnection connection = _databaseService.GetConnection())
                {
                    connection.Open();
                    string vesselQuery = @"
                    SELECT Id, Name, Type 
                    FROM Vessels";

                    using (SqlCommand command = new SqlCommand(vesselQuery, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            vessels.Add(new Vessel
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Type = reader.GetString(2)
                            });
                        }
                    }

                    string powerQuery = @"
                    SELECT VesselId, EnginePower
                    FROM PowerCalculations";

                    using(SqlCommand command1 = new SqlCommand(powerQuery, connection))
                    using(SqlDataReader reader = command1.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            calculations.Add(new PowerCalculation
                            {
                                VesselId = reader.GetInt32(0),
                                EnginePower = reader.GetDecimal(1)
                            });
                        }
                    }
                }

                var vesselStats = vessels.Select(v => new
                {
                    v.Id,
                    v.Name,
                    v.Type,
                    AvgEnginePower = calculations
                    .Where(p => p.VesselId == v.Id)
                    .Select(p => (decimal?)p.EnginePower)
                    .DefaultIfEmpty(null)
                    .Average() ?? 0,

                    Efficiency = (calculations
                    .Where(P => P.VesselId == v.Id)
                    .Select(p => (decimal?)p.EnginePower)
                    .DefaultIfEmpty(null)
                    .Average() ?? 0) / 1000
                });

                Console.WriteLine("\n--- Vessel List with Average Engine Power ---");
                foreach (var v in vesselStats)
                {
                    Console.WriteLine($"ID: {v.Id}, Name: {v.Name}, Type: {v.Type}, Efficiency: {v.Efficiency:F2} knots per ton of fuel");
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // To search for Vessel via ID with validate that the input is text instead of num
        public void FindVesselById()
        {
            Console.Write("Enter Vessel ID: ");
            if (!int.TryParse(Console.ReadLine(), out int vesselId))
            {
                Console.WriteLine("Invalid input. Please enter a valid Vessel ID");
                return;
            }

            using (SqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();
                string query = @"
                SELECT Id, Name, Type 
                FROM Vessels 
                WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", vesselId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Console.WriteLine($"ID: {reader["Id"]}, Name: {reader["Name"]}, Type: {reader["Type"]}");
                        }
                        else
                        {
                            Console.WriteLine("Vessel not found.");
                        }
                    }
                }
            }
        }

        // Search for a vessel
        public void SearchVessel()
        {
            Console.Write("Enter Vessel Name or Type to sesrch: ");
            string searchTerm = Console.ReadLine();

            using (SqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM Vessels WHERE Name LIKE @Search OR Type LIKE @Search";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Search", "%" + searchTerm + "%");

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"ID: {reader["Id"]}, Name: {reader["Name"]}, Type: {reader["Type"]}");
                        }
                    }
                }
            }
        }

        // To update vessels with validate the inputs
        public void UpdateVeseel()
        {
            Console.Write("Enter Vessel ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int vesselId))
            {
                Console.WriteLine("Invalid input. Please enter a valid Vessel ID.");
                return;
            }

            Console.Write("Enter new Vessel Name: ");
            string newName = Console.ReadLine();

            Console.Write("Enter new Vessel Type: ");
            string newType = Console.ReadLine();

            using (SqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();
                string query = @"
                UPDATE Vessels 
                SET Name = @Name, Type = @Type 
                WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", vesselId);
                    command.Parameters.AddWithValue("@Name", newName);
                    command.Parameters.AddWithValue("@Type", newType);
                    command.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Vessel Updated Successfully!");
        }

        // To delete vessel
        public void DeleteVessel()
        {
            Console.Write("Enter a vessel ID to delete: ");
            int vesselId = int.Parse(Console.ReadLine());

            using (SqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();
                string query = "DELETE FROM Vessels WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", vesselId);
                    command.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Vessel deleted successfuly!");
        }
    }
}