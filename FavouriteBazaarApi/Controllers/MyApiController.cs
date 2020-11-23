using FavouriteBazaarApi.Model;
using FavouriteBazaarApi.Models;
using FavouriteBazaarApi.Models.DAL.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FavouriteBazaarApi.Controllers
{
    public class MyApiController : Controller
    {
        ModelDb db = new ModelDb();
        // GET: MyApi
        [ValidateInput(false)]

        public ActionResult InsertUpdateCustomerAccount(clsUsers user)
        {
            string message = "";
            bool status = false;
            using (ModelDb db = new ModelDb())
            {
                try
                {
                    string returnId = "0";
                    string insertUpdateStatus = "";
                    int? AccountId = user.Id;
                    if (user.Id > 0)
                    {
                        insertUpdateStatus = "Update";
                        bool check = db.tblUsers.Where(x => x.Id == AccountId).Any(x => x.UserName.ToLower().Trim() == user.UserName.ToLower().Trim());
                        if (!check)
                        {
                            bool check2 = db.tblUsers.Any(x => x.UserName.ToLower().Trim() == user.UserName.ToLower().Trim());
                            if (check2)
                            {
                                status = false;
                                message = "User already exist.";
                                return new JsonResult { Data = new { status = status, message = message } };
                            }
                        }
                        if (user.StatusString == "Active")
                        {
                            user.Status = true;
                        }
                        else
                        {
                            user.Status = false;
                        }
                        if (!(string.IsNullOrEmpty(user.Password)))
                        {
                            user.Password = clsPasswordEncrypt.GetHash(user.Password);
                        }
                    }
                    else
                    {
                        insertUpdateStatus = "Save";
                        bool check2 = db.tblUsers.Any(x => x.UserName.ToLower().Trim() == user.UserName.ToLower().Trim());
                        if (check2)
                        {
                            status = false;
                            message = "User already exist.";
                            return new JsonResult { Data = new { status = status, message = message } };
                        }
                        user.Status = true;
                        if (!(string.IsNullOrEmpty(user.Password)))
                        {
                            user.Password = clsPasswordEncrypt.GetHash(user.Password);
                        }
                    }
                    clsResult result = InsertUpdateCustomerDb(user, insertUpdateStatus);
                    if (result.ResultMessage == "Success")
                    {
                        ModelState.Clear();
                        status = true;
                        message = "User Successfully Registered.";
                        user.Id = result.Id;
                    }
                    else
                    {
                        ModelState.Clear();
                        status = false;
                        message = result.ResultMessage;
                        user.Id = 0;
                    }
                }
                catch (Exception ex)
                {
                    status = false;
                    message = ex.Message.ToString();
                }
            }

            return new JsonResult { Data = new { status = status, message = message, id = user.Id } };
        }
        //User login api
        [ValidateInput(false)]
        public ActionResult Login(clsUsers login)
        {

            ModelDb db = new ModelDb();
            int userId = 0;
            bool status = false;
            string Message = string.Empty;
            // clsPasswordEncrypt encrypt = new clsPasswordEncrypt();
            //string password = encrypt.GetHash(login.Password);
            string hashPassword = clsPasswordEncrypt.GetHash(login.Password);
            var user = (from u in db.tblUsers
                        where u.UserName.ToLower() == login.UserName.ToLower().Trim() && u.Password == hashPassword
                        select u).FirstOrDefault();
            if (user != null)
            {
                status = true;
                Message = "Login Successfully";
                status = true;
                string[] roles = new string[1];
                roles[0] = "User";
                CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
                serializeModel.UserId = user.Id;
                serializeModel.Name = user.FirstName + " " + user.LastName;
                serializeModel.roles = roles;
                string userData = JsonConvert.SerializeObject(serializeModel);
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                         1,
                         user.FirstName + " " + user.LastName,
                         DateTime.Now,
                         DateTime.Now.AddMinutes(30),
                         false,
                         userData);

                string encTicket = FormsAuthentication.Encrypt(authTicket);
                HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                Response.Cookies.Add(faCookie);
                status = true;
                userId = user.Id;
            }
            else
            {
                status = false;
                Message = "Please Enter Correct Username or Password!";
            }
            var jsonResult = Json(new { status = status, message = Message, UserId = userId },
                   JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = Int32.MaxValue;
            return jsonResult;
        }


        private clsResult InsertUpdateCustomerDb(clsUsers st, string insertUpdateStatus)
        {
            //bool Status = false;
            //if (st.StatusString == "Active")
            //{
            //    Status = true;
            //}
            clsResult result = new clsResult();
            string returnId = "0";
            string connection = System.Configuration.ConfigurationManager.ConnectionStrings["ADO"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connection))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("spInsertUpdateUser", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = st.Id;
                        cmd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = st.UserName;
                        cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = st.Password;
                        //cmd.Parameters.Add("@TokenId", SqlDbType.NVarChar).Value = st.TokenId;
                        cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = st.FirstName;
                        cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = st.LastName;
                        cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = st.Email;
                        cmd.Parameters.Add("@Contact1", SqlDbType.NVarChar).Value = st.Contact1;
                        cmd.Parameters.Add("@Contact2", SqlDbType.NVarChar).Value = st.Contact2;
                        //cmd.Parameters.Add("@CompanyName", SqlDbType.NVarChar).Value = st.CompnayName;
                        //cmd.Parameters.Add("@Type", SqlDbType.NVarChar).Value = "Customer";
                       // cmd.Parameters.Add("@ReferenceAccountId", SqlDbType.Int).Value = st.ReferenceAccountId;
                        cmd.Parameters.Add("@Status", SqlDbType.Bit).Value = st.Status;
                       // cmd.Parameters.Add("@City", SqlDbType.NVarChar).Value = st.CityName;
                        //cmd.Parameters.Add("@PostalCode", SqlDbType.NVarChar).Value = st.PostalCode;
                        cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = st.Address;
                        cmd.Parameters.Add("@State", SqlDbType.NVarChar).Value = st.State;
                       
                        

                      //  cmd.Parameters.Add("@ActionByUserId", SqlDbType.Int).Value = 0;
                        cmd.Parameters.Add("@InsertUpdateStatus", SqlDbType.NVarChar).Value = insertUpdateStatus;
                        cmd.Parameters.Add("@CheckReturn", SqlDbType.NVarChar, 300).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@CheckReturn2", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        result.ResultMessage = cmd.Parameters["@CheckReturn"].Value.ToString();
                        result.Id = Convert.ToInt32(cmd.Parameters["@CheckReturn2"].Value.ToString());
                        cmd.Dispose();
                    }
                    con.Close();
                    con.Dispose();
                }
                catch (Exception ex)
                {
                    returnId = ex.Message.ToString();
                    int? userId = 0;
                   // clsSqlErrorLog.InsertError(userId, ex.Message.ToString(), "AccountName");
                    result.ResultMessage = ex.Message.ToString();
                    result.Id = 0;
                }
            }
            return result;
        }
    }
}