using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFPlusPlusBiggy.Model;

namespace WFPlusPlusBiggy
{
    public class DTO
    {
        public string userdata { get; set; }
    }

    public partial class _BiggySearch : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var theGoTo = Request.Params["hiddenRowNumber"];
            if (!String.IsNullOrWhiteSpace(theGoTo))
            {
                Response.Redirect("Detail.aspx?theId=" + theGoTo.ToString(), true);
            }
        }

        #region Get data method.

        /// <summary>
        /// GET: Default.aspx/GetData
        /// </summary>
        /// <returns>Return data</returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static object GetData()
        {
            // Initialization.
            DataTables result = new DataTables();

            try
            {
                // Initialization.

                string mysearch = HttpContext.Current.Request.Params["mydata1"];
                string search = HttpContext.Current.Request.Params["search[value]"];
                string draw = HttpContext.Current.Request.Params["draw"];
                string order = HttpContext.Current.Request.Params["order[0][column]"];
                string orderDir = HttpContext.Current.Request.Params["order[0][dir]"];
                int startRec = Convert.ToInt32(HttpContext.Current.Request.Params["start"]);
                int pageSize = Convert.ToInt32(HttpContext.Current.Request.Params["length"]);

                // Loading.
                List<GetbzSOD_Result> data = _BiggySearch.LoadData(mysearch);

                // Total record count.
                int totalRecords = data.Count;

                // Verification.
                if (!string.IsNullOrEmpty(search) &&
                    !string.IsNullOrWhiteSpace(search))
                {
                    // Apply search
                    data = data.Where(p => p.SalesOrderID.ToString().ToLower().Contains(search.ToLower()) ||
                                          p.SalesOrderDetailID.ToString().ToLower().Contains(search.ToLower()) ||
                                          p.CarrierTrackingNumber.ToLower().Contains(search.ToLower()) ||
                                          p.OrderQty.ToString().ToLower().Contains(search.ToLower()) ||
                                          p.ProductID.ToString().ToLower().Contains(search.ToLower()) ||
                                          p.SpecialOfferID.ToString().ToLower().Contains(search.ToLower()) ||
                                          p.UnitPrice.ToString().ToLower().Contains(search.ToLower()) ||
                                          p.UnitPriceDiscount.ToString().ToLower().Contains(search.ToLower()) ||
                        //p.LineTotal.ToString().ToLower().Contains(search.ToLower()) ||
                                          p.rowguid.ToString().ToLower().Contains(search.ToLower()) ||
                                          p.ModifiedDate.ToString().ToLower().Contains(search.ToLower())
                                     ).ToList();
                }

                // Sorting.
                data = _BiggySearch.SortByColumnWithOrder(order, orderDir, data);

                // Filter record count.
                int recFilter = data.Count;

                // Apply pagination.
                data = data.Skip(startRec).Take(pageSize).ToList();

                // Loading drop down lists.
                result.draw = Convert.ToInt32(draw);
                result.recordsTotal = totalRecords;
                result.recordsFiltered = recFilter;
                result.data = data;
            }
            catch (Exception ex)
            {
                // Info
                Console.Write(ex);
            }

            // Return info.
            //return result.data;
            return result;
        }

        #endregion

        #region Helpers

        #region Load Data

        /// <summary>
        /// Load data method.
        /// </summary>
        /// <returns>Returns - Data</returns>
        private static List<GetbzSOD_Result> LoadData(string mysearch)
        {
            // Initialization.
            //List<bzSOD> lst = new List<bzSOD>();
            var lst = new List<GetbzSOD_Result>();

            try
            {

                using (SqlConnection conn =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["DAL"].ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand comm = conn.CreateCommand())
                    {
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.CommandText = "GetbzSOD";

                        Int32? thesearch = null;
                        if (String.IsNullOrWhiteSpace(mysearch))
                        {
                            thesearch = null;
                        }
                        else
                        {
                            thesearch = Int32.Parse(mysearch);
                            comm.Parameters.Add(new SqlParameter("SalesOrderDetailId", thesearch));
                        }

                        using (SqlDataReader sr = comm.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                        {
                            while (sr.Read())
                            {
                                GetbzSOD_Result res = new GetbzSOD_Result();
                                res.SalesOrderID = sr.GetValue(0) != null ? Int32.Parse(sr.GetValue(0).ToString()) : 0;
                                res.SalesOrderDetailID = Int32.Parse(sr.GetValue(1).ToString());
                                res.CarrierTrackingNumber = sr.GetValue(2).ToString();
                                res.OrderQty = sr.GetValue(3) != null ? Int16.Parse(sr.GetValue(3).ToString()) : Int16.Parse("0");
                                res.ProductID = sr.GetValue(4) != null ? Int32.Parse(sr.GetValue(4).ToString()) : 0;
                                res.SpecialOfferID = sr.GetValue(5) != null ? Int32.Parse(sr.GetValue(5).ToString()) : 0;
                                res.UnitPrice = sr.GetValue(6) != null ? Decimal.Parse(sr.GetValue(6).ToString()) : 0;
                                res.UnitPriceDiscount = sr.GetValue(7) != null ? Decimal.Parse(sr.GetValue(7).ToString()) : 0;
                                res.rowguid = sr.GetValue(8) != null ? Guid.Parse(sr.GetValue(8).ToString()) : Guid.Empty;
                                res.ModifiedDate = sr.GetValue(9) != null ? DateTime.Parse(sr.GetValue(9).ToString()) : DateTime.Now;

                                if (!String.IsNullOrWhiteSpace(sr.GetValue(10).ToString()))
                                {
                                    res.ForeignToNames = Int32.Parse(sr.GetValue(10).ToString());
                                }

                                lst.Add(res);

                            }
                        }

                        conn.Close();

                    }

                };

                //using (MyEntities entity = new MyEntities())
                //{
                //    //lst = entity.bzSODs.Where(r => !String.IsNullOrEmpty(r.CarrierTrackingNumber)).ToList();
                //    //lst = entity.bzSODs.ToList();
                //    Int32 ?thesearch = null;
                //    if (String.IsNullOrWhiteSpace(mysearch))
                //    {
                //        thesearch = null;
                //    }
                //    else
                //    {
                //        thesearch = Int32.Parse(mysearch);
                //    }

                //    lst = entity.GetbzSOD   (
                //        salesOrderDetailId: mysearch != String.Empty ? thesearch : null
                //                            ).ToList();
                //}

            }
            catch (Exception ex)
            {
                // info.
                //Console.Write(ex);
            }

            // info.
            return lst;
        }

        #endregion

        #region Sort by column with order method

        /// <summary>
        /// Sort by column with order method.
        /// </summary>
        /// <param name="order">Order parameter</param>
        /// <param name="orderDir">Order direction parameter</param>
        /// <param name="data">Data parameter</param>
        /// <returns>Returns - Data</returns>
        //private static List<SalesOrderDetail> SortByColumnWithOrder(string order, string orderDir, List<SalesOrderDetail> data)
        private static List<GetbzSOD_Result> SortByColumnWithOrder(string order, string orderDir, List<GetbzSOD_Result> data)
        {
            // Initialization.
            //List<SalesOrderDetail> lst = new List<SalesOrderDetail>();
            List<GetbzSOD_Result> lst = new List<GetbzSOD_Result>();

            try
            {
                // Sorting
                switch (order)
                {
                    case "0":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.SalesOrderID).ToList()
                                                                                                 : data.OrderBy(p => p.SalesOrderID).ToList();
                        break;

                    case "1":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.SalesOrderDetailID).ToList()
                                                                                                 : data.OrderBy(p => p.SalesOrderDetailID).ToList();
                        break;

                    case "2":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.CarrierTrackingNumber).ToList()
                                                                                                 : data.OrderBy(p => p.CarrierTrackingNumber).ToList();
                        break;

                    case "3":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.OrderQty).ToList()
                                                                                                 : data.OrderBy(p => p.OrderQty).ToList();
                        break;

                    case "4":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.ProductID).ToList()
                                                                                                   : data.OrderBy(p => p.ProductID).ToList();
                        break;

                    case "5":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.SpecialOfferID).ToList()
                                                                                                 : data.OrderBy(p => p.SpecialOfferID).ToList();
                        break;

                    case "6":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.UnitPrice).ToList()
                                                                                                 : data.OrderBy(p => p.UnitPrice).ToList();
                        break;

                    case "7":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.UnitPriceDiscount).ToList()
                                                                                                 : data.OrderBy(p => p.UnitPriceDiscount).ToList();
                        break;

                    //case "8":
                    //    // Setting.
                    //    lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.LineTotal).ToList()
                    //                                                                             : data.OrderBy(p => p.LineTotal).ToList();
                    //    break;

                    case "8":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.rowguid).ToList()
                                                                                                 : data.OrderBy(p => p.rowguid).ToList();
                        break;

                    case "9":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.ModifiedDate).ToList()
                                                                                                 : data.OrderBy(p => p.ModifiedDate).ToList();
                        break;

                    default:

                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.SalesOrderDetailID).ToList()
                                                                                                 : data.OrderBy(p => p.SalesOrderDetailID).ToList();
                        break;
                }
            }
            catch (Exception ex)
            {
                // info.
                Console.Write(ex);
            }

            // info.
            return lst;
        }

        #endregion

        #endregion
    }
}