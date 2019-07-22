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
    /// Логика взаимодействия для wndChangeEmployee.xaml
    /// </summary>
    public partial class wndChangeEmployee : Window
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Profession { get; set; }
        public int Age { get; set; }
        public string Department { get; set; }
        public Employee ForExchange { get; set; }

        public wndChangeEmployee()
        {
            InitializeComponent();
            foreach (var department in MainWindow.myOrganization)
            {
                cbDepartments.Items.Add(department.Key);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ForExchange = new Employee(tbLastName.Text, tbFirstName.Text, tbProfession.Text, Convert.ToInt32(tbAge.Text));
                Department = cbDepartments.Text;
                this.DialogResult = true;
            }
            catch (Exception)
            {

                MessageBox.Show("Введены некорректные данные", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }            
            this.Close();
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            tbLastName.Text = LastName;
            tbFirstName.Text = FirstName;
            tbProfession.Text = Profession;
            tbAge.Text = Age.ToString();
            cbDepartments.Text = Department;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbLastName.Text = LastName;
            tbFirstName.Text = FirstName;
            tbProfession.Text = Profession;
            tbAge.Text = Age.ToString();
            cbDepartments.Text = Department;
        }
    }
}
