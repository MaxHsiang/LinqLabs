using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Starter
{
    public partial class FrmHelloLinq : Form
    {
        public FrmHelloLinq()
        {
            InitializeComponent();
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);
            this.ordersTableAdapter1.Fill(this.nwDataSet1.Orders);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            foreach(int n in nums)
            {
                listBox1.Items.Add(n);
            }
            this.listBox1.Items.Add("===================");

            System.Collections.IEnumerator en = nums.GetEnumerator();

            while(en.MoveNext())
            {
                this.listBox1.Items.Add(en.Current);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            foreach(int n in list)
            {
                this.listBox1.Items.Add(n);
            }
            //=================================
            int n2 = 100;
            var n1 = 200;
            this.listBox1.Items.Add("===================");
            List<int>.Enumerator en = list.GetEnumerator();
            while (en.MoveNext())
            {
                this.listBox1.Items.Add(en.Current);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            IEnumerable<int> q = from n in nums
                                     //where (n >=5 && n <= 8) && (n%2 == 0)
                                 where (n >= 1 && n <= 10) && (n%2 ==1)
                                 select n;
            foreach(int n in q)
            {
                this.listBox1.Items.Add(n);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            IEnumerable<int> q = from n in nums
                                 where IsEven(n)
                                 select n;
            foreach (int n in q)
            {
                this.listBox1.Items.Add(n);
            }
        }
        bool IsEven (int n)
        {
            //if(n%2==0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}

            return n % 2 == 0;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            IEnumerable<Point> q = from n in nums
                                 where n > 5
                                 select new Point(n,n*n);
            foreach (Point pt in q)
            {
                this.listBox1.Items.Add(pt.X + ","+pt.Y);
            }
            //=============================
            List<Point> list = q.ToList();
            this.dataGridView1.DataSource = list;

            this.chart1.DataSource = list;
            this.chart1.Series[0].XValueMember = "X";
            this.chart1.Series[0].YValueMembers = "Y";
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] word = { "Apple", "PineApple", "XXXapple", "Banana" };
            IEnumerable<string> q = from w in word
                                     where w.ToUpper().Contains("APPLE") && w.Length >5
                                     select w;
            foreach (string n in q)
            {
                this.listBox1.Items.Add(n);
            }

            this.dataGridView1.DataSource = q.ToList();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //this.dataGridView1.DataSource = this.nwDataSet1.Products;
            IEnumerable<global::LinqLabs.nwDataSet.ProductsRow> q = from p in this.nwDataSet1.Products
                                                                    where !p.IsUnitPriceNull() &&( p.UnitPrice > 30 && p.UnitPrice < 90 && p.ProductName.StartsWith("M"))
                                                                    select p;
                                                                  //!p.IsUnitPriceNull() 當p不是空值
            this.dataGridView1.DataSource = q.ToList(); 
        }
        
        private void button9_Click(object sender, EventArgs e)
        {
           // this.dataGridView1.DataSource = this.nwDataSet1.Orders;
            var q = from o in this.nwDataSet1.Orders
                    where o.OrderDate.Year == 1997
                    orderby o.OrderDate descending
                    select o;
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button49_Click(object sender, EventArgs e)
        {

        }
    }
}
