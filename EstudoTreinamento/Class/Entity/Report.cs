using System;
using System.Collections.Generic;

namespace EstudoTreinamento.Class.Entity
{
    public class Report
    {
        public Seller Seller { get; set; }
        public List<Vehicle> Vehicles { get; set; }

        public Report() { }

        public Report(Seller seller, List<Vehicle> vehicles)
        {
            Seller = seller;
            Vehicles = vehicles;
        }

        public override bool Equals(object obj)
        {
            return obj is Report report &&
                   EqualityComparer<Seller>.Default.Equals(Seller, report.Seller) &&
                   EqualityComparer<List<Vehicle>>.Default.Equals(Vehicles, report.Vehicles);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Seller, Vehicles);
        }
    }
}
