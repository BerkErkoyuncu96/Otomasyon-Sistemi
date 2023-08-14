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
    public partial class PERSONLLER : Form
    {
        public PERSONLLER()
        {
            InitializeComponent();
        }
        sqlBaglantisi connect = new sqlBaglantisi();
        public void Listele()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from Tbl_Personeller",connect.Baglan());
            sqlDataAdapter.Fill(dataTable);
            gridControl1.DataSource = dataTable;
        }

        public void Temizle()
        {
            txtAD.Clear();
            txtID.Clear();
            txtSoyad.Clear();
            txtMail.Clear();
            rchAdres.Clear();
            mskdTC.Clear();
            mskdTel.Clear();
            cmbIl.Clear();
            cmbIlce.Clear();
            txtDepartman.Clear();
        }

        public void SehirListesi()
        {
            SqlCommand sehirler = new SqlCommand("select SEHIR from Tbl_Iller", connect.Baglan());
            SqlDataReader dr = sehirler.ExecuteReader();
            while (dr.Read())
            {
                cmbIl.Properties.Items.Add(dr[0]);
            }
            connect.Baglan().Close();
        }

        private void PERSONLLER_Load(object sender, EventArgs e)
        {
            Listele();
            SehirListesi();
            Temizle();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Personeller (AD , SOYAD ,TELEFON,TC , MAIL , IL, ILCE ,ADRES ,DEPARTMAN) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)",connect.Baglan());
            komut.Parameters.AddWithValue("@p1",txtAD.Text);
            komut.Parameters.AddWithValue("@p2",txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3",mskdTel.Text);
            komut.Parameters.AddWithValue("@p4",mskdTC.Text);
            komut.Parameters.AddWithValue("@p5",txtMail.Text);
            komut.Parameters.AddWithValue("@p6",cmbIl.Text);
            komut.Parameters.AddWithValue("@p7",cmbIlce.Text);
            komut.Parameters.AddWithValue("@p8",rchAdres.Text);
            komut.Parameters.AddWithValue("@p9",txtDepartman.Text);
            komut.ExecuteNonQuery();
            connect.Baglan().Close();
            MessageBox.Show("Personel sisteme eklenmiştir.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            Temizle();
        }

        private void cmbIl_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbIlce.Properties.Items.Clear();
            SqlCommand komut = new SqlCommand("select ilce from Tbl_Ilceler where sehir = @p1", connect.Baglan());
            komut.Parameters.AddWithValue("@p1", cmbIl.SelectedIndex + 1);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbIlce.Properties.Items.Add(dr[0]);
            }
            connect.Baglan().Close();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(dr != null)
            {
                txtID.Text = dr["ID"].ToString();
                txtAD.Text = dr["AD"].ToString();
                txtSoyad.Text = dr["SOYAD"].ToString();
                mskdTel.Text = dr["TELEFON"].ToString();
                mskdTC.Text = dr["TC"].ToString();
                txtMail.Text = dr["MAIL"].ToString();
                cmbIl.Text = dr["IL"].ToString();
                cmbIlce.Text = dr["ILCE"].ToString();
                rchAdres.Text = dr["ADRES"].ToString();
                txtDepartman.Text = dr["DEPARTMAN"].ToString();
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from Tbl_Personeller where ID=@p1", connect.Baglan());
            komut.Parameters.AddWithValue("@p1",txtID.Text);
            komut.ExecuteNonQuery();
            MessageBox.Show("Kişi bilgisi sistemden silinmiştir.","Uyarı",MessageBoxButtons.OK, MessageBoxIcon.Warning);
            connect.Baglan().Close();
            Listele();
            Temizle();

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update Tbl_Personeller set AD=@P1,SOYAD=@P2,TELEFON=@P3 , TC=@P4 , MAIL=@P5,IL=@P6,ILCE=@P7,ADRES=@P8,DEPARTMAN=@P9 WHERE ID=@P10", connect.Baglan());
            komut.Parameters.AddWithValue("@p1", txtAD.Text);
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", mskdTel.Text);
            komut.Parameters.AddWithValue("@p4", mskdTC.Text);
            komut.Parameters.AddWithValue("@p5", txtMail.Text);
            komut.Parameters.AddWithValue("@p6", cmbIl.Text);
            komut.Parameters.AddWithValue("@p7", cmbIlce.Text);
            komut.Parameters.AddWithValue("@p8", rchAdres.Text);
            komut.Parameters.AddWithValue("@p9", txtDepartman.Text);
            komut.Parameters.AddWithValue("@P10", txtID.Text);
            komut.ExecuteNonQuery();
            connect.Baglan().Close();
            MessageBox.Show("Personel bilgisi güncellenmiştir.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            Temizle();
        }


    }
}
