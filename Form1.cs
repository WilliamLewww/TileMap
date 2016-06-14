using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace TileMap
{
    enum Tile : int
    {
        RegularTile = 1,
        PushTile = 2,
        EndPushTile = 3,
        SpawnTile = 4,
        HarmfulTile = 5,
        TrampolineTile = 6,
        SetTrampolineTile = 7,
        ReverseSetTrampolineTile = 8,
        MovingTile = 9,
        HalfMovingTile = 10,
        ReverseHalfMovingTile = 11
    };

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GenerateGrid((int)numericUpDown1.Value, (int)numericUpDown2.Value, (int)numericUpDown3.Value, (int)numericUpDown4.Value);
        }

        void GenerateGrid(int width, int height, int divisionsX, int divisionsY)
        {
            tileMap = new int[divisionsX, divisionsY];

            CreateGraphics().Clear(Color.Gray);
            CreateGraphics().DrawLine(new Pen(Color.Black), 10, 50, width + 10, 50);
            CreateGraphics().DrawLine(new Pen(Color.Black), 10, 50, 10, height + 50);
            CreateGraphics().DrawLine(new Pen(Color.Black), width + 10, 50, width + 10, height + 50);
            CreateGraphics().DrawLine(new Pen(Color.Black), 10, height + 50, width + 10, height + 50);

            for (int x = 0; x < divisionsX - 1; x++)
            {
                CreateGraphics().DrawLine(new Pen(Color.Black), 10 + ((width / divisionsX) * (1 + x)), 50, 10 + ((width / divisionsX) * (1 + x)), height + 50);
            }

            for (int y = 0; y < divisionsY - 1; y++)
            {
                CreateGraphics().DrawLine(new Pen(Color.Black), 10, 50 + ((height / divisionsY) * (1 + y)), width + 10, 50 + ((height / divisionsY) * (1 + y)));
            }
        }

        void GenerateTile(int x, int y, int width, int height, Color color)
        {
            CreateGraphics().FillRectangle(new SolidBrush(color), x + 11, y + 51, width - 1, height - 1);
        }

        static int[,] tileMap;
        List<string> stringList = new List<string>();

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();

            if (openFileDialog1.FileName.Substring(openFileDialog1.FileName.LastIndexOf('.')).Equals(".txt"))
            {
                savePath = openFileDialog1.FileName;
                textBox1.Text = openFileDialog1.FileName.Substring(openFileDialog1.FileName.LastIndexOf('\\') + 1).TrimEnd(".txt".ToCharArray());

                using (StreamReader sr = File.OpenText(openFileDialog1.FileName))
                {
                    stringList.Clear();
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        stringList.Add(s);
                    }
                }
            }

            int increment = 0;
            for (int x = 0; x < stringList[0].Length; x++)
            {
                if (stringList[0][x] == ',') increment += 1;
            }

            GenerateGrid(800, 600, increment, stringList.Count);
            increment = 0;

            string intString = "";

            for (int y = 0; y < tileMap.GetLength(1); y++)
            {
                increment = 0;
                for (int x = 0; x < stringList[y].Length; x++)
                {
                    if (stringList[y][x] == ',')
                    {
                        tileMap[increment, y] = int.Parse(intString);
                        intString = "";
                        increment += 1;
                    }
                    else
                    {
                        if (stringList[y][x] != '{' && stringList[y][x] != '}')
                        {
                            intString += stringList[y][x];
                        }
                    }
                }
            }

            for (int y = 0; y < tileMap.GetLength(1); y++)
            {
                for (int x = 0; x < tileMap.GetLength(0); x++)
                {
                    if (tileMap[x, y] == 1) GenerateTile(x * 10, y * 10, 10, 10, Color.Blue);
                    if (tileMap[x, y] == 2) GenerateTile(x * 10, y * 10, 10, 10, Color.Pink);
                    if (tileMap[x, y] == 3) GenerateTile(x * 10, y * 10, 10, 10, Color.Pink);
                    if (tileMap[x, y] == 4) GenerateTile(x * 10, y * 10, 10, 10, Color.Black);
                    if (tileMap[x, y] == 5) GenerateTile(x * 10, y * 10, 10, 10, Color.Red);
                    if (tileMap[x, y] == 6) GenerateTile(x * 10, (y * 10) + 5, 10, 5, Color.HotPink);
                    if (tileMap[x, y] == 7) GenerateTile(x * 10, (y * 10) + 5, 10, 5, Color.HotPink);
                    if (tileMap[x, y] == 8) GenerateTile(x * 10, y * 10, 10, 5, Color.HotPink);
                    if (tileMap[x, y] == 9) GenerateTile(x * 10, y * 10, 10, 10, Color.Yellow);
                    if (tileMap[x, y] == 10) GenerateTile(x * 10, (y * 10) + 5, 10, 5, Color.Yellow);
                    if (tileMap[x, y] == 11) GenerateTile(x * 10, y * 10, 10, 5, Color.Yellow);
                }
            }
        }

        string savePath = "";
        private void button3_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            savePath = folderBrowserDialog1.SelectedPath;
            savePath += "\\" + textBox1.Text + ".txt";

            using (StreamWriter sw = File.CreateText(savePath))
            {
                for (int y = 0; y < tileMap.GetLength(1); y++)
                {
                    for (int x = 0; x < tileMap.GetLength(0); x++)
                    {
                        if (x == 0) sw.Write('{');
                        sw.Write(tileMap[x, y]);
                        if (x == tileMap.GetLength(0) - 1)
                        {
                            if (y == tileMap.GetLength(1) - 1) { sw.Write(" }"); }
                            else { sw.Write(" },"); }
                        }
                        else { sw.Write(','); }
                    }
                    sw.WriteLine();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (savePath.Equals(""))
            {
                folderBrowserDialog1.ShowDialog();
                savePath = folderBrowserDialog1.SelectedPath;
                savePath += "\\" + textBox1.Text + ".txt";
            }

            using (StreamWriter sw = File.CreateText(savePath))
            {
                for (int y = 0; y < tileMap.GetLength(1); y++)
                {
                    for (int x = 0; x < tileMap.GetLength(0); x++)
                    {
                        if (x == 0) sw.Write('{');
                        sw.Write(tileMap[x, y]);
                        if (x == tileMap.GetLength(0) - 1)
                        {
                            if (y == tileMap.GetLength(1) - 1) { sw.Write(" }"); }
                            else { sw.Write(" },"); }
                        }
                        else { sw.Write(','); }
                    }
                    sw.WriteLine();
                }
            }
        }
    }
}
