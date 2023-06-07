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
    public partial class FormRecentlyPlayed : Form
    {
        public FormRecentlyPlayed()
        {
            InitializeComponent();
        }

        private void FormRecentlyPlayed_Load(object sender, EventArgs e)
        {
            Game game = FileHelper.ReadJson();
            if (game !=null)
            {
                lblUsername.Text = game.User.Username;
                lblScore.Text = game.Score.ToString();
                lblDifficulty.Text = game.Difficulty.ToString();
            }
            

        }
    }
}
