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

        private void button30_Click(object sender, EventArgs e)
        {
            System.Collections.ArrayList arrlist = new System.Collections.ArrayList();
            arrlist.Add(3);
            arrlist.Add(4);
            arrlist.Add(1);

            var q = from n in arrlist.Cast<int>()
                    where n > 2
                    select new { N = n };//匿名

            this.dataGridView1.DataSource = q.ToList();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);

            var q = (from n in this.nwDataSet1.Products
                     orderby n.UnitsInStock descending
                     select n).Take(5);
            this.dataGridView1.DataSource = q.ToList();
        }
    }
}