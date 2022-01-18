using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Durak__Fool_
{
    class RealCard : Card
    {
        public override event ChooseEventHandler CheckCardIsOpenedEvent;

        private Suit suit;
        private int rank;
        private Image cardIm;
        private Image faceDown;
        private Point point;
        private Rectangle rect;
        private bool chosen;

        public RealCard(Suit suit, int rank)
        {
            this.suit = suit;
            this.rank = rank;
            rect = new Rectangle(0, 0, 80, 120);
            cardIm = new Bitmap($@"..\..\CardSkin\{(int)suit}\{rank % 9}.png");
            faceDown = new Bitmap(@"..\..\CardSkin\up.png");
            chosen = false;
        }

        #region useless
        //public int GetNumOfCard()
        //{
        //    return ((int)lear + rang) + (int)lear * 8;
        //}
        #endregion

        public override void Draw(Graphics graphics, bool vis)
        {
            if (vis)
                graphics.DrawImage(cardIm, point.X, point.Y, rect.Width, rect.Height);
            else
                graphics.DrawImage(faceDown, point.X, point.Y, rect.Width, rect.Height);
        }
        public override void CheckPos(Point pointer)
        {
            ChooseEventArgs e = new ChooseEventArgs(this);
            if (CheckCardIsOpenedEvent?.Invoke(this, e) ?? false)
            {
                if (!chosen && pointer.X >= point.X && pointer.X <= point.X + rect.Width
                    && pointer.Y >= point.Y && pointer.Y <= point.Y + rect.Height)
                {
                    chosen = true;
                    point.Y -= 20;
                }
                else
                {
                    if (chosen && !(pointer.X >= point.X && pointer.X <= point.X + rect.Width
                    && pointer.Y >= point.Y && pointer.Y <= point.Y + 20 + rect.Height))
                    {
                        chosen = false;
                        point.Y += 20;
                    }
                }
            }
            else
            {
                if (!chosen && pointer.X >= point.X && pointer.X <= point.X + e.dX - 1
                    && pointer.Y >= point.Y && pointer.Y <= point.Y + rect.Height)
                {
                    chosen = true;
                    point.Y -= 20;
                }
                else
                {
                    if (chosen && !(pointer.X >= point.X && pointer.X <= point.X + e.dX - 1
                    && pointer.Y >= point.Y && pointer.Y <= point.Y + 20 + rect.Height))
                    {
                        chosen = false;
                        point.Y += 20;
                    }
                }
            }
        }

        public override int X
        {
            get
            {
                return this.point.X;
            }
            set
            {
                if (value > 0)
                {
                    this.point.X = value;
                }
            }
        }
        public override int Y
        {
            get
            {
                return this.point.Y;
            }
            set
            {
                if (value > 0)
                {
                    this.point.Y = value;
                }
            }
        }
        public override Suit Suit
        {
            get => this.suit;
        }
        public override int Rank
        {
            get => this.rank;
        }
        public override bool Chosen
        {
            get => this.chosen;
        }
    }
}
