using System.Collections.Generic;
using System.IO;
using MogriChess.ViewModels.DTOs;
using Newtonsoft.Json;

namespace Test.MogriChess.ViewModels.Utilities;

internal static class TestFileParser
{
    internal static List<MoveHistoryDTO> GetMoveHistoryFromFile(string filename)
    {
        var jsonText = File.ReadAllText(filename);

        return JsonConvert.DeserializeObject<List<MoveHistoryDTO>>(jsonText);
    }
}