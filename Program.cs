using Dapper;
using Newtonsoft.Json;
using System;
using System.Data.SQLite;
using System.IO;


namespace dapper_sqlite
{
    class Program
    {
         
        static string dbPath = @".\orders.sqlite";
        static string cnStr = "data source=" + dbPath;

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            using (var cn = new SQLiteConnection(cnStr))
            {
                var builder = new SqlBuilder();
                var template = builder.AddTemplate("Select * from [Orders] O ");
                builder.Select("O.OrderId")
                        .Join("Contacts UC ON UC.UserContactId = O.UserContactId");

                var list = cn.Query(template.RawSql);
                Console.WriteLine(
                    JsonConvert.SerializeObject(list, Formatting.Indented));
            }

        }
    }
}
