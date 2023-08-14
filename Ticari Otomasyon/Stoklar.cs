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
    public partial class Stoklar : Form
    {
        public Stoklar()
        {
            InitializeComponent();
        }

        sqlBaglantisi connect = new sqlBaglantisi();

        public void Listele()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select URUNAD ,  sum(ADET) as 'Toplam Ürün Sayısı' from Tbl_Urun Group by URUNAD ",connect.Baglan());
            da.Fill(dataTable);
            gridControl1.DataSource = dataTable;
        }

        public void IllerveDagilimi() 
        {
            SqlCommand komut = new SqlCommand("select IL , count(*) from Tbl_Firmalar group by IL",connect.Baglan());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                chartControl2.Series["Series 1"].Points.AddPoint(Convert.ToString(dr[0]), int.Parse(dr[1].ToString()));
            }
            connect.Baglan().Close();
        
        }
        public void StokDagilimi()
        {
            SqlCommand komut = new SqlCommand("select URUNAD ,  sum(ADET) as 'Toplam Ürün Sayısı' from Tbl_Urun Group by URUNAD", connect.Baglan());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                chartControl3.Series["Series 1"].Points.AddPoint(Convert.ToString(dr[0]), int.Parse(dr[1].ToString()));
            }
            connect.Baglan().Close();
        }


        private void Stoklar_Load(object sender, EventArgs e)
        {
            Listele();
            IllerveDagilimi();
            StokDagilimi();
        }

        
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmStokDetaylari fr = new FrmStokDetaylari();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                fr.Ad = dr["URUNAD"].ToString();
            }
            fr.Show();
        }
    }
}
