using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace ScratchPad
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<string>();
            list.Add("Hello");

            var i = new { Name = "Hello", EMail = "Hello@gmail.com" };



            

            list.FindPaul(s => s == "Hello");
        }


    }
}
