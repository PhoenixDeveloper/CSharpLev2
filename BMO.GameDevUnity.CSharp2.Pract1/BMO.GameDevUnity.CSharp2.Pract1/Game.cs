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
        // Свойства
        // Ширина и высота игрового поля
        static public int Width { get; set; }
        static public int Height { get; set; }
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
            objs = new BaseObject[30 + 1];
            for (int i = 0; i < objs.Length / 2; i++)
                objs[i] = new BaseObject(new Point(600, i * 20), new Point(15 - i, 15 - i), new Size(20, 20));
            for (int i = objs.Length / 2; i < objs.Length; i++)
                objs[i] = new Star(new Point(600, i * 20), new Point(15 - i, 15 - i), new Size(20, 20), Pens.Red);
            Image image = Image.FromFile(@"Pictures\Planet.png");
            objs[30] = new Planet(new Point(300, 300), new Point(-1, 0), new Size(100, 100), image);
        }

        static public void Draw()
        {
            //Проверяем вывод графики
            buffer.Graphics.Clear(Color.Black);
            //buffer.Graphics.DrawRectangle(Pens.White, new Rectangle(100, 100, 200, 200));
            //buffer.Graphics.FillEllipse(Brushes.Wheat, new Rectangle(100, 100, 200, 200));
            //buffer.Graphics.DrawString("123", SystemFonts.DefaultFont, Brushes.Aqua, new Point(0, 0));
            //buffer.Graphics.DrawImage()

            foreach (BaseObject obj in objs)
            {
                /*if (obj is Star) (obj as Star).Draw();
                if (obj is BaseObject) (obj as BaseObject).Draw();*/
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
