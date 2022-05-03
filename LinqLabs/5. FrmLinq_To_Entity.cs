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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NorthwindEntities dbConect = new NorthwindEntities();
            var q = from n in dbConect.Products
                    where n.UnitPrice>30
                    select n;

            this.dataGridView1.DataSource = q.ToList();
        }
    }
}
