using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public static class Common
    {
        public static int cInt(string input)
        {
            int x = 0;
            int.TryParse(input, out x);
            return x;
        }

        public static double cDouble(string input)
        {
            double x =0.0;
            double.TryParse(input, out x);
            return x;

        }

        public static float cFloat(string input)
        {
            float x = 0.0f;
            float.TryParse(input, out x);
            return x;
        }

        public static DateTime cdate(string input)
        {
            DateTime x;
            DateTime.TryParse(input, out x);
            return x;
        }
    }
}
