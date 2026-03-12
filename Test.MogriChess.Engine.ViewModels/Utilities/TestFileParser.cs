using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using MogriChess.Engine.DTOs;

namespace Test.MogriChess.ViewModels.Utilities;

internal static class TestFileParser
{
    internal static List<MoveHistoryDTO> GetMoveHistoryFromFile(string filename)
    {
        var jsonText = File.ReadAllText(filename);

        return JsonSerializer.Deserialize<List<MoveHistoryDTO>>(jsonText)!;
    }
}