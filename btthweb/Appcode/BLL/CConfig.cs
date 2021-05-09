using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace btthweb.Appcode.BLL
{
    public class CConfig
    {
        public static string CONNECTION_STRING_LOCAL = ConfigurationManager.ConnectionStrings["CONNECTION_STRING_LOCAL"].ConnectionString.ToString();
        public static string DIR_UPLOAD = ConfigurationManager.AppSettings["DIR_UPLOAD"].ToString();
        public static string REGEX_01 = ConfigurationManager.AppSettings["REGEX_01"].ToString();
        public static string TEMPLATE_DIR_DEST = ConfigurationManager.AppSettings["TEMPLATE_DIR_DEST"].ToString();
        public static string TEMPLATE_URL_DEST = ConfigurationManager.AppSettings["TEMPLATE_URL_DEST"].ToString();
        public static string TEMPLATE_URL_HOST = ConfigurationManager.AppSettings["TEMPLATE_URL_HOST"].ToString();
        public static string HOST = ConfigurationManager.AppSettings["HOST"].ToString();
        public static string SMTP_SERVER = ConfigurationManager.AppSettings["SMTP_SERVER"].ToString();
        public static string SMTP_PORT = ConfigurationManager.AppSettings["SMTP_PORT"].ToString();
        public static string SMTP_USER = ConfigurationManager.AppSettings["SMTP_USER"].ToString();
        public static string SMTP_PASS = ConfigurationManager.AppSettings["SMTP_PASS"].ToString();
        public static string SMTP_FROM = ConfigurationManager.AppSettings["SMTP_FROM"].ToString();
        public static string TEMPLATE_HTML_FORGOT_PASSWORD_EMAIL_SUBJECT = ConfigurationManager.AppSettings["TEMPLATE_HTML_FORGOT_PASSWORD_EMAIL_SUBJECT"].ToString();
        public static string FILE_TEMPLATE_EMAIL_RESET_PASSWORD = ConfigurationManager.AppSettings["FILE_TEMPLATE_EMAIL_RESET_PASSWORD"].ToString();
        public static int INTERVAL_CACHE_CHECK = Convert.ToInt32(ConfigurationManager.AppSettings["INTERVAL_CACHE_CHECK"].ToString());    // minutes
        public static string TEMPLATE_URL_UPDATE_CACHE = ConfigurationManager.AppSettings["TEMPLATE_URL_UPDATE_CACHE"].ToString();
        public static string CREDENTIALS_USERNAME = ConfigurationManager.AppSettings["CREDENTIALS_USERNAME"].ToString();
        public static string CREDENTIALS_PASSWORD = ConfigurationManager.AppSettings["CREDENTIALS_PASSWORD"].ToString();
        public static string CREDENTIALS_NETWORK = ConfigurationManager.AppSettings["CREDENTIALS_NETWORK"].ToString();
        public static string TEMPLATE_HTML_NOTIFICATION_UPLOAD_SUBJECT = ConfigurationManager.AppSettings["TEMPLATE_HTML_NOTIFICATION_UPLOAD_SUBJECT"].ToString();   // Gửi notifi khi upload bài viết
        public static string FILE_TEMPLATE_EMAIL_NOTIFICATION_UPLOAD = ConfigurationManager.AppSettings["FILE_TEMPLATE_EMAIL_NOTIFICATION_UPLOAD"].ToString();
        public static string EmailHN = ConfigurationManager.AppSettings["SMTP_MailHN"].ToString();
        public static string EmailHCM = ConfigurationManager.AppSettings["SMTP_MailHCM"].ToString();

        public static string ThemTin = "Thêm mới tin";
        public static string SuaTin = "Cập nhật tin";
        public static string XoaTin = "Xóa tin";

        //Thông tin API chiennd 08/04/2019
        public static string APILINK = ConfigurationManager.AppSettings["API_LINK"].ToString();

        public static string API_INSERT_NEW = "/insertnews";
        public static string API_UPDATE_NEW = "/updatenews";
        public static string API_DELETE_NEW = "/deletenews";

        public const int ERROR_CODE_UPLOAD_SUCCESS = 2018031205;
        public const int ERROR_CODE_UPDATE_SUCCESS = 2018290512;

        public const int ERROR_CODE_EXCEPTION = 10000;

        public const int ERROR_CODE_SUCCESS = 0;
        public const int ERROR_CODE_NO_DATA = 100;
        public const int ERROR_CODE_UNKNOWN = -999;
        public const int ERROR_CODE_DAL = -1;

        public const int ERROR_CODE_USERNAME_KHONG_DUOC_BO_TRONG = 2018020701;

        public const int ERROR_CODE_EMAIL_KHONG_DUOC_BO_TRONG = 2018020902;
        public const int ERROR_CODE_EMAIL_KHONG_HOP_LE = 2018031502;
        public const int ERROR_CODE_CAPTCHA_KHONG_DUNG = 2018031501;
        public const int ERROR_CODE_DATE_BEGIN_KHONG_DUOC_BO_TRONG = 2018020601;
        public const int ERROR_CODE_DATE_END_KHONG_DUOC_BO_TRONG = 2018020602;
        public const int ERROR_CODE_FULLNAME_KHONG_DUOC_BO_TRONG = 2018022101;
        public const int ERROR_CODE_SYMBOL_KHONG_DUOC_BO_TRONG = 2018022102;
        public const int ERROR_CODE_MAT_KHAU_KHONG_TRUNG_KHOP = 2018022103;
        public const int ERROR_CODE_CHUA_CHON_FILE = 2018022104;
        public const int ERROR_CODE_CHI_DUOC_TAI_LEN_FILE_PDF = 2018022105;
        public const int ERROR_CODE_DUNG_LUONG_FILE_LON_HON_15MB = 2018022106;
        public const int ERROR_CODE_TEN_FILE_QUA_DAI = 2018040201;
        public const int ERROR_CODE_SESSION_START = 2018030801;
        public const int ERROR_CODE_SESSION_END = 2018030802;
        public const int ERROR_CODE_LOG_OUT = 2018030803;
        public const int ERROR_CODE_SAVE_CLIENT_INFO_TO_SESSION = 2018030804;
        public const int ERROR_CODE_MAT_KHAU_KHONG_DUOC_DE_TRONG = 2018031202;
        public const int ERROR_CODE_NHAP_NEW_PASSWORD = 2018031203;
        public const int ERROR_CODE_NHAP_CONFIRM_NEW_PASSWORD = 2018031204;

        public const int ERROR_CODE_KHONG_DUOC_CHON_SYMBOL = 2018031901;
        public const int ERROR_CODE_BAT_BUOC_CHON_SYMBOL = 2018031902;
        public const int ERROR_CODE_ONLY_ONE_SESSION = 9999;
    }
}