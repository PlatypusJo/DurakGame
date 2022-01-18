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
    class NullCard : Card
    {
        public override event ChooseEventHandler CheckCardIsOpenedEvent;

        public override void Draw(Graphics graphics, bool vis)
        {

        }

        public override void CheckPos(Point pointer)
        {

        }

        public override int X { get; set; }
        public override int Y { get; set; }
        public override Suit Suit { get => Suit.NotExist; }
        public override int Rank { get => -1; }
        public override bool Chosen { get => false; }
    }
}
