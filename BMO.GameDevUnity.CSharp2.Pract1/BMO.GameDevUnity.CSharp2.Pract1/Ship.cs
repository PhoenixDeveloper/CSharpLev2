using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BMO.GameDevUnity.CSharp2.Pract1
{
    class Ship : BaseObject
    {
        public event Action<string> messageDie;

        private int energy = 100;
        public int Energy => energy;

        public void EnergyLow(int countEnergy)
        {
            energy -= countEnergy;
            if (Energy <= 0)
            {
                Die();
            }
        }

        public void EnergyHigh(int countEnergy)
        {
            energy += countEnergy;
            if (Energy > 100)
            {
                energy = 100;
            }
        }

        public override void Draw()
        {
            Game.buffer.Graphics.FillEllipse(Brushes.Wheat, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
        }

        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public void Up()
        {
            if (Pos.Y > 0)
            {
                PosY = Pos.Y - Dir.Y;
            }
        }

        public void Down()
        {
            if (Pos.Y < Game.Height)
            {
                PosY = Pos.Y + Dir.Y;
            }
        }

        public void Die()
        {
            messageDie.Invoke("We crashed!");
        }
    }
}
