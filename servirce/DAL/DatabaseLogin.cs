
using Common.Models;
using Common.Models.FormViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace servirce.DAL
{
    public class DatabaseLogin
    {
        /// <summary>
        ///   Lấy Toàn bộ thông tin của User trong DataBase
        /// </summary>
        /// <param name="User">User name và pass lấy từ view</param>
        /// <returns> Users nếu là admin , hoặc customer </returns>
        public static Object GetUser(LoginViewModel User)
        {
            CSQL objSQL = new CSQL(DatabaseProduct.connectionString);

            try
            {
                if (objSQL._OpenConnection() == false)
                    throw new Exception("Không thể kết nối");

                // input param

                SqlParameter prmUsername = new SqlParameter("@Username", SqlDbType.VarChar, 50);
                prmUsername.Direction = ParameterDirection.Input;
                objSQL.Command.Parameters.Add(prmUsername);
                // input param
                SqlParameter prmPassword = new SqlParameter("@Password", SqlDbType.VarChar, 32);
                prmPassword.Direction = ParameterDirection.Input;
                objSQL.Command.Parameters.Add(prmPassword);

                // output param
                SqlParameter Result = new SqlParameter("@MESS", SqlDbType.NChar, 50);
                Result.Direction = ParameterDirection.Output;
                Result.DbType = DbType.String;
                objSQL.Command.Parameters.Add(Result);

                //set value~
                prmUsername.Value = User.UserName;
                prmPassword.Value = User.Password;

                SqlDataReader reader = objSQL.GetDataReaderFromSP("LOGIN_INFO_GET");

                //Nếu là admin thì sẽ lấy những thông tin của Addmin
                if (HttpContext.Current.Session[ApplicationConfig.AccountType] == ApplicationConfig.Admin)
                {
                    var currentUser = new Users();
                    if (reader.Read())
                    {
                        currentUser.UserName = User.UserName;
                        currentUser.Password = User.Password/*HashMD5()*/;
                        if (!reader.IsDBNull(reader.GetOrdinal("FullName")))
                        {
                            currentUser.FullName = reader.GetString(reader.GetOrdinal("FullName"));
                        }

                        currentUser.Active = reader.GetInt32(reader.GetOrdinal("Active"));
                        if (!reader.IsDBNull(reader.GetOrdinal("Phone")))
                        {
                            currentUser.Phone = reader.GetString(reader.GetOrdinal("Phone"));
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("Email")))
                        {
                            currentUser.Email = reader.GetString(reader.GetOrdinal("Email"));
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("Note")))
                        {
                            currentUser.Note = reader.GetString(reader.GetOrdinal("Note"));
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("RegionID")))
                        {
                            currentUser.RegionID = reader.GetInt32(reader.GetOrdinal("RegionID"));
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("CreateDate")))
                        {
                            currentUser.CreateDate = reader.GetDateTime(reader.GetOrdinal("CreateDate"));
                        }

                        //                        if (!reader.IsDBNull(reader.GetOrdinal("Symbol")))
                        //                        {
                        //                            currentUser.Symbol = reader.GetString(reader.GetOrdinal("Symbol"));
                        //                        }
                        //                        if (!reader.IsDBNull(reader.GetOrdinal("Exchange")))
                        //                        {
                        //                            currentUser.Exchange = reader.GetString(reader.GetOrdinal("Exchange"));
                        //                        }

                        return currentUser;
                    }
                }

                if (HttpContext.Current.Session[ApplicationConfig.AccountType] == ApplicationConfig.Customer)
                {
                    var currentUser = new Customer();
                    if (reader.Read())
                    {
                        currentUser.UserName = User.UserName;
                        currentUser.Password = User.Password;
                        if (!reader.IsDBNull(reader.GetOrdinal("FullName")))
                        {
                            currentUser.FullName = reader.GetString(reader.GetOrdinal("FullName"));
                        }

                        currentUser.Active = reader.GetInt32(reader.GetOrdinal("Active"));
                        if (!reader.IsDBNull(reader.GetOrdinal("Phone")))
                        {
                            currentUser.Phone = reader.GetString(reader.GetOrdinal("Phone"));
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("Email")))
                        {
                            currentUser.Email = reader.GetString(reader.GetOrdinal("Email"));
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("CC")))
                        {
                            currentUser.CC = reader.GetString(reader.GetOrdinal("CC"));
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("Note")))
                        {
                            currentUser.Note = reader.GetString(reader.GetOrdinal("Note"));
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("CompanyID")))
                        {
                            currentUser.CompanyID = reader.GetInt32(reader.GetOrdinal("CompanyID"));
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("CreateDate")))
                        {
                            currentUser.CreateDate = reader.GetDateTime(reader.GetOrdinal("CreateDate"));
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("Symbol")))
                        {
                            currentUser.Symbol = reader.GetString(reader.GetOrdinal("Symbol"));
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("Exchange")))
                        {
                            currentUser.Exchange = reader.GetString(reader.GetOrdinal("Exchange"));
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("Name")))
                        {
                            currentUser.CompanyName = reader.GetString(reader.GetOrdinal("Name"));
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("Expert")))
                        {
                            currentUser.Expert = reader.GetString(reader.GetOrdinal("Expert"));
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("ExpertFullName")))
                        {
                            currentUser.ExpertFullName = reader.GetString(reader.GetOrdinal("ExpertFullName"));
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("ExpertEmail")))
                        {
                            currentUser.ExpertEmail = reader.GetString(reader.GetOrdinal("ExpertEmail"));
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("ExpertPhone")))
                        {
                            currentUser.ExpertPhone = reader.GetString(reader.GetOrdinal("ExpertPhone"));
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("RegionID")))
                        {
                            currentUser.RegionID = reader.GetInt32(reader.GetOrdinal("RegionID"));
                        }
                        return currentUser;
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile.Error(ex.ToString());   // Ghi thông tin ra file
            }
            finally
            {
                objSQL._CloseConnection();
            }

            return null;
        }

        /// <summary>
        /// Nếu đăng nhập  và lấy các thông tin thành công thì return true
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public static string User_Login(LoginViewModel User)
        {
            //Test offline

            //HttpContext.Current.Session[SessionValue.AccountType] = SessionValue.Admin;
            //return true;

            CSQL objSQL = new CSQL(DatabaseProduct.connectionString);

            try
            {
                if (objSQL._OpenConnection() == false)
                    return "Không thể kết nối";
                // input param

                SqlParameter prmUsername = new SqlParameter("@Username", SqlDbType.VarChar, 50);
                prmUsername.Direction = ParameterDirection.Input;
                objSQL.Command.Parameters.Add(prmUsername);
                // input param
                SqlParameter prmPassword = new SqlParameter("@Password", SqlDbType.VarChar, 32);
                prmPassword.Direction = ParameterDirection.Input;
                objSQL.Command.Parameters.Add(prmPassword);

                // output param
                SqlParameter Result = new SqlParameter("@MESS", SqlDbType.NChar, 50);
                Result.Direction = ParameterDirection.Output;
                Result.DbType = DbType.String;
                objSQL.Command.Parameters.Add(Result);

                //set value~
                prmUsername.Value = User.UserName;
                prmPassword.Value = User.Password;

                objSQL.ExecuteSP("SHOP_LOGIN");
                return Result.Value.ToString();
            }
            catch (Exception ex)
            {
                LogFile.Error(ex.ToString());   // Ghi thông tin ra file
            }

            return "Không thể đăng nhập";
        }
    }
}
