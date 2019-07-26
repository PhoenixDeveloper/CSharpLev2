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
    /// Логика взаимодействия для wndChangeDepartment.xaml
    /// </summary>
    public partial class wndChangeDepartment : Window
    {
        public string Department { get; set; }
        public wndChangeDepartment()
        {
            InitializeComponent();

            this.DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbNameDepartment.Text = Department;
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (tbNameDepartment.Text.Length == 0)
            {
                MessageBox.Show("Имя департамента не может быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Department = tbNameDepartment.Text;
                this.DialogResult = true;
            }

            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            tbNameDepartment.Text = Department;
        }
    }
}
