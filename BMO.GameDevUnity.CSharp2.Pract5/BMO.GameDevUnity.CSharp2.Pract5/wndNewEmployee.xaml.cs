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
    /// Логика взаимодействия для wndNewEmployee.xaml
    /// </summary>
    public partial class wndNewEmployee : Window
    {
        public Employee ForExchange { get; set; }
        public string NameOfDepartment { get; set; }

        public wndNewEmployee()
        {
            InitializeComponent();
            foreach (var department in MainWindow.myOrganization)
            {
                cbDepartments.Items.Add(department.Key);
            }
            cbDepartments.Text = cbDepartments.Items[0].ToString();
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            ForExchange = new Employee(tbLastName.Text, tbFirstName.Text, tbProfession.Text, Convert.ToInt32(tbAge.Text));
            NameOfDepartment = cbDepartments.Text;
            this.DialogResult = true;
            this.Close();
        }
    }
}
