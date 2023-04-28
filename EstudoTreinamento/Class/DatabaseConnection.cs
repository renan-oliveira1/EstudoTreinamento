using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstudoTreinamento.Class
{
    public class DatabaseConnection
    {
        private static string postgresConfigConnection = "Host=localhost; Port=5555; Username=postgres; password=1234; Database=postgres";
        public static NpgsqlConnection PostgresConnection = new NpgsqlConnection(postgresConfigConnection);
    }
}
