using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BMO.GameDevUnity.CSharp2.Pract1
{
    class Star:BaseObject
    {
        Pen color;

        public Pen Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }

        public Star(Point pos, Point dir, Size size)
        {
            PosX = pos.X;
            PosY = pos.Y;
            DirX = dir.X;
            DirY = dir.Y;
            SizeWidth = size.Width;
            SizeHeight = size.Height;
        }

        public Star(Point pos, Point dir, Size size, Pen color)
        {
            PosX = pos.X;
            PosY = pos.Y;
            DirX = dir.X;
            DirY = dir.Y;
            SizeWidth = size.Width;
            SizeHeight = size.Height;
            Color = color;
        }

        public override void Draw()
        {
            Game.buffer.Graphics.DrawLine(color, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y + Size.Width);
            Game.buffer.Graphics.DrawLine(color, Pos.X + Size.Width, Pos.Y, Pos.X, Pos.Y + Size.Width);
        }

        public override void Update()
        {
            PosX = Pos.X + Dir.X;
            PosY = Pos.Y + Dir.Y;
            if (Pos.X < 0) DirX = -Dir.X;
            if (Pos.X + Size.Width > Game.Width) DirX = -Dir.X;
            if (Pos.Y < 0) DirY = -Dir.Y;
            if (Pos.Y + Size.Height > Game.Height) DirY = -Dir.Y;
        }
    }
}
