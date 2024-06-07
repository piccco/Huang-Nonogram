using System;
using System.Drawing;
using System.Windows.Forms;

namespace Nonogram
{
    public partial class GameForm : Form
    {
        private int cellSize = 30;
        private LevelData levelData;
        public GameForm(LevelData lData)
        {
            levelData = lData;
            InitializeComponent();
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            // Initialize DataGridView
            DataGridView dataGridView1 = new DataGridView
            {
                ColumnCount = 10,
                RowCount = 10,
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToOrderColumns = false,
                AllowUserToResizeColumns = false,
                AllowUserToResizeRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.CellSelect
            };
            dataGridView1.Font = new Font("Arial", 18);
            dataGridView1.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.Width = cellSize;
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
                column.Resizable = DataGridViewTriState.False;
            }

            dataGridView1.MouseUp += DataGridView1_MouseUp;

            this.Controls.Add(dataGridView1);

            // Set default state to white
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Height = cellSize;
                row.Resizable = DataGridViewTriState.False;
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.Style.BackColor = Color.White;
                    cell.Tag = CellState.White;
                    
                }
            }
        }

        private void DataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            var grid = sender as DataGridView;
            if (grid != null)
            {
                foreach (DataGridViewCell cell in grid.SelectedCells)
                {
                    ToggleCellState(cell);
                }
            }
            grid.ClearSelection();
        }

        private void ToggleCellState(DataGridViewCell cell)
        {
            if (cell.Tag is CellState currentState)
            {
                var nextState = GetNextState(currentState);
                ApplyCellState(cell, nextState);
            }
        }

        private CellState GetNextState(CellState currentState)
        {
            switch (currentState)
            {
                case CellState.White:
                    return CellState.Black;
                case CellState.Black:
                    return CellState.Cross;
                case CellState.Cross:
                    return CellState.White;
                default:
                    return CellState.White;
            }
        }

        private void ApplyCellState(DataGridViewCell cell, CellState state)
        {
            switch (state)
            {
                case CellState.White:
                    cell.Style.BackColor = Color.White;
                    cell.Value = string.Empty;
                    break;
                case CellState.Black:
                    cell.Style.BackColor = Color.Black;
                    cell.Value = string.Empty;
                    break;
                case CellState.Cross:
                    cell.Style.BackColor = Color.White;
                    cell.Value = "✕";//✗, ✘, x, ×, X, ✕, ☓, ✖
                    break;
            }
            cell.Tag = state;
        }

        private enum CellState
        {
            White,
            Black,
            Cross
        }
    }
}
