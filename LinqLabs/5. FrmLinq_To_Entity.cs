using LinqLabs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Starter
{
    public partial class FrmLinq_To_Entity : Form
    {
        public FrmLinq_To_Entity()
        {
            InitializeComponent();
            
            this.dbConect.Database.Log = Console.Write;
            //將資料存取詳細顯示在"輸出"欄裡,看的時候再使用,一直開著會耗效能
        }

        NorthwindEntities dbConect = new NorthwindEntities();
        private void button1_Click(object sender, EventArgs e)
        {
            
            var q = from n in dbConect.Products
                    where n.UnitPrice>30
                    select n;

            this.dataGridView1.DataSource = q.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = this.dbConect.Categories.First().Products.ToList();
            //Categories裡的Product第一號的資料
            MessageBox.Show(this.dbConect.Products.First().Category.CategoryName);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource =
                this.dbConect.Sales_by_Year(new DateTime(1997, 1, 1),new DateTime(1998,1,1)).ToList();
            //Sales_by_Year(new DateTime(1997, 1, 1),new DateTime(1998,1,1)):開始到結束(到現在:DataTime.Now)
        }
        #region 方法
        //在region旁邊可以改名稱
        private void button22_Click(object sender, EventArgs e)
        {//排序:先用第一種，如果第一種相同，才用then第二種
            var q = from n in this.dbConect.Products.AsEnumerable()
                    orderby n.UnitsInStock descending, n.ProductID descending
                    select new
                    {
                        n.ProductID,
                        n.ProductName,
                        UnitPrice = $"{n.UnitPrice:f2}",
                        n.UnitsInStock,
                        //System.NotSupportedException: 'LINQ to Entities 無法辨識方法 'System.String Format(System.String, System.Object)' 方法，而且這個方法無法轉譯成存放區運算式。'
                        //AsEnumerable() 可以編輯
                        TotalPrice = $"{n.UnitPrice * n.UnitsInStock:C2}" 
                    };
            this.dataGridView1.DataSource = q.ToList();
            //==========================================================
            var q2 = this.dbConect.Products.OrderByDescending(n => n.UnitsInStock)
                .ThenByDescending(n => n.ProductID);
            this.dataGridView2.DataSource = q2.ToList();
        }
        #endregion

        private void button16_Click(object sender, EventArgs e)
        {
            var q = from n in dbConect.Products.AsEnumerable()
                    select new { 
                        n.CategoryID, 
                        n.Category.CategoryName,
                        n.ProductName, 
                        UnitPrice =$"{n.UnitPrice:c2}"
                    };
            this.dataGridView3.DataSource = q.ToList();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            //inner join
            var q = from n in dbConect.Categories.AsEnumerable()
                    from p in n.Products
                    select new
                    {
                        n.CategoryID,
                        n.CategoryName,
                        p.ProductName,
                        UnitPrice = $"{p.UnitPrice:c2}"
                    };
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var q = from n in this.dbConect.Products.AsEnumerable()
                    group n by n.Category.CategoryName into g
                    select new
                    {
                        CategoryName = g.Key,
                        AVGUniPrice =$"{ g.Average(n => n.UnitPrice):c2}"
                    };
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            bool? b;
            b = true;
            b = false;
            b = null;

            var q = from n in this.dbConect.Orders
                    group n by n.OrderDate.Value.Year into g
                    orderby g.Key ascending
                    select new
                    {
                        Year = g.Key,
                        Count = g.Count()
                    };
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button55_Click(object sender, EventArgs e)
        {
            Product prod = new Product { ProductName = "XXXXX", Discontinued = true };
            this.dbConect.Products.Add(prod);
            this.dbConect.SaveChanges();

        }
    }
}
