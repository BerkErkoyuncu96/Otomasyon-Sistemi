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
using System.Data.SqlClient;
using System.Data.Common;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;

namespace Ticari_Otomasyon
{
    public partial class FrmMusteriler : Form
    {
        public FrmMusteriler()
        {
            InitializeComponent();
        }

        sqlBaglantisi connect2 = new sqlBaglantisi();

        public void Listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Musteriler",connect2.Baglan());
            da.Fill(dt);
           gridControl1.DataSource = dt;
        }

        public void SehirListesi()
        {
            SqlCommand sehirler = new SqlCommand("select SEHIR from Tbl_Iller",connect2.Baglan());
            SqlDataReader dr= sehirler.ExecuteReader();
            while (dr.Read())
            {
                cmbIl.Properties.Items.Add(dr[0]);
            }
            connect2.Baglan().Close();
        }


        public void Temizle()
        {
            txtID.Clear();
            txtAD.Clear();
            txtSoyad.Clear();
            mskdTel.Clear();
            mskdTel2.Clear();
            mskdTC.Clear();
            txtVergi.Clear();
            cmbIl.Clear();
            cmbIlce.Clear();
            rchAdres.Clear();
        }
            
        private void FrmMusteriler_Load(object sender, EventArgs e)
        {
            Listele();
            SehirListesi();
            Temizle();
        }

        private void cmbIl_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbIlce.Properties.Items.Clear();
            SqlCommand komut = new SqlCommand("select ilce from Tbl_Ilceler where sehir = @p1",connect2.Baglan());
            komut.Parameters.AddWithValue("@p1", cmbIl.SelectedIndex+1);
            SqlDataReader dr= komut.ExecuteReader();
            while (dr.Read())
            {
                cmbIlce.Properties.Items.Add(dr[0]);
            }
            connect2.Baglan().Close();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand kaydetKomut = new SqlCommand("insert into Tbl_Musteriler (AD,SOYAD,TELEFON,TELEFON2,TC,IL,ILCE,ADRES,VERGIDAIRESI) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", connect2.Baglan());
            kaydetKomut.Parameters.AddWithValue("@p1", txtAD.Text);
            kaydetKomut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            kaydetKomut.Parameters.AddWithValue("@p3",mskdTel.Text);
            kaydetKomut.Parameters.AddWithValue("@p4", mskdTel2.Text);
            kaydetKomut.Parameters.AddWithValue("@p5", mskdTC.Text);
            kaydetKomut.Parameters.AddWithValue("@p6", cmbIl.Text);
            kaydetKomut.Parameters.AddWithValue("@p7",cmbIlce.Text);
            kaydetKomut.Parameters.AddWithValue("@p8",rchAdres.Text);
            kaydetKomut.Parameters.AddWithValue("@p9", txtVergi.Text);
            kaydetKomut.ExecuteNonQuery();
            connect2.Baglan().Close();
            MessageBox.Show("Müşteri sisteme eklenmiştir.","Bilgilendirme",MessageBoxButtons.OK,MessageBoxIcon.Information);
            Listele();
            Temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtID.Text = dr["Id"].ToString();
                txtAD.Text = dr["AD"].ToString();
                txtSoyad.Text = dr["SOYAD"].ToString();
                mskdTel.Text = dr["TELEFON"].ToString();
                mskdTel2.Text = dr["TELEFON2"].ToString();
                mskdTC.Text = dr["TC"].ToString();
                cmbIl.Text = dr["IL"].ToString();
                cmbIlce.Text = dr["ILCE"].ToString();
                rchAdres.Text = dr["ADRES"].ToString();
                txtVergi.Text = dr["VERGIDAIRESI"].ToString();
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
          DialogResult result1 =  MessageBox.Show("Müşteriyi silmek istediğinizden emin misiniz?", "Uyarı Mesajı!!!",MessageBoxButtons.YesNo);
            if (result1 == DialogResult.Yes)
            {
                SqlCommand komutsil = new SqlCommand("delete from Tbl_Musteriler where ID=@p1", connect2.Baglan());
                komutsil.Parameters.AddWithValue("@p1", txtID.Text);
                komutsil.ExecuteNonQuery();
                connect2.Baglan().Close();

                //Hareket tablolarından hareketlerin silinmesi.

                SqlCommand cmd = new SqlCommand("Delete from Tbl_MusteriHareketleri where MUSTERI=@s1",connect2.Baglan());
                cmd.Parameters.AddWithValue("@s1",txtID.Text);
                cmd.ExecuteNonQuery();
                connect2.Baglan().Close();
                MessageBox.Show("Müşteri sistemden silindi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Listele();
                Temizle();
            }
            else
            {
                Listele();
                Temizle();
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update Tbl_Musteriler set AD=@P1,SOYAD=@P2,TELEFON=@P3,TELEFON2=@P4,TC=@P5,IL=@P6,ILCE=@P7,ADRES=@P8,VERGIDAIRESI=@P9 WHERE ID=@P10", connect2.Baglan());
            komut.Parameters.AddWithValue("@p1", txtAD.Text);
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", mskdTel.Text);
            komut.Parameters.AddWithValue("@p4", mskdTel2.Text);
            komut.Parameters.AddWithValue("@p5", mskdTC.Text);
            komut.Parameters.AddWithValue("@p6", cmbIl.Text);
            komut.Parameters.AddWithValue("@p7", cmbIlce.Text);
            komut.Parameters.AddWithValue("@p8", rchAdres.Text);
            komut.Parameters.AddWithValue("@p9", txtVergi.Text);
            komut.Parameters.AddWithValue("@P10",txtID.Text);
            komut.ExecuteNonQuery();
            MessageBox.Show("Müşteri bilgisi güncellendi.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            Listele();
            Temizle();
        }
    }
}
