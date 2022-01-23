﻿using System.Linq;
using BenchmarkDotNet.Attributes;
using MogriChess.Models;
using MogriChess.ViewModels;

namespace Benchmark.MogriChess
{
    [MemoryDiagnoser]
    public class MemoryBenchmarker
    {
        private readonly PlaySession _session = new PlaySession();

        [Benchmark]
        public void Benchmark_Instantiating()
        {
            var session = new PlaySession();
        }

        [Benchmark]
        public void Benchmark_Move()
        {
            Square originationSquare =
                _session.CurrentGame.Board.Squares.First(s => s.SquareShorthand.Equals("h2"));
            Square destinationSquare =
                _session.CurrentGame.Board.Squares.First(s => s.SquareShorthand.Equals("h3"));

            _session.CurrentGame.SelectSquare(originationSquare);
            _session.CurrentGame.SelectSquare(destinationSquare);
        }
    }
}