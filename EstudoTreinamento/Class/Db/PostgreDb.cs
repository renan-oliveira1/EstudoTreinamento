using EstudoTreinamento.Class.Entity;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace EstudoTreinamento.Class.Db
{
    public class PostgreDb: IDatabase
    {
        public string connString = "Host=localhost; Port=5555; Username=postgres; password=1234; Database=postgres";
        private DbConnection connection;


        public PostgreDb() {
            connection = new NpgsqlConnection(connString);
            connection.Open();
        }

        public void InsertVehicle(Vehicle vehicle)
        {
            string sql = "INSERT INTO vehicle (name, brand, value) VALUES (@name,  @brand, @value)";

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand(sql, (NpgsqlConnection)connection))
                {
                    command.Parameters.AddWithValue("name", vehicle.Name);
                    command.Parameters.AddWithValue("brand", vehicle.Brand);
                    command.Parameters.AddWithValue("value", vehicle.Value);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nCouldn't insert data to database!!");
            }
        }

        public List<Vehicle> GetVehicles()
        {
            List<Vehicle> list = new List<Vehicle>();
            string sql = "SELECT * FROM vehicle";

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand(sql, (NpgsqlConnection)connection))
                {

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Vehicle(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetDouble(3)
                                ));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error to get vehicles!!");
            }

            return list;
        }

        public void UpdateVehicle(Vehicle vehicle)
        {
            string sql = "UPDATE vehicle " +
                "SET name = @name, brand = @brand, value = @value " +
                "WHERE cod = @cod";

            try
            {

                using (NpgsqlCommand command = new NpgsqlCommand(sql, (NpgsqlConnection)connection))
                {
                    command.Parameters.AddWithValue("name", vehicle.Name);
                    command.Parameters.AddWithValue("brand", vehicle.Brand);
                    command.Parameters.AddWithValue("value", vehicle.Value);
                    command.Parameters.AddWithValue("cod", vehicle.Cod);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error to update vehicle!!");
            }
        }

        public void DeleteVehicle(Vehicle vehicle)
        {
            try
            {
                string sql = $"DELETE FROM vehicle WHERE cod = {vehicle.Cod}";
                using (NpgsqlCommand command = new NpgsqlCommand(sql, (NpgsqlConnection)connection))
                {
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error to delete vehicle!!");
            }
        }

        public void InsertSeller(Seller seller)
        {
            //try catch
            string sql = "INSERT INTO seller (name, age) VALUES (@name,  @age)";

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand(sql, (NpgsqlConnection)connection))
                {
                    command.Parameters.AddWithValue("name", seller.Name);
                    command.Parameters.AddWithValue("age", seller.Age);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error to insert seller!!");
            }

        }

        public List<Seller> GetSellers()
        {
            List<Seller> list = new List<Seller>();
            string sql = "SELECT * FROM seller";

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand(sql, (NpgsqlConnection)connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(
                                new Seller(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2))
                                );
                        }
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Error to get seller!!");
            }
            
        }

        public void UpdateSeller(Seller seller)
        {
            string sql = "UPDATE seller " +
                "SET name = @name, age = @age" +
                " WHERE cod = @cod";

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand(sql, (NpgsqlConnection)connection))
                {
                    command.Parameters.AddWithValue("name", seller.Name);
                    command.Parameters.AddWithValue("age", seller.Age);
                    command.Parameters.AddWithValue("cod", seller.Cod);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error to update seller!!");
            }
        }

        public void DeleteSeller(Seller seller)
        {
            string sql = $"DELETE FROM seller WHERE cod = {seller.Cod}";
            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand(sql, (NpgsqlConnection)connection))
                {
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error to delete seller!!");
            }

         
        }

        public void SaleVehicle(Seller seller, Vehicle vehicle)
        {
            string sql = "INSERT INTO sales(sellerCod, vehicleCod) VALUES(@sellerCod, @vehicleCod);";
            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand(sql, (NpgsqlConnection)connection))
                {
                    command.Parameters.AddWithValue("sellerCod", seller.Cod);
                    command.Parameters.AddWithValue("vehicleCod", vehicle.Cod);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error to sell vehicle!!");
            }
            
        }

        public Report GetSales(Seller seller)
        {
            string sql = "SELECT v.cod, v.name, v.brand, v.value, s.name, s.age " +
                "FROM seller AS s " +
                "JOIN sales ON sales.sellercod = s.cod " +
                "JOIN vehicle AS v ON sales.vehiclecod = v.cod " +
                $"WHERE s.cod = {seller.Cod}";
            try
            {
                List<Vehicle> vehicles = new List<Vehicle>();
                using (NpgsqlCommand command = new NpgsqlCommand(sql, (NpgsqlConnection)connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            vehicles.Add(
                                new Vehicle(
                                reader.GetInt32(0), reader.GetString(1),
                                reader.GetString(2), reader.GetDouble(3)
                                ));
                        }
                    }
                }
                Report report = new Report(seller, vehicles);
                return report;
            }
            catch (Exception ex)
            {
                throw new Exception("Error to get sales!!");
            }

        }

    }
}
