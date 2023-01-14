using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS
{
    internal enum CardType:byte
    {
        Card1= 1,
        Card2,
        Card3
    }

    public class Customer
    {
        private int number;
        private string name;
        private CardType cardType;
        private string cardNumber;
        private string note;

        public Customer(int number)
        {
            try
            {
                AccessDB db = new AccessDB(Constants.GetConnectionString);
                string sql = "select cus_no,cus_name,cus_card_type,cus_card_no,cus_note from tblCustomers where cus_no=" + number;

                if (db.ExcuteQuery(sql))
                {
                    if (db.GetOleDbDataReader.Read())
                    {
                        this.number = Convert.ToInt32(db.GetOleDbDataReader["cus_no"].ToString());
                        this.name = (db.GetOleDbDataReader["cus_name"].ToString());

                        this.cardType = (CardType)Convert.ToByte(db.GetOleDbDataReader["cus_card_type"].ToString());
                        this.cardNumber = (db.GetOleDbDataReader["cus_card_no"].ToString());
                        this.note = (db.GetOleDbDataReader["cus_note"].ToString());
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
                this.name = "";
                this.cardType = CardType.Card1;
                this.cardNumber = "";
                this.note = "";
            }
        }

        internal Customer(
             int number,
             string name,
             CardType cardType,
             string cardNumber,
             string note
        )
        {
            this.number = number;
            this.name = name;
            this.cardType = cardType;
            this.cardNumber = cardNumber;
            this.note = note;
        }

        internal int Number
        {
            get
            {
                return this.number;
            }
        }

        internal string Name
        {
            get
            {
                return this.name;
            }
        }

        internal CardType CardTypeInfo
        {
            get
            {
                return this.cardType;
            }
        }

        internal string CardNumber
        {
            get
            {
                return this.cardNumber;
            }
        }

        internal string Note
        {
            get
            {
                return this.note;
            }
        }

        internal static bool Add(Customer customer)
        {
            bool res = false;

            try
            {
                AccessDB db = new AccessDB(Constants.GetConnectionString);
                string sql = "insert into tblCustomers (cus_no,cus_name,cus_card_type,cus_card_no,cus_note) values("+ customer.number + ",'" + customer.Name + "'," + (byte)customer.CardTypeInfo + ",'" + customer.CardNumber + "','" + customer.Note + "')";

                if(db.ExcuteNonQuery(sql) > 0)
                {
                    res = true;
                }

            }
            catch { }

            return res;
        }

        internal static bool Update(Customer customer)
        {
            bool res = false;

            try
            {
                AccessDB db = new AccessDB(Constants.GetConnectionString);
                string sql = "update tblCustomers set cus_note='"+customer.Note+"' where cus_no=" + customer.Number;

                if (db.ExcuteNonQuery(sql) > 0)
                {
                    res = true;
                }

            }
            catch { }


            return res;
        }


        internal static byte GenerateNewCustomerNumber()
        {
            byte res = 1;

            try
            {
                AccessDB db = new AccessDB(Constants.GetConnectionString);
                string sql = "select max(cus_no) as res from tblCustomers";
                if(db.ExcuteQuery(sql))
                {
                    if(db.GetOleDbDataReader.Read())
                    {
                        string temp = db.GetOleDbDataReader["res"].ToString();

                        if (byte.TryParse(temp, out res))
                        {
                            res++;
                        }
                        else
                        {
                            res = 1;
                        }

                    }
                }

                db.CloseConnection();
            }
            catch
            { }

            return res;
        }


        internal static List<Customer> GetALL()
        {
            List<Customer> res = new List<Customer>();

            try
            {
                AccessDB db = new AccessDB(Constants.GetConnectionString);
                string sql = "select cus_no,cus_name,cus_card_type,cus_card_no,cus_note from tblCustomers";

                if(db.ExcuteQuery(sql))
                {
                    while(db.GetOleDbDataReader.Read())
                    {
                        res.Add(new Customer(Convert.ToByte(db.GetOleDbDataReader["cus_no"].ToString()), (db.GetOleDbDataReader["cus_name"].ToString()), (CardType)Convert.ToByte(db.GetOleDbDataReader["cus_card_type"].ToString()),(db.GetOleDbDataReader["cus_card_no"].ToString()), (db.GetOleDbDataReader["cus_note"].ToString())));
                    }
                }

                db.CloseConnection();
            }
            catch
            { }

            return res;
        }


        internal static List<Customer> Search(string name)
        {
            List<Customer> res = new List<Customer>();

            try
            {
                AccessDB db = new AccessDB(Constants.GetConnectionString);
                string sql = "select cus_no,cus_name,cus_card_type,cus_card_no,cus_note from tblCustomers where cus_name like('%"+ name +"%')";

                if (db.ExcuteQuery(sql))
                {
                    while (db.GetOleDbDataReader.Read())
                    {
                        res.Add(new Customer(Convert.ToByte(db.GetOleDbDataReader["cus_no"].ToString()), (db.GetOleDbDataReader["cus_name"].ToString()), (CardType)Convert.ToByte(db.GetOleDbDataReader["cus_card_type"].ToString()), (db.GetOleDbDataReader["cus_card_no"].ToString()), (db.GetOleDbDataReader["cus_note"].ToString())));
                    }
                }

                db.CloseConnection();
            }
            catch
            { }

            return res;
        }


    }
}
