using System.Linq;
using BenchmarkDotNet.Attributes;
using MogriChess.Models;
using MogriChess.ViewModels;

namespace Benchmark.MogriChess;

[MemoryDiagnoser]
public class MemoryBenchmarker
{
    //private readonly PlaySession _session = new PlaySession();

    [Benchmark]
    public void Benchmark_Instantiating()
    {
        var session = new PlaySession();
    }

    [Benchmark]
    public void Benchmark_Move()
    {
        PlaySession _session = new PlaySession();

        Square originationSquare =
            _session.CurrentGame.Board.Squares["h2"];
        Square destinationSquare =
            _session.CurrentGame.Board.Squares["h3"];

        _session.CurrentGame.SelectSquare(originationSquare);
        _session.CurrentGame.SelectSquare(destinationSquare);
    }

    [Benchmark]
    public void Benchmark_BotResponseMove()
    {
        PlaySession _session = new PlaySession();

        _session.StartGame(Enums.PlayerType.Human, Enums.PlayerType.Bot);

        Square originationSquare =
            _session.CurrentGame.Board.Squares["h2"];
        Square destinationSquare =
            _session.CurrentGame.Board.Squares["h3"];

        _session.CurrentGame.SelectSquare(originationSquare);
        _session.CurrentGame.SelectSquare(destinationSquare);
    }
}