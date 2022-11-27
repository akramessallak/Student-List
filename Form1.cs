using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsListe
{
    public partial class Form1 : Form
    {
        private bool Inup = false;
        private bool Inadd = false;
        private bool Indel = false;
        public Form1()
        {
            InitializeComponent();
        }

        static string chaine = @"Data Source=DESKTOP-KIMOシ\SQLEXPRESS;Initial Catalog=dbListe;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        static SqlConnection cnx = new SqlConnection(chaine);
        static SqlCommand cmd = new SqlCommand();
        static SqlDataAdapter adapter = new SqlDataAdapter(cmd);

        private void Form1_Load(object sender, EventArgs e)
        {
            initialstate();
            display();
        }
        public void initialstate()
        {
            Cbx.Enabled = true;
            TxtID.Enabled = false;
            TxtName.Enabled = false;
            TxtPrenom.Enabled = false;
            BtnCancel.Enabled = false;
            BtnSave.Enabled = false;
            BtnUpdate.Enabled = false;
            BtnDelete.Enabled = false;
            BtnAdd.Enabled = true;
        }
        public void display()
        {
            Cbx.Items.Clear();
            cmd.Connection = cnx;
            cmd.CommandText = "select * from dbo.list";
            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.Connection.Close();
            }
            cnx.Open();
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Cbx.Items.Add(dr["ID"].ToString());
                Cbx.Items.Add(dr["Name"]);
                Cbx.Items.Add(dr["Prenom"]);
                Cbx.Items.Add("------------------------------");
            }
        }

        private void Cbx_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TxtID_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtPrenom_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Inadd = true;

            TxtID.Clear();
            TxtName.Clear();
            TxtPrenom.Clear();
            display();
            Cbx.Enabled = false;
            TxtID.Enabled = true;
            TxtName.Enabled = true;
            TxtPrenom.Enabled = true;
            BtnCancel.Enabled = true;
            BtnSave.Enabled = true;
            BtnUpdate.Enabled = false;
            BtnDelete.Enabled = false;
            BtnAdd.Enabled = false;

        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            Inup = true;

            Cbx.Enabled = true;
            TxtID.Enabled = true;
            TxtName.Enabled = true;
            TxtPrenom.Enabled = true;
            BtnCancel.Enabled = true;
            BtnSave.Enabled = true;
            BtnUpdate.Enabled = false;
            BtnDelete.Enabled = false;
            BtnAdd.Enabled = false;
            display();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {

            Indel = true;
            string msgbox = "Are you sure you want to delete this item?";

            string YNBox = "Confirm Deletion";


            if (MessageBox.Show(msgbox, YNBox, MessageBoxButtons.YesNo) == DialogResult.No)

            {
                if (cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
                cnx.Open();
                cmd.Connection = cnx;
                cmd.CommandText = "delete from List where id='" + TxtID.Text + "' ";
                cmd.ExecuteNonQuery();
                cnx.Close();
                TxtID.Clear();
                TxtName.Clear();
                TxtPrenom.Clear();
                initialstate();
            }
            else
            {
                Cbx.Enabled = true;
                TxtID.Enabled = false;
                TxtName.Enabled = false;
                TxtPrenom.Enabled = false;
                BtnCancel.Enabled = false;
                BtnSave.Enabled = false;
                BtnUpdate.Enabled = true;
                BtnDelete.Enabled = true;
                BtnAdd.Enabled = true;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.Connection.Close();
            }
            cmd.Connection = cnx;
            cnx.Open();


            if (Inadd)
            {
                cmd.CommandText = "INSERT INTO List (ID,Name,Prenom ) " + "values('" + TxtID.Text + "','" + TxtName.Text + "','" + TxtPrenom.Text + "')";
                cmd.ExecuteNonQuery();
                Inadd = false;
            }

            if (Inup)
            {
                cmd.CommandText = "UPDATE List set Name='" + TxtName.Text + "',Prenom ='" + TxtPrenom.Text + "' where ID ='" + TxtID.Text + "'"; 
                cmd.ExecuteNonQuery();
                Inup = false;

            }

            cnx.Close();

            Cbx.Enabled = true;
            TxtID.Enabled = false;
            TxtName.Enabled = false;
            TxtPrenom.Enabled = false;
            BtnCancel.Enabled = false;
            BtnSave.Enabled = false;
            BtnUpdate.Enabled = true;
            BtnDelete.Enabled = true;
            BtnAdd.Enabled = true;
            display();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (Inup)
            {
                Cbx.Enabled = true;
                TxtID.Enabled = false;
                TxtName.Enabled = false;
                TxtPrenom.Enabled = false;
                BtnCancel.Enabled = false;
                BtnSave.Enabled = false;
                BtnUpdate.Enabled = true;
                BtnDelete.Enabled = true;
                BtnAdd.Enabled = true;
            }
            else
            {
                initialstate();
            }

        }

    }
}
