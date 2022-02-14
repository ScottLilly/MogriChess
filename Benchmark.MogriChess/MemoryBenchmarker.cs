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
        var session = new Game();
    }

    [Benchmark]
    public void Benchmark_Move()
    {
        Game session = new Game();

        Square originationSquare =
            session.Board.Squares["h2"];
        Square destinationSquare =
            session.Board.Squares["h3"];

        session.SelectSquare(originationSquare);
        session.SelectSquare(destinationSquare);
    }

    [Benchmark]
    public void Benchmark_BotResponseMove()
    {
        Game session = new Game();

        session.StartGame(Enums.PlayerType.Human, Enums.PlayerType.Bot);

        Square originationSquare =
            session.Board.Squares["h2"];
        Square destinationSquare =
            session.Board.Squares["h3"];

        session.SelectSquare(originationSquare);
        session.SelectSquare(destinationSquare);
    }
}