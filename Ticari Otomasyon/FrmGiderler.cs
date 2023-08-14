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
    public partial class FrmGiderler : Form
    {
        public FrmGiderler()
        {
            InitializeComponent();
        }

        sqlBaglantisi connect = new sqlBaglantisi();

        public void Temizle()
        {
            txtID.Clear();
            cmbAy.Clear();
            cmbYıl.Clear();
            txtElektrik.Clear();
            txtSu.Clear();
            txtDogalgaz.Clear();
            txtInternet.Clear();
            txtMaaslar.Clear();
            txtExtra.Clear();
            rchExtra.Clear();
        }


        public void Listele()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from Tbl_Giderler", connect.Baglan());
            adapter.Fill(dataTable);
            gridControl1.DataSource = dataTable;
        }
        private void FrmGiderler_Load(object sender, EventArgs e)
        {

            Listele();
            Temizle();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Giderler (AY,YIL,ELEKTRIK,SU,DOGALGAZ,INTERNET,MAASLAR,EKSTRA,EKSTRAHARCAMALAR) VALUES (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", connect.Baglan());
            komut.Parameters.AddWithValue("@p1", cmbAy.Text);
            komut.Parameters.AddWithValue("@p2", cmbYıl.Text);
            komut.Parameters.AddWithValue("@p3", decimal.Parse(txtElektrik.Text));
            komut.Parameters.AddWithValue("@p4", decimal.Parse(txtSu.Text));
            komut.Parameters.AddWithValue("@p5", decimal.Parse(txtDogalgaz.Text));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(txtInternet.Text));
            komut.Parameters.AddWithValue("@p7", decimal.Parse(txtMaaslar.Text));
            komut.Parameters.AddWithValue("@p8", decimal.Parse(txtExtra.Text));
            komut.Parameters.AddWithValue("@p9", rchExtra.Text);
            komut.ExecuteNonQuery();
            MessageBox.Show("Gider bilgisi sisteme eklenmiştir.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            Temizle();
            connect.Baglan().Close();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtID.Text = dr["ID"].ToString();
                cmbAy.Text = dr["AY"].ToString();
                cmbYıl.Text = dr["YIL"].ToString();
                txtElektrik.Text = dr["ELEKTRIK"].ToString();
                txtSu.Text = dr["SU"].ToString();
                txtDogalgaz.Text = dr["DOGALGAZ"].ToString();
                txtInternet.Text = dr["INTERNET"].ToString();
                txtMaaslar.Text = dr["MAASLAR"].ToString();
                txtExtra.Text = dr["EKSTRA"].ToString();
                rchExtra.Text = dr["EKSTRAHARCAMALAR"].ToString();
                {

                }
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand("delete from Tbl_Giderler where ID=@p1",connect.Baglan());
            sqlCommand.Parameters.AddWithValue("@p1", txtID.Text);
            sqlCommand.ExecuteNonQuery();
            MessageBox.Show("Gider bilglsl silinmiştir.", "Bilgi", MessageBoxButtons.OK,MessageBoxIcon.Information);
            connect.Baglan().Close();
            Temizle();
            Listele();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update Tbl_Giderler set AY=@p1,YIL=@p2,ELEKTRIK=@p3,SU=@p4,DOGALGAZ=@p5,INTERNET=@p6,MAASLAR=@p7,EKSTRA=@p8,EKSTRAHARCAMALAR=@p9 where ID=@p10", connect.Baglan());
            komut.Parameters.AddWithValue("@p1", cmbAy.Text);
            komut.Parameters.AddWithValue("@p2", cmbYıl.Text);
            komut.Parameters.AddWithValue("@p3", decimal.Parse(txtElektrik.Text));
            komut.Parameters.AddWithValue("@p4", decimal.Parse(txtSu.Text));
            komut.Parameters.AddWithValue("@p5", decimal.Parse(txtDogalgaz.Text));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(txtInternet.Text));
            komut.Parameters.AddWithValue("@p7", decimal.Parse(txtMaaslar.Text));
            komut.Parameters.AddWithValue("@p8", decimal.Parse(txtExtra.Text));
            komut.Parameters.AddWithValue("@p9", rchExtra.Text);
            komut.Parameters.AddWithValue("@p10", txtID.Text);
            komut.ExecuteNonQuery();
            MessageBox.Show("Gider bilgisi güncellenmiştir.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            Temizle();
            connect.Baglan().Close();
        }
    }
}
