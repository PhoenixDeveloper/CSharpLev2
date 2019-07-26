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
using System.Data;

namespace BMO.GameDevUnity.CSharp2.Pract5
{
    /// <summary>
    /// Логика взаимодействия для wndChangeEmployee.xaml
    /// </summary>
    public partial class wndChangeEmployee : Window
    {
        public DataRow resultRow { get; set; }

        public wndChangeEmployee(DataRow newRow)
        {
            InitializeComponent();

            this.DataContext = this;

            resultRow = newRow;
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
                resultRow["first_name"] = tbFirstName.Text;
                resultRow["last_name"] = tbLastName.Text;
                resultRow["profession"] = tbProfession.Text;
                resultRow["age"] = tbAge.Text;
                DataRowView department = (DataRowView)cbDepartments.SelectedItem;
                resultRow["id_department"] = department.Row.ItemArray[0];
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
            tbFirstName.Text = resultRow["first_name"].ToString();
            tbLastName.Text = resultRow["last_name"].ToString();
            tbProfession.Text = resultRow["profession"].ToString();
            tbAge.Text = resultRow["age"].ToString();
            cbDepartments.Text = MainWindow.dtDepartments.Rows.Find((int)resultRow["id_department"]).ItemArray[1].ToString();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbFirstName.Text = resultRow["first_name"].ToString();
            tbLastName.Text = resultRow["last_name"].ToString();
            tbProfession.Text = resultRow["profession"].ToString();
            tbAge.Text = resultRow["age"].ToString();
            cbDepartments.DataContext = MainWindow.dtDepartments.DefaultView;
            cbDepartments.Text = MainWindow.dtDepartments.Rows.Find((int)resultRow["id_department"]).ItemArray[1].ToString();
        }
    }
}
