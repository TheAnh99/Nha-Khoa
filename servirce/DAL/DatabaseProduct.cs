
using Common.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace servirce.DAL
{
    public class DatabaseProduct
    {
        public static string connectionString = ConfigurationManager
                                               .ConnectionStrings["CONNECTION_STRING_LOCAL"]
                                              .ConnectionString.ToString();

        //
       // public static string connectionString = @"Data Source=DESKTOP-A16NIVL;Initial Catalog=CBTT;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;";

        //create customer

   
        //get company list

        public static List<SanPham> Lay_DS_SanPham()
        {
            CSQL objSQL = new CSQL(connectionString);

            try
            {
                if (objSQL._OpenConnection() == false)
                    throw new Exception("Không thể kết nối");
                // input param

                SqlDataReader reader = objSQL.GetDataReaderFromSP("Lay_DS_SanPham");

                var DsSP = new List<SanPham>();
                SanPham sp;

                while (reader.Read())
                {
                    try          //chiennd edit 18/03/2019
                    {
                       sp  = new SanPham()
                        {
                            ID = reader.GetInt32(reader.GetOrdinal("ID")),
                            Tensp = reader.GetString(reader.GetOrdinal("Tensp")),
                            Gia = reader.GetDouble(reader.GetOrdinal("Gia")),
                            MoTaSP = reader.GetString(reader.GetOrdinal("MoTaSP")),
                            Anh = reader.GetString(reader.GetOrdinal("Anh"))
                        };
                      
                        DsSP.Add(sp);
                    }
                    catch(Exception ex) { }
                }

                return DsSP;
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

        // get SanPham for UpdateSP
        public static SanPham GetProductInfo(int ID)
        {
            CSQL objSQL = new CSQL(connectionString);

            try
            {
                if (objSQL._OpenConnection() == false)
                    throw new Exception("Không thể kết nối");
                // input param

                SqlParameter prmId = new SqlParameter("@Id",SqlDbType.Int);
                prmId.Direction = ParameterDirection.Input;
                objSQL.Command.Parameters.Add(prmId);
                //set value~
                prmId.Value = ID;
                SqlDataReader reader = objSQL.GetDataReaderFromSP("Lay_Thongtin_SanPham");

                var currentSanPham = new SanPham();
                if (reader.Read())
                {
                    currentSanPham.ID = ID;

                    if (!reader.IsDBNull(reader.GetOrdinal("Tensp")))
                    {
                        currentSanPham.Tensp = reader.GetString(reader.GetOrdinal("Tensp"));
                    }

                    if (!reader.IsDBNull(reader.GetOrdinal("Gia")))
                    {
                        currentSanPham.Gia = reader.GetDouble(reader.GetOrdinal("Gia"));
                    }

                    if (!reader.IsDBNull(reader.GetOrdinal("MoTaSP")))
                    {
                        currentSanPham.MoTaSP = reader.GetString(reader.GetOrdinal("MoTaSP"));
                    }

                    if (!reader.IsDBNull(reader.GetOrdinal("Anh")))
                    {
                        currentSanPham.Anh = reader.GetString(reader.GetOrdinal("Anh"));
                    }

                    return currentSanPham;
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
    }
}