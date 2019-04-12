using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WFPlusPlusBiggy.Model
{
    public class DataTables
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        //public List<SalesOrderDetail> data { get; set; }
        public List<GetbzSOD_Result> data { get; set; }
    }
}