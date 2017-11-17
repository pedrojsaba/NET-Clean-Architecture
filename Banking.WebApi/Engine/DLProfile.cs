using Banking.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Banking.WebApi.Engine
{

    public class DLProfile
    {
        SqlDataAdapter da;
        DataSet ds;
        DataTable dt;
        SqlParameter paRetVal, paProfileID, paIdGuidAspNetUsers, paFirstName, paLastName, paAddress, paGender, paImage, paIdClienteSeguro;

        public ETProfile GetProfile(string userId)
        {
            var ePersona = new ETProfile();
            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AuthContext"].ToString()))
                {
                    con.Open();
                    var query = new SqlCommand("SpSelectProfileId", con);
                    query.Parameters.AddWithValue("@IdGuidAspNetUsers", userId);
                    query.CommandType = CommandType.StoredProcedure;

                    using (var dr = query.ExecuteReader())
                    {
                        dr.Read();
                        if (dr.HasRows)
                        {
                            ePersona.ProfileID = Convert.ToInt32(dr["ProfileID"]);
                            ePersona.IdGuidAspNetUsers = dr["IdGuidAspNetUsers"].ToString();
                            ePersona.FirstName = dr["FirstName"].ToString();
                            ePersona.LastName = dr["LastName"].ToString();
                            ePersona.Address = dr["Address"].ToString();
                            ePersona.Gender = dr["Gender"].ToString();
                            ePersona.Image = dr["Image"].ToString();
                            ePersona.IdClienteSeguro = dr["IdClienteSeguro"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return ePersona;
        }
        public bool RegisterProfile(ETProfile eProfile)
        {
            bool answer = false;
            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AuthContext"].ToString()))
                {
                    con.Open();
                    var query = new SqlCommand("SpInsertProfile", con);
                    query.CommandType = CommandType.StoredProcedure;
                    paIdGuidAspNetUsers = query.Parameters.Add("@IdGuidAspNetUsers", SqlDbType.NVarChar, 128);
                    paIdGuidAspNetUsers.Direction = ParameterDirection.Input;
                    paFirstName = query.Parameters.Add("@FirstName", SqlDbType.NVarChar, 256);
                    paFirstName.Direction = ParameterDirection.Input;
                    paLastName = query.Parameters.Add("@LastName", SqlDbType.NVarChar, 256);
                    paLastName.Direction = ParameterDirection.Input;
                    paAddress = query.Parameters.Add("@Address", SqlDbType.NVarChar, 1000);
                    paAddress.Direction = ParameterDirection.Input;
                    paGender = query.Parameters.Add("@Gender", SqlDbType.NVarChar, 256);
                    paGender.Direction = ParameterDirection.Input;
                    paImage = query.Parameters.Add("@Image", SqlDbType.NVarChar, 256);
                    paImage.Direction = ParameterDirection.Input;
                    paIdClienteSeguro = query.Parameters.Add("@IdClienteSeguro", SqlDbType.NVarChar, 256);
                    paIdClienteSeguro.Direction = ParameterDirection.Input;
                    paRetVal = query.Parameters.Add("RetVal", SqlDbType.Int);
                    paRetVal.Direction = ParameterDirection.ReturnValue;

                    paIdGuidAspNetUsers.Value = eProfile.IdGuidAspNetUsers;
                    paFirstName.Value = eProfile.FirstName;
                    paLastName.Value = eProfile.LastName;
                    paAddress.Value = eProfile.Address;
                    paGender.Value = eProfile.Gender;
                    paImage.Value = eProfile.Image;
                    paIdClienteSeguro.Value = eProfile.IdClienteSeguro;
                    query.ExecuteNonQuery();
                    answer = true;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return answer;
        }
    }

}