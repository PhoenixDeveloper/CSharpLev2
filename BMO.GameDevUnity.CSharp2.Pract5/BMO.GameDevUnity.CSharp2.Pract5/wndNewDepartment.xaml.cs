using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BMO.GameDevUnity.CSharp2.Pract5
{
    /// <summary>
    /// Логика взаимодействия для wndNewDepartment.xaml
    /// </summary>
    public partial class wndNewDepartment : Window
    {
        public string NameOfDepartment { get; set; }

        public wndNewDepartment()
        {
            InitializeComponent();
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            NameOfDepartment = tbNameDepartment.Text;
            this.DialogResult = true;
            this.Close();
        }
    }
}
