using btthweb.Appcode.BLL;
using btthweb.Models;
using btthweb.Models.FormViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace btthweb.Appcode.DAL
{
    public class DatabaseInternal
    {

        public static string Insert_Product(SanPham sanpham)
        {
            CSQL objSQL = new CSQL(DatabaseProduct.connectionString);
            try
            {
                if (objSQL._OpenConnection() == false)
                    return "Open Connection false";


                SqlParameter prmTenSP = new SqlParameter("@TenSP", SqlDbType.NVarChar, 50);
                prmTenSP.Direction = ParameterDirection.Input;
                objSQL.Command.Parameters.Add(prmTenSP);
                // input param
                SqlParameter prmGia = new SqlParameter("@Gia", SqlDbType.Float);
                prmGia.Direction = ParameterDirection.Input;
                objSQL.Command.Parameters.Add(prmGia);

                SqlParameter prmMoTaSP = new SqlParameter("@MoTaSP", SqlDbType.NVarChar, 50);
                prmMoTaSP.Direction = ParameterDirection.Input;
                objSQL.Command.Parameters.Add(prmMoTaSP);

                SqlParameter prmAnh = new SqlParameter("@Anh", SqlDbType.NVarChar, 50);
                prmMoTaSP.Direction = ParameterDirection.Input;
                objSQL.Command.Parameters.Add(prmAnh);


                // output param
                SqlParameter Result = new SqlParameter("@MESS", SqlDbType.NVarChar, 50);
                Result.Direction = ParameterDirection.Output;
                Result.DbType = DbType.String;
                objSQL.Command.Parameters.Add(Result);
                prmTenSP.Value = sanpham.Tensp;
                prmGia.Value = sanpham.Gia;
                prmMoTaSP.Value = sanpham.MoTaSP;
                prmAnh.Value = sanpham.Anh;

                objSQL.ExecuteSP("Tao_SanPham");
                return Result.Value.ToString();
            }
            catch (Exception ex)
            {
                LogFile.Error(ex.ToString());   // Ghi thông tin ra file
            }

            return "Create Product Failed";
        }
        public static bool UpdateProductInfo(SanPham sanpham)
        {
            CSQL objSQL = new CSQL(DatabaseProduct.connectionString);
            try
            {
                if (objSQL._OpenConnection() == false)
                    return false;
                // input param

                SqlParameter prmID = new SqlParameter("@ID", SqlDbType.Int);
                prmID.Direction = ParameterDirection.Input;
                objSQL.Command.Parameters.Add(prmID);

                SqlParameter prmTenSanPham = new SqlParameter("@Tensp", SqlDbType.NVarChar, 50);
                prmTenSanPham.Direction = ParameterDirection.Input;
                objSQL.Command.Parameters.Add(prmTenSanPham);

                SqlParameter prmGia = new SqlParameter("@Gia", SqlDbType.Float);
                prmGia.Direction = ParameterDirection.Input;
                objSQL.Command.Parameters.Add(prmGia);

                SqlParameter prmMoTaSP = new SqlParameter("@MoTaSP ", SqlDbType.NVarChar, 100);
                prmMoTaSP.Direction = ParameterDirection.Input;
                objSQL.Command.Parameters.Add(prmMoTaSP);

                SqlParameter prmAnh = new SqlParameter("@Anh ", SqlDbType.NVarChar, 100);
                prmAnh.Direction = ParameterDirection.Input;
                objSQL.Command.Parameters.Add(prmAnh);



                // output param
                SqlParameter Result = new SqlParameter("@Message", SqlDbType.NVarChar, 100);
                Result.Direction = ParameterDirection.Output;
                Result.DbType = DbType.String;
                objSQL.Command.Parameters.Add(Result);

                //set value~
                prmID.Value = sanpham.ID;
                prmTenSanPham.Value = sanpham.Tensp;
                prmGia.Value = sanpham.Gia;
                prmMoTaSP.Value = sanpham.MoTaSP;
                prmAnh.Value = sanpham.Anh;

                objSQL.ExecuteSP("PRODUCT_UPDATE");
                if (Result.Value.ToString() == "Cập nhật thành công")
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogFile.Error(ex.ToString());   // Ghi thông tin ra file
            }

            return false;
        }

        public static bool DeleteProduct(int ID)
        {
            CSQL objSQL = new CSQL(DatabaseProduct.connectionString);
            try
            {
                if (objSQL._OpenConnection() == false)
                    throw new Exception("Không thể kết nối");

                SqlParameter prmID = new SqlParameter("@ID", SqlDbType.Int);
                prmID.Direction = ParameterDirection.Input;
                objSQL.Command.Parameters.Add(prmID);
                prmID.Value = ID;

                SqlParameter Result = new SqlParameter("@Message", SqlDbType.NChar, 50);
                Result.Direction = ParameterDirection.Output;
                Result.DbType = DbType.String;
                objSQL.Command.Parameters.Add(Result);

                if (!objSQL.ExecuteSP("DELETE_PRODUCT")) //xóa không thành công
                {
                    return false;
                }

                if (Result.Value.ToString() == "Xóa thành công")
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogFile.Error(ex.ToString());   // Ghi thông tin ra file
            }

            return false;
        }

        public static List<SanPham> GetListProduct(ListProductViewModel viewModel)
        {
            CSQL objSQL = new CSQL(DatabaseProduct.connectionString);

            try
            {
                if (objSQL._OpenConnection() == false)
                    throw new Exception("Không thể kết nối");
                // input param
               if(viewModel.Tensp != null)
               {
                    SqlParameter prmTensp = new SqlParameter("@Tensp", SqlDbType.NVarChar, 50);
                    prmTensp.Direction = ParameterDirection.Input;
                    objSQL.Command.Parameters.Add(prmTensp);
                    prmTensp.Value = viewModel.Tensp;
               }

                SqlDataReader reader = objSQL.GetDataReaderFromSP("LIST_PRODUCT_GET");

                var ListProduct = new List<SanPham>();
                SanPham SP;

                while (reader.Read())
                {
                    try     
                     {
                        SP = new SanPham();
                        if (!reader.IsDBNull(reader.GetOrdinal("ID")))
                        {
                            SP.ID = reader.GetInt32(reader.GetOrdinal("ID"));
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("Tensp")))
                        {
                            SP.Tensp = reader.GetString(reader.GetOrdinal("Tensp"));
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("Gia")))
                        {
                            SP.Gia = reader.GetDouble(reader.GetOrdinal("Gia"));
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("MoTaSP")))
                        {
                            SP.MoTaSP = reader.GetString(reader.GetOrdinal("MoTaSP"));
                        }

                        ListProduct.Add(SP);
                    }
                    catch(Exception ex) {
                        ex.ToString();
                    }
                }

                return ListProduct;
            }
            catch (Exception ex)
            {
                LogFile.Error(ex.ToString());   // Ghi thông tin ra file
            }
            finally
            {
                objSQL._CloseConnection();
            }

            return new List<SanPham>();
        }

        public static string Insert_User(Users user)
        {
            CSQL objSQL = new CSQL(DatabaseProduct.connectionString);
            try
            {
                if (objSQL._OpenConnection() == false)
                    return "Open Connection false";
                // input param

                SqlParameter prmUsername = new SqlParameter("@Username", SqlDbType.VarChar, 50);
                prmUsername.Direction = ParameterDirection.Input;
                objSQL.Command.Parameters.Add(prmUsername);
                // input param
                SqlParameter prmPassword = new SqlParameter("@Password", SqlDbType.VarChar, 32);
                prmPassword.Direction = ParameterDirection.Input;
                objSQL.Command.Parameters.Add(prmPassword);

                SqlParameter prmFullname = new SqlParameter("@FullName", SqlDbType.NVarChar, 50);
                prmUsername.Direction = ParameterDirection.Input;
                objSQL.Command.Parameters.Add(prmFullname);

                SqlParameter prmEmail = new SqlParameter("@Email", SqlDbType.VarChar, 100);
                prmEmail.Direction = ParameterDirection.Input;
                objSQL.Command.Parameters.Add(prmEmail);

                SqlParameter prmCCEmail = new SqlParameter("@CC", SqlDbType.VarChar, 100);
                prmCCEmail.Direction = ParameterDirection.Input;
                objSQL.Command.Parameters.Add(prmCCEmail);

                SqlParameter prmPhone = new SqlParameter("@Phone", SqlDbType.NVarChar, 50);
                prmPhone.Direction = ParameterDirection.Input;
                objSQL.Command.Parameters.Add(prmPhone);

                SqlParameter prmNote = new SqlParameter("@Note", SqlDbType.NVarChar, 50);
                prmNote.Direction = ParameterDirection.Input;
                objSQL.Command.Parameters.Add(prmNote);

                SqlParameter prmActive = new SqlParameter("@Active", SqlDbType.Int, 50);
                prmActive.Direction = ParameterDirection.Input;
                objSQL.Command.Parameters.Add(prmActive);

                SqlParameter prmRegionID = new SqlParameter("@RegionID", SqlDbType.Int, 50);
                prmRegionID.Direction = ParameterDirection.Input;
                objSQL.Command.Parameters.Add(prmRegionID);

                //SqlParameter prmCompanyID = new SqlParameter("@CompanyID", SqlDbType.Int, 50);
                //prmUsername.Direction = ParameterDirection.Input;
                //objSQL.Command.Parameters.Add(prmCompanyID);

                // output param
                SqlParameter Result = new SqlParameter("@MESS", SqlDbType.NVarChar, 50);
                Result.Direction = ParameterDirection.Output;
                Result.DbType = DbType.String;
                objSQL.Command.Parameters.Add(Result);

                //set value~
                prmUsername.Value = user.UserName;
                prmPassword.Value = user.Password.HashMD5();
                prmEmail.Value = user.Email;
                prmCCEmail.Value = user.CC;
                prmFullname.Value = user.FullName;
                prmPhone.Value = user.Phone;
                prmNote.Value = user.Note;
                prmActive.Value = user.Active;
                prmRegionID.Value = user.RegionID;

                objSQL.ExecuteSP("CBTT_USER_CREATE");
                return Result.Value.ToString();
            }
            catch (Exception ex)
            {
                LogFile.Error(ex.ToString());   // Ghi thông tin ra file
            }

            return "Create User Failed";
        }
    }
}