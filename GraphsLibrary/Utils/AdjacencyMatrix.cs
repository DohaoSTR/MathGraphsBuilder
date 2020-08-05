﻿using GraphsLibrary.GraphElements;
using System;
using System.Collections.Generic;

namespace GraphsLibrary.Utils
{
    public class AdjacencyMatrix
    {
        private readonly int[,] _matrix;

        private readonly List<Vertex> _vertices;
        private readonly List<Edge> _edges;

        public AdjacencyMatrix(ICollection<Vertex> vertices, ICollection<Edge> edges)
        {
            _vertices = (List<Vertex>)vertices;
            _edges = (List<Edge>)edges;
            _matrix = new int[vertices.Count, vertices.Count];

            SetInitialValuesInMatrix();
        }

        private int Length => _vertices.Count;

        public int this[int indexVertex, int indexEdge]
        {
            set
            {
                _matrix[indexVertex, indexEdge] = value;
            }
            get
            {
                return _matrix[indexVertex, indexEdge];
            }
        }

        /// <summary>
        /// Ищет кратчайший путь с помощью волнового алгоритма.
        /// </summary>
        /// <returns>
        /// Минимальное количество шагов между двумя вершинами.
        /// </returns>
        public int SearchShortestWay(int start, int target)
        {
            start -= 1;
            target -= 1;
            int[] p = new int[Length];

            for (int i = 0; i < Length; i++)
            {
                p[i] = -1;
            }

            p[start] = 0;
            for (int i = 0; i < Length; i++)
            {
                for (int k = 0; k < Length; k++)
                {
                    if (p[k] == i)
                    {
                        for (int j = 0; j < Length; j++)
                        {
                            if (p[j] == -1 && this[j, k] == 1)
                            {
                                p[j] = i + 1;
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < Length; i++)
            {
                if (i == target)
                {
                    return p[i];
                }
            }
            throw new ArgumentNullException("Кратчайший путь не найден!");
        }

        private void SetInitialValuesInMatrix()
        {
            for (int indexVertex = 0; indexVertex < _vertices.Count; indexVertex++)
            {
                for (int indexEdge = 0; indexEdge < _vertices.Count; indexEdge++)
                {
                    _matrix[indexVertex, indexEdge] = 0;
                }
            }

            foreach (var element in _edges)
            {
                _matrix[_vertices.IndexOf(element.Start), _vertices.IndexOf(element.End)] = 1;
                _matrix[_vertices.IndexOf(element.End), _vertices.IndexOf(element.Start)] = 1;
            }
        }
    }
}
