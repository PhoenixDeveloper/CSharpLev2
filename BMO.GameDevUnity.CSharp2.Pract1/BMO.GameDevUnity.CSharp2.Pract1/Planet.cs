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

        public Planet(Point pos, Point dir, Image image) : base(pos, dir, image)
        {
        }

        public override void Draw()
        {
            Game.buffer.Graphics.DrawImage(image, pos);
        }
    }
}
