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
CREATE TABLE Player (
    Id VARCHAR(16),
    Name VARCHAR(32),
    RegDate DATETIME,
    Score INTEGER,
    BinData BLOB,
    CONSTRAINT Player_PK PRIMARY KEY (Id)
)");
            }
        }
 
        static Player[] TestData = new Player[]
        {
            new Player("P01", "Jeffrey", DateTime.Now, 32767),
            new Player("P02", "Darkthread", DateTime.Now, 65535),
        };

        static void TestInsert()
        {
            using (var cn = new SQLiteConnection(cnStr))
            {
                cn.Execute("DELETE FROM Player");
                //參數是用@paramName
                var insertScript = 
                    "INSERT INTO Player VALUES (@Id, @Name, @RegDate, @Score, @BinData)";
                cn.Execute(insertScript, TestData);
                //測試Primary Key
                try
                {
                    //故意塞入錯誤資料
                    cn.Execute(insertScript, TestData[0]);
                    throw new ApplicationException("失敗：未阻止資料重複");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"測試成功:{ex.Message}");
                }
            }
        }
 
        static void TestSelect()
        {
            using (var cn = new SQLiteConnection(cnStr))
            {
                var list = cn.Query("SELECT * FROM Player");
                Console.WriteLine(
                    JsonConvert.SerializeObject(list, Formatting.Indented));
            }
        }
    }
}
