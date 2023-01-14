using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS
{
    internal static class Constants
    {
        private static string paramPath = Environment.GetFolderPath(Environment.SpecialFolder.Windows) + "\\hmsdata.bin";  //Application.StartupPath + "\\data.bin";
        private static string logPath = Application.StartupPath + "\\Log";
        private static string dbPath = Environment.GetFolderPath(Environment.SpecialFolder.Windows) + "\\hms52.db";// Application.StartupPath + "\\DataBase\\" + "db.accdb";
        private static string connectionString = "provider=microsoft.ace.oledb.12.0;data source="+ dbPath +";";

        internal static string GetLogPath
        {
            get
            {
                return logPath;
            }
        }

        internal static string GetDbPath
        {
            get
            {
                return dbPath;
            }
        }

        internal static string GetConnectionString
        {
            get
            {
                return connectionString;
            }
        }

        internal static string GetParamsPath
        {
            get
            {
                return paramPath;
            }
        }
    }
}
