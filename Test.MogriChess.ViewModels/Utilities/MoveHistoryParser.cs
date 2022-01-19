using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Test.MogriChess.ViewModels.Utilities
{
    internal static class MoveHistoryParser
    {
        internal static List<string> GetMovesFromFile(string filename)
        {
            List<string> moveNotations = new List<string>();

            var text = File.ReadAllText(filename);

            IEnumerable<ExpandoObject> moveHistory =
                JsonConvert.DeserializeObject<IEnumerable<ExpandoObject>>(text);

            if (moveHistory != null)
            {
                moveNotations.AddRange(moveHistory
                    .Select(expandoObject =>
                        expandoObject.FirstOrDefault(x => x.Key == "MoveShorthand")
                            .Value?.ToString() ?? ""));
            }

            return moveNotations;
        }
    }
}