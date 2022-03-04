using System;
using System.Net.Mail;
using System.IO;  
using System.Net;  
using System.Text;

namespace BusinessLayer.Util
{
    public class SendMail
    {
        public void SendMailForService(string userName, string employeeMail, string ServiceName, string DemandName)
        {
            string to = employeeMail; //To address    
            string from = "companypanel@gmail.com"; //From address    
            MailMessage message = new MailMessage(from, to);

            string mailbody = "Merhaba " + userName + " vermekte olduğunuz " + ServiceName +
                              " hizmetine yeni bir talep atanmıştır." + DemandName +
                              " başlıklı talebin yeni hizmet kategorisi " + ServiceName +
                              "'dir. Talebi sistemdeki 'taleplerim' bölümünden görüntüleyebilir, düzenleyebilir ya da yanıtlayabilirsiniz.";
            message.Subject = ServiceName + " Hizmetine Yeni Talep Eklendi.";
            message.Body = mailbody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new
                System.Net.NetworkCredential("companypanel15@gmail.com", "123456Admin.");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;

            client.Send(message);
        }
        
        public void SendMailForEmployeeAnswerReminder(string userName, string employeeMail, string DemandName)
        {
            string to = employeeMail; //To address    
            string from = "companypanel@gmail.com"; //From address    
            MailMessage message = new MailMessage(from, to);

            string mailbody = "Merhaba " + userName + " 5 gün önce size atanmış olan " + DemandName + " isimli talebi henüz yanıtlamadınız. Lütfen talebe gereken süre içinde yanıt veriniz.";
            message.Subject = "3 gün önce size atanmış olan " + DemandName + " isimli talebi yanıtmalayı unutmayın";
            message.Body = mailbody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new
                System.Net.NetworkCredential("companypanel15@gmail.com", "123456Admin.");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;

            client.Send(message);
        }
        
        public void SendMailForDepartmentNotAnswered(string userName, string employeeMail, string DemandName)
        {
            string to = employeeMail; //To address    
            string from = "companypanel@gmail.com"; //From address    
            MailMessage message = new MailMessage(from, to);

            string mailbody = "Merhaba " + userName + " 5 gün önce bölümünze atanmış olan " + DemandName + " isimli talebi henüz yanıtlamadınız. Lütfen talebe gereken süre içinde yanıt veriniz.";
            message.Subject = "3 gün önce size atanmış olan " + DemandName + " isimli talebi yanıtmalayı unutmayın";
            message.Body = mailbody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new
                System.Net.NetworkCredential("companypanel15@gmail.com", "123456Admin.");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;

            client.Send(message);
        }
        
        public void SendMailForDepartment(string userName, string employeeMail, string DepartmanName, string DemandName)
        {
            string to = employeeMail; //To address    
            string from = "companypanel@gmail.com"; //From address    
            MailMessage message = new MailMessage(from, to);

            string mailbody = "Merhaba " + userName + " bakmakta olduğunuz " + DepartmanName +
                              " bölümüne yeni bir talep atanmıştır." + DemandName +
                              " başlıklı talebin yeni bölüm kategorisi " + DepartmanName +
                              "'dir. Talebi sistemdeki 'taleplerim' bölümünden görüntüleyebilir, düzenleyebilir ya da yanıtlayabilirsiniz.";
            message.Subject = DepartmanName + " Bölümüne Yeni Talep Eklendi.";
            message.Body = mailbody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new
                System.Net.NetworkCredential("companypanel15@gmail.com", "123456Admin.");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;

            client.Send(message);
        }

        public void SendMailForEmployee(string userName, string employeeMail, string DemandName)
        {
            string to = employeeMail; //To address    
            string from = "companypanel15@gmail.com"; //From address    
            MailMessage message = new MailMessage(from, to);

            string mailbody = "Merhaba " + userName + "." + DemandName +
                              " başlıklı talep size atanmıştır. Bu talebi yanıtlamak için taleplerim bölümünde, bana atanan talepler kısmından yanıt verebilirsiniz.";
            message.Subject = "Size Yeni Bir Talep Atandı";
            message.Body = mailbody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new
                System.Net.NetworkCredential("companypanel15@gmail.com", "123456Admin.");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;

            client.Send(message);
        }
        
        public bool SendEmailForConfirmMail(string userEmail, string confirmationLink)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("companypanel15@gmail.com");
            mailMessage.To.Add(new MailAddress(userEmail));
 
            mailMessage.Subject = "Üyeliğiniz oluşturulmuştur, lütfen maili doğrulayınız";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = "Üyeliğiniz başarıyla oluşturulmuştur, lütfen kaydınızı tamamlamak için <a href="+confirmationLink+">tıklayınız</a>";
           
 
            
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new
                System.Net.NetworkCredential("companypanel15@gmail.com", "123456Admin.");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
 
            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                // log exception
            }
            return false;
        }
        
        public bool SendResetPassword(string userEmail, string confirmationLink)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("companypanel15@gmail.com");
            mailMessage.To.Add(new MailAddress(userEmail));
 
            mailMessage.Subject = "CompanyPanel Şifre Sıfırlama";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = "Şifre sıfırlama isteğiniz alınmıştır, şifrenizi sıfırlamak için <a href="+confirmationLink+">tıklayınız</a>";
           
 
            
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new
                System.Net.NetworkCredential("companypanel15@gmail.com", "123456Admin.");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
 
            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                // log exception
            }
            return false;
        }

    }

}
