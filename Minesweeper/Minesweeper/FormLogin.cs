using Minesweeper.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
            AutoSize = true;
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            keyValuePairs.Add("Kolay", GameDifficulty.Easy);
            keyValuePairs.Add("Orta", GameDifficulty.Normal);
            keyValuePairs.Add("Zor", GameDifficulty.Hard);
            FillComboBox();
            
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Bir kullanıcı adı giriniz","Giriş İşlemi",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;

            }
            GameDifficulty gameDifficulty = GameDifficulty.Easy;
            keyValuePairs.TryGetValue(cbDifficulties.Text,out gameDifficulty);
            
            Game game = new Game(gameDifficulty,new User {Username = textBox1.Text});
            FormGame form = new FormGame(game);
            form.Show();
            this.Hide();

        }
        List<GameDifficulty> gameDifficultyList = new List<GameDifficulty> { GameDifficulty.Easy, GameDifficulty.Normal, GameDifficulty.Hard};
        Dictionary<string, GameDifficulty> keyValuePairs = new Dictionary<string, GameDifficulty>();
        private void FillComboBox()
        {
            cbDifficulties.DataSource = keyValuePairs.Keys.ToList();
            
        }
    }
}
