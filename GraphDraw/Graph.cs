using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace GraphDraw
{
    class Graph
    {
        private int K;
        private readonly List<Vertex> vertexList;

        private readonly int[,] matrix;
        private int N = 0;
        private const int size = 1000;
        private readonly PictureBox pb;
        private const int inf = 99999;
        private const int deleted = -1;
        private int[,] solution;
        private Bitmap bitmap;
        private Graphics gr;
        private readonly Font font;

        public Graph(PictureBox pb)
        {
            this.pb = pb;
            font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);

            vertexList = new List<Vertex>();
            matrix = new int[size, size];
            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    matrix[i, j] = 0;
                }
            }
        }

        public void setK(int K)
        {
            if (this.K == K) return;
            this.K = K;
            recalcMatrix();
        }

        public void addVertex(int x, int y, int diam)
        {
            var t = new Vertex(x, y, N, diam);
            vertexList.Add(t);
            recalcMatrix();
            N++;
        }

        public void clear()
        {
            N = 0;
            vertexList.Clear();
            draw();
        }

        public void draw()
        {
            bitmap = new Bitmap(pb.Width, pb.Height);
            gr = Graphics.FromImage(bitmap);
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            if (N > 1)
            {
                branchesAndBorders();
                for (int count = 0; count < N; count++)
                    drawEdge(findVertex(solution[0, count]), findVertex(solution[1, count]));
            }

            foreach (var v in vertexList)
            {
                drawVertex(v);
            }
            pb.Image = bitmap;
        }

        private Vertex findVertex(int index)
        {
            foreach (var u in vertexList)
            {
                if (u.index == index) return u;
            }
            return null;
        }

        private void drawVertex(Vertex vertex)
        {
            gr.FillEllipse(Brushes.SteelBlue, vertex.x, vertex.y, Vertex.VERTEX_D, Vertex.VERTEX_D);
            gr.DrawString("" + vertex.diam, font, Brushes.Yellow,
                new PointF(vertex.x + Vertex.VERTEX_D/4, vertex.y + Vertex.VERTEX_D/4));
        }

        private void drawEdge(Vertex vertex1, Vertex vertex2)
        {
            var p1 = new PointF(vertex1.x + Vertex.VERTEX_D/2, vertex1.y + Vertex.VERTEX_D/2);
            var p2 = new PointF(vertex2.x + Vertex.VERTEX_D/2, vertex2.y + Vertex.VERTEX_D/2);
            gr.DrawLine(new Pen(Brushes.MediumSlateBlue, 3), p1, p2);
        }

        private int distanceBetween(Vertex vertex1, Vertex vertex2)
        {
            return (int) Math.Sqrt(((vertex1.x - vertex2.x)*(vertex1.x - vertex2.x) + (vertex1.y - vertex2.y)*(vertex1.y - vertex2.y)));
        }

        private void recalcMatrix()
        {
            foreach (var u in vertexList)
                foreach (var v in vertexList)
                {
                    if (u.index == v.index)
                    {
                        matrix[u.index, v.index] = inf;
                        continue;
                    }
                    if (u.diam != v.diam)
                        matrix[u.index, v.index] = matrix[v.index, u.index] = distanceBetween(u, v) + K;
                    else
                        matrix[u.index, v.index] = matrix[v.index, u.index] = distanceBetween(u, v);
                }
        }

        private void branchesAndBorders()
        {
            
            solution = new int[2,N];
            int [,] cost = new int[N,N];
            int count;
            for (count = 0; count < N - 2; count++)
            {
                for (int i = 0; i < N; i++)
                {
                    int min = inf;
                    for(int j = 0;j < N; j++)
                        if (matrix[i, j] != inf && matrix[i, j] != deleted && matrix[i, j] < min)
                            min = matrix[i, j];
                    for(int j = 0;j < N;j++)
                        if (matrix[i, j] != inf && matrix[i, j] != deleted)
                            matrix[i, j] -= min;
                }
                for (int i = 0; i < N; i++)
                {
                    int min = inf;
                    for (int j = 0; j < N; j++)
                        if (matrix[j, i] != inf && matrix[j, i] != deleted && matrix[j, i] < min)
                            min = matrix[j, i];
                    for (int j = 0; j < N; j++)
                        if (matrix[j, i] != inf && matrix[j, i] != deleted)
                            matrix[j, i] -= min;
                }
                for(int i = 0;i < N;i++)
                    for (int j = 0; j < N; j++)
                        cost[i, j] = 0;
                for(int i = 0;i < N;i++)
                    for (int j = 0; j < N; j++)
                        if (matrix[i, j] == 0)
                        {
                            int min = inf;
                            for(int a = 0;a < N;a++)
                                if (matrix[i, a] != inf && matrix[i, a] != deleted && a != j && matrix[i, a] < min)
                                    min = matrix[i, a];
                            cost[i, j] += min;
                            
                            min = inf;
                            for (int a = 0; a < N; a++)
                                if (matrix[a, j] != inf && matrix[a, j] != deleted && a != i && matrix[a, j] < min)
                                    min = matrix[a, j];
                            cost[i, j] += min;
                        }
                int max = 0;
                int tI = 0, tJ = 0;
                for(int i = 0;i < N;i++)
                    for (int j = 0; j < N; j++)
                        if (cost[i, j] > max)
                        {
                            max = cost[i, j];
                            tI = i;
                            tJ = j;
                        }
                solution[0, count] = tI;
                solution[1, count] = tJ;

                for (int a = 0; a < N; a++)
                    matrix[tI, a] = matrix[a, tJ] = deleted;

                if (matrix[tJ, tI] != deleted) matrix[tJ, tI] = inf;
                else
                {
                    int iCount, dCount;
                    for (int i = 0; i < N; i++)
                    {
                        iCount = dCount = 0;
                        for (int j = 0; j < N; j++)
                        {
                            if (matrix[i, j] == inf) iCount++;
                            if (matrix[i, j] == deleted) dCount++;
                        }
                        if (iCount == 0 && dCount != N) tI = i;
                    }

                    for (int i = 0; i < N; i++)
                    {
                        iCount = dCount = 0;
                        for (int j = 0; j < N; j++)
                        {
                            if (matrix[j, i] == inf) iCount++;
                            if (matrix[j, i] == deleted) dCount++;
                        }
                        if (iCount == 0 && dCount != N) tJ = i;
                    }
                    if (matrix[tI, tJ] != deleted) 
                        matrix[tI, tJ] = inf;
                }
            }
            for(int i = 0;i < N;i++)
                for (int j = 0; j < N; j++)
                    if (matrix[i, j] != inf && matrix[i, j] != deleted)
                    {
                        solution[0, count] = i;
                        solution[1, count++] = j;
                    }
            int maxEdge = 0, edgeIndex = 0;
            for (int i = 0; i < count; i++)
            {
                Vertex v1 = findVertex(solution[0, i]);
                Vertex v2 = findVertex(solution[1, i]);
                if (v1.diam != v2.diam)
                {
                    solution[0, i] = solution[1, i] = 0;
                    return;
                }
                int d = distanceBetween(v1, v2);
                if (d > maxEdge)
                {
                    maxEdge = d;
                    edgeIndex = i;
                }
            }
            solution[0, edgeIndex] = solution[1, edgeIndex] = 0;
        }
    }
}