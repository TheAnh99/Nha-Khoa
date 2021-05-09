using btthweb.Appcode.DAL;
using btthweb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using btthweb.Models;

namespace CBTT.Appcode.BLL
{
    public class SentEmail
    {
        /// <summary>
        /// Gửi email chứa pass đã reset cho user
        /// </summary>
        /// <param name="strTo"></param>
        /// <param name="strSubject"></param>
        /// <param name="strBody"></param>
        /// <returns></returns>
        public static bool SendEmail(string strTo, string strSubject, string strBody)
        {
            try
            {
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                System.Net.Mail.SmtpClient VSmtpMail = new System.Net.Mail.SmtpClient();
                mail.From = new System.Net.Mail.MailAddress(CConfig.SMTP_FROM);
                mail.To.Add(new System.Net.Mail.MailAddress(strTo));
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.Subject = strSubject;
                mail.Body = strBody;
                mail.IsBodyHtml = true;
                VSmtpMail = new System.Net.Mail.SmtpClient();
                VSmtpMail.Host = CConfig.SMTP_SERVER;
                VSmtpMail.Port = Convert.ToInt32(CConfig.SMTP_PORT);
                VSmtpMail.Credentials = new System.Net.NetworkCredential(CConfig.SMTP_USER, CConfig.SMTP_PASS);
                VSmtpMail.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                LogFile.Error(ex.ToString());   // Ghi thông tin ra file
                return false;
            }
        }

        /// <summary>
        /// 2018-12-04 16:27:37 chiennd
        /// send email - dùng thông báo cho quản trị biết khi có khách hàng đăng tin

        /// </summary>
        /// <param name="strTo"></param>
        /// <param name="strSubject"></param>
        /// <param name="strBody"></param>
        /// <returns></returns>
        public static bool SendEmailNotification(string strTo, string strCC, string strSubject, string strBody)
        {
            try
            {
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                System.Net.Mail.SmtpClient VSmtpMail = new System.Net.Mail.SmtpClient();
                mail.From = new System.Net.Mail.MailAddress(CConfig.SMTP_FROM);
                mail.To.Add(new System.Net.Mail.MailAddress(strTo));
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.Headers.Add("Importance", "High");  // tiêu đề quan trọng
                mail.Subject = strSubject;
                mail.Body = strBody;
                mail.IsBodyHtml = true;
                if (strCC != "")
                    mail.CC.Add(strCC);
                VSmtpMail = new System.Net.Mail.SmtpClient();
                VSmtpMail.Host = CConfig.SMTP_SERVER;
                VSmtpMail.Port = Convert.ToInt32(CConfig.SMTP_PORT);
                VSmtpMail.Credentials = new System.Net.NetworkCredential(CConfig.SMTP_USER, CConfig.SMTP_PASS);
                VSmtpMail.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                LogFile.Error(ex.ToString());   // Ghi thông tin ra file
                return false;
            }
        }

        public static void SendEmail(string action, ref NewsCBTT objNews, string ExpertEmail, string CC)
        {
            // Gửi mail thông báo sự kiện
            try
            {
                string Url = "";
                string noidung = "";
                Url = objNews.URL;
                //Lấy mail gửi theo miền
                string email = CConfig.EmailHN;
                if (ExpertEmail != null || ExpertEmail != "")
                    email = ExpertEmail;

                if (action == CConfig.SuaTin)
                    noidung = "<b>Sự kiện cập nhật -</b> <a href=\"" + @CConfig.TEMPLATE_URL_HOST + objNews.URL.Replace("|", "") + "\">" + objNews.Stock_Code + ": " + objNews.Title + "</a> (" + DateTime.Now.ToString() + ")";
                if (action == CConfig.ThemTin)
                    noidung = "<b>Sự kiện thêm -</b> <a href=\"" + @CConfig.TEMPLATE_URL_HOST + objNews.URL.Replace("|", "") + "\">" + objNews.Stock_Code + ": " + objNews.Title + "</a> (" + DateTime.Now.ToString() + ")";
                if (action == CConfig.XoaTin)
                    noidung = "<b>Sự kiện xóa -</b> " + objNews.Stock_Code + ": " + objNews.Title + " (" + DateTime.Now.ToString() + ")";
                string strTemplatePath = System.Web.Hosting.HostingEnvironment.MapPath(CConfig.FILE_TEMPLATE_EMAIL_NOTIFICATION_UPLOAD); // Template Html
                string strSubject = CConfig.TEMPLATE_HTML_NOTIFICATION_UPLOAD_SUBJECT.Replace("(!Symbol)", objNews.Stock_Code).Replace("(!Action)", action); //  tieu de email
                string strBody = CBase.ReadFile(strTemplatePath).Replace("<!Content>", noidung);    // noi dung email
                                                                                                    //  LogFile.Info("1:" + strTemplatePath + "2:" + strSubject + "3:" + strBody);   // Ghi thông tin ra file
                SendEmailNotification(email, CC, strSubject, strBody);
            }
            catch (Exception ex)
            {
                LogFile.Error(ex.ToString());   // Ghi thông tin ra file
            }
        }
    }
}