using Minesweeper.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public class Game
    {
        private int row = 6;
        private int column = 6;
        public Guid GameId { get; set; }
        public GameDifficulty Difficulty { get; set; }
        public User User { get; set; }
        public int Score { get; set; }

        private List<List<Button>> minesList;

        private Control.ControlCollection gameArea;

        private Action<object, EventArgs> mine_Click;

        public Game(GameDifficulty gameDifficulty, User user)
        {
            Difficulty = gameDifficulty;
            User = user;
            GameId = Guid.NewGuid();
        }

        private void CreateMines()
        {
            minesList = new List<List<Button>>();
            int locationX = 0, locationY = 0;
            Random random = new Random();
            int minesPossibility = 0;
            int isMine = 0;

            CheckGameDifficulty(ref minesPossibility);
            for (int i = 0; i < row; i++)
            {
                List<Button> mines = new List<Button>();
                for (int j = 0; j < column; j++)
                {
                    isMine = random.Next(0, minesPossibility);

                    mines.Add(new Button
                    {
                        BackColor = Color.White,
                        Width = 45,
                        Height = 35,
                        Location = new Point(30 + locationX, 12 + locationY),
                        Tag = isMine >= 0 && isMine <= 2 ? random.Next(1, 10).ToString() : isMine >= 3 && isMine <= 4 ? $"{j},{i}" : "bomb"
                    });



                    locationX += 50;

                }
                locationY += 40;
                locationX = 0;
                minesList.Add(mines);

            }



        }

        public void CreateGame(Control.ControlCollection control, Action<object, EventArgs> mine_Click)
        {
            CreateMines();

            foreach (var item in minesList)
            {
                foreach (var mine in item)
                {
                    mine.Click += new EventHandler(mine_Click);
                    control.Add(mine);
                }
            }
            gameArea = control;
            this.mine_Click = mine_Click;
        }

        private void CheckGameDifficulty(ref int minesPossibility)
        {
            switch (Difficulty)
            {
                case GameDifficulty.Easy:
                    row = 8; column = 8;
                    minesPossibility = 6;
                    break;
                case GameDifficulty.Normal:
                    row = 10; column = 10;
                    minesPossibility = 6;
                    break;
                case GameDifficulty.Hard:
                    row = 14; column = 15;
                    minesPossibility = 6;
                    break;
            }
        }

        public bool CheckMine(Button selectedMine)
        {
            Button mine = null;
            foreach (var item in minesList)
            {
                foreach (var listItem in item)
                {
                    if (listItem == selectedMine)
                    {
                        mine = listItem; break;

                    }

                }
            }

            string tag = (string)mine.Tag;

            if (tag.Equals("bomb"))
            {
                FinishedGame();
                return false;
            }


            else if (tag.Contains(","))
            {
                int xCoordinate, yCoordinate;
                string[] coordinates = tag.Split(',');
                xCoordinate = int.Parse(coordinates[0]);
                yCoordinate = int.Parse(coordinates[1]);
                Random random = new Random();
                int randomX = random.Next(3, 7);
                int randomY = random.Next(3, 7);

                for (int y = 0; y < randomY; y++)
                {
                    for (int x = 0; x < randomX; x++)
                    {
                        Button nextMine = null;


                        if (yCoordinate + y >= row)
                        {
                            if (xCoordinate + x >= column)
                            {
                                nextMine = minesList[row - 1][column - 1];
                                ShowMine(nextMine);
                                continue;
                            }
                            nextMine = minesList[row - 1][xCoordinate + x];
                            ShowMine(nextMine);
                            continue;
                        }
                       
                        else if (xCoordinate + x >= column)
                        {
                            if (yCoordinate + y >= row)
                            {
                                nextMine = minesList[row - 1][column - 1];
                                ShowMine(nextMine);
                                continue;
                            }
                            nextMine = minesList[yCoordinate + y][column - 1];
                            ShowMine(nextMine);
                            continue;
                        }

                        nextMine = minesList[yCoordinate + y][xCoordinate + x];
                        ShowMine(nextMine);
                    }
                }
            }
            else
            {
                mine.Text = tag;
                ShowMine(mine);
            }

            return true;

        }


        private bool CheckGame()
        {
            foreach (Button mine in gameArea)
            {
                if (!(mine.Tag.ToString() != "bomb" && mine.FlatStyle == FlatStyle.Flat))
                {
                    return true;
                }
            }
            return false;

        }

        private void ShowMine(Button mine, bool isFinish = false)
        {
            mine.Click -= new EventHandler(mine_Click);

            if (mine.Tag.ToString() == "bomb")
            {
                mine.BackgroundImage = Resources.mine;
                mine.BackgroundImageLayout = ImageLayout.Stretch;
            }


            else if (!mine.Tag.ToString().Contains(","))
            {
                mine.Text = mine.Tag.ToString();
                if (!isFinish && !(mine.FlatStyle == FlatStyle.Flat))
                    Score += int.Parse(mine.Tag.ToString());
            }

            mine.FlatAppearance.BorderSize = 0;
            mine.FlatAppearance.BorderColor = Color.WhiteSmoke;
            mine.FlatAppearance.MouseOverBackColor = Color.Transparent;
            mine.FlatAppearance.MouseDownBackColor = Color.Transparent;
            mine.BackColor = Color.Transparent;
            mine.FlatStyle = FlatStyle.Flat;
        }

        private void FinishedGame()
        {
            foreach (Button mine in gameArea)
            {
                if (string.IsNullOrEmpty(mine.Name))
                {
                    ShowMine(mine, true);

                }

            }
        }
    }
}
