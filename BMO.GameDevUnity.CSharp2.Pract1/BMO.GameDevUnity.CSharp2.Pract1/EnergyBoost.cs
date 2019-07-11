using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BMO.GameDevUnity.CSharp2.Pract1
{
    class EnergyBoost : BaseObject
    {
        public int BoostEnergy { get; set; }

        public EnergyBoost(Point pos, Point dir, Size size, Image image, int countEnergy) : base(pos, dir, size, image)
        {
            BoostEnergy = countEnergy;
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
            if (Pos.X > Game.Width) DirX = -Dir.X;
            if (Pos.Y < 0) DirY = -Dir.Y;
            if (Pos.Y > Game.Height) DirY = -Dir.Y;
        }
    }
}
