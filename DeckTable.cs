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
    class DeckTable : Deck
    {
        public event DeckEvent GetEvent;

        private Bitmap trumpCardImage;

        public DeckTable(out Suit trump)
        {
            point.X = 350;
            point.Y = 400;
            countOfCards = 35;
            cards = new Card[36];
            for (Suit i = Suit.Hearts; i <= Suit.Clubs; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    cards[((int)i + j) + (int)i * 8] = new RealCard(i, j);
                }
            }
            Mix();
            trump = cards[0].Suit;
            trumpCardImage = new Bitmap($@"..\..\CardSkin\{(int)cards[0].Suit}\{cards[0].Rank % 9}.png");
            
        }
        public override void GiveCard(object sender, DeckEventArgs eArgs) //выдаёт картуDu
        {
            if (countOfCards > 0)
            {
                for (int i = countOfCards; i > countOfCards - eArgs.count && countOfCards - eArgs.count >= 0; i--)
                {
                    eArgs.sendcard = cards[i];
                    GetEvent?.Invoke(sender, eArgs);
                }
                countOfCards -= eArgs.count;
                Array.Clear(cards, countOfCards + 1, eArgs.count);
                if (countOfCards == 0) 
                        trumpCardImage = ChangeImageOpacity(trumpCardImage, 170); 
            }
        }
        private void Mix() // функция перемешивания
        { 
            Random random = new Random();
            for (int i = cards.Length-1; i >= 0; i--) 
            { 
                int j = random.Next(i + 1);
                if (j != i)
                {
                    Card temp = cards[j];
                    cards[j] = cards[i];
                    cards[i] = temp;
                }
            }
        }
        public override void Draw(Graphics graphics, bool vis) // рисует колоду
        {
            graphics.DrawImage(trumpCardImage, point.X + 40, point.Y - 40, 80, 120);
            for (int i = 1; i < countOfCards; i++)
            {
                graphics.DrawImage(new Bitmap(@"..\..\CardSkin\up.png"), point.X - i * 2, point.Y, 80, 120);
            }   
        }
        private Bitmap ChangeImageOpacity(Bitmap image, int a) // меняет прозрачность козырной карты (когда колода пуста)
        {
            Bitmap bmp = new Bitmap(image.Width, image.Height);
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    bmp.SetPixel(i, j, Color.FromArgb(a, image.GetPixel(i, j).R, image.GetPixel(i, j).G, image.GetPixel(i, j).B));
                }
            }
            return bmp;
        }
    }
}
