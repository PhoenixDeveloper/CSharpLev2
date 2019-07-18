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
        static public Organization myOrganization;
        static string fileName = "data.xml";

        public MainWindow()
        {
            InitializeComponent();
            myOrganization = new Organization();
            //myOrganization.organization = new Dictionary<string, Department>();
            myOrganization.Add("Информационный", new Department() { Name = "Информационный" });
            myOrganization.Add("Административный", new Department() { Name = "Административный" });
            myOrganization.Add("Экономический", new Department() { Name = "Экономический" });
            UpdateCheckBoxDepartments();
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

        void UpdateCheckBoxDepartments()
        {
            cbDepartments.Items.Clear();
            foreach (var department in myOrganization)
            {
                cbDepartments.Items.Add(department.Key);
            }
            if (cbDepartments.Items.Count != 0)
            {
                cbDepartments.SelectedItem = cbDepartments.Items[cbDepartments.Items.Count - 1];
                cbDepartments.Text = (cbDepartments.Items.Count == 1)?(cbDepartments.Text = cbDepartments.Items[cbDepartments.Items.IndexOf(cbDepartments.SelectedItem)].ToString()):(cbDepartments.Items[0].ToString());
                UpdateListBoxEmployee(cbDepartments.Text);
            }
            else
            {
                lbEmployees.Items.Clear();
                MessageBox.Show("Список департаментов пуст. Для продолжения работы создайте департаменты.", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        void UpdateListBoxEmployee(string NameOfDepartment)
        {
            lbEmployees.Items.Clear();
            foreach (Employee employee in myOrganization[NameOfDepartment])
                lbEmployees.Items.Add(employee);
        }

        private void MiSave_Click(object sender, RoutedEventArgs e)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Organization));
            FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            xmlSerializer.Serialize(fileStream, myOrganization);
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
                myOrganization = (Organization)xmlFormat.Deserialize(fStream);
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
                xmlSerializer.Serialize(fileStream, myOrganization);
                fileStream.Close();
            }
        }

        private void BtnAddDepartment_Click(object sender, RoutedEventArgs e)
        {
            wndNewDepartment wndNewDepartment = new wndNewDepartment();
            if (wndNewDepartment.ShowDialog() == true)
            {
                myOrganization.Add(wndNewDepartment.NameOfDepartment, new Department() { Name = wndNewDepartment.NameOfDepartment });
                UpdateCheckBoxDepartments();
                cbDepartments.Text = wndNewDepartment.NameOfDepartment;
                UpdateListBoxEmployee(wndNewDepartment.NameOfDepartment);
            };
        }

        private void BtnDeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (lbEmployees.SelectedItem != null)
            {
                myOrganization[cbDepartments.Text].Remove((Employee)lbEmployees.SelectedItem);
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
                myOrganization.Remove(cbDepartments.Text);
                UpdateCheckBoxDepartments();
            }
            else
            {
                MessageBox.Show("Список департаментов пуст", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
