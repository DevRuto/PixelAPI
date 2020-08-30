using System.Collections;

namespace PixelAPI.ViewModels
{
    public class ResultView
    {
        public int Offset { get; set; }
        public int Count { get; set; }
        public int Total { get; set; }
        public IEnumerable Data { get; set; }
    }
}