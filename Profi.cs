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
    class Profi : BotPlayer
    {
        public Profi(int index) : base(index)
        {

        }

        public override void Move(DeckEventArgs eArgs)
        {
            #region example
            //Card[] cardsInHand = new Card[36];
            //Array.Copy(myHand.ArrayOfCards, cardsInHand, 36);
            //int x = cardsInHand[0].X;
            #endregion
        }
    }
}
