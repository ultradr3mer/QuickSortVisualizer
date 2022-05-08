namespace QuickVisualizer.Data
{
    public class SwitchData
    {
        public int Left { get; set; }
        public int Right { get; set; }

        public SwitchData(int from, int to)
        {
            Left = from; Right = to;
        }

        public SwitchData()
        { }
    }
}
