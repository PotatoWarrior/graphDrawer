using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace GraphDraw
{
    class Graph
    {
        private int K;
        private List<Vertex> vertexList;

        private int[,] matrix;
        private int N = 0;
        private int size = 1000;
        private PictureBox pb;

        private Bitmap bitmap;
        private Graphics gr;
        private Font font;

        public Graph(PictureBox pb)
        {
            this.pb = pb;
            font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);

            vertexList = new List<Vertex>();
            matrix = new int[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = 0;
                }
            }
        }

        public void setK(int K)
        {
            if (this.K != K)
            {
                this.K = K;
                recalcMatrix();
            }
        }

        public void addVertex(int x, int y, int diam)
        {
            Vertex t = new Vertex(x, y, N, diam);
            vertexList.Add(t);
            foreach (Vertex u in vertexList)
            {
                if (u.index == N) continue;
                if (u.diam != t.diam)
                    matrix[u.index, t.index] = matrix[t.index, u.index] = distanceBetween(u, t) + K;
                else
                    matrix[u.index, t.index] = matrix[t.index, u.index] = distanceBetween(u, t);
            }
            matrix[t.index, t.index] = 0;
            N++;
            System.GC.Collect();
        }

        public void clear()
        {
            N = 0;
            vertexList.Clear();
            System.GC.Collect();
            draw();
        }

        public void draw()
        {
            bitmap = new Bitmap(pb.Width, pb.Height);
            gr = Graphics.FromImage(bitmap);
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            foreach(Vertex u in vertexList)
                foreach (Vertex v in vertexList)
                {
                    if (v.index <= u.index) continue;
                    if (matrix[u.index, v.index] != 0)
                        drawEdge(u, v);
                }
            foreach(Vertex v in vertexList)
            {
                drawVertex(v);
            }
            pb.Image = bitmap;
        }

        private void drawVertex(Vertex vertex)
        {
            gr.FillEllipse(Brushes.SteelBlue, vertex.x, vertex.y, Vertex.VERTEX_D, Vertex.VERTEX_D);
            gr.DrawString("" + vertex.diam, font, Brushes.Yellow, new PointF(vertex.x + Vertex.VERTEX_D / 4, vertex.y + Vertex.VERTEX_D / 4));
        }

        private void drawEdge(Vertex vertex1, Vertex vertex2)
        {
            PointF p1 = new PointF(vertex1.x + Vertex.VERTEX_D / 2, vertex1.y + Vertex.VERTEX_D / 2);
            PointF p2 = new PointF(vertex2.x + Vertex.VERTEX_D / 2, vertex2.y + Vertex.VERTEX_D / 2);
            gr.DrawLine(new Pen(Brushes.MediumSlateBlue, 3), p1, p2);
        }

        private int distanceBetween(Vertex vertex1, Vertex vertex2)
        {
            return (int)Math.Sqrt((double)((vertex1.x - vertex2.x) * (vertex1.x - vertex2.x) + (vertex1.y - vertex2.y) * (vertex1.y - vertex2.y)));
        }

        private void recalcMatrix()
        {
            foreach(Vertex u in vertexList)
                foreach (Vertex v in vertexList)
                {
                    if (u.index == v.index) continue;
                    if (u.diam != v.diam)
                        matrix[u.index, v.index] = matrix[v.index, u.index] = distanceBetween(u, v) + K;
                    else
                        matrix[u.index, v.index] = matrix[v.index, u.index] = distanceBetween(u, v);
                }
        }
    }
}
