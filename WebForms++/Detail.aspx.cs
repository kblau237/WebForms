using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFPlusPlusBiggy.Model;

namespace WFPlusPlusBiggy
{
    public class SelectListItem
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }

    public class NameValue
    {
        public string name { get; set; }
        public string value { get; set; }

    }

    public class Validation
    {
        public string key { get; set; }
        public string error { get; set; }
    }

    public partial class _Detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["theId"] != "add")
            {
                this.theHiddenValue.Value = Request.QueryString["theId"];
            }
        }

        [WebMethod]
        public static string Delete(string theId) //string GivenStaffID
        {
            using (MyEntities entity = new MyEntities())
            {
                bzSOD bz = entity.bzSODs.Find(Int32.Parse(theId));
                entity.bzSODs.Remove(bz);
                entity.SaveChanges();
            }

            return "Success";
        }

        [WebMethod]
        public static object GetSelecting()
        {
            List<SelectListItem> ddls = new List<SelectListItem>();

            using (MyEntities entity = new MyEntities())
            {
                entity.bzFDTs.ToList().ForEach(r => ddls.Add(new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Id.ToString()
                }
                ));
            }

            return JsonConvert.SerializeObject(ddls);
        }

        [WebMethod]
        public static object GetFields(string theHiddenValue)
        {
            List<bzSOD> fields = new List<bzSOD>();
            if (!String.IsNullOrWhiteSpace(theHiddenValue))
            {
                var salesorderdetailid = Int32.Parse(theHiddenValue);

                using (MyEntities entity = new MyEntities())
                {
                    entity.bzSODs.Where(r => r.SalesOrderDetailID ==
                        salesorderdetailid).
                        ToList().ForEach(r => fields.Add(new bzSOD
                    {
                        CarrierTrackingNumber = r.CarrierTrackingNumber,
                        ForeignToNames = r.ForeignToNames,
                        ModifiedDate = r.ModifiedDate,
                        OrderQty = r.OrderQty,
                        ProductID = r.ProductID,
                        rowguid = r.rowguid,
                        SalesOrderDetailID = r.SalesOrderDetailID,
                        SalesOrderID = r.SalesOrderID,
                        SpecialOfferID = r.SpecialOfferID,
                        UnitPrice = r.UnitPrice,
                        UnitPriceDiscount = r.UnitPriceDiscount
                    }
                    ));
                }

            }
            else
            {
                return "Add";
            }

            return JsonConvert.SerializeObject(fields);
        }

        [WebMethod(EnableSession = true)]
        public static string PostDetail(NameValue[] formVars)
        {
            var AddMode = false;

            foreach (NameValue nameValue in formVars)
            {
                if (nameValue.name == "hiddenSalesOrderDetailId" && nameValue.value == "")
                {
                    AddMode = true;
                }
            }
            
            Dictionary<string, string> todb = new Dictionary<string, string>();

            IList<Validation> validationList = new List<Validation>();

            bzSOD obj = new bzSOD();
            foreach (NameValue nameValue in formVars)
            {
                if (!nameValue.name.Contains("hidden") && !nameValue.name.Contains("txt"))
                {
                    continue;
                }

                var theName = String.Empty;
                if (nameValue.name.Contains("hidden"))
                {
                    theName = nameValue.name.Substring(6);
                }
                if (nameValue.name.Contains("txt"))
                {
                    theName = nameValue.name.Substring(3);
                }

                if (AddMode)
                {
                    switch (theName)
                    {
                        case "rowguid":
                            nameValue.value = Guid.NewGuid().ToString();
                            break;
                        case "ModifiedDate":
                            nameValue.value = DateTime.Now.ToShortDateString();
                            break;
                        default:
                            break;
                    }
                }

                PropertyInfo prop = obj.GetType().GetProperty(theName, BindingFlags.Public | BindingFlags.Instance);
                try
                {
                    if (null != prop && prop.CanWrite)
                    {
                        if (prop.PropertyType.Name == "Int16")
                        {
                            prop.SetValue(obj, Int16.Parse(nameValue.value), null);
                        }
                        if (prop.PropertyType.Name == "Int32")
                        {
                            prop.SetValue(obj, Int32.Parse(nameValue.value), null);
                        }
                        if (prop.PropertyType.Name == "String")
                        {
                            prop.SetValue(obj, nameValue.value, null);
                        }
                        if (prop.PropertyType.Name == "DateTime")
                        {
                            prop.SetValue(obj, DateTime.Parse(nameValue.value), null);
                        }
                    }
                }
                catch (Exception e)
                {
                    Validation val = new Validation { key = "txt" + prop.Name, error = "Problem with " + prop.Name };
                    validationList.Add(val);
                }

                todb.Add(theName, nameValue.value);
            }

            //did nuget JSON-js json2 

            if (validationList.Count == 0)
            {
                if (String.IsNullOrWhiteSpace(todb["SalesOrderDetailId"])) //add
                {
                    using (MyEntities entity = new MyEntities())
                    {
                        entity.bzSODs.Add(new bzSOD
                        {
                            CarrierTrackingNumber = todb["CarrierTrackingNumber"],
                            ForeignToNames = Int32.Parse(todb["MySelect"]),
                            ModifiedDate = DateTime.Parse(todb["ModifiedDate"]),
                            OrderQty = Int16.Parse(todb["OrderQty"]),
                            ProductID = Int32.Parse(todb["ProductID"]),
                            rowguid = Guid.Parse(todb["rowguid"]),
                            SalesOrderID = Int32.Parse(todb["SalesOrderID"]),
                            SpecialOfferID = Int32.Parse(todb["SpecialOfferID"]),
                            UnitPrice = Decimal.Parse(todb["UnitPrice"]),
                            UnitPriceDiscount = Decimal.Parse(todb["UnitPriceDiscount"])

                        });
                        entity.SaveChanges();
                    }
                }
                else
                {
                    //edit to db
                    using (MyEntities entity = new MyEntities())
                    {
                        int salesOrderDetailId = Int32.Parse(todb["SalesOrderDetailId"]);
                        bzSOD bz = entity.bzSODs.Find(salesOrderDetailId);
                        bz.CarrierTrackingNumber = todb["CarrierTrackingNumber"];
                        bz.ForeignToNames = Int32.Parse(todb["MySelect"]);
                        bz.ModifiedDate = DateTime.Parse(todb["ModifiedDate"]);
                        bz.OrderQty = Int16.Parse(todb["OrderQty"]);
                        bz.ProductID = Int32.Parse(todb["ProductID"]);
                        bz.SalesOrderID = Int32.Parse(todb["SalesOrderID"]);
                        bz.SpecialOfferID = Int32.Parse(todb["SpecialOfferID"]);
                        bz.UnitPrice = Decimal.Parse(todb["UnitPrice"]);
                        bz.UnitPriceDiscount = Decimal.Parse(todb["UnitPriceDiscount"]);

                        entity.Entry(bz).State = EntityState.Modified;
                        entity.SaveChanges();
                    }

                }

                return "Success";
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string output = serializer.Serialize(validationList);
            return output;
        }
    }
}