using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            Preparing();
        }


        private bool Preparing()
        {
            PrintRooms();
            return true;
        }


        private void PrintRooms()
        {
            listView1.Items.Clear();

            for(int i=0;i< ReservationsManager.Rooms.Count;i++)
            {
                ListViewItem lvi = new ListViewItem(ReservationsManager.Rooms[i].Number.ToString());
                lvi.SubItems.Add( Utilities.RoomStateToString(ReservationsManager.Rooms[i].ReservationInfo.StateInfo));
                lvi.SubItems.Add(ReservationsManager.Rooms[i].ReservationInfo.CustomerInfo.Name);
                lvi.SubItems.Add(Utilities.DateTimeToString(ReservationsManager.Rooms[i].ReservationInfo.StartAt));
                lvi.SubItems.Add(Utilities.DateTimeToString(ReservationsManager.Rooms[i].ReservationInfo.End));

                listView1.Items.Add(lvi);

            }
        }


        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout fa = new frmAbout();
            fa.ShowDialog();
        }

        private void UpdateRoomInfo(Room room)
        {
            ListViewItem lvi = listView1.FindItemWithText(room.Number.ToString());

            if (lvi != null)
            {
                int index = lvi.Index;
                listView1.Items[index].SubItems[1].Text = Utilities.RoomStateToString(room.ReservationInfo.StateInfo);
                listView1.Items[index].SubItems[2].Text = (room.ReservationInfo.CustomerInfo.Name);

                listView1.Items[index].SubItems[3].Text = Utilities.DateTimeToString((room.ReservationInfo.StartAt));
                listView1.Items[index].SubItems[4].Text = Utilities.DateTimeToString((room.ReservationInfo.End));
            }
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            TreeNode tn = treeView1.SelectedNode;

            if(tn != null)
            {
                int num = 0;

                if (tn.Tag != null)
                {
                    if (int.TryParse(tn.Tag.ToString(), out num))
                    {
                        switch (num)
                        {
                            case 1:
                                {
                                    frmSettings fs = new frmSettings();
                                    fs.ShowDialog();
                                    break;
                                }
                            case 2:
                                {
                                    frmCustomerViewer fcv = new frmCustomerViewer();
                                    fcv.Show();
                                    break;
                                }
                            case 3:
                                {
                                    frmReport fr = new frmReport();
                                    fr.ShowDialog();
                                    break;
                                }
                            case 4:
                                {
                                    frmChangePassword fcp = new frmChangePassword();
                                    fcp.ShowDialog();
                                    break;
                                }
                            case 5:
                                {
                                    break;
                                }
                            case 6:
                                {
                                    break;
                                }
                        }



                    }
                }
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count >0)
            {
                byte index = byte.Parse(listView1.Items[listView1.SelectedItems[0].Index].Text);

                Room room = new Room(index);

                switch (room.ReservationInfo.StateInfo)
                {
                    case State.State1:
                        {
                            frmReservationMgr fr = new frmReservationMgr(room);
                            fr.ShowDialog();

                            break;
                        }

                    case State.State2:
                        {
                            ReservationsManager.ChangeRoomState(room);
                            break;
                        }


                    case State.State3:
                        {
                            ReservationsManager.ChangeRoomState(room);
                            break;
                        }
                }
            
                UpdateRoomInfo(room);

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = User.ActiveUser.UserName;
            toolStripStatusLabel2.Text = DateTime.Now.ToString();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Event ev = new Event("", new DateTime());
            while(Event.Read(ref ev))
            {
                ListViewItem lvi = new ListViewItem(ev.Message);
                lvi.SubItems.Add(ev.TimeInfo.ToString());
                listView2.Items.Add(lvi);
                listView2.EnsureVisible(listView2.Items.Count - 1);
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult res = MessageBox.Show("هل تريد إغلاق التطبيق فعلاً؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if(res == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sod = new SaveFileDialog();

            DialogResult res = sod.ShowDialog();

            if(res == DialogResult.OK)
            {
                string path = sod.FileName;
                if (!(Utilities.BackupDB(path)))
                {
                    MessageBox.Show("تعذر عملية التنفيذ");
                }
            }
        }
    }
}
