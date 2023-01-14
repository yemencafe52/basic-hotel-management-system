using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HMS
{
    internal static class Param
    {
        private static string hotelName;
        private static string hotelAddress;
        private static string hotelPhone;
        //===============================
        private static byte roomsCount =1;
        private static double price;

        internal static string HotelName
        {
            get
            {
                return hotelName;
            }
        }

        internal static string HotelAddress
        {
            get
            {
                return hotelAddress;
            }
        }

        internal static string HotelPhone
        {
            get
            {
                return hotelPhone;
            }
        }

        internal static byte RoomCount
        {
            get
            {
                return roomsCount;
            }
        }

        internal static double Price
        {
            get
            {
                return price;
            }
        }

        internal static bool Read()
        {
            bool res = false;

            try
            {
                FileStream fs = File.Open(Constants.GetParamsPath, FileMode.Open);
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                fs.Close();

                string[] info = Encoding.UTF8.GetString(buffer).Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                hotelName = info[0];
                hotelAddress = info[1];
                hotelPhone = info[2];
                roomsCount = byte.Parse(info[3]);
                price = double.Parse(info[4]);
                res = true;

            }
            catch
            { }

            //hotelName = "Hotel1";
            //hotelAddress = "Address1";
            //hotelPhone = "Phone1";
            //roomsCount = 3;
            //price = 3000;

            return res;
        }
        internal static bool Update(string p1,string p2,string p3,byte p4,double p5)
        {
            bool res = false;

            try
            {
                if (CanUpdate())
                {
                    if (File.Exists(Constants.GetParamsPath))
                    {
                        File.Delete(Constants.GetParamsPath);
                    }

                    FileStream fs = new FileStream(Constants.GetParamsPath, FileMode.OpenOrCreate);
                    StringBuilder sb = new StringBuilder();

                    sb.Append(p1);
                    sb.Append(";");
                    sb.Append(p2);
                    sb.Append(";");
                    sb.Append(p3);
                    sb.Append(";");
                    sb.Append(p4.ToString());
                    sb.Append(";");
                    sb.Append(p5.ToString());
                    sb.Append(";");

                    byte[] buffer = Encoding.UTF8.GetBytes(sb.ToString());
                    fs.Write(buffer, 0, buffer.Length);
                    fs.Close();

                    res = true;
                }

            }
            catch
            {

            }

            return res;
        }

        private static bool CanUpdate()
        {
            bool res = false;

            for(int i=0;i<ReservationsManager.Rooms.Count;i++)
            {
                if(ReservationsManager.Rooms[i].ReservationInfo.StateInfo != State.State1)
                {
                    return res;
                }
            }

            res = true;
            return res;
        }
    }
}
