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
    public partial class FrmLINQ架構介紹_InsideLINQ : Form
    {
        public FrmLINQ架構介紹_InsideLINQ()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            this.listBox1.Items.Add("sum = " + nums.Where(n => n % 2 == 0).Sum());
            this.listBox1.Items.Add("min = " + nums.Where(n => n % 2 == 0).Min());
            this.listBox1.Items.Add("max = " + nums.Where(n => n % 2 == 0).Max());
            this.listBox1.Items.Add("avg = " + $"{nums.Where(n => n % 2 == 0).Average():f2}") ;
            this.listBox1.Items.Add("count = " + nums.Where(n => n % 2 == 0).Count());
            //===============================================
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);
            this.listBox1.Items.Add("sum UnitsInStock = " + this.nwDataSet1.Products.Sum(p => p.UnitsInStock));
            this.listBox1.Items.Add("max UnitsInStock = " + this.nwDataSet1.Products.Max(p => p.UnitsInStock));
            this.listBox1.Items.Add("min UnitsInStock = " + this.nwDataSet1.Products.Min(p => p.UnitsInStock));
            this.listBox1.Items.Add("Average UnitsInStock = " + this.nwDataSet1.Products.Average(p => p.UnitsInStock));
            this.listBox1.Items.Add("Count UnitsInStock = " + this.nwDataSet1.Products.Count());

        }
    }
}