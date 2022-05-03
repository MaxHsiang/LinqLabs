using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Starter
{
    public partial class FrmLINQ_To_XXX : Form
    {
        public FrmLINQ_To_XXX()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //IEnumerable=可列舉的
            IEnumerable<IGrouping<string, int>> q = from n in nums
                                                 group n by n % 2 ==0?"偶數":"奇數";//by key,n%2 如果等於0就是偶數,否則是奇數
            this.dataGridView1.DataSource = q.ToList();

            //====================================
            foreach(var group in q)
            {
               TreeNode node =  this.treeView1.Nodes.Add(group.Key.ToString());
                foreach(var items in group)
                {
                    node.Nodes.Add(items.ToString());
                }

            }

            foreach (var group in q)
            {
                ListViewGroup lvg = this.listView1.Groups.Add(group.Key.ToString(), group.Key.ToString());
                foreach (var items in group)
                {
                    listView1.Items.Add(items.ToString()).Group = lvg;
                }

            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10,11,12,13 };
            //IEnumerable=可列舉的
            var q = from n in nums
                    group n by n % 2 == 0 ? "偶數" : "奇數" into g
                    select new
                    {
                        MyKey = g.Key,
                        MySum = g.Sum(),
                        MyMax = g.Max(),
                        MyMin = g.Min(),
                        MyCount = g.Count(),
                        MyAvg = g.Average(),
                        MyGroup = g
                    };
            this.dataGridView1.DataSource = q.ToList();
            //==============================
            foreach (var group in q)
            {
                string s = $"{group.MyKey}({group.MyCount})";//在奇數偶數旁邊加上數量
                TreeNode node = this.treeView1.Nodes.Add(group.MyKey.ToString(),s);//s:node的標示
                foreach (var items in group.MyGroup) 
                {
                    node.Nodes.Add(items.ToString());
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
            //IEnumerable=可列舉的
            var q = from n in nums
                    group n by MyKey(n) into g
                    select new
                    {
                        MyKey = g.Key,
                        MySum = g.Sum(),
                        MyMax = g.Max(),
                        MyMin = g.Min(),
                        MyCount = g.Count(),
                        MyAvg = g.Average(),
                        MyGroup = g
                    };
            this.dataGridView1.DataSource = q.ToList();

            foreach (var group in q)
            {
                string s = $"{group.MyKey}({group.MyCount})";//在奇數偶數旁邊加上數量
                TreeNode node = this.treeView1.Nodes.Add(group.MyKey.ToString(), s);//s:node的標示
                foreach (var items in group.MyGroup)
                {
                    node.Nodes.Add(items.ToString());
                }

            }

            //=======================
            this.chart1.DataSource = q.ToList();
            this.chart1.Series[0].XValueMember ="Mykey";
            this.chart1.Series[0].YValueMembers = "MyCount";
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            this.chart1.Series[1].XValueMember = "Mykey";
            this.chart1.Series[1].YValueMembers = "MyAvg";
            this.chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

        }

        private string MyKey(int n)
        {
            if (n < 5)
                return "Small";
            else if (n < 10)
                return "Medium";
            else
                return "Large";
        }

        private void button38_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new DirectoryInfo(@"C:\Windows");
            FileInfo[] file = dir.GetFiles();
            this.dataGridView1.DataSource = file;
            //==========================
            var q = from n in file
                    group n by n.Extension into g
                    orderby g.Count() descending
                    select new
                    {
                        g.Key,Mycount = g.Count()
                    };
            this.dataGridView2.DataSource = q.ToList();
            
        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.ordersTableAdapter1.Fill(this.nwDataSet1.Orders);
            var q = from n in nwDataSet1.Orders
                    group n by n.OrderDate.Year into g
                    orderby g.Key descending
                    select new
                    {
                        OrderYear = g.Key,
                        OrderCount = g.Count()
                    };
            this.dataGridView2.DataSource = q.ToList();

            //==========================================
            int count = (from n in nwDataSet1.Orders
                     where n.OrderDate.Year == 1997
                     select n).Count();

            MessageBox.Show("Count = " + count);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //System.IO.DirectoryInfo dir = new DirectoryInfo(@"C:\Windows");
            //FileInfo[] files = dir.GetFiles();
            //this.dataGridView1.DataSource = files;
            System.IO.DirectoryInfo dir = new DirectoryInfo(@"C:\Windows");
            FileInfo[] files = dir.GetFiles();
            this.dataGridView1.DataSource = files;

            int q = (from n in files
                     let s = n.Extension
                     where s == ".exe"
                     select n).Count();

            MessageBox.Show(".exe共有: " + q);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string s = "This a book. this is a pen. this is an apple";
            char[] chars = { ' ', ',', '.', '?' };
            string[] words = s.Split(chars,StringSplitOptions.RemoveEmptyEntries);
            //StringSplitOptions.RemoveEmptyEntries : 刪除字串裡的空白

            var q = from n in words
                    group n by n.ToUpper() into g
                    select new
                    {
                        MyKey = g.Key, MyCount = g.Count()
                    };
            this.dataGridView1.DataSource = q.ToList();
   
        }


        private void button14_Click(object sender, EventArgs e)
        {
            int[] nums1 = { 1, 2, 3, 4, 5, 6, 7, 2 };
            int[] nums2 = { 1, 3, 5, 7, 9, 111, 222 };
            IEnumerable<int> q;
            q = nums1.Intersect(nums2);
            q = nums1.Distinct();
            q = nums1.Union(nums2);

            q = nums1.Take(2);
     

            bool result;
            result = nums1.Any(n => n > 5);
            result = nums1.All(n => n >= 1);

            int i;
            i = nums1.First();
            i = nums1.Last(); 
            i = nums1.ElementAt(3);
            i = nums1.ElementAtOrDefault(13);

            var w1 = Enumerable.Range(1, 1000).Select(n => new { n });//Range: 1~1000 
            this.dataGridView1.DataSource = w1.ToList();
            var w2 = Enumerable.Repeat(666, 1000).Select(n => new { n });// Repeat: 666*10000
            this.dataGridView2.DataSource = w2.ToList();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.categoriesTableAdapter1.Fill(this.nwDataSet1.Categories);
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);
            var q = from n in nwDataSet1.Products
                    group n by n.CategoryID into g
                    orderby g.Key ascending
                    select new { CategoryID = g.Key, MyAVG = $"{g.Average(p => p.UnitPrice):f2}" };
            this.dataGridView1.DataSource = q.ToList();

            //==================================================

            var a = from c in this.nwDataSet1.Categories join p in this.nwDataSet1.Products
                    on c.CategoryID equals p.CategoryID
                    group p by c.CategoryName into g
                    
                    select new { CategoryName = g.Key, MyAVG = $"{g.Average(p => p.UnitPrice):f2}" };
            this.dataGridView2.DataSource = a.ToList();
        }
    }
}
