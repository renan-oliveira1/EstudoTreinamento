using EstudoTreinamento.Class.Entity;
using System.Collections.Generic;

namespace EstudoTreinamento.Class
{
    public interface IDatabase
    {
        bool InsertVehicle(Vehicle vehicle);
        List<Vehicle> GetVehicles();
        bool UpdateVehicle(Vehicle vehicle);
        bool DeleteVehicle(Vehicle vehicle);
        bool InsertSeller(Seller seller);
        List<Seller> GetSellers();
        bool UpdateSeller(Seller seller);
        bool DeleteSeller(Seller seller);
        bool SaleVehicle(Seller seller, Vehicle vehicle);
        Report GetSales(Seller seller);
    }
}
