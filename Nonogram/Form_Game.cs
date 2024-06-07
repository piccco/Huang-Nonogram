using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Nonogram
{
    public partial class Form_Game : Form
    {
        public string levelInfo;
        public string levelName;
        private Dictionary<string, string> infoDict = new Dictionary<string, string>();
        int[][] row;
        int[][] col;

        public Form_Game()
        {
            InitializeComponent();

        }

        private void Form_Game_Load(object sender, EventArgs e)
        {

            //DataPreparation();
        }



        private void GameField_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
