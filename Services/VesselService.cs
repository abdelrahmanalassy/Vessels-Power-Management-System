using System;
using System.Data.SqlClient;
using System.Security.Cryptography;

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
            Console.WriteLine("Enter Vessel Name: ");
            string name = Console.ReadLine();

            Console.WriteLine("Enter Vessel Type: ");
            string type = Console.ReadLine();

            using (SqlConnection connection = _databaseService.Getconnection())
            {
                connection.Open();
                // Add Validation
                string checkQuery = "SELECT COUNT(*) FROM Vessels WHERE Name = @Name";
                using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@Name", name);
                    int count = (int)checkCommand.ExecuteScalar();

                    if (count > 0)
                    {
                        Console.WriteLine("Vassel already exists! Try another name.");
                        return;
                    }
                }

                string insertQuery = "INSERT INTO Vessels (Name, Type) VALUES (@Name, @Type)";
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Type", type);
                    command.ExecuteNonQuery();
                }

            }

            Console.WriteLine("Vessel added successfully!");
        }

        //Display all Vessels with efficiency calculations
        public void DisplayAllVessels()
        {
            using (SqlConnection connection = _databaseService.Getconnection())
            {
                connection.Open();
                string query = "SELECT V.Id, V.Name, V.Type, COALESCE(AVG(P.Speed / NULLIF(P.FuelConsumption, 0)), 0) AS Efficiency " + "FROM Vessels V LEFT JOIN PowerCalculations P ON V.Id = P.VesselId " + "GROUP BY V.Id, V.Name, V.Type";
                
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"ID: {reader["Id"]}, Name: {reader["Name"]}, Type: {reader["Type"]}, Efficiency: {reader["Efficiency"]:0.00} Knots per ton of fuel");
                        }
                    }
                }
            }
        }

        // To search for Vessel via ID
        public void FindVesselById()
        {
            Console.WriteLine("Enter Vessel ID: ");
            int vesselId = int.Parse(Console.ReadLine());

            using (SqlConnection connection = _databaseService.Getconnection())
            {
                connection.Open();
                string query = "SELECT * FROM Vessels WHERE Id = @Id";

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

            using (SqlConnection connection = _databaseService.Getconnection())
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

        // To update vessels
        public void UpdateVeseel()
        {
            Console.Write("Enter Vessel ID to update: ");
            int vesselId = int.Parse(Console.ReadLine());

            Console.Write("Enter new Vessel Name: ");
            string newName = Console.ReadLine();

            Console.Write("Enter new Vessel Type: ");
            string newType = Console.ReadLine();

            using (SqlConnection connection = _databaseService.Getconnection())
            {
                connection.Open();
                string query = "UPDATE Vessels SET Name = @Name, Type = @Type WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", vesselId);
                    command.Parameters.AddWithValue("@Name", newName);
                    command.Parameters.AddWithValue("@Type", newType);
                    command.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Vessel updated successfully!");
        }

        // To delete vessel
        public void DeleteVessel()
        {
            Console.Write("Enter a vessel ID to delete: ");
            int vesselId = int.Parse(Console.ReadLine());

            using (SqlConnection connection = _databaseService.Getconnection())
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