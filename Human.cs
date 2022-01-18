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
    class Human : Player
    {
        public delegate void MessageHandler(Point point);
        public event MessageHandler Choose;
        public Human(int index) : base(index)
        {
            show = true;
            behavior = RoleOfPlayer.Attacker;
        }
        public override void Move(DeckEventArgs eArgs)
        {
            if (eArgs.pos.X >= 0 & eArgs.pos.X <= 1600 & eArgs.pos.Y >= 530 & eArgs.pos.Y <= 850)
                for (int i = 0; i < 6; i++)
                {
                    if (myHand[i].Chosen)
                    {
                        eArgs.role = behavior;
                        eArgs.index = i;
                        eArgs.sendcard = myHand[i];
                        myHand.GiveCard(this, eArgs);
                    }
                }
            else    
            {
                if (eArgs.pos.X >= 510 & eArgs.pos.X <= 960 & eArgs.pos.Y >= 380 & eArgs.pos.Y <= 530) { }
            }
        }
        public void CheckPoint(Point loc)
        {
            if(loc.X >= 0 && loc.X <= 1600 && loc.Y >= 530 && loc.Y <= 850)
            {
                Choose?.Invoke(loc);
            }
        }
    }
}
