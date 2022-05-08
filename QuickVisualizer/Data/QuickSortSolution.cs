namespace QuickVisualizer.Data
{
    public class QuickSortSolution
    {
        public List<QuickSortSolutionStep> Steps { get; set; } = new List<QuickSortSolutionStep>();
    }

    public class QuickSortSolutionStep
    {
        public QuickSortSolutionStep(int[] arr)
        {
            Arr = arr;
        }

        public int[] Arr { get; }
    }
}
