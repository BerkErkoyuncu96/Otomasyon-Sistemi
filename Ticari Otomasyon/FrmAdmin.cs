using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Ticari_Otomasyon
{
    public partial class FrmAdmin : Form
    {
        public FrmAdmin()
        {
            InitializeComponent();
        }

        sqlBaglantisi connect = new sqlBaglantisi();

        private void FrmAdmin_Load(object sender, EventArgs e)
        {

        }

        public string Kullanici;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select * from Tbl_Admin where KullanıcıAdi = @p1 and Sifre = @p2", connect.Baglan());
            komut.Parameters.AddWithValue("@p1", txtKulanıcıAd.Text);
            komut.Parameters.AddWithValue("@p2",txtSifre.Text);
            SqlDataReader dr =  komut.ExecuteReader();
            if (dr.Read())
            {
                Form1 fr = new Form1();
                fr.kullanici = txtKulanıcıAd.Text;
                fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Kullanıcı adı veya şifre girişi", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void simpleButton1_MouseHover(object sender, EventArgs e)
        {
            simpleButton1.BackColor = Color.Teal;
        }

        private void simpleButton1_MouseLeave(object sender, EventArgs e)
        {
            simpleButton1.BackColor= Color.White;
        }
    }
}
