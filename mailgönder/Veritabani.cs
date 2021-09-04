using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Text;

namespace mailgönder {
   public class Veritabani {
       private static OleDbConnection con;
        public Veritabani() {
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=Database11.accdb");
            con.Open();
        }
       
        public bool insert(string mail) {
            try {
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = con;
                cmd.CommandText = "insert into Tablo1 (mail) values ('" + mail + "')";
                cmd.ExecuteNonQuery();
                return true;
            }
            catch(Exception) {
                return false;
            }

        }
        public bool varmi( string mail) {
            OleDbCommand cmd = new OleDbCommand("Select COUNT(*) from Tablo1 where mail =@maill ", con);
            cmd.Parameters.Add("@maill", mail);
            int count = (int) cmd.ExecuteScalar();
            if(count > 0) return true;
            else return false;
        }
    }
}
