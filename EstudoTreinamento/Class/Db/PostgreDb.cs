using EstudoTreinamento.Class.Entity;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace EstudoTreinamento.Class.Db
{
    public class PostgreDb : IDatabase
    {
        public string connString = "Host=localhost; Port=5555; Username=postgres; password=1234; Database=postgres";
        private DbConnection connection;


        public PostgreDb(DbConnection connec)
        {
            connection = connec;
            connection.Open();
        }

        public bool InsertVehicle(Vehicle vehicle)
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
                return true;
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
                throw new Exception(ex.Message + "\nCouldn't get data from database!!");
            }

            return list;
        }

        public bool UpdateVehicle(Vehicle vehicle)
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
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nCouldn't update data from database!!");
            }
        }

        public bool DeleteVehicle(Vehicle vehicle)
        {
            try
            {
                string sql = $"DELETE FROM vehicle WHERE cod = {vehicle.Cod}";
                using (NpgsqlCommand command = new NpgsqlCommand(sql, (NpgsqlConnection)connection))
                {
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nCouldn't delete data from database!!");
            }
        }

        public bool InsertSeller(Seller seller)
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
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nCouldn't insert data to database!!");
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
                throw new Exception(ex.Message + "\nCouldn't get data from database!!");
            }

        }

        public bool UpdateSeller(Seller seller)
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
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nCouldn't update data from database!!");
            }
        }

        public bool DeleteSeller(Seller seller)
        {
            string sql = $"DELETE FROM seller WHERE cod = {seller.Cod}";
            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand(sql, (NpgsqlConnection)connection))
                {
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nCouldn't delete data from database!!");
            }


        }

        public bool SaleVehicle(Seller seller, Vehicle vehicle)
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
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nCouldn't insert data to database!!");
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
                throw new Exception(ex.Message + "\nCouldn't get data from database!!");
            }

        }

    }
}
