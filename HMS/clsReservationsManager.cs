using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS
{
    internal static class ReservationsManager
    {
        private static List<Room> rooms = new List<Room>();


        internal static List<Room> Rooms
        {
            get
            {
                return rooms;
            }
        }
        internal static bool Init()
        {
            bool res = false;

            try
            {
                for(byte i=1;i<=Param.RoomCount;i++)
                {
                    Room r = new Room(i);
                    if (r.Number > 0)
                    {
                        rooms.Add(r);
                    }
                    else
                    {
                        throw new Exception();
                    }
                }

                res = true;
            }
            catch { }

            return res;
        }

        internal static bool Shutdown()
        {
            bool res = true;

            try
            {
                while (rooms.Count > 0)
                {
                    rooms.RemoveAt(0);
                }
            }
            catch
            {
                res = false;
            }


            return res;
        }

        internal static bool ChangeRoomState(Room room,Resevation resevation = null)
        {
            bool res = false;

            AccessDB db = new AccessDB(Constants.GetConnectionString);

            try
            {
                switch (room.ReservationInfo.StateInfo)
                {
                    case State.State1:
                        {
                            //if (room.ReservationInfo.StateInfo == State.State1)
                            //{
                                string sql = "insert into tblReservations (res_start,res_end,cus_no,room_no,res_price,res_note,user_no,res_state) values('" + resevation.StartAt.ToString() + "','" + resevation.End.ToString() + "'," + resevation.CustomerInfo.Number + "," + room.Number + "," + resevation.Price + ",'" + resevation.Note + "'," + User.ActiveUser.Number + ",1)";

                            if (db.ExcuteNonQuery(sql) > 0)
                            {
                                room.ReservationInfo = resevation;//new Resevation(0, DateTime.Now, DateTime.Now, custoemr, 0, "", User.ActiveUser, State.State2);
                                Event.Add(new Event("حجز الغرفة " + room.Number, DateTime.Now));
                                res = true;
                            }
                            //}
                            break;
                        }
                    case State.State2:
                        {

                            //if (room.ReservationInfo.StateInfo == State.State2)
                            //{
                                string sql = "update tblReservations set res_state=2 where res_state=" + (byte)room.ReservationInfo.StateInfo + " and room_no=" + room.Number;

                                if (db.ExcuteNonQuery(sql) > 0)
                                {
                                    room.ReservationInfo.StateInfo = State.State3;
                                    
                                Event.Add(new Event("الغرفة تحت خمة الفندق  " + room.Number, DateTime.Now));
                                res = true;
                                }
                            //}

                            break;
                        }
                    case State.State3:
                        {

                            //if (room.ReservationInfo.StateInfo == State.State3)
                            //{
                                string sql = "update tblReservations set res_state=0 where res_state=" + (byte)room.ReservationInfo.StateInfo + " and room_no=" + room.Number;

                                if (db.ExcuteNonQuery(sql) > 0)
                                {
                                    //room.ReservationInfo.StateInfo = State.State1;
                                    room.ReservationInfo = new Resevation(0, new DateTime(), new DateTime(), new Customer(0), 0, "",User.ActiveUser, State.State1);

                                    Event.Add(new Event("الغرفة شاغرة " + room.Number, DateTime.Now));
                                    res = true;
                                }
                            //}
                           

                            break;
                        }
                }
            }
            catch { }

            return res;
        }

    }
}
