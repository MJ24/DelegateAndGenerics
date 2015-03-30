using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateAndGenerics
{
    public delegate bool DelCompare<T>(T item1, T item2);
    public delegate void MulticastDelegate();
    class Program
    {
        static void Main(string[] args)
        {
            //获取int数组的最大值
            int[] intItems = { 3, 5, 87, 9, 11 };
            int intItemsMax = GetMaxItem<int>(intItems, delegate(int a, int b) { return a < b; });
            Console.WriteLine(intItemsMax);

            //获取String数组的最长元素
            string[] strItems = { "213", "xx", "asdasdasd", "" };
            string strItemsMax = GetMaxItem<string>(strItems, (string a, string b) => { return a.Length < b.Length; });
            Console.WriteLine(strItemsMax);

            //多播委托
            MulticastDelegate multiDel = () => { Console.WriteLine("我是委托multiDel指向的第1个函数"); };
            multiDel += () => { Console.WriteLine("我是委托multiDel指向的第2个函数"); };
            multiDel += () => { Console.WriteLine("我是委托multiDel指向的第3个函数"); };
            multiDel += Test;
            multiDel -= Test;
            multiDel();

            Console.ReadLine();
        }

        /// <summary>
        /// 获取任意数组的最大元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">参数1，数组</param>
        /// <param name="delCompare">参数2，比较两个数组元素大小的委托方法，要求返回第一个比第二个小的bool值</param>
        /// <returns>返回值为最大元素(泛型)</returns>
        private static T GetMaxItem<T>(T[] items, DelCompare<T> delCompare)
        {
            T max = items[0];
            foreach (T item in items)
            {
                if (delCompare(max, item))
                {
                    max = item;
                }
            }
            return max;
        }

        private static void Test()
        {
            Console.WriteLine("我是委托multiDel指向的Test函数");
        }
    }
}
