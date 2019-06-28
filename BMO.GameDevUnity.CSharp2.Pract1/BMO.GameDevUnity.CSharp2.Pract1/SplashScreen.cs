using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMO.GameDevUnity.CSharp2.Pract1
{
    static class SplashScreen
    {
        static Form formTransport;
        static int width, height;
        static Button startButton, recordsButton, endButton, showButtons;
        static bool showButtonsBool = true;
        // Свойства
        // Ширина и высота игрового поля
        static public int Width
        {
            get
            {
                return width;
            }
            set
            {
                if (value < 0)
                {
                    width = -value;
                }
                else
                {
                    width = value;
                }
            }
        }
        static public int Height
        {
            get
            {
                return height;
            }
            set
            {
                if (value < 0)
                {
                    height = -value;
                }
                else
                {
                    height = value;
                }
            }
        }

        static public void Init(Form form)
        {
            formTransport = form;
            Width = form.Width;
            Height = form.Height;
            startButton = new Button
            {
                Text = "Начать",
                Top = 100,
                Left = form.ClientSize.Width/2 - 150,
                Height = 150,
                Width = 300
            };
            form.Controls.Add(startButton);
            startButton.Click += StartButton_Click;
            recordsButton = new Button
            {
                Text = "Рекорды",
                Top = 300,
                Left = form.ClientSize.Width / 2 - 150,
                Height = 150,
                Width = 300
            };
            form.Controls.Add(recordsButton);
            recordsButton.Click += RecordsButton_Click;
            endButton = new Button()
            {
                Text = "Выход",
                Top = 500,
                Left = form.ClientSize.Width / 2 - 150,
                Height = 150,
                Width = 300
            };
            form.Controls.Add(endButton);
            endButton.Click += EndButton_Click;
            showButtons = new Button()
            {
                Text = "Интерфейс",
                Top = form.ClientSize.Height - 720,
                Left = form.ClientSize.Width - 100,
                Height = 30,
                Width = 100
            };
            form.Controls.Add(showButtons);
            showButtons.Click += ShowButtons_Click;
        }

        private static void ShowButtons_Click(object sender, EventArgs e)
        {
            if (showButtonsBool)
            {
                formTransport.Controls.Remove(startButton);
                formTransport.Controls.Remove(recordsButton);
                formTransport.Controls.Remove(endButton);
                showButtonsBool = false;
            }
            else
            {
                formTransport.Controls.Add(startButton);
                formTransport.Controls.Add(recordsButton);
                formTransport.Controls.Add(endButton);
                showButtonsBool = true;
            }
        }

        private static void EndButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private static void RecordsButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Данная функция еще в разработке", "Ахтунг!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private static void StartButton_Click(object sender, EventArgs e)
        {
            Game.Init(formTransport);
        }
    }
}
