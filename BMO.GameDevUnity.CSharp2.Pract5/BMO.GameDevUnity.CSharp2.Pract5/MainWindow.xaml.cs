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
using System.Data;
using System.Data.SqlClient;

namespace BMO.GameDevUnity.CSharp2.Pract5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //static public Organization MyOrganization { get; set; }

        static string fileName = "data.xml";

        SqlConnection connection;
        SqlDataAdapter adapterEmployees;
        public static DataTable dtEmployees;
        SqlDataAdapter adapterDepartments;
        public static DataTable dtDepartments;

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;          

            btnDeleteEmployee.Visibility = Visibility.Hidden;
            btnChangeEmployee.Visibility = Visibility.Hidden;
            
            
        }

        private void WndMain_Loaded(object sender, RoutedEventArgs e)
        {
            //string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MyDatabaseCSharpLev2Pract7;Integrated Security=True;Pooling=False";
            // Из-за особенностей игнорирования Git, необходимо перенести базу данных из корня проекта в попку выполнения bin/Debug
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\MyDatabaseCSharpLev2Pract7.mdf;Integrated Security=True;Pooling=False";
            connection = new SqlConnection(connectionString);
            adapterEmployees = new SqlDataAdapter();
            adapterDepartments = new SqlDataAdapter();

            // SELECT Departments
            SqlCommand command = new SqlCommand("SELECT Id, name FROM Departments", connection);
            adapterDepartments.SelectCommand = command;

            // INSERT Department
            command = new SqlCommand(@"INSERT INTO Departments(name) VALUES (@name); SET @Id = @@IDENTITY;", connection);
            command.Parameters.Add("@name", SqlDbType.NVarChar, 50, "name");
            SqlParameter param = command.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            param.Direction = ParameterDirection.Output;
            adapterDepartments.InsertCommand = command;

            // UPDATE Department
            command = new SqlCommand("UPDATE Departments SET name = @name WHERE Id = @Id", connection);
            command.Parameters.Add("@name", SqlDbType.NVarChar, 50, "name");
            param = command.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            param.SourceVersion = DataRowVersion.Original;
            adapterDepartments.UpdateCommand = command;

            // DELETE Department
            command = new SqlCommand("DELETE FROM Departments WHERE Id = @Id", connection);
            param = command.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            param.SourceVersion = DataRowVersion.Original;
            adapterDepartments.DeleteCommand = command;

            // Заполнение департаментов
            dtDepartments = new DataTable();
            adapterDepartments.Fill(dtDepartments);
            dtDepartments.PrimaryKey = new DataColumn[1] { dtDepartments.Columns[0] };
            dtDepartments.Columns[0].AutoIncrement = true;
            dtDepartments.Columns[0].AutoIncrementStep = 1;
            cbDepartments.DataContext = dtDepartments.DefaultView;
            cbDepartments.Text = dtDepartments.Rows[0].ItemArray[1].ToString();
            if (cbDepartments.Items.Count == 0)
            {
                btnChangeDepartment.Visibility = Visibility.Hidden;
                btnDeleteDepartment.Visibility = Visibility.Hidden;
            }


            // SELECT Employees
            command = new SqlCommand($"SELECT Id, last_name, first_name, profession, age, id_department FROM Employees WHERE id_department = {dtDepartments.Rows[0].ItemArray[0]}", connection);
            adapterEmployees.SelectCommand = command;

            // INSERT Employee
            command = new SqlCommand(@"INSERT INTO Employees(last_name, first_name, profession, age, id_department) VALUES (@last_name, @first_name, @profession, @age, @id_department); SET @Id = @@IDENTITY;", connection);
            command.Parameters.Add("@last_name", SqlDbType.NVarChar, 50, "last_name");
            command.Parameters.Add("@first_name", SqlDbType.NVarChar, 50, "first_name");
            command.Parameters.Add("@profession", SqlDbType.NVarChar, 50, "profession");
            command.Parameters.Add("@age", SqlDbType.Int, 0, "age");
            command.Parameters.Add("@id_department", SqlDbType.Int, 0, "id_department");
            param = command.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            param.Direction = ParameterDirection.Output;
            adapterEmployees.InsertCommand = command;

            // UPDATE Employee
            command = new SqlCommand("UPDATE Employees SET last_name = @last_name, first_name = @first_name, profession = @profession, age = @age, id_department = @id_department WHERE Id = @Id", connection);
            command.Parameters.Add("@last_name", SqlDbType.NVarChar, 50, "last_name");
            command.Parameters.Add("@first_name", SqlDbType.NVarChar, 50, "first_name");
            command.Parameters.Add("@profession", SqlDbType.NVarChar, 50, "profession");
            command.Parameters.Add("@age", SqlDbType.Int, 0, "age");
            command.Parameters.Add("@id_department", SqlDbType.Int, 0, "id_department");
            param = command.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            param.SourceVersion = DataRowVersion.Original;
            adapterEmployees.UpdateCommand = command;

            // DELETE Emmployee
            command = new SqlCommand("DELETE FROM Employees WHERE Id = @Id", connection);
            param = command.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            param.SourceVersion = DataRowVersion.Original;
            adapterEmployees.DeleteCommand = command;

            // Заполнение сотрудников
            dtEmployees = new DataTable();
            adapterEmployees.Fill(dtEmployees);
            dtEmployees.PrimaryKey = new DataColumn[1] { dtEmployees.Columns[0] };
            lvEmployees.DataContext = dtEmployees.DefaultView;
        }

        private void MiExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        void UpdateCheckBoxDepartments()
        {
            if (cbDepartments.Items.Count != 0)
            {
                if (cbDepartments.SelectedItem == null)
                {
                    cbDepartments.SelectedItem = cbDepartments.Items[cbDepartments.Items.Count - 1];
                }
                DataRowView department = (DataRowView)cbDepartments.SelectedItem;
                cbDepartments.Text = department.Row.ItemArray[1].ToString();
                UpdateListBoxEmployee();
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

        void UpdateListBoxEmployee()
        {
            DataRowView department = (DataRowView)cbDepartments.SelectedItem; 
            SqlCommand command = new SqlCommand($"SELECT Id, last_name, first_name, profession, age, id_department FROM Employees WHERE id_department = {department.Row.ItemArray[0]}", connection);
            adapterEmployees.SelectCommand = command;
            dtEmployees.Clear();
            adapterEmployees.Fill(dtEmployees);

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
            //xmlSerializer.Serialize(fileStream, MyOrganization);
            fileStream.Close();

        }

        private void CbDepartments_DropDownClosed(object sender, EventArgs e)
        {
            if (cbDepartments.Items.Count != 0)
            {
                UpdateListBoxEmployee();
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
                //MyOrganization = (Organization)xmlFormat.Deserialize(fStream);
                UpdateCheckBoxDepartments();
                if (cbDepartments.Items.Count != 0)
                {                    
                    UpdateListBoxEmployee();
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
                //xmlSerializer.Serialize(fileStream, MyOrganization);
                fileStream.Close();
            }
        }

        private void BtnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            DataRow newRow = dtEmployees.NewRow();
            wndNewEmployee wndNewEmployee = new wndNewEmployee(newRow);
            if (wndNewEmployee.ShowDialog() == true)
            {
                dtEmployees.Rows.Add(wndNewEmployee.resultRow);
                adapterEmployees.Update(dtEmployees);
                cbDepartments.Text = dtDepartments.Rows.Find((int)wndNewEmployee.resultRow["id_department"]).ItemArray[1].ToString();
                UpdateListBoxEmployee();
            };
        }

        private void BtnAddDepartment_Click(object sender, RoutedEventArgs e)
        {
            DataRow newRow = dtDepartments.NewRow();
            wndNewDepartment wndNewDepartment = new wndNewDepartment(newRow);
            if (wndNewDepartment.ShowDialog() == true)
            {
                dtDepartments.Rows.Add(wndNewDepartment.resultRow);
                adapterDepartments.Update(dtDepartments);
                cbDepartments.SelectedItem = dtDepartments.Rows.Find((int)wndNewDepartment.resultRow["Id"]);
                UpdateCheckBoxDepartments();
                cbDepartments.Text = dtDepartments.Rows.Find((int)wndNewDepartment.resultRow["Id"]).ItemArray[1].ToString();
                UpdateListBoxEmployee();
            };
        }

        private void BtnDeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            DataRowView newRow = (DataRowView)lvEmployees.SelectedItem;
            if (lvEmployees.SelectedItem != null)
            {
                newRow.Row.Delete();
                adapterEmployees.Update(dtEmployees);
                UpdateListBoxEmployee();
            }
            else
            {
                MessageBox.Show("Не выбран сотрудник", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnDeleteDepartment_Click(object sender, RoutedEventArgs e)
        {
            DataRowView newRow = (DataRowView)cbDepartments.SelectedItem;
            if (cbDepartments.Items.Count != 0)
            {
                newRow.Row.Delete();
                adapterDepartments.Update(dtDepartments);
                UpdateCheckBoxDepartments();
            }
            else
            {
                MessageBox.Show("Список департаментов пуст", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnChangeEmployee_Click(object sender, RoutedEventArgs e)
        {
            DataRowView newRow = (DataRowView)lvEmployees.SelectedItem;
            newRow.BeginEdit();
            wndChangeEmployee wndChangeEmployee = new wndChangeEmployee(newRow.Row);            
            if (lvEmployees.SelectedItem != null)
            {
                if (wndChangeEmployee.ShowDialog() == true)
                {
                    newRow.EndEdit();
                    adapterEmployees.Update(dtEmployees);
                    cbDepartments.Text = dtDepartments.Rows.Find((int)wndChangeEmployee.resultRow["id_department"]).ItemArray[1].ToString();
                    UpdateListBoxEmployee();
                }
                else
                {
                    newRow.CancelEdit();
                }
            }
            else
            {
                MessageBox.Show("Не выбран сотрудник", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }            
        }

        private void BtnChangeDepartment_Click(object sender, RoutedEventArgs e)
        {
            DataRowView newRow = (DataRowView)cbDepartments.SelectedItem;
            newRow.BeginEdit();
            wndChangeDepartment wndChangeDepartment = new wndChangeDepartment(newRow.Row);
            if (cbDepartments.Items.Count != 0)
            {                
                if (wndChangeDepartment.ShowDialog() == true)
                {
                    newRow.EndEdit();
                    adapterDepartments.Update(dtDepartments);
                    cbDepartments.SelectedItem = dtDepartments.Rows.Find((int)wndChangeDepartment.resultRow["Id"]);
                    cbDepartments.Text = dtDepartments.Rows.Find((int)wndChangeDepartment.resultRow["Id"]).ItemArray[1].ToString();
                    UpdateCheckBoxDepartments();
                    UpdateListBoxEmployee();
                }
                else
                {
                    newRow.CancelEdit();
                }
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
