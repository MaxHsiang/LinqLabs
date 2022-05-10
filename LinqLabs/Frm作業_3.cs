using LinqLabs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHomeWork
{
    public partial class Frm作業_3 : Form
    {
        public Frm作業_3()
        {
            InitializeComponent();
        }
        #region int[]  分三群 - No LINQ
        private void button4_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            var q = from n in nums
                    group n by MyKey(n) into g
                    select new
                    {
                        MyKey = g.Key,
                        MyCount = g.Count(),
                        MyGroup = g
                    };
            foreach (var group in q)
            {
                string s = $"{group.MyKey}({group.MyCount})";
                TreeNode node = this.treeView1.Nodes.Add(group.MyKey.ToString(), s);//s:node的標示
                foreach (var items in group.MyGroup)
                {
                    node.Nodes.Add(items.ToString());
                }

            }
        }
        private object MyKey(int n)
        {
            if (n < 5)
                return "Small";
            else if (n < 10)
                return "Medium";
            else
                return "Large";
        }
        #endregion

        #region 依 檔案大小 分組檔案 (大=>小)
        private void button38_Click(object sender, EventArgs e)
        {
            this.treeView1.Nodes.Clear();
            System.IO.DirectoryInfo dir = new DirectoryInfo(@"C:\Windows");
            FileInfo[] file = dir.GetFiles();
            this.dataGridView1.DataSource = file;
            var q = from n in file
                    group n by MyLength(n) into g
                    orderby g.Key descending
                    select new
                    {
                        Length = g.Key,
                        Count = g.Count(),
                        MyGroup = g
                    };
            
            foreach (var group in q)
            {
                string s = $"{group.Length}({group.Count})";
                TreeNode node = this.treeView1.Nodes.Add(group.Length.ToString(), s);//s:node的標示
                foreach (var items in group.MyGroup)
                {
                    node.Nodes.Add($"{items.ToString()}{"- 容量:"}{items.Length:f1}");
                }
            }
        }
        private object MyLength(FileInfo n)
        {
            if (n.Length < 10000)
                return "小";
            else if (n.Length < 250000)
                return "普通";
            else
                return "大";
        }
        #endregion

        #region   依 年 分組檔案 (大=>小)
        private void button6_Click(object sender, EventArgs e)
        {
            this.treeView1.Nodes.Clear();
            System.IO.DirectoryInfo dir = new DirectoryInfo(@"C:\Windows");
            FileInfo[] file = dir.GetFiles();
            this.dataGridView1.DataSource = file;

            var q = from n in file
                    group n by n.CreationTime.Year into g
                    select new
                    {
                        Year = g.Key,
                        MyCount = g.Count(),
                        MyGroup = g
                    };

               this.dataGridView2.DataSource = q.ToList();
            foreach(var g in q)
            {
                string s = $"{g.Year}({g.MyCount})";
                TreeNode node = this.treeView1.Nodes.Add(g.Year.ToString(),s);
                foreach(var items in g.MyGroup)
                {
                    node.Nodes.Add($"{items.ToString()}{"- 容量:"}{items.Length:f1}");
                }
            }

        }
        #endregion

        #region NW Products 低中高 價產品 
        NorthwindEntities dbContext = new NorthwindEntities();
        private void button8_Click(object sender, EventArgs e)
        {
            this.treeView1.Nodes.Clear();
            var q = from n in this.dbContext.Products.AsEnumerable()
                    group n by MyPrice(n) into g
                    select new
                    {
                        MyPrice = g.Key,
                        MyCount = g.Count(),
                        MyGroup = g
                    };
            foreach (var g in q)
            {
                string s = $"{g.MyPrice}({g.MyCount})";
                TreeNode node = this.treeView1.Nodes.Add(g.MyPrice.ToString(), s);
                foreach (var items in g.MyGroup)
                {
                    node.Nodes.Add($"{items.ProductName.ToString()}({items.UnitPrice:C2})");
                }
            }
        }

        private object MyPrice(Product n)
        {
            if (n.UnitPrice < 30)
                return "低價";
            else if (n.UnitPrice < 50)
                return "中價";
            else
                return "高價";
        }
        #endregion

        #region  Orders -  Group by 年
        private void button15_Click(object sender, EventArgs e)
        {
            this.treeView1.Nodes.Clear();
            var q = from n in this.dbContext.Orders
                    group n by n.OrderDate.Value.Year into g
                    orderby g.Key descending
                    select new
                    {
                        OrderDate = g.Key,
                        MyCount = g.Count(),
                        MyGroup = g
                    };
            foreach (var g in q)
            {
                string s = $"{g.OrderDate}({g.MyCount})";
                TreeNode node = this.treeView1.Nodes.Add(g.OrderDate.ToString(), s);
                foreach (var items in g.MyGroup)
                {
                    node.Nodes.Add($"{items.CustomerID.ToString()}({items.OrderID.ToString()})");
                }
            }
        }
        #endregion

        #region  Orders -  Group by 年 / 月
        private void button10_Click(object sender, EventArgs e)
        {
            this.treeView1.Nodes.Clear();
            var q = from n in this.dbContext.Orders
                    group n by new { n.OrderDate.Value.Year,n.OrderDate.Value.Month } into g
                    orderby  g.Key descending
                    select new
                    {
                        OrderYear = g.Key,
                        Mycount = g.Count(),
                        MyGroup = g
                    };
            foreach(var g in q)
            {
                //string s = $"{g.OrderYear.Value.Year}({g.OrderYear.Value.Month})";
                 TreeNode node = this.treeView1.Nodes.Add(g.OrderYear.ToString());
                foreach(var items in g.MyGroup)
                {
                    node.Nodes.Add(items.OrderID.ToString());
                }
            }
        }
        #endregion

        #region 總銷售金額
        private void button2_Click(object sender, EventArgs e)
        {
            this.treeView1.Nodes.Clear();
            var q = from n in this.dbContext.Order_Details.AsEnumerable()
                    group n by n.Order.OrderDate.Value.Year into g
                    select new
                    {
                        Year = g.Key,
                        Performance  = g.Sum(n => n.UnitPrice * n.Quantity * (decimal)(1 - n.Discount)),
                       
                    };
            this.dataGridView1.DataSource = q.ToList();

            foreach (var g in q)
            {
                TreeNode node = this.treeView1.Nodes.Add(g.Year.ToString());
                node.Nodes.Add(g.Performance.ToString());
            }
            var q1 = this.dbContext.Order_Details.AsEnumerable().Sum(n => n.UnitPrice * n.Quantity / (decimal)(1 - n.Discount));
            MessageBox.Show("銷售總金額是:\n"+$"{q1:c2}");
        }
        #endregion

        #region 銷售最好的top 5業務員
        private void button1_Click(object sender, EventArgs e)
        {
            var q = (from n in this.dbContext.Order_Details.AsEnumerable()
                     group n by new { n.Order.Employee.FirstName, n.Order.Employee.LastName } into g
                     orderby g.Sum(n => n.UnitPrice * n.Quantity * (decimal)(1 - n.Discount)) descending
                     select new
                     {
                         EmployeeName = $"{g.Key.FirstName}{g.Key.LastName}",
                         ToTalPerformance =
                         ($"{g.Sum(n => n.UnitPrice * n.Quantity * (decimal)(1 - n.Discount)):c2}")
                     }).Take(5);
            this.dataGridView1.DataSource = q.ToList();
        }
        #endregion

        #region  NW 產品最高單價前 5 筆 (包括類別名稱)
        private void button9_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = null;
            var q = (from n in this.dbContext.Products.AsEnumerable()
                     orderby n.UnitPrice descending
                     select new
                     {
                         ProductName = n.ProductName,
                         Category = n.Category.CategoryName,
                         UnitPrice = $"{n.UnitPrice:c2}",
                     }).Take(5);
            this.dataGridView1.DataSource = q.ToList();
        }

        #endregion

        #region  NW 產品有任何一筆單價大於300 ?
        private void button7_Click(object sender, EventArgs e)
        {
           bool result = this.dbContext.Products.Any(n=>n.UnitPrice >300);
            MessageBox.Show("是否有單價大於300商品?\n"+result);
        }
        #endregion
    }
}
