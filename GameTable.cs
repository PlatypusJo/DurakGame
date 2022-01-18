using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Durak__Fool_
{
    public enum Suit
    {
        NotExist = -1,
        Hearts,
        Diamonds,
        Spades,
        Clubs   
    }

    public enum RoleOfPlayer
    {
        Attacker = 1,
        Defender
    }

    public partial class GameTable : Form
    {
        Suit trumpSuit;
        DeckTable maindeck;
        BattleField field;
        List<Player> players;
        Graphics g1, g2;
        Bitmap fon1, fon2;
        public GameTable()
        {
            InitializeComponent();
            Init();
            for (int i = 0; i < 2; i++)
            {
                WhoNeedCards(players[i], maindeck);
            }
            for (int i=0;i<players[0].MyDeck.NumberOfCards;i++)
            {
                ((Human)players[0]).Choose += players[0].MyDeck[i].CheckPos;
            }
            #region useless
            /*handpl.Draw();
            this.Controls.Add(Form1.deckpict);
            this.Controls.Add(Form1.cardpict[maindeck.NumCardInDeck(0)]);
            for (int i = 0; i < handpl.GetNumOfCardsInHand(); i++)
            {
                this.Controls.Add(Form1.cardpict[handpl.NumCardInDeck(i)]);
            }*/
            #endregion
        }
        public void Init()
        {
            field = new BattleField();
            maindeck = new DeckTable(out trumpSuit);
            players = new List<Player>();
            players.Add(new Human(0));
            for (int i=0;i<1;i++)
            {
                players.Add(new Beginner(i + 1));
            }
            players[0].MyDeck.GetEvent += field.AddCard;
            players[1].MyDeck.GetEvent += field.AddCard;
            fon1 = new Bitmap(Image.FromFile(@"D:\Программы Visual studio\FoolPROJECT\поле.png"));
            fon2 = new Bitmap(Image.FromFile(@"D:\Программы Visual studio\FoolPROJECT\поле.png"));
            g1 = this.CreateGraphics();
            g2 = Graphics.FromImage(fon2);
            timer1.Enabled = true;
        }
        private void DrawTable()
        {
            maindeck.Draw(g2, true);
            field.Draw(g2);
            for (int i = 0; i < 2; i++)
            {
                players[i].Draw(g2);
            }
            g1.DrawImage(fon2, 0, 0);
            g2.DrawImage(fon1, 0, 0);
        }
        private void WhoNeedCards(Player player, DeckTable deckTable)
        {
            if (player.MyDeck.NumberOfCards < 6)
            {
                DeckEventArgs e = new DeckEventArgs();
                deckTable.GetEvent += player.MyDeck.GetCard;
                e.count = 6 - player.MyDeck.NumberOfCards;
                deckTable.GiveCard(player, e);
                deckTable.GetEvent -= player.MyDeck.GetCard;
            }
            player.MyDeck.Sort(trumpSuit);
        }

        private void MClick(object sender, MouseEventArgs e)
        {
            DeckEventArgs ev = new DeckEventArgs();
            ev.pos = e.Location;
            ev.trump = trumpSuit;
            players[0].Move(ev);
            if (ev.mademove)
            {
                ev.mademove = false;
                Array.Copy(field.Cells, ev.gamefield, 12);
                players[1].Move(ev);
                ev.mademove = false;
            }
        }

        private void CheckMouse(object sender, MouseEventArgs e)
        {
            ((Human)players[0]).CheckPoint(e.Location);
        }
        private void Tick(object sender, EventArgs e)
        {
            DrawTable();
        }
    }
}
