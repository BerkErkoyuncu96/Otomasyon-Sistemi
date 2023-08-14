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
    public partial class FirmaForm : Form
    {
        public FirmaForm()
        {
            InitializeComponent();
        }

        sqlBaglantisi connect = new sqlBaglantisi();

        public void Listele()
        {
            DataTable dt=new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from Tbl_Firmalar",connect.Baglan());
            adapter.Fill(dt);
            gridControl1.DataSource = dt;
        }

        public void Temizle()
        {
            txtAd.Clear();
            txtAdSoyad.Clear();
            mskdTc.Clear();
            txtID.Clear();
            txtDepartman.Clear();
            mskdTelefon.Clear();
            mskdTelefon2.Clear();
            mskdTelefon3.Clear();
            txtMail.Clear();
            txtFax.Clear();
            cmbIl.Clear();
            cmbIlce.Clear();
            rchAdres.Clear();
            txtVergiDairesi.Clear();
            txtSektor.Clear();
            txtOzelKod.Clear();
            txtOzelKod2.Clear();
            txtOzelKod3.Clear();
        }

        public void CariKodAciklaması()
        {

            SqlCommand komut =new SqlCommand("select OZELKOD from Tbl_OzelKodlar", connect.Baglan());
            SqlDataReader rdr = komut.ExecuteReader();
            while (rdr.Read())
            {
                rchOzelKod.Text = rdr[0].ToString();
            }
            connect.Baglan().Close();
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

        private void FirmaForm_Load(object sender, EventArgs e)
        {
            CariKodAciklaması();
            Listele();
            SehirListesi();
            Temizle();  
        }

        private void cmbIl_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbIlce.Properties.Items.Clear();
            SqlCommand komut = new SqlCommand("select ilce from Tbl_Ilceler where sehir = @p1", connect.Baglan());
            komut.Parameters.AddWithValue("@p1", cmbIl.SelectedIndex + 1);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                cmbIlce.Properties.Items.Add(reader[0]);
            }
            connect.Baglan().Close();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtID.Text = dr["ID"].ToString();
                txtAd.Text= dr["AD"].ToString();
                mskdTc.Text = dr["YETKILITC"].ToString();
                txtAdSoyad.Text = dr["YETKILIADSOYAD"].ToString();
                txtDepartman.Text = dr["YETKILIDEPARTMAN"].ToString();
                mskdTelefon.Text = dr["TELEFON"].ToString();
                mskdTelefon2.Text = dr["TELEFON2"].ToString();
                mskdTelefon3.Text = dr["TELEFON3"].ToString();
                txtMail.Text = dr["MAIL"].ToString();
                txtFax.Text = dr["FAX"].ToString();
                cmbIl.Text = dr["IL"].ToString();
                cmbIlce.Text = dr["ILCE"].ToString();
                rchAdres.Text = dr["ADRES"].ToString();
                txtVergiDairesi.Text = dr["VERGIDAIRESI"].ToString();
                txtSektor.Text = dr["SEKTOR"].ToString();
                txtOzelKod.Text = dr["OZELKOD"].ToString();
                txtOzelKod2.Text = dr["OZELKOD2"].ToString();
                txtOzelKod3.Text = dr["OZELKOD3"].ToString();
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Firmalar (AD,YETKILITC,YETKILIADSOYAD,YETKILIDEPARTMAN,TELEFON,TELEFON2,TELEFON3,MAIL,FAX,IL,ILCE,ADRES,VERGIDAIRESI,SEKTOR,OZELKOD,OZELKOD2,OZELKOD3) VALUES (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17)",connect.Baglan());


            komut.Parameters.AddWithValue("@p1",txtAd.Text);
            komut.Parameters.AddWithValue("@p2",mskdTc.Text);
            komut.Parameters.AddWithValue("@p3",txtAdSoyad.Text);
            komut.Parameters.AddWithValue("@p4",txtDepartman.Text);
            komut.Parameters.AddWithValue("@p5",mskdTelefon.Text);
            komut.Parameters.AddWithValue("@p6",mskdTelefon2.Text);
            komut.Parameters.AddWithValue("@p7",mskdTelefon3.Text);
            komut.Parameters.AddWithValue("@p8",txtMail.Text);
            komut.Parameters.AddWithValue("@p9",txtFax.Text);
            komut.Parameters.AddWithValue("@p10",cmbIl.Text);
            komut.Parameters.AddWithValue("@p11",cmbIlce.Text);
            komut.Parameters.AddWithValue("@p12",rchAdres.Text);
            komut.Parameters.AddWithValue("@p13",txtVergiDairesi.Text);
            komut.Parameters.AddWithValue("@p14",txtSektor.Text);
            komut.Parameters.AddWithValue("@p15",txtOzelKod.Text);
            komut.Parameters.AddWithValue("@p16",txtOzelKod2.Text);
            komut.Parameters.AddWithValue("@p17",txtOzelKod3.Text);
            komut.ExecuteNonQuery();
            connect.Baglan().Close();
            MessageBox.Show("Firma sisteme eklenmiştir.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            Temizle();


        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand  komut = new SqlCommand("Delete from Tbl_Firmalar where ID=@p1",connect.Baglan());
            komut.Parameters.AddWithValue("@p1", txtID.Text);
            komut.ExecuteNonQuery();
            MessageBox.Show("Firma bilgisi sistemden silinmiştir.","Bilgilendirme",MessageBoxButtons.OK,MessageBoxIcon.Information);
            Listele();
            Temizle();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update Tbl_Firmalar set AD=@P1,YETKILITC=@P2,YETKILIADSOYAD=@P3,YETKILIDEPARTMAN=@P4,TELEFON=@P5,TELEFON2=@P6,TELEFON3=@P7,MAIL=@P8,FAX=@P9,IL=@P10,ILCE=@P11,ADRES=@P12,VERGIDAIRESI=@P13,SEKTOR=@P14,OZELKOD=@P15,OZELKOD2=@P16,OZELKOD3=@P17 WHERE ID=@P18", connect.Baglan());
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", mskdTc.Text);
            komut.Parameters.AddWithValue("@p3", txtAdSoyad.Text);
            komut.Parameters.AddWithValue("@p4", txtDepartman.Text);
            komut.Parameters.AddWithValue("@p5", mskdTelefon.Text);
            komut.Parameters.AddWithValue("@p6", mskdTelefon2.Text);
            komut.Parameters.AddWithValue("@p7", mskdTelefon3.Text);
            komut.Parameters.AddWithValue("@p8", txtMail.Text);
            komut.Parameters.AddWithValue("@p9", txtFax.Text);
            komut.Parameters.AddWithValue("@p10", cmbIl.Text);
            komut.Parameters.AddWithValue("@p11", cmbIlce.Text);
            komut.Parameters.AddWithValue("@p12", rchAdres.Text);
            komut.Parameters.AddWithValue("@p13", txtVergiDairesi.Text);
            komut.Parameters.AddWithValue("@p14", txtSektor.Text);
            komut.Parameters.AddWithValue("@p15", txtOzelKod.Text);
            komut.Parameters.AddWithValue("@p16", txtOzelKod2.Text);
            komut.Parameters.AddWithValue("@p17", txtOzelKod3.Text);
            komut.Parameters.AddWithValue("@p18",txtID.Text);
            komut.ExecuteNonQuery();
            connect.Baglan().Close();
            MessageBox.Show("Firma bilgisi güncellenmiştir.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            Temizle();
        }
    }
}
