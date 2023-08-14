using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Ticari_Otomasyon
{
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Form1()
        {
            InitializeComponent();
        }
        FrmAnaSayfa ana;
        private void Form1_Load(object sender, EventArgs e)
        {
            if (ana == null || ana.IsDisposed)
            {
                ana = new FrmAnaSayfa();
                ana.MdiParent = this;
                ana.Show();
            }
        }
        FrmUrunler fr;
        private void btnUrunler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        if (fr == null || fr.IsDisposed) 
            {
                fr = new FrmUrunler();
                fr.MdiParent = this;
                fr.Show();
            }

        }

        FrmMusteriler frm;
        private void btnMusteriler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(frm == null || frm.IsDisposed)
            {
                frm = new FrmMusteriler();
                frm.MdiParent = this;
                frm.Show();
            }
        }
        FirmaForm FRMF;
        private void btnFirmalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if(fr == null || fr.IsDisposed)
            {
                FRMF = new FirmaForm();
                FRMF.MdiParent = this;
                FRMF.Show();
            }
        }
        PERSONLLER personel;
        private void btnPersoneller_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if(personel == null || personel.IsDisposed)
            {
                personel = new PERSONLLER();
                personel.MdiParent = this;
                personel.Show();
            }

        }

        Rehber rehber;
        private void btnRehber_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(rehber == null || rehber.IsDisposed)
            {
                rehber = new Rehber();  
                rehber.MdiParent = this;
                rehber.Show();
            }
        }

        FrmGiderler frmGiderler;
        private void btnGiderler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(frmGiderler == null || frmGiderler.IsDisposed)
            {
                frmGiderler = new FrmGiderler();
                frmGiderler.MdiParent = this;
                frmGiderler.Show(); 
            }
        }
        FrmBankalar bank;
        private void btnBankalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(bank == null || bank.IsDisposed)
            {
                bank = new FrmBankalar();
                bank.MdiParent = this;
                bank.Show();
            }
        }

        Faturalar fat;
        private void btnFaturalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(fat == null || fat.IsDisposed)
            {
                fat = new Faturalar();
                fat.MdiParent = this;
                    fat.Show();
            }
        }

        FrmNotlar not;
        private void btnNotlar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(not == null || not.IsDisposed)
            {
                not = new FrmNotlar();
                not.MdiParent = this;
                not.Show();
            }
           

        }
        Hareketler frmh;
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(frmh == null || frmh.IsDisposed)
            {
                frmh= new Hareketler();
                frmh.MdiParent = this;
                frmh.Show();
            }
        }

        Raporlar rp;
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (rp == null || rp.IsDisposed)
            {
                rp = new Raporlar();
                rp.MdiParent = this;
                rp.Show();
            }
        }

        Stoklar stok;
        private void btnStoklar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(stok == null || stok.IsDisposed)
            {
                stok = new Stoklar();
                stok.MdiParent = this;
                stok.Show();
            }
        }

        FrmAyarlar ayar;
        private void btnAyarlar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ayar = new FrmAyarlar();
            ayar.Show();
        }

        FrmKasa kasa;
        public string kullanici;
        private void btnKasa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if(kasa == null || kasa.IsDisposed)
            {
                kasa = new FrmKasa();
                kasa.Ad = kullanici;
                kasa.MdiParent = this;
                kasa.Show();
            }
        }


        private void btnAnasayfa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ana == null || ana.IsDisposed)
            {
                ana = new FrmAnaSayfa(); 
                ana.MdiParent= this;
                ana.Show();
            }
        }
    }
}
