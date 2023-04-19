using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Vehicle
{
    private int cod;
    private string name;
    private string brand;
    private double value;


    public Vehicle() { }

    public Vehicle(string name, string brand, double value)
    {
        this.name = name;
        this.brand = brand;
        this.value = value;
    }
    public Vehicle(int cod, string name, string brand, double value)
    {
        this.cod = cod;
        this.name = name;
        this.brand = brand;
        this.value = value;
    }


    public int Cod
    {
        get { return cod;  }
        set { cod = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public string Brand
    {
        get { return brand; }
        set { brand = value; }
    }

    public double Value
    {
        get { return value; }
        set { this.value = value;  }
    }

}
