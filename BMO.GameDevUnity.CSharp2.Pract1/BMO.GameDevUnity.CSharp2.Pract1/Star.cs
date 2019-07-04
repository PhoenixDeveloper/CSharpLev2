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

        public Star(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            color = Pens.Aqua;
        }

        public Star(Point pos, Point dir, Size size, Pen color) : this(pos, dir, size) //base(pos,dir,size)
        {

            this.color = color;
        }

        public override void Draw()
        {
            Game.buffer.Graphics.DrawLine(color, pos.X, pos.Y, pos.X + size.Width, pos.Y + size.Width);
            Game.buffer.Graphics.DrawLine(color, pos.X + size.Width, pos.Y, pos.X, pos.Y + size.Width);
        }

        public override void Update()
        {
            pos.X = pos.X + dir.X;
            pos.Y = pos.Y + dir.Y;
            if (pos.X < 0) dir.X = -dir.X;
            if (pos.X > Game.Width) dir.X = -dir.X;
            if (pos.Y < 0) dir.Y = -dir.Y;
            if (pos.Y > Game.Height) dir.Y = -dir.Y;
        }
    }
}
