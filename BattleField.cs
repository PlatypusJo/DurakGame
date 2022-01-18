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
    public class BattleField
    {
        protected Card[,] cells;
        private int countfillcells1, countfillcells2;
        private Point point;
        public BattleField()
        {
            countfillcells1 = 0;
            countfillcells2 = 0;
            cells = new Card[2, 6];
            point = new Point(510, 400);
        }
        private bool Compare(Suit thrump, Card card) //сравнивает карты
        {
            if (cells[0, 0] is null)
                return true;
            else
            {
                bool flag = false;
                for (int i = 0; i < 6 && !flag; i++)
                {
                    if (!(cells[0, i] is null))
                        if (cells[0, i].Rank == card.Rank)
                            flag = true;
                    if (!(cells[1, i] is null))
                        if (cells[1, i].Rank == card.Rank)
                            flag = true;
                }
                if (flag)
                    return true;
                else
                    return false;
            }
        }
        public void AddCard(object sender,DeckEventArgs eArgs) //добавляет карты
        {
            
            if (eArgs.role == RoleOfPlayer.Attacker)
            { 
                if (countfillcells1 < 6)
                {
                    if (cells[0, countfillcells1] is null)
                    {
                        if (sender is Human)
                        {
                            if (Compare(eArgs.trump, eArgs.sendcard))
                            {
                                cells[0, countfillcells1] = eArgs.sendcard;
                                cells[0, countfillcells1].X = point.X + countfillcells1 * 90;
                                cells[0, countfillcells1].Y = point.Y - 50;
                                countfillcells1++;
                                eArgs.mademove = true;
                            }
                            else
                                eArgs.mademove = false;
                        }
                        else
                        {
                            cells[0, countfillcells1] = eArgs.sendcard;
                            cells[0, countfillcells1].X = point.X + countfillcells1 * 90;
                            cells[0, countfillcells1].Y = point.Y - 50;
                            countfillcells1++;
                            eArgs.mademove = true;
                        }
                    
                    }
                    else eArgs.mademove = false;
                }
                else eArgs.mademove = false;
            }
            else
            {
                if (cells[1, countfillcells2] is null)
                {
                    {
                        cells[1, countfillcells2] = eArgs.sendcard;
                        cells[1, countfillcells2].X = point.X + countfillcells2 * 90;
                        cells[1, countfillcells2].Y = point.Y;
                        countfillcells2++;
                        eArgs.mademove = true;
                    }
                }
                else eArgs.mademove = false;
            }
        }

        public void Take() //игрок берёт карты
        {

        }

        public void Beat() //сброс
        {

        }
        public void Draw(Graphics graphics)
        {
            int i = 0, j = 0;
            while (i < countfillcells1 || j < countfillcells2)
            {
                cells[0, i]?.Draw(graphics, true);
                cells[1, j]?.Draw(graphics, true);
                i++;
                j++;
            }
        }

        public Card[,] Cells
        {
            get
            {
                return cells;
            }
        }
    }
}
