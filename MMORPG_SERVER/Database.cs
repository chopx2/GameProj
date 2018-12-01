using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MMORPG_SERVER
{
    class Database
    {
           public void AddAccount(string username, string password)
        {
            var db = Globals.mysql.DB_RS;
            {
                db.Open("SELECT * FROM accounts WHERE 0=1", Globals.mysql.DB_CONN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic);
                db.AddNew();
                db.Fields["Username"].Value = username;
                db.Fields["Password"].Value = password;
                db.Update();
                db.Close();
            }

        }
      
    }
}
