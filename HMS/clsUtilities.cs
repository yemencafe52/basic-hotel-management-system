using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace HMS
{
    internal static class Utilities
    {
        internal static bool CheckDataBaseFile()
        {
            bool res = false;

            try
            {
                if (File.Exists(Constants.GetDbPath))
                {
                    res = true;
                }
            }
            catch
            { }


            return res;
        }
        internal static bool TestDB()
        {
            bool res = false;

            try
            {
                if (CheckDataBaseFile())
                {
                    AccessDB db = new AccessDB(Constants.GetConnectionString);
                    string sql = "select count(*) from tblUsers";

                    if (db.ExcuteQuery(sql))
                    {
                        res = true;
                    }

                    db.CloseConnection();
                }
            }
            catch
            { }

            return res;
        }
        internal static bool InstallDB()
        {
            bool res = false;

            try
            {
                string path = Application.StartupPath + "\\hmsdb.db"; 
                File.Copy(path, Constants.GetDbPath, false);
                res = true;
            }
            catch
            { }

            return res;
        }
        internal static bool BackupDB(string path)
        {
            bool res = false;

            try
            {
                File.Copy(Constants.GetDbPath, path, false);
            }
            catch
            { }

            return res;
        }
        internal static bool ResotreDB(string path)
        {
            bool res = false;

            try
            {
                File.Copy(path,Constants.GetDbPath, false);
            }
            catch
            { }

            return res;
        }
        internal static string RoomStateToString(State state)
        {
            string res = "";

            switch (state)
            {
                case State.State1:
                    {
                        res = "شاغرة";
                        break;
                    }
                case State.State2:
                    {
                        res = "محجوزة";
                        break;
                    }
                case State.State3:
                    {
                        res = "تحت خدمة الفندق";
                        break;
                    }

            }

            return res;
        }
        internal static string DateTimeToString(DateTime date)
        {
            string res = date.ToString();

            if (date.Ticks == 0)
            {
                res = "غير محدد";
            }

            return res;
        }
        internal static bool CanWR()
        {
            bool res = false;

            try
            {
                string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Windows) + "\\bhmstestfile.file";
                FileStream fs = File.Open(filePath,FileMode.OpenOrCreate);
                fs.Write(new byte[]{ 0 }, 0, 1);
                fs.Close();

                File.Delete(filePath);
                res = true;
            }
            catch { }
            return res;
        }

    }
}
