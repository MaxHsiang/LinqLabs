using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqLabs
{
    public partial class Frm考試 : Form
    {
        public Frm考試()
        {
            InitializeComponent();
  
            students_scores = new List<Student>()
                                         {
                                            new Student{ Name = "aaa", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Male" },
                                            new Student{ Name = "bbb", Class = "CS_102", Chi = 80, Eng = 80, Math = 100, Gender = "Male" },
                                            new Student{ Name = "ccc", Class = "CS_101", Chi = 60, Eng = 50, Math = 75, Gender = "Female" },
                                            new Student{ Name = "ddd", Class = "CS_102", Chi = 80, Eng = 70, Math = 85, Gender = "Female" },
                                            new Student{ Name = "eee", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Female" },
                                            new Student{ Name = "fff", Class = "CS_102", Chi = 80, Eng = 80, Math = 80, Gender = "Female" },

                                          };
   
        }



        List<Student> students_scores;


        public class Student
        {
            public string Name { get; set; }
            public string Class { get;  set; }
            public int Chi { get; set; }
            public int Eng { get; internal set; }
            public int Math { get;  set; }
            public string Gender { get; set; }
        }

        private void button36_Click(object sender, EventArgs e)
        {
            #region 搜尋 班級學生成績

            // 
            // 共幾個 學員成績 ?			
            MessageBox.Show("共幾個 學員成績 ?", "《壹》", MessageBoxButtons.OK);
            this.chart1.Series.Clear();
            var w = from n in students_scores
                    group n by new { n.Name, n.Chi, n.Eng, n.Math } into g
                    select new
                    {
                        Name = g.Key.Name,
                        國文 = g.Key.Chi,
                        英文 = g.Key.Eng,
                        數學 = g.Key.Math
                    };
            foreach (var c in w)
            {

                this.chart1.Series.Add(c.Name);
                this.chart1.Series[c.Name].Points.AddXY("國文", c.國文);
                this.chart1.Series[c.Name].Points.AddXY("英文", c.英文);
                this.chart1.Series[c.Name].Points.AddXY("數學", c.數學);
                this.chart1.Series[c.Name].IsValueShownAsLabel = true;
            };

            // 找出 前面三個 的學員所有科目成績	

            MessageBox.Show("找出 前面三個 的學員所有科目成績", "《貳》", MessageBoxButtons.OK);
            this.chart1.Series.Clear();
            var a = from n in students_scores.Take(3)
                    group n by new { n.Name, n.Chi, n.Eng, n.Math } into g
                    select new
                    {
                        Name = g.Key.Name,
                        國文 = g.Key.Chi,
                        英文 = g.Key.Eng,
                        數學 = g.Key.Math
                    };
            foreach (var c in a)
            {

                this.chart1.Series.Add(c.Name);
                this.chart1.Series[c.Name].Points.AddXY("國文", c.國文);
                this.chart1.Series[c.Name].Points.AddXY("英文", c.英文);
                this.chart1.Series[c.Name].Points.AddXY("數學", c.數學);
                this.chart1.Series[c.Name].IsValueShownAsLabel = true;
            };
            // 				
            // 找出 後面兩個 的學員所有科目成績
            MessageBox.Show("找出 後面兩個 的學員所有科目成績", "《參》", MessageBoxButtons.OK);
            this.chart1.Series.Clear();
           
            var q = from n in students_scores.Skip(students_scores.Count - 2)
                    group n by new { n.Name, n.Chi, n.Eng, n.Math } into g
                    select new
                    {
                        Name = g.Key.Name,
                        國文 = g.Key.Chi,
                        英文 = g.Key.Eng,
                        數學 = g.Key.Math
                    };
            foreach (var c in q)
            {

                this.chart1.Series.Add(c.Name);
                this.chart1.Series[c.Name].Points.AddXY("國文", c.國文);
                this.chart1.Series[c.Name].Points.AddXY("英文", c.英文);
                this.chart1.Series[c.Name].Points.AddXY("數學", c.數學);
                this.chart1.Series[c.Name].IsValueShownAsLabel = true;
            };


            // 找出 Name 'aaa','bbb','ccc' 的學員國文英文科目成績		
            MessageBox.Show("找出 Name 'aaa','bbb','ccc' 的學員國文英文科目成績", "《肆》", MessageBoxButtons.OK);
            this.chart1.Series.Clear();
            var i = from n in students_scores
                    where n.Name =="aaa"||n.Name=="bbb"|| n.Name == "ccc"
                    select new
                    {
                        Name = n.Name,
                        國文 = n.Chi,
                        英文 = n.Eng,
                 
                    };
            foreach (var c in i)
            {

                this.chart1.Series.Add(c.Name);
                this.chart1.Series[c.Name].Points.AddXY("國文", c.國文);
                this.chart1.Series[c.Name].Points.AddXY("英文", c.英文);
                this.chart1.Series[c.Name].IsValueShownAsLabel = true;
            };


            // 找出學員 'bbb' 的成績	                          
            MessageBox.Show("找出學員 'bbb' 的成績", "《伍》", MessageBoxButtons.OK);
            this.chart1.Series.Clear();
            var b = from n in students_scores
                    where n.Name == "bbb"
                    select new
                    {
                        Name = n.Name,
                        國文 = n.Chi,
                        英文 = n.Eng,

                    };
            foreach (var c in b)
            {

                this.chart1.Series.Add(c.Name);
                this.chart1.Series[c.Name].Points.AddXY("國文", c.國文);
                this.chart1.Series[c.Name].Points.AddXY("英文", c.英文);
                this.chart1.Series[c.Name].IsValueShownAsLabel = true;
            };
            // 找出除了 'bbb' 學員的學員的所有成績 ('bbb' 退學)
            MessageBox.Show("找出除了 'bbb' 學員的學員的所有成績 ('bbb' 退學)", "《陸》", MessageBoxButtons.OK);
            this.chart1.Series.Clear();
            var y = from n in students_scores
                    where n.Name != "bbb"
                    select new
                    {
                        Name = n.Name,
                        國文 = n.Chi,
                        英文 = n.Eng,

                    };
            foreach (var c in y)
            {

                this.chart1.Series.Add(c.Name);
                this.chart1.Series[c.Name].Points.AddXY("國文", c.國文);
                this.chart1.Series[c.Name].Points.AddXY("英文", c.英文);
                this.chart1.Series[c.Name].IsValueShownAsLabel = true;
            };

            // 找出 'aaa', 'bbb' 'ccc' 學員 國文數學兩科 科目成績  |				
            MessageBox.Show("找出 'aaa', 'bbb' 'ccc' 學員 國文數學兩科 科目成績", "《柒》", MessageBoxButtons.OK);
            this.chart1.Series.Clear();
            var k = from n in students_scores
                    where n.Name == "aaa" || n.Name == "bbb" || n.Name == "ccc"
                    select new
                    {
                        Name = n.Name,
                        國文 = n.Chi,
                        數學 = n.Math,

                    };
            foreach (var c in k)
            {

                this.chart1.Series.Add(c.Name);
                this.chart1.Series[c.Name].Points.AddXY("國文", c.國文);
                this.chart1.Series[c.Name].Points.AddXY("數學", c.數學);
                this.chart1.Series[c.Name].IsValueShownAsLabel = true;
            };
            // 數學不及格 ... 是誰 
            MessageBox.Show("數學不及格 ... 是誰", "《捌》", MessageBoxButtons.OK);
            this.chart1.Series.Clear();
            var l = from n in this.students_scores
                    where n.Math < 60
                    select new
                    {
                        Name = n.Name,
                        數學 = n.Math,
                    };
            foreach (var c in l)
            {

                this.chart1.Series.Add(c.Name);
                this.chart1.Series[c.Name].Points.AddXY("數學", c.數學);
                this.chart1.Series[c.Name].IsValueShownAsLabel = true;
            };
            #endregion

        }

        private void button37_Click(object sender, EventArgs e)
        {
            //個人 sum, min, max, avg
            MessageBox.Show("個人 sum, min, max, avg", "每個學生個人成績", MessageBoxButtons.OK);
            this.chart1.Series.Clear();
            var q = from n in students_scores
                    select new
                    {
                        Name = n.Name,
                        Sum = n.Chi + n.Eng + n.Math,
                        Min = Math.Min(Math.Min(n.Math, n.Chi), n.Eng),
                        Max = Math.Max(Math.Max(n.Math, n.Chi), n.Eng),
                        平均 = (n.Chi + n.Eng + n.Math) / 3
                    };
     
            dataGridView1.DataSource = q.ToList();
            foreach (var c in q)
            {
                this.chart1.Series.Add(c.Name);
                this.chart1.Series[c.Name].Points.AddXY("Sum", c.Sum);
                this.chart1.Series[c.Name].Points.AddXY("min", c.Min);
                this.chart1.Series[c.Name].Points.AddXY("max", c.Max);
                this.chart1.Series[c.Name].Points.AddXY("平均", c.平均);
                this.chart1.Series[c.Name].IsValueShownAsLabel = true;
            };


            //各科 sum, min, max, avg.
            MessageBox.Show("各科 sum, min, max, avg", "每個學生個人成績", MessageBoxButtons.OK);
            this.chart1.Series.Clear();
            var v = from n in students_scores
                    group n by true into g
                    select new
                    {
                        國文總和 = g.Sum(n=>n.Chi),
                        國文Min = g.Min(n => n.Chi),
                        國文Max = g.Max(n => n.Chi),
                        國文平均 = $"{g.Average(n => n.Chi):f2}",

                        英文總和 = g.Sum(n => n.Eng),
                        英文Min = g.Min(n => n.Eng),
                        英文Max = g.Max(n => n.Eng),
                        英文平均 = $"{g.Average(n => n.Eng):f2}",

                        數學總和 = g.Sum(n => n.Math),
                        數學Min = g.Min(n => n.Math),
                        數學Max = g.Max(n => n.Math),
                        數學平均 = $"{g.Average(n => n.Math):f2}",
                    };
            foreach (var c in v)
            {
                this.chart1.Series.Add("國文");
                this.chart1.Series["國文"].Points.AddXY("總和", c.國文總和);
                this.chart1.Series["國文"].Points.AddXY("Min", c.國文Min);
                this.chart1.Series["國文"].Points.AddXY("Max", c.國文Max);
                this.chart1.Series["國文"].Points.AddXY("平均", c.國文平均);
                this.chart1.Series.Add("英文");
                this.chart1.Series["英文"].Points.AddXY("總和", c.英文總和);
                this.chart1.Series["英文"].Points.AddXY("Min", c.英文Min);
                this.chart1.Series["英文"].Points.AddXY("Max", c.英文Max);
                this.chart1.Series["英文"].Points.AddXY("平均", c.英文平均);
                this.chart1.Series.Add("數學");
                this.chart1.Series["數學"].Points.AddXY("總和", c.數學總和);
                this.chart1.Series["數學"].Points.AddXY("Min", c.數學Min);
                this.chart1.Series["數學"].Points.AddXY("Max", c.數學Max);
                this.chart1.Series["數學"].Points.AddXY("平均", c.數學平均);

                this.chart1.Series["國文"].IsValueShownAsLabel = true;
                this.chart1.Series["英文"].IsValueShownAsLabel = true;
                this.chart1.Series["數學"].IsValueShownAsLabel = true;
            };



        }

        private void button33_Click(object sender, EventArgs e)
        {
            //// split=> 分成 三群 '待加強'(60~69) '佳'(70~89) '優良'(90~100) 

            //// print 每一群是哪幾個 ? (每一群 sort by 分數 descending)
        }   

        private void button35_Click(object sender, EventArgs e)
        {
            // 統計 :　所有隨機分數出現的次數/比率; sort ascending or descending
            // 63     7.00%
            // 100    6.00%
            // 78     6.00%
            // 89     5.00%
            // 83     5.00%
            // 61     4.00%
            // 64     4.00%
            // 91     4.00%
            // 79     4.00%
            // 84     3.00%
            // 62     3.00%
            // 73     3.00%
            // 74     3.00%
            // 75     3.00%
            this.chart1.Series.Clear();


        }
        NorthwindEntities dbContext = new NorthwindEntities();
        private void button34_Click(object sender, EventArgs e)
        {
            // 年度最高銷售金額 年度最低銷售金額
            /*this.listBox1.Items.Clear();
            var q = from n in this.dbContext.Orders.AsEnumerable()
                    orderby n.Freight descending
                    select $"{n.Freight:c2}";
            this.listBox1.Items.Add("總年度最高銷售金額"  + q.First());
            this.listBox1.Items.Add("總年度最低銷售金額" + q.Last());*/

            // 那一年總銷售最好 ? 那一年總銷售最不好 ?
            this.listBox1.Items.Clear();
            var q1 = from n in this.dbContext.Orders
                     group n by n.OrderDate.Value.Year into g
                     orderby g.Sum(o => o.Order_Details.Sum(od => od.UnitPrice * od.Quantity * (decimal)(1 - od.Discount))) descending
                     select new
                     {
                         Year = g.Key,
                         Max = g.Max(o => o.Order_Details.Sum(od => od.UnitPrice * od.Quantity * (decimal)(1 - od.Discount))),
                         Min = g.Min(o => o.Order_Details.Sum(od => od.UnitPrice * od.Quantity * (decimal)(1 - od.Discount))),
                     };
            this.listBox1.Items.Add("總銷售最高金額" +q1.First());
            this.listBox1.Items.Add("總銷售最低金額" +q1.Last());


            // 那一個月總銷售最好 ? 那一個月總銷售最不好 ?

            // 每年 總銷售分析 圖
            // 每月 總銷售分析 圖


        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }
}
