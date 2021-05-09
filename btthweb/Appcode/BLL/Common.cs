using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using btthweb.Appcode.DAL;
using btthweb.Models;
using Newtonsoft.Json.Linq;

namespace btthweb.Appcode.BLL
{
    public static class Common
    {
        public static byte[] encryptData(string data)
        {
            try
            {
                System.Security.Cryptography.MD5CryptoServiceProvider md5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] hashedBytes;
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(data));
                return hashedBytes;
            }
            catch (Exception ex)
            {
                LogFile.Error(ex.ToString());   // Ghi thông tin ra file
                return null;
            }
        }

        public static string HashMD5(this string data)
        {
            return BitConverter.ToString(encryptData(data)).Replace("-", "").ToLower();
        }

        //check file type
        public static Boolean CheckTypeFile(string strFileName)
        {
            try
            {
                int i = strFileName.Length;
                //  string strTail = strFileName.Substring(i - 4).ToLower();
                // 2018-05-24 15:06:40 chien Cho upload thêm file .rar, .doc, .docx
                string strTail = strFileName.Split('.').Last();    // Lấy đuôi file
                if (strTail.ToLower() == "pdf" || strTail.ToLower() == "rar" || strTail.ToLower() == "doc" || strTail.ToLower() == "docx")

                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                LogFile.Error(ex.ToString());   // Ghi thông tin ra file
                return false;
            }
        }

        public static string ProcessFileName(string strFileName)
        {
            try
            {
                string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
                    "đ",
                    "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
                    "í","ì","ỉ","ĩ","ị",
                    "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
                    "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
                    "ý","ỳ","ỷ","ỹ","ỵ",};
                string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
                    "d",
                    "e","e","e","e","e","e","e","e","e","e","e",
                    "i","i","i","i","i",
                    "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
                    "u","u","u","u","u","u","u","u","u","u","u",
                    "y","y","y","y","y",};
                // bo dau tieng viet
                for (int i = 0; i < arr1.Length; i++)
                {
                    strFileName = strFileName.Replace(arr1[i], arr2[i]);
                    strFileName = strFileName.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
                }
                //chuyen khoang trang thanh "_"
                strFileName = strFileName.Replace(" ", "_");

                // 2017-12-12 11:13:24 ngocta2, bo cac ky tu la => bo tat ca ky tu chi de giu lai a-z A-Z 0-9 _ - ( ) .
                // "bao_cao_~!@#$%^&*(abc)_+-z.pdf"  =>  "bao_cao_(abc)_-z.pdf"
                strFileName = Regex.Replace(strFileName, "[^a-zA-Z0-9_\\-\\(\\)\\.]", "");

                // 2017-12-13 09:44:40 tiepbx them 6 ky tu random vao ten file
                // ví dụ "bao_cao_thang_12.pdf"  => "bao_cao_thang_12_abc123.pdf"
                //   string strRandom = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 6);
                //  string strTail = strFileName.Substring(strFileName.Length - 4);

                // 2018-05-24 15:06:40 chien sua lai chuoi ramdom
                Random random = new Random();
                string strRandom = random.Next(1000, 9999).ToString();
                strFileName = strRandom + "_" + strFileName;
            }
            catch (Exception ex)
            {
                LogFile.Error(ex.ToString());   // Ghi thông tin ra file
            }
            return strFileName;
        }

        public static bool ValidateUser(string Username, string Email)
        {
            return false;
        }

        public static string ReadFile(string strFullPath)
        {
            try
            {
                // read
                string strBody = "";
                using (StreamReader sr = new StreamReader(strFullPath))
                {
                    strBody = sr.ReadToEnd();
                }

                return strBody;
            }
            catch (Exception ex)
            {
                // do nothing
                LogFile.Error(ex.ToString());   // Ghi thông tin ra file
                return "";
            }
            finally
            {
            }
        }

        public async static Task<bool> CheckRecaptcha(string captchaRespone)
        {
            JObject result = await GetRecaptchaResult(captchaRespone);
            if ((bool)result.SelectToken("success"))
            {
                return true;
            }

            return false;
        }

        public async static Task<JObject> GetRecaptchaResult(string captchaRespone)
        {
            var HostRequest = "https://www.google.com/recaptcha/api/siteverify";
            var KeyCapchaRecret = ConfigurationManager.AppSettings["KEY_CAPCHA_SECRET"].ToString();
            var content = new FormUrlEncodedContent(
                new KeyValuePair<string, string>[]
                {
                    new KeyValuePair<string, string>("secret",KeyCapchaRecret),
                    new KeyValuePair<string, string>("response",captchaRespone)
                }
            );
            HttpResponseMessage result = new HttpResponseMessage();
            var client = new HttpClient();

            result = await client.PostAsync(HostRequest, content);
            var result2 = await result.Content.ReadAsStringAsync();
            return JObject.Parse(result2);
        }

        /// <summary>
        /// 2017-12-08 10:56:41 ngocta2
        ///
        /// full logic:
        ///     + website host tai D:\website
        ///     + client upload file : "Báo cáo quý 3.pdf"
        ///     + server code save 2 file da upload vao D:\website\upload => upload la folder con cua folder cha D:\website
        ///     + vi du file da upload la => D:\website\upload\Bao_cao_quy_3_aabbcc.pdf
        ///     + folder dich can copy file vao la E:\file\(yyyy)\(mm)\(dd)\(filename) => (yyyy) va (mm) va (dd) la year, month, day cua thoi diem hien tai, (filename) la ten file da xu ly
        ///     + neu hom nay la ngay 8 thang 12 nam 2017 thi nghia la file copy file tu D:\website\upload vao E:\file\2017\12\08
        ///     + sau khi copy la E:\file\2017\12\08\Bao_cao_quy_3_aabbcc.pdf
        ///     + return string theo mau http://www.fpts.com.vn/FileStore2/File/(yyyy)/(mm)/(dd)/(filename). vi du: http://www.fpts.com.vn/FileStore2/File/2017/12/08/Bao_cao_quy_3_aabbcc.pdf
        ///
        /// yeu cau
        ///     + khai bao key TEMPLATE_DIR_DEST trong web.config chua gia tri "E:\file\(yyyy)\(mm)\(dd)\(filename)"
        ///     + khai bao key TEMPLATE_URL_DEST trong web.config chua gia tri "http://www.fpts.com.vn/FileStore2/File/(yyyy)/(mm)/(dd)/(filename)"
        /// </summary>
        /// <param name="strFullSoucePath">D:\website\upload\Bao_cao_quy_3_aabbcc.pdf</param>
        /// <returns>http://www.fpts.com.vn/FileStore2/File/2017/12/08/Bao_cao_quy_3_aabbcc.pdf</returns>
        //public static string CopyFileUploaded(string strFullSoucePath, DateTime datDatePublish)
        //{
        //    try
        //    {
        //        //gan gia tri cho path
        //        string strFullDestURL, strPath;
        //        string strFileName = System.IO.Path.GetFileName(strFullSoucePath);              //test_af2f73.pdf
        //        string[] arrstrTemplatePath = CConfig.TEMPLATE_DIR_DEST.Split('|');          //C:\file\(yyyy)\(mm)\(dd)\|D:\file\(yyyy)\(mm)\(dd)\
        //                                                                                     //   CLog.LogEx("DEBUG.js", "Common.TEMPLATE_DIR_DEST = " + Common.TEMPLATE_DIR_DEST);
        //                                                                                     // copy 1 file vao X folder dich
        //        foreach (string strTemplatePath in arrstrTemplatePath)                          // C:\file\(yyyy)\(mm)\(dd)\
        //        {
        //            if (strTemplatePath != "")                                                      // blank string thi ko copy nua vi du trong config la "C:\file\|"
        //            {
        //                //2018-04-03 17:29:03 ngocta2 fix error : Message = Access to the path '\\172.16.0.18\e$\FileStore2\File\2018\04\03\' is denied.
        //                NetworkCredential netCredential = new System.Net.NetworkCredential(CConfig.CREDENTIALS_USERNAME, CConfig.CREDENTIALS_PASSWORD);
        //                using (new CNetworkConnection(CConfig.CREDENTIALS_NETWORK, netCredential)) // webserver host tai 51 , file up len 51, tu 51 copy sang 18 phai cung cap NetworkCredential
        //                {
        //                    strPath = strTemplatePath                                                   // C:\file\2017\12\16\
        //                    .Replace("(yyyy)", datDatePublish.Year.ToString())
        //                    .Replace("(mm)", CBase.Right("00" + datDatePublish.Month.ToString(), 2))
        //                    .Replace("(dd)", CBase.Right("00" + datDatePublish.Day.ToString(), 2));
        //                    // CLog.LogEx("DEBUG.js", "strPath = " + strPath);
        //                    //// Use Path class to manipulate file and directory paths.
        //                    string strFullDestPath = Path.Combine(strPath, strFileName);                // C:\file\2017\12\16\test_af2f73.pdf
        //                                                                                                //    CLog.LogEx("DEBUG.js", "strFullDestPath = " + strFullDestPath);
        //                                                                                                // To copy a folder's contents to a new location:
        //                                                                                                // Create a new target folder, if necessary.
        //                    if (!Directory.Exists(strPath))
        //                        Directory.CreateDirectory(strPath);
        //                    //     CLog.LogEx("DEBUG.js", "strFullSoucePath => strFullDestPath = " + strFullSoucePath + " => " + strFullDestPath);
        //                    // To copy a file to another location and
        //                    // overwrite the destination file if it already exists.
        //                    File.Copy(strFullSoucePath, strFullDestPath, true);                         // H:\Project\TP_ENT\ENT\WebApp\Uploads\test_af2f73.pdf => C:\file\2017\12\16\test_af2f73.pdf
        //                }
        //            }
        //        }

        //        // tao 1 URL duy nhat
        //        strFullDestURL = CConfig.TEMPLATE_URL_DEST
        //            .Replace("(yyyy)", datDatePublish.Year.ToString())
        //            .Replace("(mm)", CBase.Right("00" + datDatePublish.Month.ToString(), 2))
        //            .Replace("(dd)", CBase.Right("00" + datDatePublish.Day.ToString(), 2))
        //            .Replace("(filename)", strFileName);

        //        return strFullDestURL;                                                          // http://www.fpts.com.vn/FileStore2/File/2017/12/16/test_af2f73.pdf
        //    }
        //    catch (Exception ex)
        //    {
        //        // CLog.LogError(CBase.GetDeepCaller(), CBase.GetDetailError(ex));
        //        LogFile.Error(ex.ToString());   // Ghi thông tin ra file
        //        return null;
        //    }
        //}

        /// <summary>
        /// Validate dữ liệu thêm mới
        /// </summary>
        /// <param name="CN"></param>
        /// <returns></returns>
        //static public CResult ValidateUpload(NewsSanPham CN, HttpPostedFileBase file)
        //{
        //    CResult CR = new CResult();
        //    try
        //    {
        //        //Tiêu đề không được để trống
        //        if (string.IsNullOrEmpty(CN.Title))
        //        {
        //            CR.ErrorCode = -1;
        //            CR.ErrorMessage = Resources.Resource.TitleCannotBeBlank;
        //            CR.Data = null;

        //            return CR;
        //        }
        //        //Tài liệu không được để trống nếu là loại báo cáo năm tài chính
        //        if ((CN.DocTypeID == 1 || CN.DocTypeID == 7 || CN.DocTypeID == 8) && string.IsNullOrEmpty(CN.ReportYear))
        //        {
        //            CR.ErrorCode = -1;
        //            CR.ErrorMessage = Resources.Resource.ReportYearCannotBeBlank;
        //            CR.Data = null;

        //            return CR;
        //        }
        //        //Tài liệu không được để trống
        //        if (file == null)
        //        {
        //            CR.ErrorCode = -1;
        //            CR.ErrorMessage = Resources.Resource.FileCannotBeBlank;
        //            CR.Data = null;

        //            return CR;
        //        }
        //        else
        //        {
        //            if (Common.CheckTypeFile(file.FileName) == true)
        //            {
        //                string strName = Common.ProcessFileName(file.FileName);    //kiểm tra độ dài tài liệu
        //                if (strName.Length > 200)
        //                {
        //                    CR.ErrorCode = -1;
        //                    CR.ErrorMessage = Resources.Resource.FileMax200;
        //                    CR.Data = null;

        //                    return CR;
        //                }
        //                //kiểm tra dung lượng của file
        //                int MaxSize = Convert.ToInt32(ConfigurationManager.AppSettings["UPLOAD_MAX_SIZE"].ToString());
        //                int intSize = file.ContentLength;
        //                if (intSize > MaxSize * 1024 * 1024)
        //                {
        //                    CR.ErrorCode = -1;
        //                    CR.ErrorMessage = Resources.Resource.FileMax30MB;
        //                    CR.Data = null;

        //                    return CR;
        //                }
        //            }
        //            else  // Không đúng định dạng
        //            {
        //                CR.ErrorCode = -1;
        //                CR.ErrorMessage = Resources.Resource.FileTypeInvaild;
        //                CR.Data = null;

        //                return CR;
        //            }
        //        }

        //        CR.ErrorCode = CConfig.ERROR_CODE_SUCCESS;
        //        // CR.ErrorMessage = "Tải lên thành công ";
        //        CR.Data = null;

        //        return CR;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogFile.Error(ex.ToString());   // Ghi thông tin ra file
        //        CR.ErrorCode = CConfig.ERROR_CODE_UNKNOWN;
        //        CR.ErrorMessage = ex.Message;
        //        CR.Data = null;
        //        return CR;
        //    }
        //}

        ///// <summary>
        ///// Validate dữ liệu cập nhật
        ///// </summary>
        ///// <param name="CN"></param>
        ///// <returns></returns>
        //static public CResult ValidateUpdate(NewsSanPham CN, HttpPostedFileBase file)
        //{
        //    CResult CR = new CResult();
        //    try
        //    {
        //        //Tiêu đề không được để trống
        //        if (string.IsNullOrEmpty(CN.Title))
        //        {
        //            CR.ErrorCode = -1;
        //            CR.ErrorMessage = Resources.Resource.TitleCannotBeBlank;
        //            CR.Data = null;

        //            return CR;
        //        }
        //        //Tài liệu không được để trống nếu là loại báo cáo năm tài chính
        //        if ((CN.DocTypeID == 1 || CN.DocTypeID == 7 || CN.DocTypeID == 8) && string.IsNullOrEmpty(CN.ReportYear))
        //        {
        //            CR.ErrorCode = -1;
        //            CR.ErrorMessage = Resources.Resource.ReportYearCannotBeBlank;
        //            CR.Data = null;

        //            return CR;
        //        }

        //        //Tài liệu không được để trống
        //        if (file == null)
        //        {
        //        }
        //        else
        //        {
        //            if (Common.CheckTypeFile(file.FileName) == true)
        //            {
        //                string strName = Common.ProcessFileName(file.FileName);    //kiểm tra độ dài tài liệu
        //                if (strName.Length > 200)
        //                {
        //                    CR.ErrorCode = -1;
        //                    CR.ErrorMessage = Resources.Resource.FileMax200;
        //                    CR.Data = null;

        //                    return CR;
        //                }
        //                //kiểm tra dung lượng của file
        //                int MaxSize = Convert.ToInt32(ConfigurationManager.AppSettings["UPLOAD_MAX_SIZE"].ToString());
        //                int intSize = file.ContentLength;
        //                if (intSize > MaxSize * 1024 * 1024)
        //                {
        //                    CR.ErrorCode = -1;
        //                    CR.ErrorMessage = Resources.Resource.FileMax30MB;
        //                    CR.Data = null;

        //                    return CR;
        //                }
        //            }
        //            else
        //            {
        //                CR.ErrorCode = -1;
        //                CR.ErrorMessage = Resources.Resource.FileTypeInvaild;
        //                CR.Data = null;

        //                return CR;
        //            }
        //        }

        //        CR.ErrorCode = CConfig.ERROR_CODE_SUCCESS;
        //        // CR.ErrorMessage = "Tải lên thành công ";
        //        CR.Data = null;

        //        return CR;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogFile.Error(ex.ToString());   // Ghi thông tin ra file
        //        CR.ErrorCode = CConfig.ERROR_CODE_UNKNOWN;
        //        CR.ErrorMessage = ex.Message;
        //        CR.Data = null;
        //        return CR;
        //    }
        //}
    }
}