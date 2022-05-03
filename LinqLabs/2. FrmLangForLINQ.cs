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
    public partial class FrmLangForLINQ : Form
    {
        public FrmLangForLINQ()
        {
            InitializeComponent();
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int n1, n2;
            n1 = 100;
            n2 = 200;

            MessageBox.Show(n1 + "," + n2);

            swap(ref n1, ref n2);

            MessageBox.Show(n1 + "," + n2);
            string s1, s2;
            s1 = "aaa";
            s2 = "bbb";
            MessageBox.Show(s1 + "," + s2);
            swap(ref s1, ref s2);
            MessageBox.Show(s1 + "," + s2);
        }
        static void swap(ref int n1, ref int n2)
        {
            int temp = n1;
            n1 = n2;
            n2 = temp;
        }
        static void swap(ref string n1, ref string n2)
        {
            string temp = n1;
            n1 = n2;
            n2 = temp;
        }

        static void swap(ref Point n1, ref Point n2)
        {
            Point temp = n1;
            n1 = n2;
            n2 = temp;
        }
        static void swapType<T>(ref T n1, ref T n2)
        {
            T temp = n1;
            n1 = n2;
            n2 = temp;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int n1, n2;
            n1 = 100;
            n2 = 200;

            MessageBox.Show(n1 + "," + n2);

            //swapType<int>(ref n1, ref n2);
            swapType(ref n1, ref n2); //<T>推斷型別
            MessageBox.Show(n1 + "," + n2);
            //=============================
            string s1, s2;
            s1 = "aaa";
            s2 = "bbb";
            MessageBox.Show(s1 + "," + s2);
            swapType(ref s1, ref s2);
            MessageBox.Show(s1 + "," + s2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.buttonX.Click += ButtonX_Click;
            //C# 1.0 具名方法
            this.buttonX.Click += new EventHandler(aaa);
            this.buttonX.Click += bbb;
            //C# 2.0 匿名方法
            this.buttonX.Click += delegate (object sender1, EventArgs e1)
                                           {
                                               MessageBox.Show("C# 2.0 匿名方法");
                                           };

            //C# 3.0 匿名方法 Lambda 運算式 => Goes to
            this.buttonX.Click += (object sender1, EventArgs e1) =>
                                  {
                                      MessageBox.Show("C# 3.0 匿名方法Lambda");
                                  };
        }

        private void ButtonX_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ButtonX");
        }
        private void aaa(object sender, EventArgs e)
        {
            MessageBox.Show("aaa");
        }
        private void bbb(object sender, EventArgs e)
        {
            MessageBox.Show("bbb");
        }
        bool Test(int n)
        {
            return n > 5;
        }
        bool Test1(int n)
        {
            return n%2==0;
        }

        //Step 1:Create Delegate 型別
        //Step 2:Create Delegate object(new.....)
        //Step 3:invoke / call method
        delegate bool MyDelegate(int n);
        private void button9_Click(object sender, EventArgs e)
        {
            bool result = Test(6);
            MessageBox.Show("result = " + result);
            //=================================
            MyDelegate delegateObj = new MyDelegate(Test);
            result = delegateObj.Invoke(7);//call method(__)
            MessageBox.Show("result = " + result);
            //================================== 
            delegateObj = Test1; //syntax suger
            result = delegateObj(6);
            MessageBox.Show("result = " + result);
            //C# 2.0 匿名方法
            delegateObj = delegate (int n)//delegate(參數){敘述}
            {
               return n > 5;
            };
            result = delegateObj.Invoke(6);//inboke(調和)可省略
            MessageBox.Show("result = " + result);
            //C# 3.0 匿名方法 Lambda 運算式 => Goes to
            delegateObj = n => n > 5;
            result = delegateObj(1);
            MessageBox.Show("result = " + result);
        }
        List<int> MyWhere(int[] nums,MyDelegate delegateObj)//需要符合"MyDelegate"=輸入參數int 回傳布林值
        {
            List<int> list = new List<int>();

            foreach(int n in nums)
            {
                if (delegateObj(n))//如果n符合MyDelegate就會加入到list集合中
                {
                    list.Add(n);
                }
            }
            return list;//int
        }
        private void button10_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            List<int> Large_list = MyWhere(nums, Test);

            //foreach (int n in Large_list)
            //{
            //    listBox1.Items.Add(n);
            //}
            //===================================
            List<int> list1 = MyWhere(nums, n => n > 5);
            List<int> odd_list = MyWhere(nums, n => n %2==1);
            List<int> even_list = MyWhere(nums, n => n % 2 == 0);
            foreach(int n in odd_list)
            {
                listBox1.Items.Add(n);
            }
            foreach(int n in even_list)
            {
                listBox2.Items.Add(n);
            }
        }

        IEnumerable<int> MyIterator(int[] nums ,MyDelegate delegateObj)
        {
            foreach(int n in nums)
            {
                if (delegateObj(n))//call method
                {
                  yield return n;
                }
            }
        }
             

        private void button13_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            IEnumerable<int> list = MyIterator(nums, n => n > 3);
            foreach(int n in list)
            {
                listBox1.Items.Add(n);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            IEnumerable<int> q = nums.Where(n => n > 5);

            foreach (int n in q)
            {
                listBox1.Items.Add(n);
            }
            ////===========================
            string[] words = { "aaa", "bbbb", "ccccc", "dddddd" };
            IEnumerable<string> q1 = words.Where<string>(w => w.Length > 3);

            foreach (string w in q1)
            {
                this.listBox2.Items.Add(w);
            }
            this.dataGridView1.DataSource = q1.ToList();
            //===========================
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);
            //this.listBox1.DataSource = this.nwDataSet1.Products;
            var p = this.nwDataSet1.Products.Where(p1 => p1.UnitPrice > 30);

            this.dataGridView2.DataSource = p.ToList(); 


        }

        private void button45_Click(object sender, EventArgs e)
        {
            var n = 100;
            var s = "aaaa";
            var p = new Point(100, 100);
        }

        private void button41_Click(object sender, EventArgs e)
        {
            MyPoint pt1 = new MyPoint();//小括號呼叫方法
            pt1.P1 = 100;//set
            int w = pt1.P1;//get


            //MessageBox.Show(pt1.P1.ToString());
            List<MyPoint> list = new List<MyPoint>();
            list.Add(new MyPoint());
            list.Add(new MyPoint(100));
            list.Add(new MyPoint(100, 300));
            list.Add(new MyPoint("xxxxx"));
            //{} : 大括號,初始化值
            list.Add(new MyPoint { P1 = 1, P2 = 1, Field1 = "aaa", Field2 = "aaa" });
            list.Add(new MyPoint { P1 = 555 });
            list.Add(new MyPoint { P1 = 250, P2 = 500 });
            this.dataGridView1.DataSource = list;
            //===============================
            List<MyPoint> list2 = new List<MyPoint>
            {
                new MyPoint{P1 = 1 , P2= 1 , Field1 = "aaa", Field2 = "aaa" } ,
                new MyPoint{P1 = 222 , P2=333 , Field1 = "aaa", Field2 = "aaa" },
                new MyPoint{P1 = 555 , P2=77 }
             };
            this.dataGridView2.DataSource = list2;
        }

    public class MyPoint
        {
            public MyPoint()
            {

            }
            public MyPoint(int p1)
            {
               this.P1 = p1;
            }
            public MyPoint(int p1,int p2)
            {
                this.P1 = p1;
                this.P2 = p2;
            }
            public MyPoint(string filede1)
            {

            }

            private int m_p1;

            public int P1
            {
                get
                {
                    //logic.......
                    return m_p1;
                }
                set
                {
                    //logic.......
                    m_p1 = value;
                }
            }
            public string Field1 = "xxx", Field2 = "yyy";
            public int P2 { get; set; }
        }

        private void button43_Click(object sender, EventArgs e)
        {
            var x = new { P1 = 1, P2 = 2, P3 = 3 };
            var y = new { P1 = 1, P2 = 2, P3 = 3 };
            var z = new { UserName = "aaa" , Password = "bbb" };

            this.listBox1.Items.Add(x.GetType());//GetType:找出var屬性 
            this.listBox1.Items.Add(y.GetType());
            this.listBox1.Items.Add(z.GetType());
            //=============================================

            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };

            //var q = from n in nums
            //        where n > 5
            //        select new { A = n, S = n * n, C = n * n * n };
            var q = nums.Where(n => n > 5).Select(n => new { A = n, S = n * n, C = n * n * n });
            //nums.Where(n => n > 5).Select(n => new { A = n, S = n * n, C = n * n * n });
            //where:n >5 之後 select 投射 goes to => new { A = n, S = n * n, C = n * n * n }
            this.dataGridView1.DataSource = q.ToList();
            //===============================================
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);
            var q2 = from n in this.nwDataSet1.Products
                     where n.UnitPrice > 30
                     select new
                     {
                         ID = n.ProductID,
                         產品名稱 = n.ProductName,
                         n.UnitPrice,
                         n.UnitsInStock,
                         Total = $"{ n.UnitPrice* n.UnitsInStock:C2}"
                     };
            this.dataGridView2.DataSource = q2.ToList();
        }



        private void button32_Click(object sender, EventArgs e)
        {
            string s1 = "aaa";
            int n = s1.WordCount();
            MessageBox.Show("WordCount = " + n);

            string s2 = "123456798";
            n = s2.WordCount();
            MessageBox.Show("WordCount = " + n);
            //=============================
            char ch = s2.chars(3);
            MessageBox.Show("WordCount = " + ch);

        }
    }
public static class MyStringExtend
{
    public static int WordCount(this string s)
    {
       return s.Length;
    }
    public static char chars(this string s , int index)
        {
            return s[index];
        }
}
}
