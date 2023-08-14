using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
namespace Ticari_Otomasyon
{
    public partial class SendMail : Form
    {
        public SendMail()
        {
            InitializeComponent();
        }

        public string mailAdress;
        private void SendMail_Load(object sender, EventArgs e)
        {
            textEdit1.Text = mailAdress;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Credentials = new System.Net.NetworkCredential("Mail", "Şifre");
            smtpClient.Port = 587;
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.EnableSsl = true;
            message.From = new MailAddress("Mail");
            message.Subject = textEdit2.Text;
            message.Body = richTextBox1.Text;
            smtpClient.Send(message);
        }
    }
}
