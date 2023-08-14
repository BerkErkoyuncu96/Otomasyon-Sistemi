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
    public partial class Rehber : Form
    {
        public Rehber()
        {
            InitializeComponent();
        }

        sqlBaglantisi connect= new sqlBaglantisi();

        public void FirmaIletisim()
        {
            DataTable dt= new DataTable();
            SqlDataAdapter da= new SqlDataAdapter("select AD , YETKILIADSOYAD , TELEFON , TELEFON2, TELEFON3,MAIL,FAX FROM Tbl_Firmalar",connect.Baglan());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        public void musteriIletisim()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT AD , SOYAD ,TELEFON , TELEFON2 FROM Tbl_Musteriler ", connect.Baglan());
            da.Fill(dt);
            gridControl2.DataSource = dt;
        }
        private void Rehber_Load(object sender, EventArgs e)
        {
            FirmaIletisim();
            musteriIletisim();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
           SendMail mail= new SendMail();
           DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(dr != null ) { 
            mail.mailAdress = dr["MAIL"].ToString();
                mail.Show();
            }
            
        }
    }
}
