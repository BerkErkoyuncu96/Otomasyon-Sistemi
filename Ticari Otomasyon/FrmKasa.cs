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
using DevExpress.Charts;
namespace Ticari_Otomasyon
{
    public partial class FrmKasa : Form
    {
        public FrmKasa()
        {
            InitializeComponent();
        }

        sqlBaglantisi connect = new sqlBaglantisi();

        public void MusteriHareket()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("execute MusteriHareketleri", connect.Baglan());
            da.Fill(dt);
            gridControl1.DataSource = dt;
            connect.Baglan().Close();
        }

        public void FirmaHareket()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("execute FirmaHareketleri", connect.Baglan());
            da.Fill(dt);
            gridControl3.DataSource = dt;
            connect.Baglan().Close();
        }

        public void ToplamTutar()
        {
            SqlCommand komut1 = new SqlCommand("select sum(TUTAR) from Tbl_UrunFaturasi", connect.Baglan());
            SqlDataReader dr = komut1.ExecuteReader();
            while (dr.Read())
            {
                lblToplamTutar.Text = dr[0].ToString() + " TL";
            }
            connect.Baglan().Close();
        }

        public void FaturaGiderleri()
        {
            SqlCommand cmd = new SqlCommand("select (ELEKTRIK+SU+DOGALGAZ+INTERNET+EKSTRA) FROM Tbl_Giderler order by ID asc", connect.Baglan());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblOdemeler.Text = dr[0].ToString() + " TL";
            }
            connect.Baglan().Close();
        }

        public void PersonelMaaslari()
        {
            SqlCommand cmd = new SqlCommand("select (MAASLAR) from Tbl_Giderler order by ID asc", connect.Baglan());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblMaaslar.Text = dr[0].ToString() + " TL";
            }
            connect.Baglan().Close();
        }

        public void MusteriSayisi()
        {
            SqlCommand cmd = new SqlCommand("select count(*) from Tbl_Musteriler", connect.Baglan());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblMusteri.Text = dr[0].ToString();
            }
            connect.Baglan().Close();
        }
        public void FirmaSayisi()
        {
            SqlCommand cmd = new SqlCommand("select count(*) from Tbl_Firmalar", connect.Baglan());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblFirma.Text = dr[0].ToString();
            }
            connect.Baglan().Close();
        }

        public void SehirSayisi()
        {
            SqlCommand cmd = new SqlCommand("select count(DISTINCT(IL)) from Tbl_Firmalar", connect.Baglan());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblSehir.Text = dr[0].ToString();
            }
            connect.Baglan().Close();
        }

        public void PersonelSayisi()
        {
            SqlCommand cmd = new SqlCommand("select count(*) from Tbl_Personeller", connect.Baglan());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblPersonelSayisi.Text = dr[0].ToString();
            }
            connect.Baglan().Close();
        }

        public void StokSayisi()
        {
            SqlCommand cmd = new SqlCommand("select sum(ADET) from Tbl_Urun", connect.Baglan());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblStokSayisi.Text = dr[0].ToString();
            }
            connect.Baglan().Close();
        }

        public void Elektrik()
        {
            chartControl1.Series["Aylar"].Points.Clear();
            SqlCommand cmd = new SqlCommand("select top 4 AY , ELEKTRIK from Tbl_Giderler order by ID desc", connect.Baglan());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr[0], dr[1]));
            }
            connect.Baglan().Close();
        }

        public void Su()
        {
            chartControl1.Series["Aylar"].Points.Clear();
            SqlCommand cmd = new SqlCommand("select top 4 AY , SU from Tbl_Giderler order by ID desc", connect.Baglan());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr[0], dr[1]));
            }
            connect.Baglan().Close();
        }

        public void Dogalgaz()
        {
            chartControl1.Series["Aylar"].Points.Clear();
            SqlCommand cmd = new SqlCommand("select top 4 AY , DOGALGAZ from Tbl_Giderler order by ID desc", connect.Baglan());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr[0], dr[1]));
            }
            connect.Baglan().Close();
        }
        public void Internet()
        {
            chartControl1.Series["Aylar"].Points.Clear();
            SqlCommand cmd = new SqlCommand("select top 4 AY , INTERNET from Tbl_Giderler order by ID desc", connect.Baglan());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr[0], dr[1]));
            }
            connect.Baglan().Close();
        }

        public void Ekstra()
        {
            chartControl1.Series["Aylar"].Points.Clear();
            SqlCommand cmd = new SqlCommand("select top 4 AY , EKSTRA from Tbl_Giderler order by ID desc", connect.Baglan());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr[0], dr[1]));
            }
            connect.Baglan().Close();
        }

        public void Maaslar()
        {
            SqlCommand cmd = new SqlCommand("select top 4 AY , MAASLAR from Tbl_Giderler order by ID desc", connect.Baglan());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr[0], dr[1]));
            }
            connect.Baglan().Close();
        }
        public void Listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Giderler", connect.Baglan());
            da.Fill(dt);
            gridControl2.DataSource = dt;
            connect.Baglan().Close();
        }

        public string Ad;
        private void FrmKasa_Load(object sender, EventArgs e)
        {
            lblAktifKullanici.Text = Ad;
            Listele();
            MusteriHareket();
            FirmaHareket();
            ToplamTutar();
            FaturaGiderleri();
            PersonelMaaslari();
            MusteriSayisi();
            FirmaSayisi();
            SehirSayisi();
            PersonelSayisi();
            StokSayisi();
            groupControl11.Text = "Personel Maaşları";
            Maaslar();
        }

        public int sayac = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac++;

            if (0 < sayac && sayac <= 5)
            {
                groupControl10.Text = "Elektrik Giderleri";
                Elektrik();
            }
            if (5 < sayac && sayac <= 10)
            {
                groupControl10.Text = "Su Giderleri";
                Su();
            }
            if (10 < sayac && sayac <= 15)
            {
                groupControl10.Text = "Doğalgaz Giderleri";
                Dogalgaz();
            }
            if (15 < sayac && sayac <= 20)
            {
                groupControl10.Text = "İnternet Giderleri";
                Internet();
            }
            if (20 < sayac && sayac <= 25)
            {
                groupControl10.Text = "Ekstra Giderler";
                Ekstra();
            }
            if (sayac == 26)
            {
                sayac = 0;
            }

        }
    }
}
