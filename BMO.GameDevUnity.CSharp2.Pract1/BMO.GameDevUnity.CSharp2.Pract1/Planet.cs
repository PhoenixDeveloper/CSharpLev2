using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BMO.GameDevUnity.CSharp2.Pract1
{
    class Planet:BaseObject
    {

        public Planet(Point pos, Point dir, Size size, Image image)
        {
            PosX = pos.X;
            PosY = pos.Y;
            DirX = dir.X;
            DirY = dir.Y;
            SizeWidth = size.Width;
            SizeHeight = size.Height;
            Image = image;
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
            if (Pos.X+Size.Width > Game.Width) DirX = -Dir.X;
            if (Pos.Y < 0) DirY = -Dir.Y;
            if (Pos.Y+Size.Height > Game.Height) DirY = -Dir.Y;
        }
    }
}
