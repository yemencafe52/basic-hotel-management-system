using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace HMS
{
    internal class AccessDB
    {
        private OleDbConnection con;
        private OleDbCommand cmd;
        private OleDbDataReader dr;


        private string connectionString;

        internal AccessDB(string cn)
        {
            this.connectionString = cn;
        }

        internal int ExcuteNonQuery(string sql)
        {
            int res = 0;

            try
            {
                this.con = new OleDbConnection(this.connectionString);
                this.con.Open();

                this.cmd = new OleDbCommand(sql, this.con);
                res = this.cmd.ExecuteNonQuery();
                this.con.Close();
            }
            catch
            {

            }

            return res;
        }

        internal bool ExcuteQuery(string sql)
        {
            bool res = false;

            try
            {
                this.con = new OleDbConnection(this.connectionString);
                this.con.Open();

                this.cmd = new OleDbCommand(sql, this.con);
                this.dr = this.cmd.ExecuteReader();

                if(this.dr.HasRows)
                {
                    res = true;
                }

            }
            catch
            {

            }

            return res;
        }

        internal void CloseConnection()
        {
            if(this.con != null)
            {
                try
                {
                    this.con.Close();
                }
                catch
                {

                }
            }

            GC.Collect();
        }

        ~AccessDB()
        {
            CloseConnection();
        }

        internal OleDbDataReader GetOleDbDataReader
        {
            get
            {
                return this.dr;
            }
        }

    }
}
