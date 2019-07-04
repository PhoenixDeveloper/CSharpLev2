using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BMO.GameDevUnity.CSharp2.Pract1
{
    class Bullet : BaseObject
    {
        public Bullet(Point pos, Point dir, Size size)
        {
            PosX = pos.X;
            PosY = pos.Y;
            DirX = dir.X;
            DirY = dir.Y;
            SizeWidth = size.Width;
            SizeHeight = size.Height;
        }

        public override void Draw()
        {
            Game.buffer.Graphics.DrawRectangle(Pens.OrangeRed, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            PosX = Pos.X + 3;
        }
    }
}
