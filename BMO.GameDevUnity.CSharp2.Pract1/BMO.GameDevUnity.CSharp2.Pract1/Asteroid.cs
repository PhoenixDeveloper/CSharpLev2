using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BMO.GameDevUnity.CSharp2.Pract1
{
    class Asteroid : BaseObject
    {
        public int Power { get; set; }
        public int Cost { get; set; }

        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Power = 10;
            Cost = 10;
        }

        public Asteroid(Point pos, Point dir, Size size, int power) : base(pos, dir, size)
        {
            Power = power;
            Cost = 10;
        }

        public Asteroid(Point pos, Point dir, Size size, int power, int cost) : base(pos, dir, size)
        {
            Power = power;
            Cost = cost;
        }

        public override void Draw()
        {
            Game.buffer.Graphics.FillEllipse(Brushes.White, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            PosX = Pos.X + Dir.X;
            PosY = Pos.Y + Dir.Y;
            if (Pos.X < 0) DirX = -Dir.X;
            if (Pos.X > Game.Width) DirX = -Dir.X;
            if (Pos.Y < 0) DirY = -Dir.Y;
            if (Pos.Y > Game.Height) DirY = -Dir.Y;
        }
    }
}
