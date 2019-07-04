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
        static BufferedGraphicsContext context;
        static public BufferedGraphics buffer;

        static int width, height;
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
                if (value<0)
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
        static BaseObject[] objs;

        static public void Init(Form form)
        {
            // Графическое устройство для вывода графики            
            Graphics g;
            // предоставляет доступ к главному буферу графического контекста для текущего приложения
            context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();// Создаём объект - поверхность рисования и связываем его с формой
                                      // Запоминаем размеры формы
            Width = form.Width;
            Height = form.Height;
            // Связываем буфер в памяти с графическим объектом.
            // для того, чтобы рисовать в буфере
            buffer = context.Allocate(g, new Rectangle(0, 0, Width, Height));
            //Draw();
            Load();
            Timer timer = new Timer();
            timer.Interval = 100;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Update();
            Draw();
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
                objs[i] = new Comet(new Point(600, i*15), new Point(5+2*i*(int)Math.Pow(-1, i), 5 - i * (int)Math.Pow(-1, i)), imageComet);
            for (int i = (objs.Length * 99 / 100) ; i < objs.Length; i++)
                objs[i] = new Planet(new Point(300, 300+i*(int)Math.Pow(-1, i)), new Point((int)Math.Pow(-1.05, i), -(int)Math.Pow(-1.05, i)), imagePlanet);
        }

        static public void Draw()
        {
            //Проверяем вывод графики
            buffer.Graphics.Clear(Color.Black);

            foreach (BaseObject obj in objs)
            {
                obj.Draw();
            }
            buffer.Render();
        }

        static public void Update()
        {
            foreach (BaseObject obj in objs)
                obj.Update();
        }
    }
}
