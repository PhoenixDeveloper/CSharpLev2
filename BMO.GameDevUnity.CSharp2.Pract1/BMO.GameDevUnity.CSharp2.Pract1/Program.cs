﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMO.GameDevUnity.CSharp2.Pract1
{
    class Program
    {
        static void Main(string[] args)
        {
            Form form = new System.Windows.Forms.Form();
            form.Width = 1366;
            form.Height = 768;
            form.Show();
            SplashScreen.Init(form);
            Application.Run(form);
        }
    }
}
