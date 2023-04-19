using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
