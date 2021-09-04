using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mailgönder {
    public partial class Form1 : Form {
        public Form1() {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }
        Veritabani veri = new Veritabani();
        MailGonder mail = new MailGonder();
        //  
        //string[] mailler = "odevimp@gmail.com niyazikeklik@gmail.com".Split(' ');
        string[] mailler = kaynakMail.Mailller.Split(' ');
        int countBasarili = 0;
        int countZatenGonderildi = 0;
        int countBasarisiz = 0;

        public void gonder() {
            for(int i = 0; i < mailler.Length; i++) {
                if(!veri.varmi(mailler[i]) || mailler[i] == "odevimp@gmail.com" || mailler[i] == "niyazikeklik@gmail.com") {
                    if(mail.Send(mailler[i])) {
                        countBasarili++;
                        label1.Text = countBasarili + "/" + mailler.Length + " Başarısız: " + countBasarisiz + " Zaten Gönderilmişti: " + countZatenGonderildi;
                        progressBar1.Value += 1;
                        if(!veri.insert(mailler[i])) MessageBox.Show("Veritabanına kaydedilemedi: " + mailler[i]);
                    }
                    else {
                        MessageBox.Show("Mail Gönderilemedi: " + mailler[i]);
                        countBasarisiz++;
                        label1.Text = countBasarili + "/" + mailler.Length + " Başarısız: " + countBasarisiz + " Zaten Gönderilmişti: " + countZatenGonderildi;
                        progressBar1.Value += 1;
                    }
                }
                else {
                    countZatenGonderildi++;
                    label1.Text = countBasarili + "/" + mailler.Length + " Başarısız: " + countBasarisiz + " Zaten Gönderilmişti: " + countZatenGonderildi;
                    progressBar1.Value += 1;
                }

            }
            MessageBox.Show(countBasarili + "/" + mailler.Length + " Mail Gönderildi. \n " + countZatenGonderildi + " Maile zaten gönderilmişti.");
        }
        private void button1_Click(object sender, EventArgs e) {
              
                mail.Cred = new System.Net.NetworkCredential("niyazikeklik@outlook.com", "*******");
                mail.Smtp = new System.Net.Mail.SmtpClient("smtp-mail.outlook.com", 587);
                mail.From = new MailAddress("niyazikeklik@outlook.com");
            /*

            mail.From = new MailAddress("niyazikeklikk@gmail.com");
            mail.Cred = new System.Net.NetworkCredential("niyazikeklikk@gmail.com", "*******");
            mail.Smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);*/

            progressBar1.Maximum = mailler.Length;
            Thread baslat = new Thread(new ThreadStart(gonder));
            baslat.Start();

        }
    }
}
