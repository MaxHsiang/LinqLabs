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
    public partial class Frm作業_2 : Form
    {
        public Frm作業_2()
        {
            InitializeComponent(); 
            this.productPhotoTableAdapter1.Fill(this.awDataSet1.ProductPhoto);
            comboBox3addYear();
           

        }

        private void comboBox3addYear()
        {
            var q = awDataSet1.ProductPhoto.OrderBy(p => p.ModifiedDate.Year).Select(p => p.ModifiedDate.Year);
            foreach (var year in q.Distinct())
            {
                comboBox3.Items.Add(year);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            pictureBox1.Image = null;
            var q = from n in awDataSet1.ProductPhoto
                    select n;
            bindingSource1.DataSource = q.ToList();
            dataGridView1.DataSource = bindingSource1;
            LargeP();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = null;
            this.pictureBox1.Image = null;
            var q = awDataSet1.ProductPhoto.Where(d => d.ModifiedDate >= dateTimePicker1.Value && d.ModifiedDate <= dateTimePicker2.Value);
            this.bindingSource1.DataSource = q.ToList();
            this.dataGridView1.DataSource = this.bindingSource1;
            LargeP();
        }

        void LargeP()
        {
            try
            {
                var Image = awDataSet1.ProductPhoto.
                    Where(p => p.ProductPhotoID == (int)(dataGridView1.Rows[bindingSource1.Position].Cells["ProductPhotoID"].Value)).Select(p => p.LargePhoto);
                foreach (byte[] a in Image)
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    ms.Write(a, 0, Convert.ToInt32(a.Length));
                    pictureBox1.Image = System.Drawing.Image.FromStream(ms);
                }
            }
            catch
            {
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
                this.dataGridView1.DataSource = null;
                this.pictureBox1.Image = null;
                var q = awDataSet1.ProductPhoto.Where(n => n.ModifiedDate.Year.ToString() == comboBox3.Text);
                this.bindingSource1.DataSource = q.ToList();
                this.dataGridView1.DataSource = this.bindingSource1;
                 LargeP(); 
        }

        private void button10_Click(object sender, EventArgs e)
        {

            this.dataGridView1.DataSource = null;
            this.pictureBox1.Image = null;
            var s1 = awDataSet1.ProductPhoto.
                Where(n => n.ModifiedDate.Month >=1 && n.ModifiedDate.Month <=3);
         if(comboBox2.Text == "第一季")
            {
                this.bindingSource1.DataSource = s1.ToList();
                this.dataGridView1.DataSource = this.bindingSource1;
            }
            var s2 = awDataSet1.ProductPhoto.
           Where(n => n.ModifiedDate.Month >= 4 && n.ModifiedDate.Month <= 6);
            if (comboBox2.Text == "第二季")
            {
                this.bindingSource1.DataSource = s2.ToList();
                this.dataGridView1.DataSource = this.bindingSource1;
            }
            var s3 = awDataSet1.ProductPhoto.
           Where(n => n.ModifiedDate.Month >=7 && n.ModifiedDate.Month <= 9);
            if (comboBox2.Text == "第三季")
            {
                this.bindingSource1.DataSource = s3.ToList();
                this.dataGridView1.DataSource = this.bindingSource1;
            }
            var s4 = awDataSet1.ProductPhoto.
           Where(n => n.ModifiedDate.Month >=10 && n.ModifiedDate.Month <=12);
            if (comboBox2.Text == "第四季")
            {
                this.bindingSource1.DataSource = s4.ToList();
                this.dataGridView1.DataSource = this.bindingSource1;
            }
            LargeP();

        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            LargeP();
        }
    }
}
