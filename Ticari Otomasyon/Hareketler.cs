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
    public partial class Hareketler : Form
    {
        public Hareketler()
        {
            InitializeComponent();
        }

        sqlBaglantisi connect = new sqlBaglantisi();

        public void MusteriHareketleri()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("execute MusteriHareketleri",connect.Baglan());
            da.Fill(dataTable);
            gridControl1.DataSource = dataTable;
        }

        public void FirmaHareketleri()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("execute FirmaHareketleri", connect.Baglan());
            da.Fill(dataTable);
            gridControl3.DataSource = dataTable;
        }

        private void Hareketler_Load(object sender, EventArgs e)
        {
            MusteriHareketleri();
            FirmaHareketleri();
        }
    }
}
