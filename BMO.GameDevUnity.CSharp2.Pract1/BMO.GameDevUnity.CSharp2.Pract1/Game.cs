using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace BMO.GameDevUnity.CSharp2.Pract1
{
    static class Game
    {
        static Form gameForm;

        static int countAsteroids = 5;

        static BufferedGraphicsContext context;
        static public BufferedGraphics buffer;

        static Random random = new Random();

        static Timer timer = new Timer();

        public delegate void LogDelegate(BaseObject object1, BaseObject object2);
        static LogDelegate logCollision;

        static Image imageEnergyBoost = Image.FromFile(@"Pictures\EnergyBoost.png");

        static int store = 0;
        static public int Store
        {
            get
            {
                return store;
            }
            set
            {
                if (value < 0)
                {
                    throw new GameObjectException();
                }
                else
                {
                    store = value;
                }
            }
        }
        // Свойства
        // Ширина и высота игрового поля
        static public int Width
        {
            get
            {
                return gameForm.ClientSize.Width;
            }
            set
            {
                if (value>=0 && value <= 1000)
                {
                    gameForm.Width = value;
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
                return gameForm.ClientSize.Height;
            }
            set
            {
                if (value >= 0 && value <= 1000)
                {
                    gameForm.Height = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        static BaseObject[] objs;
        static List<Asteroid> asteroids = new List<Asteroid>();
        static List<Bullet> bullets = new List<Bullet>();
        static List<BaseObject> objectsRemove = new List<BaseObject>();
        static List<EnergyBoost> energyBoosts = new List<EnergyBoost>();
        static Ship ship;

        static public void Init(Form form)
        {            
            gameForm = form;
            asteroids.Clear();
            bullets.Clear();
            energyBoosts.Clear();
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

            gameForm.KeyDown += GameForm_KeyDown;

            logCollision = LogConsoleCollision;
            logCollision += LogFileCollision;

            Store = 0;
        }

        private static void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey) bullets.Add(new Bullet(new Point(ship.Rect.X + ship.SizeWidth, ship.Rect.Y + ship.SizeHeight/2), new Point(4, 0), new Size(5, 1)));
            if (e.KeyCode == Keys.Up) ship.Up();
            if (e.KeyCode == Keys.Down) ship.Down();
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            if (asteroids.Count == 0)
            {
                CreateAsteroids();
            }            
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
            objs = new BaseObject[1000];
            Image imagePlanet = Image.FromFile(@"Pictures\Planet.png");
            Image imageComet = Image.FromFile(@"Pictures\Comet.png");
            Image imageShip = Image.FromFile(@"Pictures\SpaceShip.png");
            for (int i = 0; i < (objs.Length *  70 / 100) ; i++)
            {
                objs[i] = new Star(new Point(random.Next(Width), random.Next(Height)), new Point(random.Next(7, 15)*(int)Math.Pow(-1, i), random.Next(7, 15) * (int)Math.Pow(-1, i)), new Size(1, 1), Pens.Silver);
            }
            for (int i = (objs.Length * 70 / 100); i < (objs.Length * 85 / 100) ; i++)
            {
                objs[i] = new Comet(new Point(random.Next(Width), random.Next(Height)), new Point(random.Next(10, 25) * (int)Math.Pow(-1, i), random.Next(10, 25) * (int)Math.Pow(-1, i)), new Size(3, 3), imageComet);
            }
            for (int i = (objs.Length * 85 / 100) ; i < objs.Length; i++)
            {
                objs[i] = new Planet(new Point(random.Next(Width), random.Next(Height)), new Point(random.Next(10) * (int)Math.Pow(-1, i), random.Next(10) * (int)Math.Pow(-1, i)), new Size(5, 5), imagePlanet);            
            }
            CreateAsteroids();
            ship = new Ship(new Point(10, 200), new Point(5, 5), new Size(50, 50), imageShip);
            ship.messageDie += Ship_messageDie;
        }

        private static void CreateAsteroids()
        {
            for (int i = 0; i < countAsteroids; i++)
            {
                asteroids.Add(new Asteroid(new Point(Width - 10, random.Next(Height)), new Point(-random.Next(3, 30), random.Next(3, 30)), new Size(30, 30), random.Next(10, 20)));
            }
            countAsteroids++;
        }


        private static void Ship_messageDie(string obj)
        {
            gameForm.Visible = false;
            timer.Stop();
            timer.Tick -= Timer_Tick;
            MessageBox.Show($"{obj}\nStore: {Store}");
            SplashScreen.ViewForm();
        }

        static public void Draw()
        {
            //Проверяем вывод графики
            buffer.Graphics.Clear(Color.Black);
            buffer.Graphics.DrawString($"Energy: {ship.Energy}%\nStore: {Store}", SystemFonts.DefaultFont, Brushes.Aqua, new Point(0, 0));
            ship.Draw();
            foreach (BaseObject obj in objs)
            {
                obj.Draw();
            }
            foreach (var asteroid in asteroids)
            {
                asteroid.Draw();
            }
            foreach (var bullet in bullets)
            {
                bullet.Draw();
            }
            foreach (var energyBoost in energyBoosts)
            {
                energyBoost.Draw();
            }
            buffer.Render();
        }

        static public void Update()
        {
            objectsRemove.Clear();

            ship.Update();

            foreach (BaseObject obj in objs)
            {
                obj.Update();              
            }

            foreach (var energyBoost in energyBoosts)
            {
                energyBoost.Update();
                if (energyBoost.Collision(ship))
                {
                    ship.EnergyHigh(energyBoost.BoostEnergy);
                    objectsRemove.Add(energyBoost);
                    logCollision(energyBoost, ship);
                }
            }

            foreach (var asteroid in asteroids)
            {
                asteroid.Update();
                foreach (var bullet in bullets)
                {
                    if (asteroid.Collision(bullet))
                    {
                        System.Media.SystemSounds.Hand.Play();
                        Store += asteroid.Cost;

                        objectsRemove.Add(asteroid);
                        objectsRemove.Add(bullet);
                        logCollision(asteroid, bullet);

                        if (random.NextDouble() * 100 > ship.Energy)
                        {
                            energyBoosts.Add(new EnergyBoost(new Point(Width - 10, random.Next(Height)), new Point(-random.Next(3, 30), random.Next(3, 30)), new Size(25, 25), imageEnergyBoost, random.Next(10, 30)));
                        }
                    }
                }
                if (asteroid.Collision(ship))
                {
                    ship.EnergyLow(asteroid.Power);
                    asteroid.DirX = -asteroid.DirX;
                    asteroid.DirY = -asteroid.DirY;
                    logCollision(asteroid, ship);
                }
            }
            
            foreach (var bullet in bullets)
            {
                if (bullet.PosX + bullet.SizeWidth > Game.Width) objectsRemove.Add(bullet);
                if (bullet.PosY + bullet.SizeHeight > Game.Height) objectsRemove.Add(bullet);
                bullet.Update();
            }

            foreach (var objectRemove in objectsRemove)
            {
                if (objectRemove is Asteroid)
                {
                    asteroids.Remove((Asteroid)objectRemove);
                }
                if (objectRemove is Bullet)
                {
                    bullets.Remove((Bullet)objectRemove);
                }
                if (objectRemove is EnergyBoost)
                {
                    energyBoosts.Remove((EnergyBoost)objectRemove);
                }
            }
        }

        static private void CheckWindowSize(Form form)
        {
            if (!(form.Height>=0 && form.Height <= 1000 && form.Width >= 0 && form.Width <= 1000))
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        static void LogConsoleCollision(BaseObject object1, BaseObject object2)
        {
            Console.WriteLine($"{DateTime.Now}:{DateTime.Now.Millisecond} : В точке ({object1.DirX}; {object1.DirY}) столкнулись {object1.GetType().Name} и {object2.GetType().Name}");
        }

        static void LogFileCollision(BaseObject object1, BaseObject object2)
        {
            File.AppendAllText("LogCollision.log", $"{DateTime.Now}:{DateTime.Now.Millisecond} : В точке ({object1.DirX}; {object1.DirY}) столкнулись {object1.GetType().Name} и {object2.GetType().Name}\n");
        }
    }
}
