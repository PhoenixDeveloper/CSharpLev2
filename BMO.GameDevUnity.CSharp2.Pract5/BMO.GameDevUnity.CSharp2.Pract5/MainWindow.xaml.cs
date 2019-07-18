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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Xml.Serialization;

namespace BMO.GameDevUnity.CSharp2.Pract5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static public Organization myOrganization;

        public MainWindow()
        {
            InitializeComponent();
            myOrganization = new Organization();
            //myOrganization.organization = new Dictionary<string, Department>();
            myOrganization.Add("Информационный", new Department() { Name = "Информационный" });
            myOrganization.Add("Административный", new Department() { Name = "Административный" });
            myOrganization.Add("Экономический", new Department() { Name = "Экономический" });
            foreach (var department in myOrganization)
            {
                cbDepartments.Items.Add(department.Key);
            }
            cbDepartments.Text = cbDepartments.Items[0].ToString();
        }

        private void BtnButton1_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Press");
        }



        private void MiExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void WndMain_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void BtnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            wndNewEmployee wndNewEmployee = new wndNewEmployee();
            if (wndNewEmployee.ShowDialog() == true)
            {
                myOrganization[wndNewEmployee.NameOfDepartment].Add(wndNewEmployee.ForExchange);
                cbDepartments.Text = wndNewEmployee.NameOfDepartment;
                UpdateListBoxEmployee(wndNewEmployee.NameOfDepartment);
            };
        }

        void UpdateListBoxEmployee(string NameOfDepartment)
        {
            lbEmployees.Items.Clear();
            foreach (Employee employee in myOrganization[NameOfDepartment])
                lbEmployees.Items.Add(employee);
        }

        private void MiSave_Click(object sender, RoutedEventArgs e)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Department));
            FileStream fileStream = new FileStream("data.xml", FileMode.Create, FileAccess.Write);
            xmlSerializer.Serialize(fileStream, myOrganization["Информационный"]);
            fileStream.Close();

        }

        private void CbDepartments_DropDownClosed(object sender, EventArgs e)
        {
            UpdateListBoxEmployee(cbDepartments.Text);
        }
    }
}
