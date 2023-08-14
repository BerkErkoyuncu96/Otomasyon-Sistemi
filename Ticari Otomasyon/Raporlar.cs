using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ticari_Otomasyon
{
    public partial class Raporlar : Form
    {
        public Raporlar()
        {
            InitializeComponent();
        }

        private void Raporlar_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'Dbo_TicariOtomasyonDataSet3.Tbl_Personeller' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.Tbl_PersonellerTableAdapter.Fill(this.Dbo_TicariOtomasyonDataSet3.Tbl_Personeller);
            // TODO: Bu kod satırı 'Dbo_TicariOtomasyonDataSet2.Tbl_Giderler' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.Tbl_GiderlerTableAdapter.Fill(this.Dbo_TicariOtomasyonDataSet2.Tbl_Giderler);
            // TODO: Bu kod satırı 'Dbo_TicariOtomasyonDataSet1.Tbl_Musteriler' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.Tbl_MusterilerTableAdapter.Fill(this.Dbo_TicariOtomasyonDataSet1.Tbl_Musteriler);
            // TODO: Bu kod satırı 'Dbo_TicariOtomasyonDataSet.Tbl_Firmalar' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.Tbl_FirmalarTableAdapter.Fill(this.Dbo_TicariOtomasyonDataSet.Tbl_Firmalar);

            this.reportViewer2.RefreshReport();
            this.reportViewer3.RefreshReport();
            this.reportViewer4.RefreshReport();
            this.reportViewer5.RefreshReport();
        }
    }
}
