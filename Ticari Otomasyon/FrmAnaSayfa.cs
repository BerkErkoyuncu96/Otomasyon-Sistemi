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
using System.Xml;

namespace Ticari_Otomasyon
{
    public partial class FrmAnaSayfa : Form
    {
        public FrmAnaSayfa()
        {
            InitializeComponent();
        }
        sqlBaglantisi connect = new sqlBaglantisi();
        public void AzStoklar()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select URUNAD , sum(ADET)  from Tbl_Urun group by URUNAD having  sum(ADET)<=20 order by sum(ADET)", connect.Baglan());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        public void Ajanda()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select top 5 TARIH , SAAT ,BASLIK  from Tbl_Notlar order by ID desc", connect.Baglan());
            da.Fill(dt);
            gridControl2.DataSource = dt;
        }

        public void Hareket()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("execute FirmaHareketleri2",connect.Baglan());
            da.Fill(dt);
            gridControl3.DataSource = dt;
        }

        public void FirmaIletisim()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select AD , TELEFON from Tbl_Firmalar", connect.Baglan());
            da.Fill(dt);
            gridControl4.DataSource = dt;
        }

        public void Haber()
        {
            XmlTextReader xml = new XmlTextReader("https://www.ntv.com.tr/son-dakika.rss");
            while (xml.Read())
            {
                if(xml.Name == "title")
                {
                    listBox1.Items.Add(xml.ReadString());
                }
            }
        }
        private void FrmAnaSayfa_Load(object sender, EventArgs e)
        {
            AzStoklar();
            Ajanda();
            Hareket();
            FirmaIletisim();
            webBrowser1.Navigate("https://www.tcmb.gov.tr/wps/wcm/connect/tr/tcmb+tr/main+page+site+area/bugun");
            webBrowser2.Navigate("https://www.google.com.tr/");
            Haber();
        }


    }
}
