using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace HMS
{
    internal static class Report
    {
        internal static DSet.tblTempReservationsDataTable Report1(DateTime from , DateTime to )
        {
            DSet.tblTempReservationsDataTable res = new DSet.tblTempReservationsDataTable();

            try
            {
                string sql = "SELECT room_no, sum(res_price) AS res_price0 FROM tblReservations WHERE DateValue(Format(res_start,\"yyyy/MM/dd\")) Between DateValue(Format(\""+ from.ToString("yyyy/MM/dd") +"\",\"yyyy/MM/dd\")) And DateValue(Format(\""+ to.ToString("yyyy/MM/dd") +"\",\"yyyy/MM/dd\")) GROUP BY room_no";

                using (OleDbDataAdapter ad = new OleDbDataAdapter(sql,new OleDbConnection(Constants.GetConnectionString)))
                {
                    ad.Fill(res);
                }

            }
            catch
            { }


            //SELECT room_no, sum(res_price) AS res_price0 FROM tblReservations WHERE DateValue(Format(res_end,"yyyy/MM/dd")) Between DateValue(Format("2022/01/01","yyyy/MM/dd")) And DateValue(Format("2022/12/14","yyyy/MM/dd")) GROUP BY room_no
            return res;
        }

    }
}
