using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Nonogram
{
    public partial class TitleForm : Form
    {
        static string folderpath = @"G:\@Code\20240516nonogram\Nonogram\NonogramDataBase\db\";
        public TitleForm()
        {
            InitializeComponent();

            //CenteredPosition(StartButton,0);
            //CenteredPosition(ExitButton,50);
        }

        public void CenteredPosition<T>(T obj, int yAligin) where T : Control
        {
            int x = (this.ClientSize.Width - obj.Width) / 2;
            int y = (this.ClientSize.Height - obj.Height) / 2 + yAligin;
            obj.Location = new Point(x, y);
        }


        private void StartButton_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            // 设置 DataGridView 的属性
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DefaultCellStyle.Font = new System.Drawing.Font("Arial Narrow", 18);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Arial Narrow", 18, FontStyle.Bold);


            // 添加 DataGridView 列
            DataGridViewTextBoxColumn colLevelName = new DataGridViewTextBoxColumn();
            colLevelName.DataPropertyName = "LevelName";
            colLevelName.HeaderText = "关卡名称";
            colLevelName.Name = "colLevelName";
            colLevelName.Selected = false;
            colLevelName.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; 
            dataGridView1.Columns.Add(colLevelName);

            DataGridViewTextBoxColumn colLevelHeight = new DataGridViewTextBoxColumn();
            colLevelHeight.DataPropertyName = "LevelInfo";
            colLevelHeight.HeaderText = "Height";
            colLevelHeight.Name = "colLevelHeight";
            colLevelHeight.Selected = false;
            colLevelHeight.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; 
            dataGridView1.Columns.Add(colLevelHeight);
            // "width", "height",
            DataGridViewTextBoxColumn colLevelWidth = new DataGridViewTextBoxColumn();
            colLevelWidth.DataPropertyName = "LevelInfo";
            colLevelWidth.HeaderText = "Width";
            colLevelWidth.Name = "colLevelWidth";
            colLevelWidth.Selected = false;
            colLevelWidth.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns.Add(colLevelWidth);

            DataGridViewButtonColumn colEnterButton = new DataGridViewButtonColumn();
            colEnterButton.HeaderText = "进入关卡";

            colEnterButton.Text = "进入";
            colEnterButton.Name = "colEnterButton";
            colEnterButton.Selected = false;
            colEnterButton.Width = 120;
            colEnterButton.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(colEnterButton);

            // 加载关卡信息到 DataGridView
            LoadLevelsFromFolder(folderpath);
        }

        private void LoadLevelsFromFolder(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                // 获取文件夹中的所有关卡文件
                string[] levelFiles = Directory.GetFiles(folderPath, "*.non");

                // 清空 DataGridView
                dataGridView1.Rows.Clear();

                foreach (string filePath in levelFiles)
                {
                    // 从文件名获取关卡名称
                    string levelName = Path.GetFileNameWithoutExtension(filePath);
                    // 读取关卡信息
                    string levelInfo = File.ReadAllText(filePath);

                    string[] rowColInfo = RowColInfoCutter(levelInfo);
                    // 添加行到 DataGridView
                    int rowIndex = dataGridView1.Rows.Add();
                    dataGridView1.Rows[rowIndex].Cells["colLevelName"].Value = levelName;
                    dataGridView1.Rows[rowIndex].Cells["colLevelHeight"].Value = rowColInfo[1];
                    dataGridView1.Rows[rowIndex].Cells["colLevelWidth"].Value = rowColInfo[0];


                    dataGridView1.Rows[rowIndex].Height = 33;
                }
            }
            else
            {
                MessageBox.Show("关卡文件夹不存在！");
            }
        }


        private string[] RowColInfoCutter(string levelInfo)
        {
            // 定义开始和结束标识字符串
            //string startMarker1 = "height";
            //string startMarker2 = "width";

            //string endMarker1 = "rows";
            //string endMarker2 = "columns";

            string[] result = new string[2];
            //分除了row和col的dict
            string[] keywords = { "width", "height" };
            string[] levelInfoArray = levelInfo.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string pair in levelInfoArray)
            {
                //Console.WriteLine($"levelInfoArray    {pair}");
                string[] info = pair.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (keywords.Contains(info[0]))
                {
                    if (info[0] == "width")
                    {
                        result[0] = pair.Substring(info[0].Length, pair.Length - info[0].Length);
                    }
                    else
                    {
                        result[1] = pair.Substring(info[0].Length, pair.Length - info[0].Length);

                    }
                    //infoDict[info[0]] = pair.Substring(info[0].Length, pair.Length - info[0].Length);
                    //Console.WriteLine(info[0] + " : " + infoDict[info[0]]);
                }
            }

            return result;

            //// 查找开始标识的位置
            //int startIndex = Math.Min(
            //    levelInfo.IndexOf(startMarker1),
            //    levelInfo.IndexOf(startMarker2));
            //if (startIndex == -1)
            //{
            //    // 如果找不到开始标识，则处理错误或返回
            //    Console.WriteLine("未找到开始标识");
            //    return new string[2];
            //}

            //// 查找结束标识的位置，从开始标识后开始查找
            //int endIndex = Math.Min(
            //    levelInfo.IndexOf(endMarker1, startIndex + startMarker1.Length),
            //    levelInfo.IndexOf(endMarker2, startIndex + startMarker1.Length));
            //if (endIndex == -1)
            //{
            //    // 如果找不到结束标识，则处理错误或返回
            //    Console.WriteLine("未找到结束标识");
            //    return new string[2];
            //}

            //// 计算需要截取的子字符串的起始位置和长度
            //int substringStart = startIndex;// + startMarker.Length;
            //int substringLength = endIndex - substringStart;

            //// 截取子字符串
            //string extractedPart = levelInfo.Substring(substringStart, substringLength).Trim();
            ////Console.WriteLine("截取的部分：" + extractedPart);
            //return extractedPart;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 检查用户点击的是进入关卡按钮列
            if (e.ColumnIndex == dataGridView1.Columns["colEnterButton"].Index && e.RowIndex >= 0)
            {

                // 获取关卡名称

                LevelData levelData = new LevelData();
                levelData.name = dataGridView1.Rows[e.RowIndex].Cells["colLevelName"].Value.ToString();
                levelData.info = File.ReadAllText(folderpath+ levelData.name + ".non");
                levelData.DataPreparation();
                // 在这里处理进入关卡的逻辑，例如打开新窗体或加载关卡数据等
                //MessageBox.Show($"进入关卡：{levelName}");
                //Form_Game form_Game = new Form_Game();
                //form_Game.levelInfo = levelInfo;

                //form_Game.levelName = levelName;
                //form_Game.ShowDialog();

                GameForm form1 = new GameForm(levelData);
                form1.ShowDialog();
                //this.Close();

            }
        }



    }
}
