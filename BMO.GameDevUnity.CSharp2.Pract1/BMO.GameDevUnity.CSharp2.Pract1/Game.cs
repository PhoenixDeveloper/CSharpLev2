using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace BMO.GameDevUnity.CSharp2.Pract1
{
    static class Game
    {
        static Form gameForm;

        static BufferedGraphicsContext context;
        static public BufferedGraphics buffer;

        static int width, height;
        static Random random = new Random();

        static Timer timer = new Timer();
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
                if (value>=0 && value <= 1000)
                {
                    width = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
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
                if (value >= 0 && value <= 1000)
                {
                    height = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        static BaseObject[] objs;

        static Bullet bullet;

        static public void Init(Form form)
        {
            gameForm = form;
            // Графическое устройство для вывода графики            
            Graphics g;
            // предоставляет доступ к главному буферу графического контекста для текущего приложения
            context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();// Создаём объект - поверхность рисования и связываем его с формой
                                      // Запоминаем размеры формы
            CheckWindowSize(form);
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            // Связываем буфер в памяти с графическим объектом.
            // для того, чтобы рисовать в буфере
            buffer = context.Allocate(g, new Rectangle(0, 0, Width, Height));
            //Draw();
            Load();
            gameForm.FormClosing += IsClose;
            timer.Interval = 100;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Update();
            Draw();
        }

        private static void IsClose(object sender, EventArgs e)
        {
            gameForm.Visible = false;
            timer.Stop();
            MessageBox.Show("Спасибо за игру");                
            SplashScreen.ViewForm();
        }

        static public void Load()
        {
            objs = new BaseObject[46];
            Image imagePlanet = Image.FromFile(@"Pictures\Planet.png");
            Image imageComet = Image.FromFile(@"Pictures\Comet.png");
            Image imageCircle = Image.FromFile(@"Pictures\Circle.png");

            for (int i = 0; i < (objs.Length * 90 / 100) ; i++)
                objs[i] = new Star(new Point(650, i * 20), new Point(15 - i, 15 - i), new Size(35, 35), Pens.Red);
            for (int i = (objs.Length * 90 / 100); i < (objs.Length * 99 / 100) ; i++)
                objs[i] = new Comet(new Point(600, i*15), new Point(2*i*(int)Math.Pow(-1, i), i * (int)Math.Pow(-1, i)), new Size(60, 60), imageComet);
            for (int i = (objs.Length * 99 / 100) ; i < objs.Length; i++)
                objs[i] = new Planet(new Point(300, 300+i*(int)Math.Pow(-1, i)), new Point((int)Math.Pow(-1.05, i), -(int)Math.Pow(-1.05, i)), new Size(160, 160), imagePlanet);
            bullet = new Bullet(new Point(0, 200), new Point(0, 0), new Size(10, 10));
        }

        static public void Draw()
        {
            //Проверяем вывод графики
            buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in objs)
            {
                obj.Draw();
            }
            bullet.Draw();
            buffer.Render();
        }

        static public void Update()
        {
            foreach (BaseObject obj in objs)
            {
                obj.Update();
                if (obj.Collision(bullet))
                {
                    obj.PosX = random.Next(Width);
                    obj.PosY = random.Next(Height);
                }
            }
                
            bullet.Update();
        }

        static private void CheckWindowSize(Form form)
        {
            if (!(form.Height>=0 && form.Height <= 1000 && form.Width >= 0 && form.Width <= 1000))
            {
                throw new ArgumentOutOfRangeException();
            }
        }

    }
}
