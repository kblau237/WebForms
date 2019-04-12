using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFPlusPlusBiggy.Model;

namespace WFPlusPlusBiggy
{
    public partial class _Reference : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            thePlaceHolder.Controls.Clear();

            TableRow row = new TableHeaderRow();

            Table table = new Table();
            table.ID = "example";
            table.CssClass = "display table table-striped table-bordered";

            var cellaheader = new TableHeaderCell();
            row.TableSection = TableRowSection.TableHeader;

            cellaheader.Text = "Id";

            row.Cells.Add(cellaheader);
            var cell1header = new TableHeaderCell();
            row.TableSection = TableRowSection.TableHeader;
            cell1header.Text = "Name";
            row.Cells.Add(cell1header);
            var cell2header = new TableHeaderCell();
            row.TableSection = TableRowSection.TableHeader;
            cell2header.Text = "Position";
            row.Cells.Add(cell2header);
            var cell3header = new TableHeaderCell();
            row.TableSection = TableRowSection.TableHeader;
            cell3header.Text = "Office";
            row.Cells.Add(cell3header);
            var cell4header = new TableHeaderCell();
            row.TableSection = TableRowSection.TableHeader;
            cell4header.Text = "Age";
            row.Cells.Add(cell4header);
            var cell5header = new TableHeaderCell();
            row.TableSection = TableRowSection.TableHeader;
            cell5header.Text = "Start date";
            row.Cells.Add(cell5header);
            var cell6header = new TableHeaderCell();
            row.TableSection = TableRowSection.TableHeader;
            cell6header.Text = "Salary";
            row.Cells.Add(cell6header);
            var cell7header = new TableHeaderCell();
            row.TableSection = TableRowSection.TableHeader;
            cell7header.Text = "Edit";
            row.Cells.Add(cell7header);

            var cell8header = new TableHeaderCell();
            row.TableSection = TableRowSection.TableHeader;
            cell8header.Text = "Del";
            row.Cells.Add(cell8header);

            List<bzFDT> fre = new List<bzFDT>();
            using (MyEntities entity = new MyEntities())
            {
                fre = entity.bzFDTs.ToList();
            }

            table.Rows.Add(row);

            foreach (bzFDT item in fre)
            {
                TableRow arow = new TableRow();
                foreach (var prop in item.GetType().GetProperties())
                {
                    if (prop.Name == "bzSODs")
                    {
                        continue;
                    }
                    TableCell cellContent = new TableCell();
                    cellContent.Text = prop.GetValue(item, null).ToString();
                    arow.Cells.Add(cellContent);
                }

                TableCell cellEdit = new TableCell();
                cellEdit.Text = "Edit";
                cellEdit.CssClass = "theEdit";
                arow.Cells.Add(cellEdit);

                TableCell cellDel = new TableCell();
                cellDel.Text = "Del";
                cellDel.CssClass = "theDel";
                arow.Cells.Add(cellDel);

                table.Rows.Add(arow);

            }

            var cell3 = new TableCell();

            thePlaceHolder.Controls.Add(table);
        }

        protected void SaveModal_Click(Object sender, EventArgs e)
        {
            //if id is spaces then add, else edit
            var Id = param0.Text;
            var Name = param1.Text;
            var Position = param2.Text;
            var Office = param3.Text;
            var Age = param4.Text;
            var StartDate = param5.Text;
            var Salary = param6.Text;

            //add
            if (String.IsNullOrWhiteSpace(Id))
            {
                using (MyEntities entity = new MyEntities())
                {
                    entity.bzFDTs.Add(new bzFDT
                    {
                        Age = Int32.Parse(Age),
                        Name = Name,
                        Office = Office,
                        Position = Position,
                        Salary = Decimal.Parse(Salary),
                        StartDate = DateTime.Parse(StartDate)
                    });
                    entity.SaveChanges();
                }
            }
            else
            {
                using (MyEntities entity = new MyEntities())
                {
                    bzFDT bz = entity.bzFDTs.Find(Int32.Parse(Id));
                    bz.Age = Int32.Parse(Age);
                    bz.Name = Name;
                    bz.Position = Position;
                    bz.Salary = Decimal.Parse(Salary);
                    bz.StartDate = DateTime.Parse(StartDate);
                    bz.Office = Office;
                    entity.Entry(bz).State = EntityState.Modified;
                    entity.SaveChanges();
                }

            }

            Page_Load(null, null);
        }


        protected void DeleteModal_Click(Object sender, EventArgs e)
        {
            var Id = this.theDeleteId.Value;
            using (MyEntities entity = new MyEntities())
            {
                bzFDT bz = entity.bzFDTs.Find(Int32.Parse(Id));
                entity.bzFDTs.Remove(bz);
                entity.SaveChanges();
            }

            Page_Load(null, null);
        }
    }
}