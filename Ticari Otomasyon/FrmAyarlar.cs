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
    public partial class FrmAyarlar : Form
    {
        public FrmAyarlar()
        {
            InitializeComponent();
        }

        sqlBaglantisi connect = new sqlBaglantisi();

        public void Temizle()
        {
            textEdit1.Clear();
            textEdit2.Clear();
        }

        public void Listele()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from Tbl_Admin",connect.Baglan());
            adapter.Fill(dataTable);
            gridControl1.DataSource = dataTable;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {

            if (simpleButton3.Text == "Kaydet")
            {
                SqlCommand komut = new SqlCommand("insert into Tbl_Admin (KullanıcıAdi , Sifre ) values (@p1,@p2)", connect.Baglan());
                komut.Parameters.AddWithValue("@p1", textEdit1.Text);
                komut.Parameters.AddWithValue("@p2", textEdit2.Text);
                komut.ExecuteNonQuery();
                connect.Baglan().Close();
                MessageBox.Show("Yeni kullanıcı sisteme kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Listele();
            }
            if(simpleButton3.Text == "Güncelle")
            {
                SqlCommand komut = new SqlCommand("update Tbl_Admin set Sifre=@p1 where KullanıcıAdi=@p2", connect.Baglan());
                komut.Parameters.AddWithValue("@p2", textEdit1.Text);
                komut.Parameters.AddWithValue("@p1", textEdit2.Text);
                komut.ExecuteNonQuery();
                connect.Baglan().Close();
                MessageBox.Show("Kullanıcı şifresi güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Listele();
            }
        }

        private void FrmAyarlar_Load(object sender, EventArgs e)
        {
            Listele();
            Temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if(dr != null)
            {
                textEdit1.Text = dr["KullanıcıAdi"].ToString();
                textEdit2.Text = dr["Sifre"].ToString();
            }
        }

        private void textEdit1_TextChanged(object sender, EventArgs e)
        {
            if (textEdit1.Text != "")
            {
                simpleButton3.Text = "Güncelle";
            }
            else
            {
                simpleButton3.Text = "Kaydet";
            }
        }
    }
}
