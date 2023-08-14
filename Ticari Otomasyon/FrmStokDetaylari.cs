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
    public partial class FrmStokDetaylari : Form
    {
        public FrmStokDetaylari()
        {
            InitializeComponent();
        }

        sqlBaglantisi connect = new sqlBaglantisi();

        public string Ad;

        public void Listele() 
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Urun where URUNAD = '" +Ad +"'",connect.Baglan());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void FrmStokDetaylari_Load(object sender, EventArgs e)
        {
            Listele();
        }
    }
}
