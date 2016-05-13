using System;
using System.Windows.Forms;

namespace GraphDraw
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            openFileDialog1.Filter = "Текстовые файлы|*.txt";
            saveFileDialog1.Filter = "Текстовые файлы|*.txt";
        }

       private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new
                   System.IO.StreamReader(openFileDialog1.FileName);
                textMatrix.Text = "";
                textMatrix.Text += sr.ReadToEnd();
                sr.Close();
                textMatrix.Refresh();
            }
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            try
            {
                int N = textMatrix.Lines[0].Trim().Length;
                int[,] matrix = new int[N,N];
            
                for (int i = 0; i < N; i++)
                    for (int j = 0; j < N; j++)
                    {
                        int a = textMatrix.Lines[i][j] - '0';
                        if (a != 1 && a != 0)
                        {
                            MessageBox.Show("Некорректный ввод");
                            return;
                        }
                        matrix[i, j] = a;
                    }
                Graph g = new Graph(matrix, N, pictureBox1, checkBox1.Checked);
                g.draw();
            }
            catch (Exception exception)
            {
                
                MessageBox.Show(exception.ToString());
                
            }
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamWriter sw = new
                   System.IO.StreamWriter(saveFileDialog1.OpenFile());
                sw.Write(textMatrix.Text);
                sw.Close();
            }
           
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            int N = Decimal.ToInt32(numericUpDown1.Value);
            int [,] mas = new int[N, N];
            if (!checkBox1.Checked)
            {
                for (int i = 0; i < N; i++)
                    for (int j = i + 1; j < N; j++)
                        if (r.Next(0, 3) == 1) mas[i, j] = mas[j, i] = 1;
                        else mas[i, j] = mas[j, i] = 0;
            }
            else
            {
                for (int i = 0; i < N; i++)
                    for (int j = 0; j < N; j++)
                        if (r.Next(0, 3) == 1) mas[i, j] = 1;
                        else mas[i, j] = 0;
            }

            textMatrix.Text = "";
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                    textMatrix.Text += mas[i, j];
                textMatrix.Text += "\r\n";
            }

        }
    }
}
