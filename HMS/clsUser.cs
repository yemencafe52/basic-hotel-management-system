using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS
{
    internal class User
    {
        private byte number;
        private string username;
        private string password;

        internal User(byte number)
        {
            try
            {
                AccessDB db = new AccessDB(Constants.GetConnectionString);
                string sql = "select user_no,user_username,user_password from tblUsers where user_no=" + number;

                if (db.ExcuteQuery(sql))
                {
                    if (db.GetOleDbDataReader.Read())
                    {
                        this.number = Convert.ToByte(db.GetOleDbDataReader["user_no"].ToString());
                        this.username = (db.GetOleDbDataReader["user_username"].ToString());
                        this.password = (db.GetOleDbDataReader["user_password"].ToString());
                    }
                }

                db.CloseConnection();
            }
            catch
            {
                this.number = 0;
            }
        }

        internal User(byte number,string username,string password)
        {
            this.number = number;
            this.username = username;
            this.password = password;
        }

        internal byte Number
        {
            get
            {
                return this.number;
            }
        }

        internal string UserName
        {
            get
            {
                return this.username;
            }
        }

        internal string Password
        {
            get
            {
                return this.password;
            }
        }

        internal static bool Add(User user)
        {
            bool res = false;

            try
            {
                AccessDB db = new AccessDB(Constants.GetConnectionString);
                string sql = "insert into tblUsers (user_no,user_username,user_password) values(" + user.Number + ",'" + user.UserName + "','" + user.Password + "')";

                if (db.ExcuteNonQuery(sql) > 0)
                {
                    res = true;
                }

            }
            catch { }

            return res;
        }

        internal static bool Update(User user)
        {
            bool res = false;

            try
            {
                AccessDB db = new AccessDB(Constants.GetConnectionString);

                string sql = "update tblUsers set user_password='"+ user.Password +"' where user_no=" + user.Number;

                if (db.ExcuteNonQuery(sql) > 0)
                {
                    res = true;
                }

            }
            catch { }

            return res;
        }

        internal static bool Login(User user)
        {
            bool res = false;

            try
            {
                AccessDB db = new AccessDB(Constants.GetConnectionString);
                string sql = "select user_no,user_username,user_password from tblUsers where user_username='"+user.username+"' and user_password='"+user.password+"'";

                if(db.ExcuteQuery(sql))
                {
                    if(db.GetOleDbDataReader.Read())
                    {
                        User u = new User(Convert.ToByte(db.GetOleDbDataReader["user_no"].ToString()),(db.GetOleDbDataReader["user_username"].ToString()), (db.GetOleDbDataReader["user_password"].ToString()));
                        activeUser = u;
                        res = true;
                    }
                }

                db.CloseConnection();
                    
            }
            catch
            {

            }

            return res;
        }

        private static User activeUser = null;

        internal static User ActiveUser
        {
            get
            {
                return activeUser;
            }
        }

    }
}
