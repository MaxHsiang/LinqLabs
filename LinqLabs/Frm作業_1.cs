using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHomeWork
{
    public partial class Frm作業_1 : Form
    {
        public Frm作業_1()
        {
           
            InitializeComponent();
            this.ordersTableAdapter1.Fill(this.nwDataSet1.Orders);
            this.order_DetailsTableAdapter1.Fill(this.nwDataSet1.Order_Details);
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);
            LoadYeartoForm();
        }
        int skip = 0;
        int take_now;
        int take_old;

        private void LoadYeartoForm()
        {
            var q = from n in nwDataSet1.Orders
                    orderby n.OrderDate.Year descending
                    select n.OrderDate.Year;
            foreach(var i in q)
            {
                if(comboBox1.Items.Contains(i))
                //Contains:如果i的集合中沒有相同資料,會輸入並且true,再輸入相同重複則會false,
                {
                    continue;
                }
                else
                {
                    comboBox1.Items.Add(i);
                }
            }
        
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)//下一頁
        {
            //this.nwDataSet1.Products.Take(10);//Top 10 Skip(10)

            //Distinct()
            take_now = int.Parse(textBox1.Text);
            this.dataGridView1.DataSource = this.nwDataSet1.Products.Skip(skip).Take(take_now).ToList();
            skip = skip + take_now;
            take_old = take_now;
        }
        private void button12_Click(object sender, EventArgs e)//上一頁
        {
            
            skip -= take_now;
            take_old = take_now;
            take_now = int.Parse(textBox1.Text);
            this.dataGridView1.DataSource = this.nwDataSet1.Products.Skip(skip).Take(take_now).ToList();

        }

        private void button14_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files =  dir.GetFiles();

            this.dataGridView1.DataSource = files;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            var q = from or in nwDataSet1.Orders 
                    where !or.IsShipRegionNull()&&!or.IsShippedDateNull() && !or.IsShipPostalCodeNull()
                    select or;
            this.dataGridView1.DataSource = q.ToList();
            var w = from od in nwDataSet1.Order_Details select od;
            this.dataGridView2.DataSource = w.ToList();
        }



        private void button1_Click(object sender, EventArgs e)
        {

            //var q = from od in nwDataSet1.Order_Details 
            //        join or in nwDataSet1.Orders
            //        on od.OrderID equals or.OrderID
            //        where !or.IsShipRegionNull() && !or.IsShippedDateNull() && !or.IsShipPostalCodeNull() && or.OrderDate.Year.ToString()== comboBox1.Text
            //        orderby or.OrderID descending
            //        select or;
            //this.dataGridView2.DataSource = q.ToList();
            //===========================================
            var q = from od in nwDataSet1.Order_Details
                    where OrderIDfilter(od.OrderID)
                    orderby od.OrderID descending
                    select od;
            dataGridView2.DataSource = q.ToList();

        }
        bool OrderIDfilter(int a)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (int.Parse(dataGridView1.Rows[i].Cells["OrderID"].Value.ToString()) == a)
                {
                    return true;
                }

            }
            return false;
            //=================================================


        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var w = from or in nwDataSet1.Orders
                    where !or.IsShipRegionNull() && !or.IsShippedDateNull() && !or.IsShipPostalCodeNull() && or.OrderDate.Year == int.Parse(comboBox1.Text)
                    orderby or.OrderID descending
                    select or;
            this.dataGridView1.DataSource = w.ToList();
        }



        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
