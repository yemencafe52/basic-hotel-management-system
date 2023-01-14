using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace HMS
{
    static class clsEntryPoint
    {
        private static Mutex myMutex = null;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool mres = false;

            myMutex = new Mutex(false, "HMS52", out mres);

            if(!mres)
            {
                MessageBox.Show("التطبيق يعمل حالياً...");
                return;
            }


            if (!(Utilities.CanWR()))
            {
                Event.Add(new Event("يتطلب تشغيل النظام تحت إمتيازات مسؤول", DateTime.Now));
                MessageBox.Show("يتطلب التطبيق العمل تحت إمتيازات مسؤول");
                return;
            }

            if (!(Utilities.CheckDataBaseFile()))
            {
                Event.Add(new Event("تعذر العثور على قاعدة البيانات", DateTime.Now));
                DialogResult res =  MessageBox.Show("تعذر العثور على قاعدة البيانات, هل تريد إستعادة قاعدة بيانات إحتياطية؟","",MessageBoxButtons.YesNo);

                if(res == DialogResult.Yes)
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    DialogResult res1 = ofd.ShowDialog();

                    if(res1 == DialogResult.OK)
                    {
                        if(Utilities.ResotreDB(ofd.FileName))
                        {
                            Event.Add(new Event("تم إستعادة قاعدة البيانات بنجاح.", DateTime.Now));
                            MessageBox.Show("تم إستعادة قاعدة البيانات بنجاح, يجب إعادة تشغيل النظام.");
                        }
                        else
                        {
                            Event.Add(new Event("تعذر عملية إستعادة قاعدة البيانات.", DateTime.Now));
                            MessageBox.Show("تعذر إستعادة قاعدة البيانات.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("تم إلغاء العملية.");
                    }
                }
                else
                {
                    if(!(Utilities.InstallDB()))
                    {
                        MessageBox.Show("تعذر انشاء قاعدة بيانات جديدة.");
                    }
                    else
                    {
                        MessageBox.Show("تم انشاء قاعدة بيانات جديدة");
                    }
                }
                return;
            }

            if (!(Utilities.TestDB()))
            {
                Event.Add(new Event("تعذر الإتصال بقاعدة البيانات", DateTime.Now));
                MessageBox.Show("تعذر الإتصال بقاعدة البيانات.");
                return;
            }

            Event.Add(new Event("تشغيل النظام", DateTime.Now));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!Param.Read())
            {
                MessageBox.Show("تعذر قراءة الاعدادت");

                frmSettings fs = new frmSettings();
                fs.ShowDialog();
                return;
            }
           
            frmLogin flogin = new frmLogin();
            flogin.ShowDialog();

            if (flogin.Sucess)
            {
                if(!(ReservationsManager.Init()))
                {
                    Event.Add(new Event("تعذر تهيئة النظام وبدأ تشغيله.", DateTime.Now));
                    MessageBox.Show("تعذر تهيئة النظام وبدأ تشغيله.");
                    return;
                }

                Application.Run(new frmMain());


                if(!(ReservationsManager.Shutdown()))
                {
                    Event.Add(new Event("لم يتم إيقاف تشغيل النظام بصورة صحيحة.", DateTime.Now));
                    MessageBox.Show("لم يتم إيقاف تشغيل النظام بصورة صحيحة..");
                    return;
                }
            }
            else
            {

            }

            myMutex.Close();
            Event.Add(new Event("إيقاف تشغيل النظام", DateTime.Now));

        }
    }
}
