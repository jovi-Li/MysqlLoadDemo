// See https://aka.ms/new-console-template for more information
using MySql.Data.MySqlClient;
using System.Data;
using TestMysqlLoad;

Console.WriteLine("Hello, World!");

string constring = "server=rm-bp1c18j266i5jz40bwo.mysql.rds.aliyuncs.com;port=3306;database=Zeus_PIS;user=;password=;Charset=utf8mb4;ConvertZeroDatetime=true;AllowZeroDateTime=true;AllowLoadLocalInfile=true;sslmode=none;";

using (MySqlConnection connection = new MySqlConnection(constring))
{
    MySqlTransaction? sqlTransaction = null;
    try
    {
        connection.Open();
        sqlTransaction = connection.BeginTransaction();

        DataTable dt = new DataTable
        {
            TableName = "data"
        };
        dt.Columns.Add("name");
        dt.Columns.Add("age");

        for (int i = 1; i <= 100000; i++)
        {
            dt.Rows.Add(new Object[] { "测试数据" + i, i });
        }

        dt.ToCsv();

        MyHelper.BulkLoad(connection, dt);

        sqlTransaction.Commit();

        Console.WriteLine("done！");
    }
    catch (Exception e)
    {
        if (sqlTransaction != null)
        {
            sqlTransaction.Rollback();
        }
        Console.WriteLine("Error：" + e.Message);
    }

}

Console.ReadLine();