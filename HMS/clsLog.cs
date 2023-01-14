using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HMS
{
    internal class Event
    {
        private string msg;
        private DateTime time;
        //====================
        private static List<Event> events = new List<Event>();

        internal Event(string msg,DateTime time)
        {
            this.msg = msg;
            this.time = time;
        }

        internal string Message
        {
            get
            {
                return this.msg;
            }
        }

        internal DateTime TimeInfo
        {
            get
            {
                return this.time;
            }
        }


        internal static bool Add(Event e)
        {
            bool res = false;

            try
            {
                try
                {
                    if (!Directory.Exists(Constants.GetLogPath))
                    {
                        Directory.CreateDirectory(Constants.GetLogPath);

                        
                    }
                }
                catch
                {
                    events.Add(new Event("تعذر إنشاء مجلد خاص بـ السجل في القرص الصلب.",DateTime.Now));
                }

                events.Add(e);

                string path = Constants.GetLogPath + "\\" + DateTime.Now.ToString("YH@yyyy-MM-dd") + ".txt";

                string eventInfo = "[" + e.time.ToString() + "]:" + e.msg + ".\r\n";

                FileStream fs = new FileStream(path, FileMode.OpenOrCreate);

                fs.Position = fs.Length;

                byte[] buffer = Encoding.UTF8.GetBytes(eventInfo);
                fs.Write(buffer, 0, buffer.Length);
                fs.Close();


                res = true;
            }
            catch
            { }


            return res;
        }

        internal static bool Read(ref Event e)
        {
            bool res = false;

            try
            {
                while (events.Count > 0)
                {
                    e = events[0];
                    events.RemoveAt(0);
                    res = true;
                }

            }
            catch { }

            return res;
        }
    }
    
}
