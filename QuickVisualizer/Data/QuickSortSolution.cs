using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace QuickVisualizer.Data
{
    public class QuickSortSolution
    {
        public List<QuickSortSolutionStep> Steps { get; set; } = new List<QuickSortSolutionStep>();
    }

    [DataContract]
    public class QuickSortSolutionStep
    {
        public QuickSortSolutionStep(int[] arr, int? pivot = null)
        {
            Arr = arr;
            Pivot = pivot;
            Json = JsonConvert.SerializeObject(this);
        }

        [DataMember(Name = "A")]
        public int[] Arr { get; }

        [DataMember(Name = "P")]
        public int? Pivot { get; }

        public string Json { get; }
    }
}
