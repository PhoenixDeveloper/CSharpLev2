﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BMO.GameDevUnity.CSharp2.Pract1
{
    abstract class BaseObject:ICollision
    {
        Point pos;
        Point dir;
        Size size;
        Image image;

        protected Point Pos
        {
            get
            {
                return pos;
            }
        }

        protected Point Dir
        {
            get
            {
                return dir;
            }
        }

        protected Size Size
        {
            get
            {
                return size;
            }
        }

        public Image Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
            }
        }

        public int PosX
        {
            get
            {
                return pos.X;
            }
            set
            {
                pos.X = value;
            }
        }

        public int PosY
        {
            get
            {
                return pos.Y;
            }
            set
            {
                pos.Y = value;
            }
        }

        public int DirX
        {
            get
            {
                return dir.X;
            }
            set
            {
                dir.X = value;
            }
        }

        public int DirY
        {
            get
            {
                return dir.Y;
            }
            set
            {
                dir.Y = value;
            }
        }

        public int SizeWidth
        {
            get
            {
                return size.Width;
            }
            set
            {
                if (value < 0)
                {
                    size.Width = -value;
                }
                else
                {
                    size.Width = value;
                }                
            }
        }

        public int SizeHeight
        {
            get
            {
                return size.Height;
            }
            set
            {
                if (value < 0)
                {
                    size.Height = -value;
                }
                else
                {
                    size.Height = value;
                }
            }
        }

        public Rectangle Rect => new Rectangle(Pos, Size);

        public abstract void Draw();

        public abstract void Update();

        public bool Collision(ICollision obj)
        {
            return obj.Rect.IntersectsWith(this.Rect);
        }
    }
}
