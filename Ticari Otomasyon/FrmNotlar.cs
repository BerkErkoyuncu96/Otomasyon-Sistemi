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
using System.Management.Instrumentation;

namespace Ticari_Otomasyon
{
    public partial class FrmNotlar : Form
    {
        public FrmNotlar()
        {
            InitializeComponent();
        }

        sqlBaglantisi connect = new sqlBaglantisi();

        public void Listele()
        {
            DataTable dt= new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Tbl_Notlar",connect.Baglan());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        public void Temizle()
        {
            txtId.Clear();
            mskdSaat.Clear();
            mskdTarih.Clear();
            txtAlici.Clear();
            txtBaslik.Clear();
            txtMesajinSahibi.Clear();
            rchDetay.Clear();
        }


        private void FrmNotlar_Load(object sender, EventArgs e)
        {
            Listele();
            Temizle();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Insert into Tbl_Notlar (TARIH , SAAT ,BASLIK , SAHIBI , HITAP , DETAY) VALUES (@p1,@p2,@p3,@p4,@p5,@p6)",connect.Baglan());
            komut.Parameters.AddWithValue("@p1",mskdTarih.Text);
            komut.Parameters.AddWithValue("@p2",mskdSaat.Text);
            komut.Parameters.AddWithValue("@p3",txtBaslik.Text);
            komut.Parameters.AddWithValue("@p4",txtMesajinSahibi.Text);
            komut.Parameters.AddWithValue("@p5",txtAlici.Text);
            komut.Parameters.AddWithValue("@p6",rchDetay.Text);
            komut.ExecuteNonQuery();
            connect.Baglan().Close();
            MessageBox.Show("Notunuz sisteme kaydedilmiştir.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            Temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtId.Text = dr["ID"].ToString();
                mskdTarih.Text = dr["TARIH"].ToString();
                mskdSaat.Text = dr["SAAT"].ToString();
                txtBaslik.Text = dr["BASLIK"].ToString();
                txtMesajinSahibi.Text = dr["SAHIBI"].ToString();
                txtAlici.Text = dr["HITAP"].ToString();
                rchDetay.Text = dr["DETAY"].ToString();
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut= new SqlCommand("Delete from Tbl_Notlar where ID=@p1",connect.Baglan());
            komut.Parameters.AddWithValue("@p1",txtId.Text);
            komut.ExecuteNonQuery();
            connect.Baglan().Close();
            MessageBox.Show("Notunuz sistemden silinmiştir.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Listele();
            Temizle();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update Tbl_Notlar set TARIH=@P1 , SAAT=@P2 , BASLIK=@P3 , SAHIBI=@P4 , HITAP=@P5 , DETAY=@P6 WHERE ID=@P7",connect.Baglan());
            komut.Parameters.AddWithValue("@p1", mskdTarih.Text);
            komut.Parameters.AddWithValue("@p2", mskdSaat.Text);
            komut.Parameters.AddWithValue("@p3", txtBaslik.Text);
            komut.Parameters.AddWithValue("@p4", txtMesajinSahibi.Text);
            komut.Parameters.AddWithValue("@p5", txtAlici.Text);
            komut.Parameters.AddWithValue("@p6", rchDetay.Text);
            komut.Parameters.AddWithValue("@P7", txtId.Text);
            komut.ExecuteNonQuery();
            connect.Baglan().Close();
            MessageBox.Show("Notunuz başarıyla güncellenmiştir.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            Temizle();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmMetinMesaji text =  new FrmMetinMesaji();
            DataRow dr= gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(dr != null )
            {
                text.metin = dr["DETAY"].ToString();
            }
            text.Show();
        }
    }
}
