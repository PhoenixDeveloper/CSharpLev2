using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BMO.GameDevUnity.CSharp2.Pract1
{
    class Comet:BaseObject
    {

        public Comet(Point pos, Point dir, Size size, Image image) : base(pos, dir, size, image)
        {
        }

        public override void Draw()
        {
            Game.buffer.Graphics.DrawImage(Image, Pos.X, Pos.Y, Size.Width, Size.Height);
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
