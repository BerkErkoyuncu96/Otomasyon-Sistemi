using DevExpress.DataAccess.Native.DataFederation;
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
using DevExpress.ExpressApp.Model.Core;

namespace Ticari_Otomasyon
{
    public partial class FaturaUrunDetayi : Form
    {
        public FaturaUrunDetayi()
        {
            InitializeComponent();
        }
        public string ID;

        sqlBaglantisi connect = new sqlBaglantisi();

        public void Listele()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("select * from Tbl_UrunFaturasi where  FATURAID = '" + ID + "'", connect.Baglan());
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);    
            gridControl1.DataSource = dataTable;
        }

        private void FaturaUrunDetayi_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            UrunDetayIslemleri fr = new UrunDetayIslemleri();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(dr != null )
            {
                fr.ID = dr["ID"].ToString();
            }
            fr.Show();
        }
    }
}
