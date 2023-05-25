using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkDM_shumkova
{

    public partial class Authorization_frm : Form
    {
        int count = 1;

        public Authorization_frm()
        {
            InitializeComponent();
        }
       
        private bool log(string login)
        {
            string zapros = $"select top(1000) \r\n [Login]\r\n  \r\n from[SecuretyDB_shumkova].[dbo].[User_tbl] where [Login]='{login}'";
            SqlConnection connect = new SqlConnection(@"Data Source=PC325L11\SQLEXPRESS;Initial Catalog=SecuretyDB_shumkova;Integrated Security=True");
            connect.Open( );
            SqlDataAdapter adptr = new SqlDataAdapter("select * from User_tbl", connect);
            DataTable table = new DataTable( );
            adptr.Fill(table);
            connect.Close( );
            if ( table.Rows.Count > 0 ) return false;
            else return true;
        }
        private bool pass (string password)
        {
            string zapros = $"select top(1000) \r\n [Password]\r\n  \r\n from[SecuretyDB_shumkova].[dbo].[User_tbl] where [Password]='{password}'";
            SqlConnection connect = new SqlConnection(@"Data Source=PC325L11\SQLEXPRESS;Initial Catalog=SecuretyDB_shumkova;Integrated Security=True");
            connect.Open( );
            SqlDataAdapter adptr = new SqlDataAdapter("select * from User_tbl", connect);
            DataTable table = new DataTable( );
            adptr.Fill(table);
            connect.Close( );
            if ( table.Rows.Count > 0 ) return false;
            else return true;
        }

        public void set_tbl()
        {
            SqlConnection connect = new SqlConnection(@"Data Source=PC325L11\SQLEXPRESS;Initial Catalog=SecuretyDB_shumkova;Integrated Security=True");
            connect.Open();
            SqlDataAdapter adptr = new SqlDataAdapter("select * from User_tbl", connect);
            DataTable table = new DataTable();
            adptr.Fill(table);
            dataGridView1.DataSource = table;
            connect.Close();
        }


        private void Input_btn_Click (object sender, EventArgs e)
        {
            SqlConnection connect = new SqlConnection(@"Data Source=PC325L11\SQLEXPRESS;Initial Catalog=SecuretyDB_shumkova;Integrated Security=True");

            string log = Log_Txt.Text;
            string pas = Pass_txt.Text;
            if ( log == "" || pas == "" )
            {
                MessageBox.Show("заполните все поля", "error");
            }
            else
            {
                connect.Open( );
                string prov = $"select Role,Active,Login from [dbo].[User_tbl] where Login='{log}' and password='{pas}'";
                string s = $"update dbo.User_tbl set count+=1 where Login='{log}'";
                string slct = $"select Login,active From dbo.User_tbl where Login='{log}' and Active='False'";


                SqlDataAdapter adptr = new SqlDataAdapter(slct, connect);
                DataTable table = new DataTable( );
                adptr.Fill(table);
                if ( table.Rows.Count == 1 )
                {
                    MessageBox.Show("пользователь заблокирован", "error");
                    connect.Close( );
                }
                else
                {
                    SqlDataAdapter adptr1 = new SqlDataAdapter(prov, connect);
                    adptr1.Fill(table);
                    if ( table.Rows.Count == 0 )
                    {
                        MessageBox.Show("вы ввели неправильный пароль или логин", "error");
                        SqlDataAdapter adptr2 = new SqlDataAdapter(s, connect);
                        adptr2.Fill(table);
                        string sq = $"update dbo.User_tbl set Active='False' where count>=3";
                        SqlDataAdapter adptr3 = new SqlDataAdapter(sq, connect);
                        adptr3.Fill(table);
                        connect.Close( );

                    }
                    else
                    {
                        if ( table.Rows [ 0 ] [ 0 ].ToString( ) == "User" )
                        {
                             Root_frm f1 = new Root_frm( );
                             f1.Show( );
                             this.Hide( );
                        }
                        if ( table.Rows [ 0 ] [ 0 ].ToString( ) == "Admin" )
                        {
                            Admin_frm f2 = new Admin_frm( );
                            f2.Show( );
                            this.Hide( );
                        }
                        connect.Close( );
                    }
                }
            }
        }

        private void Authorization_frm_Load(object sender, EventArgs e)
        {
            set_tbl();
        }

        private void button1_Click (object sender, EventArgs e)
        {
            if ( textBox2.Text.Length == 0 ) MessageBox.Show("введите логин", "error");
            else if ( textBox1.Text.Length == 0 ) MessageBox.Show("введите пароль ", "error");
            else if ( textBox1.Text != textBox3.Text ) MessageBox.Show("пароль не совпадает", "error");
            else if ( log(textBox2.Text) == false ) MessageBox.Show("такой логин уже существует", "error");
            else
            {
                string query = $"insert into dbo.User_tbl(login,Password,Count,Date,Active,Role) values ('{textBox2.Text}','{textBox1.Text}',0,{DateTime.Today.Year}-{DateTime.Today.Month}-{DateTime.Today.Day}','True','user')";
                SqlConnection connect = new SqlConnection(@"Data Source=PC325L11\SQLEXPRESS;Initial Catalog=SecuretyDB_shumkova;Integrated Security=True");
                connect.Open( );
                SqlDataAdapter adptr = new SqlDataAdapter("select * from User_tbl", connect);
                DataTable table = new DataTable( );
                adptr.Fill(table);
                connect.Close( );
                set_tbl( );
            }
        }
    }
}
