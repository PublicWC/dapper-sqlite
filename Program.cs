using Dapper;
using Newtonsoft.Json;
using System;
using System.Data.SQLite;
using System.IO;

namespace dapper_sqlite
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            InitSQLiteDb();
            TestInsert();
            TestSelect();
        }
 
        static string dbPath = @".\Test.sqlite";
        static string cnStr = "data source=" + dbPath;
 
        static void InitSQLiteDb()
        {
            if (File.Exists(dbPath)) return;
            using (var cn = new SQLiteConnection(cnStr))
            {
                cn.Execute(@"
CREATE TABLE Orders 
(
    OrderId         VARCHAR(16),
    UserContactId   VARCHAR(16),
    InsertDate      DATE,
    CONSTRAINT Orders_PK PRIMARY KEY (OrderId)
);");
            }
        }
 


        static void TestInsert()
        {
            using (var cn = new SQLiteConnection(cnStr))
            {
                //參數是用@paramName
                var insertScript = 
                    "INSERT INTO Orders VALUES('1', 'B', '2018-06-19')";
                cn.Execute(insertScript);
            }
        }
 
        static void TestSelect()
        {
            using (var cn = new SQLiteConnection(cnStr))
            {
                var list = cn.Query("SELECT * FROM Orders");
                Console.WriteLine(
                    JsonConvert.SerializeObject(list, Formatting.Indented));
            }
        }
    }
}
