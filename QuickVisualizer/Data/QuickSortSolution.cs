using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace QuickVisualizer.Data
{
    public class QuickSortSolution
    {
        public List<QuickSortSolutionStep> Steps { get; set; } = new List<QuickSortSolutionStep>();
    }

    public class QuickSortSolutionStep
    {
        public QuickSortSolutionStep(int[] arr, int left, int right, int? pivotIndex = null)
        {
            Arr = arr;
            Left = left;
            Right = right;
            PivotIndex = pivotIndex;
        }

        public int[] Arr { get; set; }
        public int Left { get; }
        public int Right { get; }
        public int? PivotIndex { get; set; }

        public SwitchData Switch { get; set; }
    }
}
