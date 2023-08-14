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
    public partial class FrmUrunler : Form
    {
        public FrmUrunler()
        {
            InitializeComponent();
        }

        sqlBaglantisi connect1 = new sqlBaglantisi();

        public void Listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter= new SqlDataAdapter("select * from Tbl_Urun",connect1.Baglan());
            adapter.Fill(dt); 
            gridControl1.DataSource = dt;
        }

        public void TEMIZLE()
        {
            txtID.Clear();
            txtAd.Clear();
            txtMarka.Clear();
            txtModel.Clear();
            mskdYıl.Clear();
            numericAdet.Value =0;
            txtAlıs.Clear();
            txtSatıs.Clear();
            rchDetay.Clear();
        }

        private void FrmUrunler_Load(object sender, EventArgs e)
        {
            Listele();
            TEMIZLE();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutkaydet = new SqlCommand("insert into Tbl_Urun (URUNAD,URUNMARKA,URUNMODEL,YIL,ADET,ALISFIYATI,SATISFIYAT,DETAY) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)",connect1.Baglan());
            komutkaydet.Parameters.AddWithValue("@p1", txtAd.Text);
            komutkaydet.Parameters.AddWithValue("@p2", txtMarka.Text);
            komutkaydet.Parameters.AddWithValue("@p3", txtModel.Text);
            komutkaydet.Parameters.AddWithValue("@p4", mskdYıl.Text);
            komutkaydet.Parameters.AddWithValue("@p5",numericAdet.Text);
            komutkaydet.Parameters.AddWithValue("@p6",decimal.Parse(txtAlıs.Text));
            komutkaydet.Parameters.AddWithValue("@p7", decimal.Parse(txtSatıs.Text));
            komutkaydet.Parameters.AddWithValue("@p8",rchDetay.Text);
            komutkaydet.ExecuteNonQuery();
            connect1.Baglan().Close();
            MessageBox.Show("Ürün sisteme eklendi.","Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Information);
            TEMIZLE();
            Listele();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutsil = new SqlCommand("delete from Tbl_Urun where ID=@p1",connect1.Baglan());
            komutsil.Parameters.AddWithValue("@p1",txtID.Text);
            komutsil.ExecuteNonQuery();
            connect1.Baglan() .Close();
            MessageBox.Show("Ürün sistemden silinmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            TEMIZLE();
            Listele();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komutguncelle = new SqlCommand("update Tbl_Urun set URUNAD = @P1 , URUNMARKA=@P2,URUNMODEL=@P3,YIL=@P4,ADET=@P5,ALISFIYATI=@P6,SATISFIYAT=@P7,DETAY=@P8 where ID=@P9",connect1.Baglan());
            komutguncelle.Parameters.AddWithValue("@p1", txtAd.Text);
            komutguncelle.Parameters.AddWithValue("@p2", txtMarka.Text);
            komutguncelle.Parameters.AddWithValue("@p3", txtModel.Text);
            komutguncelle.Parameters.AddWithValue("@p4", mskdYıl.Text);
            komutguncelle.Parameters.AddWithValue("@p5", numericAdet.Text);
            komutguncelle.Parameters.AddWithValue("@p6", decimal.Parse(txtAlıs.Text));
            komutguncelle.Parameters.AddWithValue("@p7", decimal.Parse(txtSatıs.Text));
            komutguncelle.Parameters.AddWithValue("@p8", rchDetay.Text);
            komutguncelle.Parameters.AddWithValue("@P9",txtID.Text);
            komutguncelle.ExecuteNonQuery();
            connect1.Baglan().Close();
            MessageBox.Show("Ürün bilgisi güncellenmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            TEMIZLE();
            Listele();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(dr!= null)
            {
                txtID.Text = dr["ID"].ToString();
                txtAd.Text = dr["URUNAD"].ToString();
                txtMarka.Text = dr["URUNMARKA"].ToString();
                txtModel.Text = dr["URUNMODEL"].ToString();
                mskdYıl.Text = dr["YIL"].ToString();
                numericAdet.Value = int.Parse(dr["ADET"].ToString());
                txtAlıs.Text = dr["ALISFIYATI"].ToString();
                txtSatıs.Text = dr["SATISFIYAT"].ToString();
                rchDetay.Text = dr["DETAY"].ToString();
            }

        }
    }
}
