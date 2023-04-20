namespace EstudoTreinamento.Class.Entity
{
    public class Seller
    {
        private int cod;
        private string name;
        private int age;

        public Seller() { }

        public Seller(string name, int age)
        {
            this.name = name;
            this.age = age;
        }

        public Seller(int cod, string name, int age)
        {
            this.cod = cod;
            this.name = name;
            this.age = age;
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

        public int Age
        {
            get { return age; }
            set { age = value; }
        }
    }
}
