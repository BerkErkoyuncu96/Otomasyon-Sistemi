using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticari_Otomasyon
{
    internal class sqlBaglantisi
    {

        public SqlConnection Baglan()
        {
            SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-D1NE1FK\SQLEXPRESS;Initial Catalog=Dbo_TicariOtomasyon;Integrated Security=True");
           baglanti.Open();
            return baglanti;
        }
    }
}
