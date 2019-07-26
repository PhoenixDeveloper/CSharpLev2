using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;

namespace BMO.GameDevUnity.CSharp2.Pract5
{
    public class Employee: INotifyPropertyChanged
    {
        string lastName;
        string firstName;
        string profession;
        int age;

        public string LastName {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
                this.NotifyPropertyChanged("LastName");
            }
        }

        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
                this.NotifyPropertyChanged("FirstName");
            }
        }

        public string Profession
        {
            get
            {
                return profession;
            }
            set
            {
                profession = value;
                this.NotifyPropertyChanged("Profession");
            }
        }

        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                try
                {
                    age = value;
                    this.NotifyPropertyChanged("Age");
                }
                catch (Exception)
                {

                    MessageBox.Show("Введены некорректные данные", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public Employee(string lastName, string firstName, string profession, int age)
        {
            LastName = lastName;
            FirstName = firstName;
            Profession = profession;
            Age = age;
        }

        public Employee()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public override string ToString()
        {
            return LastName + " " + FirstName + " " + Profession + " " + Age;
        }
    }
}
