using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BMO.GameDevUnity.CSharp2.Pract1
{
    class BaseObject
    {
        protected Point pos;
        protected Point dir;
        protected Size size;
        protected Image image;

        public BaseObject(Point pos, Point dir, Size size)
        {
            this.pos = pos;
            this.dir = dir;
            this.size = size;
        }

        public BaseObject(Point pos, Point dir, Image image)
        {
            this.pos = pos;
            this.dir = dir;
            this.image = image;
        }

        public virtual void Draw()
        {
            if (image == null)
            {
                Game.buffer.Graphics.DrawEllipse(Pens.Wheat, pos.X, pos.Y, size.Width, size.Height);
            }
            else
            {
                Game.buffer.Graphics.DrawImage(image, pos);
            }
            
        }



        public void Update()
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
