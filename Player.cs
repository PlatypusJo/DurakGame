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
    abstract class Player
    {
        protected Point location;
        protected int playerNum;
        protected bool show;
        protected DeckHand myHand;
        protected RoleOfPlayer behavior;
        public Player(int index)
        {           
            playerNum = index;
            switch (index)
            {
                case 0:
                    location = new Point(600, 600);
                    break;
                case 1:
                    location = new Point(600, 40);
                    break;
                case 2:
                    location = new Point(40, 200);
                    break;
                case 3:
                    location = new Point(1100, 200);
                    break;   
            }
            myHand = new DeckHand(location.X, location.Y);
        }
        public virtual void Move(DeckEventArgs eArgs) { }
        public void Draw(Graphics g)
        {
            myHand.Draw(g, this.show);
        }
        public DeckHand MyDeck
        {
            get 
            { 
                return this.myHand; 
            }
        }

    }
}
