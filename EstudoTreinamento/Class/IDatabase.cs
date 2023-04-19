﻿using EstudoTreinamento.Class.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstudoTreinamento.Class
{
    public interface IDatabase
    {
        void InsertVehicle(Vehicle vehicle);
        List<Vehicle> GetVehicles();
        void UpdateVehicle(Vehicle vehicle);
        void DeleteVehicle(Vehicle vehicle);
        void InsertSeller(Seller seller);
        List<Seller> GetSellers();
        void UpdateSeller(Seller seller);
        void DeleteSeller(Seller seller);
        void SaleVehicle(Seller seller, Vehicle vehicle);
        Report GetSales(Seller seller);
    }
}