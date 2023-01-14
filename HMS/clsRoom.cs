using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS
{
    public class Room 
    {
        private byte number;
        private string desc;
        private double price;
        private Resevation reservation;

        public Room 
            (
               byte number,
               string desc,
               double price
            )
        {
            this.number = number;
            this.desc = desc;
            this.price = price;
        }

        public Room(byte number)
        {
            try
            {
                AccessDB db = new AccessDB(Constants.GetConnectionString);
                string sql = "select room_no,room_desc,room_price from tblRooms where room_no="+ number;

                if(db.ExcuteQuery(sql))
                {
                    if (db.GetOleDbDataReader.Read())
                    {
                        this.number = Convert.ToByte(db.GetOleDbDataReader["room_no"].ToString());
                        this.desc = (db.GetOleDbDataReader["room_desc"].ToString());
                        this.price = Convert.ToDouble(db.GetOleDbDataReader["room_price"].ToString());
                        this.reservation = new Resevation(this.number);

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

                db.CloseConnection();
            }
            catch
            {
                this.number = 0;
                //this.reservation = new Resevation(0)
            }
        }

        internal byte Number
        {
            get
            {
                return this.number;
            }
        }

        internal string Descrption
        {
            get
            {
                return this.desc;
            }
        }

        internal double Price
        {
            get
            {
                return this.price;
            }
        }

        internal Resevation ReservationInfo
        {
            get
            {
                return this.reservation;
            }
            set
            {
                this.reservation = value;
            }
        }

        internal static bool Update(Room room)
        {
            bool res = false;

            try
            {
                AccessDB db = new AccessDB(Constants.GetConnectionString);
                string sql = "update tblRooms set room_desc='" + room.Descrption + "',room_price=" + room.Price+ " where room_no=" + room.Number;

                if (db.ExcuteNonQuery(sql) > 0)
                {
                    res = true;
                }

            }
            catch { }

            return res;
        }
    }
}
