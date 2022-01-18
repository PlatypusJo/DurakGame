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
    public class ChooseEventArgs : EventArgs
    {
        public Card sentCard;
        public int dX;
        public ChooseEventArgs(Card card)
        {
            sentCard = card;
        }
    }

    public delegate bool ChooseEventHandler(object sender, ChooseEventArgs eArgs);
    public abstract class Card
    {
        public abstract event ChooseEventHandler CheckCardIsOpenedEvent;

        public abstract void Draw(Graphics graphics, bool vis);
        public abstract void CheckPos(Point pointer);
        public static bool operator ==(Card card1, Card card2)
        {
            return (card1 is null && card2 is null) ? true : (card1 is null || card2 is null) ? false : (card1.Suit == card2.Suit && card1.Rank == card2.Rank);
        }
        public static bool operator !=(Card card1, Card card2)
        {
            return (card1 is null && card2 is null) ? false : (card1 is null || card2 is null) ? true : (card1.Suit != card2.Suit || card1.Rank != card2.Rank);
        }
        public abstract int X { get; set; }
        public abstract int Y { get; set; }
        public abstract int Rank { get; }
        public abstract Suit Suit { get; }
        public abstract bool Chosen { get; }

        //public void ComeToThePlace()
        //{
        //    int dx = point.X - oldpoint.X;
        //    int dy = point.Y - oldpoint.Y;
        //    while (oldpoint.X != point.X && oldpoint.Y != point.Y)
        //    {

        //    }
        //}
    }
}
