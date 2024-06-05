﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
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

            DataPreparation();
        }

        private void DataPreparation()
        {
            Console.WriteLine("Huang: " + levelName);
            Console.WriteLine("Huang: " + levelInfo);
            char[] separators = new char[] { '\n' };
            Console.WriteLine("Huang: 1");

            //分除了row和col的dict
            string[] keywords = { "catalogue", "title", "by", "copyright", "license", "width", "height",  "goal" };
            string[] levelInfoArray = levelInfo.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            foreach (string pair in levelInfoArray)
            {
                //Console.WriteLine($"levelInfoArray    {pair}");
                string[] info = pair.Split(new char[] { ' '}, StringSplitOptions.RemoveEmptyEntries);
                if (keywords.Contains(info[0])){
                    infoDict[info[0]] = pair.Substring(info[0].Length, pair.Length - info[0].Length);
                    Console.WriteLine(info[0] + " : " + infoDict[info[0]]);
                }
            }

            //分row和col
            //string[] keywords2 = { "columns", "rows" };
            int startIndex = Math.Min(
                levelInfo.IndexOf("columns"),
                levelInfo.IndexOf("rows"));
            int endIndex = levelInfo.IndexOf("goal");
            string[] rowcol = levelInfo.Substring(startIndex, endIndex - startIndex)
                .Split(new string[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
            row = new int[Int32.Parse(infoDict["height"])][];
            col = new int[Int32.Parse(infoDict["width"])][];
            foreach(string unit in rowcol)
            {
                string[] splittedUnit = unit.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                if (splittedUnit[0] == "rows")
                {
                    for(int i = 1; i < splittedUnit.Length; i++)
                    {
                        string[] rowsunit = splittedUnit[i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        row[i - 1] = rowsunit.Select(s => int.TryParse(s, out int num) ? num : 0).ToArray();
                        //Console.WriteLine($"row[{i - 1}] = ");
                        //foreach (int x in row[i - 1])
                        //{
                        //    Console.Write(x.ToString() + ", ");
                        //}
                        //Console.WriteLine();
                    }

                }
                //if (splittedUnit[0] == "colums")
                else
                {
                    for (int i = 1; i < splittedUnit.Length; i++)
                    {
                        string[] colsunit = splittedUnit[i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        col[i - 1] = colsunit.Select(s => int.TryParse(s, out int num) ? num : 0).ToArray();
                        //Console.WriteLine($"row[{i - 1}] = ");
                        //foreach (int x in col[i - 1])
                        //{
                        //    Console.Write(x.ToString() + ", ");
                        //}
                        //Console.WriteLine();
                    }

                }
            }




            //Console.WriteLine("Huang: 4");

            //// 遍历匹配结果，并将结果添加到字典中

            //Console.WriteLine("Huang: 5 " + infoDict.Count.ToString());

            //foreach (var pair in infoDict)
            //{
            //    Console.WriteLine($"Dictionary    {pair.Key}: {pair.Value}");
            //}
            //Console.WriteLine("Huang: 6");

        }

        private void GameField_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
