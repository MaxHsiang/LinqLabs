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
        }
    }
}
