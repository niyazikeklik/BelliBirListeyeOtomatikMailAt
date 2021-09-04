using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;

namespace mailgönder {
   public class MailGonder {
        static System.Net.NetworkCredential cred;
        System.Net.Mail.SmtpClient smtp;
        System.Net.Mail.MailAddress from;
        public MailAddress From { get { return from; } set { from = value; } }
        public SmtpClient Smtp { get { return smtp; } set { smtp = value; } }
        public System.Net.NetworkCredential Cred { get { return cred; } set { cred = value; } }
        public bool Send(string MailAdresi) {
            try {
              
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                mail.From = from;
                mail.Subject = "Staj Başvurusu";
                mail.IsBodyHtml = true;
                mail.Body =  "";
                mail.Attachments.Clear();
                mail.Attachments.Add(new Attachment("NiyaziKeklikCV.pdf"));
                mail.Attachments.Add(new Attachment("TranscriptReport.pdf"));
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                smtp.Credentials = cred;
                mail.To.Clear();
                mail.To.Add(MailAdresi);
                smtp.Send(mail);
                return true;
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    
    }
}
