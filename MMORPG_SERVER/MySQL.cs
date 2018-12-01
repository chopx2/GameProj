using System;
using ADODB;

namespace MMORPG_SERVER
{
    class MySQL
    {
        public Recordset DB_RS;
        public Connection DB_CONN;

        public void MySQLInit()
        {
            try
            {
                DB_RS = new Recordset();
                DB_CONN = new Connection();

                DB_CONN.ConnectionString = "Driver={MySQL ODBC 3.51 Driver};Server=192.168.1.239;Port=3306;Database=mmorpg;User=admin;Password=11341134;Option=3;";
                DB_CONN.CursorLocation = CursorLocationEnum.adUseServer;
                DB_CONN.Open();
                Console.WriteLine("Connection to MYSQL Server was successfully.");

                var db = DB_RS;
                {
                    //db.Open("SELECT * FROM accounts WHERE 0=1", DB_CONN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic);
                    //db.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
            }
        }
    }
}
