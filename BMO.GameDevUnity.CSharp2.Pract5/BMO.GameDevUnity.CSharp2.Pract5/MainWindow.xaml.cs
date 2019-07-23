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
using Microsoft.Win32;

namespace BMO.GameDevUnity.CSharp2.Pract5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static public Organization MyOrganization { get; set; }

        static string fileName = "data.xml";
        public string TestStr { get; set; }

        public MainWindow()
        {
            MyOrganization = new Organization()
            {
                //{"Информационный", new Department() { Name = "Информационный" } },
                //{ "Административный", new Department() { Name = "Административный" } },
                //{ "Экономический", new Department() { Name = "Экономический" } }
            };
            InitializeComponent();

            this.DataContext = this;

            if (cbDepartments.Items.Count == 0)
            {
                btnChangeDepartment.Visibility = Visibility.Hidden;
                btnDeleteDepartment.Visibility = Visibility.Hidden;
            }
            else
            {
                cbDepartments.Text = MyOrganization.Keys.First();
            }

            btnDeleteEmployee.Visibility = Visibility.Hidden;
            btnChangeEmployee.Visibility = Visibility.Hidden;
            
            
        }

        private void BtnButton1_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Press");
        }



        private void MiExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            wndNewEmployee wndNewEmployee = new wndNewEmployee();
            if (wndNewEmployee.ShowDialog() == true)
            {
                MyOrganization[wndNewEmployee.NameOfDepartment].Add(wndNewEmployee.ForExchange);
                cbDepartments.Text = wndNewEmployee.NameOfDepartment;
                UpdateListBoxEmployee(wndNewEmployee.NameOfDepartment);
            };
        }

        void UpdateCheckBoxDepartments()
        {
            cbDepartments.Items.Refresh();
            if (cbDepartments.Items.Count != 0)
            {
                cbDepartments.SelectedItem = cbDepartments.Items[cbDepartments.Items.Count - 1];
                cbDepartments.Text = (cbDepartments.Items.Count == 1)?(cbDepartments.Text = cbDepartments.Items[cbDepartments.Items.IndexOf(cbDepartments.SelectedItem)].ToString()):(cbDepartments.Items[0].ToString());
                UpdateListBoxEmployee(cbDepartments.Text);
                btnDeleteDepartment.Visibility = Visibility.Visible;
                btnChangeDepartment.Visibility = Visibility.Visible;
            }
            else
            {                
                btnDeleteDepartment.Visibility = Visibility.Hidden;
                btnChangeDepartment.Visibility = Visibility.Hidden;
                MessageBox.Show("Список департаментов пуст. Для продолжения работы создайте департаменты.", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        void UpdateListBoxEmployee(string NameOfDepartment)
        {
            lvEmployees.ItemsSource = MyOrganization[cbDepartments.Text];
            lvEmployees.Items.Refresh();
            if (cbDepartments.Items.Count != 0)
            {
                btnDeleteDepartment.Visibility = Visibility.Visible;
                btnChangeDepartment.Visibility = Visibility.Visible;
            }
            else
            {
                btnDeleteDepartment.Visibility = Visibility.Hidden;
                btnChangeDepartment.Visibility = Visibility.Hidden;

                btnDeleteEmployee.Visibility = Visibility.Hidden;
                btnChangeEmployee.Visibility = Visibility.Hidden;
            }
        }

        private void MiSave_Click(object sender, RoutedEventArgs e)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Organization));
            FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            xmlSerializer.Serialize(fileStream, MyOrganization);
            fileStream.Close();

        }

        private void CbDepartments_DropDownClosed(object sender, EventArgs e)
        {
            if (cbDepartments.Items.Count != 0)
            {
                UpdateListBoxEmployee(cbDepartments.Text);
            }
            else
            {
                MessageBox.Show("Список департаментов пуст. Для продолжения работы создайте департаменты.", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
            }            
        }

        private void MiLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XML files(*.xml)| *.xml";
            if (ofd.ShowDialog() == true)
            {
                fileName = ofd.FileName;
                XmlSerializer xmlFormat = new XmlSerializer(typeof(Organization));
                Stream fStream = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read);
                MyOrganization = (Organization)xmlFormat.Deserialize(fStream);
                UpdateCheckBoxDepartments();
                if (cbDepartments.Items.Count != 0)
                {                    
                    UpdateListBoxEmployee(cbDepartments.Text);
                }                
                fStream.Close();
            }
                
        }

        private void MiSaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "XML files(*.xml)| *.xml";
            if (sfd.ShowDialog() == true)
            {
                fileName = sfd.FileName;
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Organization));
                FileStream fileStream = new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write);
                xmlSerializer.Serialize(fileStream, MyOrganization);
                fileStream.Close();
            }
        }

        private void BtnAddDepartment_Click(object sender, RoutedEventArgs e)
        {
            wndNewDepartment wndNewDepartment = new wndNewDepartment();
            if (wndNewDepartment.ShowDialog() == true)
            {
                MyOrganization.Add(wndNewDepartment.NameOfDepartment, new Department() { Name = wndNewDepartment.NameOfDepartment });
                UpdateCheckBoxDepartments();
                cbDepartments.Text = wndNewDepartment.NameOfDepartment;
                UpdateListBoxEmployee(wndNewDepartment.NameOfDepartment);
            };
        }

        private void BtnDeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (lvEmployees.SelectedItem != null)
            {
                MyOrganization[cbDepartments.Text].Remove((Employee)lvEmployees.SelectedItem);
                UpdateListBoxEmployee(cbDepartments.Text);
            }
            else
            {
                MessageBox.Show("Не выбран сотрудник", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnDeleteDepartment_Click(object sender, RoutedEventArgs e)
        {
            if (cbDepartments.Items.Count != 0)
            {
                MyOrganization.Remove(cbDepartments.Text);
                UpdateCheckBoxDepartments();
            }
            else
            {
                MessageBox.Show("Список департаментов пуст", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnChangeEmployee_Click(object sender, RoutedEventArgs e)
        {
            wndChangeEmployee wndChangeEmployee = new wndChangeEmployee();            
            if (lvEmployees.SelectedItem != null)
            {
                Employee employeeBuffer = (Employee)lvEmployees.SelectedItem;
                wndChangeEmployee.LastName = employeeBuffer.LastName;
                wndChangeEmployee.FirstName = employeeBuffer.FirstName;
                wndChangeEmployee.Profession = employeeBuffer.Profession;
                wndChangeEmployee.Age = employeeBuffer.Age;
                wndChangeEmployee.Department = cbDepartments.Text;
                if (wndChangeEmployee.ShowDialog() == true)
                {
                    MyOrganization[cbDepartments.Text].Remove((Employee)lvEmployees.SelectedItem);
                    MyOrganization[wndChangeEmployee.Department].Add(wndChangeEmployee.ForExchange);
                    cbDepartments.Text = wndChangeEmployee.Department;
                    UpdateListBoxEmployee(wndChangeEmployee.Department);
                };
            }
            else
            {
                MessageBox.Show("Не выбран сотрудник", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }            
        }

        private void BtnChangeDepartment_Click(object sender, RoutedEventArgs e)
        {
            wndChangeDepartment wndChangeDepartment = new wndChangeDepartment();
            if (cbDepartments.Items.Count != 0)
            {
                Department employeesBuffer = MyOrganization[cbDepartments.Text];
                wndChangeDepartment.Department = cbDepartments.Text;
                if (wndChangeDepartment.ShowDialog() == true)
                {
                    MyOrganization.Remove(cbDepartments.Text);
                    employeesBuffer.Name = wndChangeDepartment.Department;
                    MyOrganization.Add(wndChangeDepartment.Department, employeesBuffer);
                    UpdateCheckBoxDepartments();
                    cbDepartments.Text = wndChangeDepartment.Department;
                    UpdateListBoxEmployee(wndChangeDepartment.Department);
                };                
            }
            else
            {
                MessageBox.Show("Список департаментов пуст", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void LbEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvEmployees.SelectedItem != null)
            {
                btnDeleteEmployee.Visibility = Visibility.Visible;
                btnChangeEmployee.Visibility = Visibility.Visible;
            }
            else
            {
                btnDeleteEmployee.Visibility = Visibility.Hidden;
                btnChangeEmployee.Visibility = Visibility.Hidden;
            }
        }
    }
}
