using System;

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
        get { return cod; }
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
        set { this.value = value; }
    }

    public override bool Equals(object obj)
    {
        return obj is Vehicle vehicle &&
               cod == vehicle.cod &&
               name == vehicle.name &&
               brand == vehicle.brand &&
               value == vehicle.value &&
               Cod == vehicle.Cod &&
               Name == vehicle.Name &&
               Brand == vehicle.Brand &&
               Value == vehicle.Value;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(cod, name, brand, value, Cod, Name, Brand, Value);
    }
}
