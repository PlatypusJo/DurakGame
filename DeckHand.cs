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
    public class DeckEventArgs : EventArgs
    {
        public Card[,] gamefield;
        public Card sendcard;
        public Suit trump;
        public RoleOfPlayer role;
        public Point pos;
        public int count;
        public int index;
        public bool mademove;
        public DeckEventArgs() : base() 
        {
            gamefield = new Card[2, 6];
        }
    }

    public delegate void DeckEvent(object sender, DeckEventArgs eArgs);
    class DeckHand : Deck
    {
        public event DeckEvent GetEvent;

        int dX;
        internal enum Key
        {
            Rang,
            Lear
        }

        public DeckHand(int xcoord, int ycoord) 
        {
            dX = 50;
            cards = new RealCard[36];
            point = new Point(xcoord, ycoord);
            countOfCards = 0;
        }
        public void GetCard(object sender, DeckEventArgs eArgs) //принимает карту
        {
            cards[countOfCards] = eArgs.sendcard;
            if (sender is Human human)
            {
                ((RealCard)cards[countOfCards]).CheckCardIsOpenedEvent += CheckOpenedCard;
                human.Choose += cards[countOfCards].CheckPos;
            }
            countOfCards++;
            
        }
        public override void GiveCard(object sender, DeckEventArgs eArgs) //выдаёт карту
        {       
            if (GetEvent != null)
            {
                GetEvent.Invoke(sender, eArgs);
                if (eArgs.mademove)
                {
                    if (sender is Human human)
                    {
                        ((RealCard)cards[eArgs.index]).CheckCardIsOpenedEvent -= CheckOpenedCard;
                        human.Choose -= cards[eArgs.index].CheckPos;
                    }
                    eArgs.sendcard = cards[eArgs.index] = null;
                    eArgs.index = 0;
                    countOfCards--;
                }
            }
        }
        private bool CheckOpenedCard(object sender, ChooseEventArgs eArgs)
        {
            eArgs.dX = dX;
            int indexOfCard = Array.FindIndex(cards, 0, 36, card => card is null ? false : card == eArgs.sentCard);
            return indexOfCard == 35 || cards[indexOfCard + 1] is null;
        }
        public void Sort(Suit thrLear) // функция сортировки
        {
            int i = 0, end = i;
            for (; i < countOfCards; i++)
            {
                if (cards[i].Suit != thrLear)
                {
                    bool flag = true;
                    for (int j = i + 1; j < countOfCards && flag; j++)
                        if (cards[j].Suit == thrLear)
                        {
                            flag = false;
                            end = i;
                            Card buf = cards[i];
                            cards[i] = cards[j];
                            cards[j] = buf;
                        }
                }
                else
                {
                    end = i;
                }
            }
            qSort(0, end, Key.Rang);
            int beg = i = end;
            if (cards[beg].Suit == thrLear)
                qSort(i = beg = ++end, countOfCards - 1, Key.Lear);
            else
                qSort(i = beg = end++, countOfCards - 1, Key.Lear);
            while (i != countOfCards) 
            {
                if (cards[i + 1] is null || cards[i].Suit != cards[i + 1]?.Suit)
                {
                    end = i;
                    qSort(beg, end, Key.Rang); 
                    beg = ++i;   
                }
                else i++;
            }
            SetCoordinates();
            #region nafig
            //bool flag = false;
            //for (int i=0;i<LastCard;i++)
            //{
            //    if(cards[i].Lear != thrLear)
            //    {
            //        for (int j=i+1;j<LastCard;j++)
            //            if(cards[j].Lear==thrLear)
            //            {
            //                endLine=i;
            //                Card bufCard = cards[i];
            //                cards[i] = cards[j];
            //                cards[j] = bufCard;
            //            }
            //    }
            //    else
            //    {
            //        endLine=i;
            //    }
            //}
            //if (endLine >= 0)
            //{
            //    qSort(0, endLine);
            //    flag = true;
            //}
            //else
            //    qSort(0, LastCard-1);
            //if (flag)
            //{
            //    endLine++;
            //    qSort(endLine, LastCard-1);
            //}
            #endregion
        }
        private void qSort(int left, int right, Key key) //быстрая сортировка
        {
            int i = left, j = right;
            Card tCard = cards[(left + right) / 2];
            do
            {
                if (key == Key.Lear)
                {
                    while (cards[i].Suit < tCard.Suit) i++;
                    while (tCard.Suit < cards[j].Suit) j--;
                }
                else
                {
                    if (key == Key.Rang)
                    {
                        while (cards[i].Rank > tCard.Rank) i++;
                        while (tCard.Rank > cards[j].Rank) j--;
                    }
                }
                if (i <= j)
                {
                    Card buf = cards[i];
                    cards[i] = cards[j];
                    cards[j] = buf;
                    i++;
                    j--;
                }
            } while (i <= j);
            if (left < j) qSort(left, j, key);
            if (i < right) qSort(i, right, key);
        }
        private void SetCoordinates() // устанавливает координаты карт
        {
            for (int i=0;i < countOfCards;i++)
            {
                cards[i].X = point.X + i * dX;
                cards[i].Y = point.Y;
            }
        } 
        public override void Draw(Graphics graphics, bool vis)
        {
            for (int i = 0, j = 0; i < 36 && j <= countOfCards; i++)
            {
                if (!(cards[i] is null))
                {
                    cards[i].Draw(graphics, vis);
                    j++;
                }
            }
        }
        public Card this[int index]
        {
            get
            {
                if (index < 36 ? cards[index] is null ? false : true : false)
                    return cards[index];
                else
                    return new NullCard();
            }
        }
    }
    #region useless
    //public int GetNumOfCardsInHand()
    //{
    //    int numberofcards = 0;
    //    for (int i = 0; i < 36; i++)
    //        if (this.cards[i] != null)
    //            numberofcards++;
    //        else
    //        {
    //            //LastCard = i;
    //            break;
    //        }
    //    return numberofcards;
    //}
    #endregion
}
