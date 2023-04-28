using Autofac.Extras.Moq;
using EstudoTreinamento.Class;
using EstudoTreinamento.Class.Db;
using EstudoTreinamento.Class.Entity;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Moq;
using Npgsql;
using System.Data.Common;

namespace TestProject
{
    public class Tests
    {
        PostgreDb postgresDbTest = new PostgreDb(DatabaseForTest.PostgresConnection);

        [Test] 
        public void Test_VehicleClass()
        {
            Vehicle vehicle = new Vehicle(2, "Ferrari", "Ferrari", 1000000);

            Assert.That(vehicle.Cod, Is.EqualTo(2));
            Assert.That(vehicle.Name, Is.EqualTo("Ferrari"));
            Assert.That(vehicle.Brand, Is.EqualTo("Ferrari"));
            Assert.That(vehicle.Value, Is.EqualTo(1000000));
        }

        [Test]
        public void Test_SellerClass()
        {
            Seller seller = new Seller(2, "Renan", 22);

            Assert.That(seller.Cod, Is.EqualTo(2));
            Assert.That(seller.Name, Is.EqualTo("Renan"));
            Assert.That(seller.Age, Is.EqualTo(22));
        }

        [Test]
        public void Test_DbGetVehicles()
        {

            List<Vehicle> vehicles = postgresDbTest.GetVehicles();

            List<Vehicle> expected = GetSampleVehicle();

            var result = vehicles.SequenceEqual(expected);

            Assert.IsTrue(result);

        }

        [Test]
        public void Test_DbGetSellers() {
            List<Seller> sellers = postgresDbTest.GetSellers();
            List<Seller> expected = GetSampleSeller();


            var result = sellers.SequenceEqual(expected);

            Assert.That(sellers, Is.EqualTo(expected));
        }

        [Test]
        public void Test_DbSaveVehicle() {
            Vehicle vehicleInsert = new Vehicle("Agile", "Chevrolet", 40000);
            bool result = postgresDbTest.InsertVehicle(vehicleInsert);
            Assert.IsTrue(result);
        }

        [Test]
        public void Test_DbUpdateVehicle()
        {
            Vehicle vehicleUpdate = new Vehicle(2, "Fox", "Volks", 25000);
            bool result = postgresDbTest.UpdateVehicle(vehicleUpdate);
            Assert.IsTrue(result);
        }

        [Test] public void Test_DbInsertSeller()
        {
            Seller newSeller = new Seller("New Seller", 50);
            bool result = postgresDbTest.InsertSeller(newSeller);
            Assert.IsTrue(result);
        }

        [Test]
        public void Test_DbUpdateSeller()
        {
            Seller updatedSeller = new Seller(2, "Gabriel", 25);
            bool result = postgresDbTest.UpdateSeller(updatedSeller);
            Assert.IsTrue(result);
        }

        [Test]
        public void Test_DbReport()
        {
            Seller seller = new Seller(1, "Renan", 22);
            Report report = postgresDbTest.GetSales(seller);

            Report expected = GetSampleReport();

            Assert.That(report.Seller.Name, Is.EqualTo(expected.Seller.Name));

            for (int i = 0; i < expected.Vehicles.Count; i++)
            {
                Assert.That(report.Vehicles[i].Cod, Is.EqualTo(expected.Vehicles[i].Cod));
                Assert.That(report.Vehicles[i].Name, Is.EqualTo(expected.Vehicles[i].Name));
                Assert.That(report.Vehicles[i].Brand, Is.EqualTo(expected.Vehicles[i].Brand));
            }
        }

        [Test]
        public void Test_MockInsertSeller()
        {
            Seller seller = new Seller("Outro Vedendor", 22);
            var mockObject = new Mock<IDatabase>();
            mockObject.Setup(x => x.InsertSeller(seller)).Returns(true);

            var result = mockObject.Object.InsertSeller(seller);
            Assert.IsTrue(result);
        }

        [Test]
        public void Test_MockInsertVehicle()
        {
            Vehicle vehicle = new Vehicle("Opala", "Uma marca", 10000);
            var mockObject = new Mock<IDatabase>();
            mockObject.Setup(x => x.InsertVehicle(vehicle)).Returns(true);

            var result = mockObject.Object.InsertVehicle(vehicle);
            Assert.IsTrue(result);
        }

        [Test]
        public void Test_MockUpdateVehicle() {
            Vehicle vehicle = new Vehicle();
            var mockObject = new Mock<IDatabase>();
            mockObject.Setup(x => x.UpdateVehicle(vehicle)).Returns(true);

            var result = mockObject.Object.UpdateVehicle(vehicle);
            Assert.IsTrue(result);
        }

        [Test]
        public void Test_MockUpdateSeller()
        {
            Seller seller = new Seller();
            var mockObject = new Mock<IDatabase>();
            mockObject.Setup(x => x.UpdateSeller(seller)).Returns(true);

            var result = mockObject.Object.UpdateSeller(seller);
            Assert.IsTrue(result);
        }

        [Test]
        public void Test_MockDeleteVehicle()
        {
            Vehicle vehicle = new Vehicle();
            var mockObject = new Mock<IDatabase>();
            mockObject.Setup(x => x.DeleteVehicle(vehicle)).Returns(true);

            var result = mockObject.Object.DeleteVehicle(vehicle);
            Assert.IsTrue(result);
        }

        [Test]
        public void Test_MockDeleteSeller()
        {
            Seller seller = new Seller();
            var mockObject = new Mock<IDatabase>();
            mockObject.Setup(x => x.DeleteSeller(seller)).Returns(true);

            var result = mockObject.Object.DeleteSeller(seller);
            Assert.IsTrue(result);
        }


  

        private List<Vehicle> GetSampleVehicle()
        {
            List<Vehicle> output = new List<Vehicle>()
            {
                new Vehicle
                {
                    Cod = 1,
                    Name = "Ferrari",
                    Brand = "Ferrari",
                    Value = 1000000
                },
                 new Vehicle
                {
                    Cod = 2,
                    Name = "Fox",
                    Brand = "Volks",
                    Value = 25000
                }
            };
            return output;
        }

        private List<Seller> GetSampleSeller()
        {
            List<Seller> output = new List<Seller>()
            {
                new Seller {Cod = 1,Name = "Renan",Age = 22},
                new Seller {Cod = 2,Name = "Gabriel",Age = 25}
            };
            return output;
        }

        private Report GetSampleReport()
        {
            Seller seller = new Seller(1, "Renan", 22);
            List<Vehicle> vehicles = new List<Vehicle>()
            {
                new Vehicle
                {
                    Cod = 1,
                    Name = "Ferrari",
                    Brand = "Ferrari",
                    Value = 1000000
                }
            };

            Report report = new Report(seller, vehicles);

            return report;
        }
    }
}