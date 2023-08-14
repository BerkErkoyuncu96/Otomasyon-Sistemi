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
    public partial class UrunDetayIslemleri : Form
    {
        public UrunDetayIslemleri()
        {
            InitializeComponent();
        }
        public string ID;

        sqlBaglantisi connect = new sqlBaglantisi();

        private void UrunDetayIslemleri_Load(object sender, EventArgs e)
        {
            txtDetayId.Text = ID;

            SqlCommand komut = new SqlCommand("select * from Tbl_UrunFaturasi where ID=@p1", connect.Baglan());
            komut.Parameters.AddWithValue("@p1", txtDetayId.Text);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                txtUrunAd.Text = reader[1].ToString();
                txtMiktar.Text = reader[2].ToString();
                txtFiyat.Text = reader[3].ToString();
                txtTutar.Text = reader[4].ToString();
            }
            connect.Baglan().Close();

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand("delete from Tbl_UrunFaturasi where ID=@p1",connect.Baglan());
            sqlCommand.Parameters.AddWithValue("@p1",txtDetayId.Text);
            sqlCommand.ExecuteNonQuery();
            connect.Baglan().Close();
            MessageBox.Show("Sistemde mevcut ürün bilgisi silinmiştir.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update Tbl_UrunFaturasi set URUNAD=@P1 , MIKTAR = @P2 , FIYAT=@P3 , TUTAR=@P4 WHERE ID=@P5",connect.Baglan());
            komut.Parameters.AddWithValue("@P1", txtUrunAd.Text);
            komut.Parameters.AddWithValue("@P2", txtMiktar.Text);
            komut.Parameters.AddWithValue("@P3",double.Parse(txtFiyat.Text));
            komut.Parameters.AddWithValue("@P4",double.Parse(txtTutar.Text));
            komut.Parameters.AddWithValue("@P5", txtDetayId.Text);
            komut.ExecuteNonQuery();
            connect.Baglan().Close();
            MessageBox.Show("Sistemde mevcut ürün bilgisi güncellenmiştir.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
