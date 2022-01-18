using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Durak__Fool_
{
    class Beginner : BotPlayer
    {
        public Beginner(int index) : base(index)
        {
            behavior = RoleOfPlayer.Defender;
        }

        public override void Move(DeckEventArgs eArgs)
        {
            for (int i=0; i < 6 && !eArgs.mademove; i++)
            {
                if (eArgs.gamefield[0, i] != null)
                    if (!(eArgs.gamefield[1, i] is null))
                        continue;
                    else 
                    { 
                        for (int j = 0; j < 36; j++)
                            if (!(myHand[j] is NullCard))
                            {
                                if ((myHand[j].Suit == eArgs.trump && eArgs.gamefield[0, i].Suit != eArgs.trump) || 
                                    (myHand[j].Suit == eArgs.trump && eArgs.gamefield[0, i].Suit == eArgs.trump && myHand[j].Rank > eArgs.gamefield[0, i].Rank) || 
                                    (eArgs.gamefield[0, i].Suit == myHand[j].Suit && myHand[j].Rank > eArgs.gamefield[0, i].Rank))
                                {
                                    eArgs.role = behavior;
                                    eArgs.index = j;
                                    eArgs.sendcard = myHand[j];
                                    myHand.GiveCard(this, eArgs);
                                    break;
                                }
                            }
                    } 
            }
        }
    }
}
