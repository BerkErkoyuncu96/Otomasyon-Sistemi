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
using DevExpress.XtraEditors.Design;

namespace Ticari_Otomasyon
{
    public partial class Faturalar : Form
    {
        public Faturalar()
        {
            InitializeComponent();
        }

        sqlBaglantisi connect = new sqlBaglantisi();

        public void Temizle()
        {
            txtBilgiID.Clear();
            txtSeriNo.Clear();
            txtSiraNo.Clear();
            mskdSaat.Clear();
            mskdTarih.Clear();
            txtAlici.Clear();
            txtTeslimAlan.Clear();
            txtTeslimEden.Clear();
            txtVergiDairesi.Clear();

        }

        public void Listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from Tbl_FaturaBilgisi", connect.Baglan());
            adapter.Fill(dt);
            gridControl1.DataSource = dt;
            connect.Baglan().Close();
        }


        public void FirmaListesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select ID,AD from Tbl_Firmalar", connect.Baglan());
            da.Fill(dt);
            lookUpFirma.Properties.ValueMember = "ID";
            lookUpFirma.Properties.DisplayMember = "AD";
            lookUpFirma.Properties.DataSource = dt;
        }

        public void MusteriListesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select ID,AD,SOYAD from Tbl_Musteriler", connect.Baglan());
            da.Fill(dt);
            lookUpMusteri.Properties.ValueMember = "ID";
            lookUpMusteri.Properties.DisplayMember = "AD";
            lookUpMusteri.Properties.DataSource = dt;
        }

        public void PersonelListesi()
        {
             DataTable dt = new DataTable();
             SqlDataAdapter da = new SqlDataAdapter("select ID,AD,SOYAD from Tbl_Personeller",connect.Baglan());
             da.Fill(dt);
             lookUpPersonel.Properties.ValueMember = "ID";
             lookUpPersonel.Properties.DisplayMember = "AD";
             lookUpPersonel.Properties.DataSource = dt;
        }

        private void Faturalar_Load(object sender, EventArgs e)
        {

            Listele();
            Temizle();
            FirmaListesi();
            MusteriListesi();
            PersonelListesi();

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {

            if (txtFaturaID.Text == "")
            {
                SqlCommand komut = new SqlCommand("Insert into Tbl_FaturaBilgisi (SERINO , SIRANO,TARIH , SAAT , VERGIDAIRESI , ALICI , TESLIMEDEN , TESLIMALAN) VALUES (@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", connect.Baglan());
                komut.Parameters.AddWithValue("@p2",txtSeriNo.Text);
                komut.Parameters.AddWithValue("@p3",txtSiraNo.Text);
                komut.Parameters.AddWithValue("@p4",mskdTarih.Text);
                komut.Parameters.AddWithValue("@p5",mskdSaat.Text);
                komut.Parameters.AddWithValue("@p6",txtVergiDairesi.Text);
                komut.Parameters.AddWithValue("@p7",txtAlici.Text);
                komut.Parameters.AddWithValue("@p8",txtTeslimEden.Text);
                komut.Parameters.AddWithValue("@p9",txtTeslimAlan.Text);
                komut.ExecuteNonQuery();
                connect.Baglan().Close();
                MessageBox.Show("Fatura bilgisi sisteme eklenmiştir.", "Bilgilendirme",MessageBoxButtons.OK,MessageBoxIcon.Information);
                Listele();
                Temizle();
            }

            if (txtFaturaID.Text != "" && comboBox1.Text=="Firma Faturası")
            {
                string ToplamTutar = Convert.ToString(double.Parse(txtFiyat.Text) * int.Parse(txtMiktar.Text));
                SqlCommand komut = new SqlCommand("Insert into Tbl_UrunFaturasi (URUNAD , MIKTAR , FIYAT , TUTAR , FATURAID) VALUES (@p1,@p2,@p3,@p4,@p5)", connect.Baglan());
                komut.Parameters.AddWithValue("@p1",txtUrunAd.Text);
                komut.Parameters.AddWithValue("@p2",txtMiktar.Text);
                komut.Parameters.AddWithValue("@p3",double.Parse( txtFiyat.Text));
                komut.Parameters.AddWithValue("@p4",double.Parse(ToplamTutar));
                komut.Parameters.AddWithValue("@p5",txtFaturaID.Text);
                komut.ExecuteNonQuery();
                connect.Baglan().Close();

                // Hareketler Tablosuna Veri Ekleme
                SqlCommand cmd = new SqlCommand("Insert into Tbl_FirmaHareketleri (URUNID,ADET,PERSONEL,FIRMA,FIYAT,TOPLAM,FATURAID) VALUES (@w1,@w2,@w3,@w4,@w5,@w6,@w7)",connect.Baglan());
                cmd.Parameters.AddWithValue("@w1",txtDetayId.Text);
                cmd.Parameters.AddWithValue("@w2",txtMiktar.Text);
                cmd.Parameters.AddWithValue("@w3",lookUpPersonel.EditValue);
                cmd.Parameters.AddWithValue("@w4",lookUpFirma.EditValue);
                cmd.Parameters.AddWithValue("@w5",double.Parse(txtFiyat.Text));
                cmd.Parameters.AddWithValue("@w6",double.Parse(ToplamTutar ));
                cmd.Parameters.AddWithValue("@w7",txtFaturaID.Text);
                cmd.ExecuteNonQuery();
                connect.Baglan().Close();

                //Stoktan Adet Düş
                SqlCommand c = new SqlCommand("update Tbl_Urun set ADET = ADET - @s1 where ID=@s2",connect.Baglan());
                c.Parameters.AddWithValue("@s1", txtMiktar.Text);
                c.Parameters.AddWithValue("@s2", txtDetayId.Text);
                c.ExecuteNonQuery();
                connect.Baglan().Close();
                MessageBox.Show("Fatura ürün datay bilgisi sisteme eklenmiştir.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Listele();
                Temizle();
            }

            if (txtFaturaID.Text != "" && comboBox1.Text=="Müşteri Faturası")
            {
                string ToplamTutar = Convert.ToString(double.Parse(txtFiyat.Text) * int.Parse(txtMiktar.Text));
                SqlCommand komut = new SqlCommand("Insert into Tbl_UrunFaturasi (URUNAD , MIKTAR , FIYAT , TUTAR , FATURAID) VALUES (@p1,@p2,@p3,@p4,@p5)", connect.Baglan());
                komut.Parameters.AddWithValue("@p1", txtUrunAd.Text);
                komut.Parameters.AddWithValue("@p2", txtMiktar.Text);
                komut.Parameters.AddWithValue("@p3", double.Parse(txtFiyat.Text));
                komut.Parameters.AddWithValue("@p4", double.Parse(ToplamTutar));
                komut.Parameters.AddWithValue("@p5", txtFaturaID.Text);
                komut.ExecuteNonQuery();
                connect.Baglan().Close();

                // Hareketler Tablosuna Veri Ekleme
                SqlCommand cmd = new SqlCommand("Insert into Tbl_MusteriHareketleri (URUNID,ADET,PERSONEL,MUSTERİ,FIYAT,TOPLAM,FATURAID) VALUES (@w1,@w2,@w3,@w4,@w5,@w6,@w7)", connect.Baglan());
                cmd.Parameters.AddWithValue("@w1", txtDetayId.Text);
                cmd.Parameters.AddWithValue("@w2", txtMiktar.Text);
                cmd.Parameters.AddWithValue("@w3", lookUpPersonel.EditValue);
                cmd.Parameters.AddWithValue("@w4", lookUpMusteri.EditValue);
                cmd.Parameters.AddWithValue("@w5", double.Parse(txtFiyat.Text));
                cmd.Parameters.AddWithValue("@w6", double.Parse(ToplamTutar));
                cmd.Parameters.AddWithValue("@w7", txtFaturaID.Text);
                cmd.ExecuteNonQuery();
                connect.Baglan().Close();

                //Stoktan Adet Düş
                SqlCommand c = new SqlCommand("update Tbl_Urun set ADET = ADET - @s1 where ID=@s2", connect.Baglan());
                c.Parameters.AddWithValue("@s1", txtMiktar.Text);
                c.Parameters.AddWithValue("@s2", txtDetayId.Text);
                c.ExecuteNonQuery();
                connect.Baglan().Close();
                MessageBox.Show("Fatura ürün datay bilgisi sisteme eklenmiştir.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Listele();
                Temizle();
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtBilgiID.Text = dr["ID"].ToString();
                txtSeriNo.Text = dr["SERINO"].ToString();
                txtSiraNo.Text = dr["SIRANO"].ToString();
                mskdTarih.Text = dr["TARIH"].ToString();
                mskdSaat.Text = dr["SAAT"].ToString();
                txtAlici.Text = dr["ALICI"].ToString();
                txtTeslimAlan.Text = dr["TESLIMALAN"].ToString();
                txtTeslimEden.Text = dr["TESLIMEDEN"].ToString();
                txtVergiDairesi.Text = dr["VERGIDAIRESI"].ToString();
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from TBl_FaturaBilgisi where ID = @p1", connect.Baglan());
            komut.Parameters.AddWithValue("@p1", txtBilgiID.Text);
            komut.ExecuteNonQuery();
            connect.Baglan().Close();
            MessageBox.Show("Fatura bilgisi sistemden silinmiştir..", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Listele();
            Temizle();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update Tbl_FaturaBilgisi set SERINO=@P1 , SIRANO=@P2 , TARIH=@P3 , SAAT=@P4 , ALICI=@P5 ,TESLIMALAN=@P6,TESLIMEDEN=@P7,VERGIDAIRESI=@P8 WHERE ID=@P9", connect.Baglan());
            komut.Parameters.AddWithValue("@p1", txtSeriNo.Text);
            komut.Parameters.AddWithValue("@p2", txtSiraNo.Text);
            komut.Parameters.AddWithValue("@p3", mskdTarih.Text);
            komut.Parameters.AddWithValue("@p4", mskdSaat.Text);
            komut.Parameters.AddWithValue("@p5", txtAlici.Text);
            komut.Parameters.AddWithValue("@p6", txtTeslimEden.Text);
            komut.Parameters.AddWithValue("@p7", txtTeslimAlan.Text);
            komut.Parameters.AddWithValue("@p8", txtVergiDairesi.Text);
            komut.Parameters.AddWithValue("@p9", txtBilgiID.Text);
            komut.ExecuteNonQuery();
            connect.Baglan().Close();
            MessageBox.Show("Fatura bilgisi güncellenmiştir.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            Temizle();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FaturaUrunDetayi detay =  new FaturaUrunDetayi();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(dr != null)
            {
                detay.ID = dr["ID"].ToString();
            }
            detay.Show();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand("select URUNAD , SATISFIYAT from Tbl_Urun where ID = @p1",connect.Baglan());
            sqlCommand.Parameters.AddWithValue("@p1", txtDetayId.Text);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            while(dr.Read())
            {
                txtUrunAd.Text = dr[0].ToString();
                txtFiyat.Text = dr[1].ToString();
            }
            connect.Baglan().Close();
        }
    }
}
