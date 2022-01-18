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
    abstract class Deck
    {
        protected int countOfCards;
        protected Point point;
        protected Card[] cards;

        public Deck() { }
        abstract public void GiveCard(object sender, DeckEventArgs eArgs);
        abstract public void Draw(Graphics graphics, bool vis);
        public int NumberOfCards
        {
            get { return countOfCards; }
        }
    }
}
