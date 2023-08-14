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
    public partial class FrmBankalar : Form
    {
        public FrmBankalar()
        {
            InitializeComponent();
        }
        sqlBaglantisi connect = new sqlBaglantisi();

        public void Listele()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("execute BankaBilgileri", connect.Baglan());
            dataAdapter.Fill(dataTable);
            gridControl1.DataSource = dataTable;
            connect.Baglan().Close();
        }

        public void Temizle()
        {
            txtID.Clear();
            txtIBAN.Clear();
            txtHesapTuru.Clear();
            lookUpEdit1.Clear();
            txtBanka.Clear();
            txtSube.Clear();
            txtHesapNo.Clear();
            txtYetkili.Clear();
            mskdTarih.Clear();
            mskdTel.Clear();
            cmbIl.Clear();
            cmbIlce.Clear();
        }

        public void SehirListesi()
        {
            SqlCommand sehirler = new SqlCommand("select SEHIR from Tbl_Iller", connect.Baglan());
            SqlDataReader dr = sehirler.ExecuteReader();
            while (dr.Read())
            {
                cmbIl.Properties.Items.Add(dr[0]);
            }
            connect.Baglan().Close();
        }

        public void FirmaListesi()
        {
            DataTable dt= new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select ID,AD from Tbl_Firmalar",connect.Baglan());
            da.Fill(dt);
            lookUpEdit1.Properties.ValueMember = "ID";
            lookUpEdit1.Properties.DisplayMember = "AD"; 
            lookUpEdit1.Properties.DataSource = dt;
        }


        private void FrmBankalar_Load(object sender, EventArgs e)
        {

            Listele();
            SehirListesi();
            FirmaListesi();
            Temizle();
        }


        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Insert into Tbl_Bankalar (BANKA , IL ,ILCE ,SUBE ,TELEFON ,IBAN,HESAPNO ,YETKILI , TARIH,HESAPTURU ,FIRMAID) VALUES (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11) ",connect.Baglan());
            komut.Parameters.AddWithValue("@p1",txtBanka.Text);
            komut.Parameters.AddWithValue("@p2",cmbIl.Text);
            komut.Parameters.AddWithValue("@p3",cmbIlce.Text);
            komut.Parameters.AddWithValue("@p4",txtSube.Text);
            komut.Parameters.AddWithValue("@p5",mskdTel.Text);
            komut.Parameters.AddWithValue("@p6",txtIBAN.Text);
            komut.Parameters.AddWithValue("@p7",txtHesapNo.Text);
            komut.Parameters.AddWithValue("@p8",txtYetkili.Text);
            komut.Parameters.AddWithValue("@p9",mskdTarih.Text);
            komut.Parameters.AddWithValue("@p10",txtHesapTuru.Text);
            komut.Parameters.AddWithValue("@p11",lookUpEdit1.EditValue);
            komut.ExecuteNonQuery();
            MessageBox.Show("Banka bilgisi sisteme kaydedildi.","Bilgilendirme",MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            Temizle();
            connect.Baglan().Close();
        }

        private void cmbIl_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbIlce.Properties.Items.Clear();
            SqlCommand komut = new SqlCommand("select ilce from Tbl_Ilceler where sehir = @p1", connect.Baglan());
            komut.Parameters.AddWithValue("@p1", cmbIl.SelectedIndex + 1);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbIlce.Properties.Items.Add(dr[0]);
            }
            connect.Baglan().Close();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtID.Text = dr["ID"].ToString();
                txtBanka.Text = dr["BANKA"].ToString();
                cmbIl.Text = dr["IL"].ToString();
                cmbIlce.Text = dr["ILCE"].ToString();
                txtSube.Text = dr["SUBE"].ToString();
                mskdTel.Text = dr["TELEFON"].ToString();
                txtIBAN.Text = dr["IBAN"].ToString();
                txtHesapNo.Text = dr["HESAPNO"].ToString();
                txtYetkili.Text = dr["YETKILI"].ToString();
                mskdTarih.Text = dr["TARIH"].ToString();
                txtHesapTuru.Text = dr["HESAPTURU"].ToString();
                lookUpEdit1.Text = dr["AD"].ToString();
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from Tbl_Bankalar where ID=@p1", connect.Baglan());
            komut.Parameters.AddWithValue("@p1", txtID.Text);
            komut.ExecuteNonQuery();
            MessageBox.Show("Banka bilgileri silinmiştir.","Bilgilendirme",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            Listele();
            Temizle();
            connect.Baglan().Close();

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update Tbl_Bankalar set BANKA=@P1 , IL=@P2,ILCE=@P3,SUBE=@P4,TELEFON=@P5,IBAN=@P6,HESAPNO=@P7,YETKILI=@P8,TARIH=@P9 , HESAPTURU=@P10,FIRMAID=@P11 WHERE ID=@P12",connect.Baglan());
            komut.Parameters.AddWithValue("@p1", txtBanka.Text);
            komut.Parameters.AddWithValue("@p2", cmbIl.Text);
            komut.Parameters.AddWithValue("@p3", cmbIlce.Text);
            komut.Parameters.AddWithValue("@p4", txtSube.Text);
            komut.Parameters.AddWithValue("@p5", mskdTel.Text);
            komut.Parameters.AddWithValue("@p6", txtIBAN.Text);
            komut.Parameters.AddWithValue("@p7", txtHesapNo.Text);
            komut.Parameters.AddWithValue("@p8", txtYetkili.Text);
            komut.Parameters.AddWithValue("@p9", mskdTarih.Text);
            komut.Parameters.AddWithValue("@p10", txtHesapTuru.Text);
            komut.Parameters.AddWithValue("@p11", lookUpEdit1.EditValue);
            komut.Parameters.AddWithValue("@P12", txtID.Text);
            komut.ExecuteNonQuery();
            MessageBox.Show("Banka bilgisi güncellendi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            Temizle();
            connect.Baglan().Close();
        }
    }
}
