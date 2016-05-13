﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace GraphDraw
{
    class Graph
    {
        private List<Vertex> vertexList;

        private int[,] matrix;
        private int N;
        private PictureBox pb;

        private Bitmap bitmap;
        private Graphics gr;
        private Font font;
        private CustomLineCap cap;
        private bool isDir = false;
        public Graph(int[,] matrix, int N, PictureBox pb, bool isDir)
        {
            this.N = N;
            this.matrix = matrix;
            this.pb = pb;
            this.isDir = isDir;
            font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);

            bitmap = new Bitmap(pb.Width, pb.Height);
            gr = Graphics.FromImage(bitmap);
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            GraphicsPath gp = new GraphicsPath();
            int startArrow = -10;
            int endArrow = -5;
            int hArrow = 3;
            gp.AddLine(new Point(-hArrow, startArrow), new Point(hArrow, startArrow));
            gp.AddLine(new Point(hArrow, startArrow), new Point(0, endArrow));
            gp.AddLine(new Point(0, endArrow), new Point(-hArrow, startArrow));
            
            cap = new CustomLineCap(null, gp);

            vertexList = new List<Vertex>(N);

            double x = pb.Width / 2;
            double y = pb.Height / 2;
            double angle, dx, dy;
            for (int count = 0; count < N; count++)
            {
                angle = ((2.0 * Math.PI * count) / N) + - Math.PI / 2;
                dx = ((pb.Width / 2) - 50) * Math.Cos(angle);
                dy = ((pb.Height / 2) - 50) * Math.Sin(angle);
                vertexList.Add(new Vertex((float)(x + dx), (float)(y + dy), count));
            }
        }

        public void draw()
        {
            foreach(Vertex u in vertexList)
                foreach (Vertex v in vertexList)
                {
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
            gr.DrawString("" + vertex.index, font, Brushes.Yellow, new PointF(vertex.x + Vertex.VERTEX_D / 4, vertex.y + Vertex.VERTEX_D / 4));
        }

        private void drawEdge(Vertex vertex1, Vertex vertex2)
        {
            PointF p1 = new PointF(vertex1.x + Vertex.VERTEX_D / 2, vertex1.y + Vertex.VERTEX_D / 2);
            PointF p2 = new PointF(vertex2.x + Vertex.VERTEX_D / 2, vertex2.y + Vertex.VERTEX_D / 2);
            
            Pen p = new Pen(Brushes.MediumSlateBlue, 3);
            if(isDir) p.CustomEndCap = cap;
            gr.DrawLine(p, p1, p2);
        }
    }
}
