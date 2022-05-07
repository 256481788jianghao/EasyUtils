using EasyUtils;
using System;

namespace EasyUtilsTest
{
    class Program
    {
        static void Main(string[] args)
        {
            SmartList<Byte> testList = new SmartList<Byte>(10);
            try
            {
                testList.AppendAtEnd(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });

                Byte[] outs = testList.OutputFromFirst(10);
                Console.WriteLine("out:" + outs[9].ToString());
            }
            catch(Exception ex)
            {
                Console.WriteLine("ex:" + ex.ToString());
            }
            
            Console.WriteLine("input:" + testList);
            Console.WriteLine("Hello World!");
        }
    }
}
