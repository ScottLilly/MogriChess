using BenchmarkDotNet.Attributes;
using MogriChess.Models;
using MogriChess.ViewModels;

namespace Benchmark.MogriChess;

[MemoryDiagnoser]
public class MemoryBenchmarker
{
    [Benchmark]
    public void Benchmark_Instantiating()
    {
        var session = new PlaySession();
    }

    [Benchmark]
    public void Benchmark_Move()
    {
        PlaySession session = new PlaySession();

        Square originationSquare =
            session.CurrentGame.Board.Squares["h2"];
        Square destinationSquare =
            session.CurrentGame.Board.Squares["h3"];

        session.SelectSquare(originationSquare);
        session.SelectSquare(destinationSquare);
    }

    [Benchmark]
    public void Benchmark_BotResponseMove()
    {
        PlaySession session = new PlaySession();

        session.StartGame(Enums.PlayerType.Human, Enums.PlayerType.Bot);

        Square originationSquare =
            session.CurrentGame.Board.Squares["h2"];
        Square destinationSquare =
            session.CurrentGame.Board.Squares["h3"];

        session.SelectSquare(originationSquare);
        session.SelectSquare(destinationSquare);
    }
}