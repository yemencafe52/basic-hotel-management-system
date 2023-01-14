using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS
{

    internal enum State:byte
    {
        State1 = 0,
        State2,
        State3
    }

    internal class Resevation
    {
        private int number;
        private DateTime start;
        private DateTime end;
        private Customer custoemr;
        private double price;
        private string note;
        private User user;
        private State state;

        internal Resevation(int number)
        {
            AccessDB db = new AccessDB(Constants.GetConnectionString);

            try
            {
               
                string sql = "select res_no,res_start,res_end,cus_no,room_no,res_price,res_note,user_no,res_state from tblReservations where res_state <> 0 and room_no=" + number;

                if (db.ExcuteQuery(sql))
                {
                    if (db.GetOleDbDataReader.Read())
                    {
                        this.number = Convert.ToInt32(db.GetOleDbDataReader["res_no"].ToString());
                        this.start = Convert.ToDateTime(db.GetOleDbDataReader["res_start"].ToString());
                        this.end = Convert.ToDateTime(db.GetOleDbDataReader["res_end"].ToString());
                        this.custoemr = new Customer(Convert.ToInt32(db.GetOleDbDataReader["cus_no"].ToString()));
                        this.price = Convert.ToDouble(db.GetOleDbDataReader["res_price"].ToString());
                        this.user = new User(Convert.ToByte(db.GetOleDbDataReader["user_no"].ToString()));
                        this.note = db.GetOleDbDataReader["res_note"].ToString();
                        this.state = (State)Convert.ToByte(db.GetOleDbDataReader["res_state"].ToString());
                        
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                this.number = 0;
                this.start = new DateTime();
                this.end = new DateTime();
                this.custoemr = new Customer(0);
                this.price = 0;
                this.user = User.ActiveUser; //new User(1);
                this.note = "";
                this.state = State.State1;

            }

            db.CloseConnection();
        }

        internal Resevation(
             int number,
             DateTime start,
             DateTime end,
             Customer custoemr,
             double price,
             string note,
             User user,
             State state

        )
        {
            this.number = number;
            this.start = start;
            this.end = end;
            this.custoemr = custoemr;
            this.price = price;
            this.note = note;
            this.user = user;
            this.state = state;

        }

        internal int Number
        {
            get
            {
                return this.number;
            }
        }

        internal DateTime StartAt
        {
            get
            {
                return this.start;
            }
        }

        internal DateTime End
        {
            get
            {
                return this.end;
            }
        }

        internal Customer CustomerInfo
        {
            get
            {
                return this.custoemr;
            }
        }

        internal double Price
        {
            get
            {
                return this.price;
            }
        }

        internal string Note
        {
            get
            {
                return this.note;
            }
        }

        internal User UserInfo
        {
            get
            {
                return this.user;
            }
        }

        internal State StateInfo
        {
            get
            {
                return this.state;
            }
            set
            {
                this.state = value;
            }
        }
    }
}
