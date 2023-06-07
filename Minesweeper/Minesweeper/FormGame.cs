using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class FormGame : Form
    {
        Game _game;
        public FormGame(Game game)
        {
            InitializeComponent();
            _game = game;
            panelMines.AutoSize = true;
            AutoSize = true;
        }

        private void FormGame_Load(object sender, EventArgs e)
        {
            lblUsername.Text = _game.User.Username;
            lblScore.Text = _game.Score.ToString();
            _game.CreateGame(panelMines.Controls,Mine_Click);
        }

        private void Mine_Click(object sender, EventArgs e)
        {
            var result = _game.CheckMine((Button)sender);
            lblScore.Text = _game.Score.ToString();
            if (!result)
            {
                MessageBox.Show("KAYBETTİN");
                string jsonResult = JsonConvert.SerializeObject(_game,Formatting.Indented);
                FileHelper.WriteJson(jsonResult);
            }/*
            else
            {
                MessageBox.Show("KAZANDINIZ !!!!!!!!!!!!!");
                string jsonResult = JsonConvert.SerializeObject(_game, Formatting.Indented);
                FileHelper.WriteJson(jsonResult);
            }*/
          
        }

        private void hakkındaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Minesweeper Beta");
        }

        private void RecentlyPlayedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormRecentlyPlayed formRecently = new FormRecentlyPlayed();
            formRecently.Show();
        }
    }
}
