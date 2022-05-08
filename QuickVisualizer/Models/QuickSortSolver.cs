using QuickVisualizer.Data;

namespace QuickVisualizer.Models
{
    public class QuickSortSolver
    {
        public static QuickSortSolution QuickSort(int[] arr)
        {
            var solution = new QuickSortSolution();

            QuickSortSolver.QuickSort(arr, 0, arr.Length - 1, solution);

            solution.Steps.Add(new QuickSortSolutionStep(arr));

            return solution;
        }

        public static void QuickSort(int[] arr, int left, int right, QuickSortSolution quickSortSolution)
        {
            quickSortSolution.Steps.Add(new QuickSortSolutionStep(arr));

            if (left < right)
            {
                int pivot = Partition(arr, left, right);

                quickSortSolution.Steps.Add(new QuickSortSolutionStep(arr));

                if (pivot > 1)
                {
                    QuickSort(arr, left, pivot - 1, quickSortSolution);
                }
                if (pivot + 1 < right)
                {
                    QuickSort(arr, pivot + 1, right, quickSortSolution);
                }
            }
        }

        private static int Partition(int[] arr, int left, int right)
        {
            int pivot = arr[left];
            while (true)
            {

                while (arr[left] < pivot)
                {
                    left++;
                }

                while (arr[right] > pivot)
                {
                    right--;
                }

                if (left < right)
                {
                    if (arr[left] == arr[right]) return right;

                    int temp = arr[left];
                    arr[left] = arr[right];
                    arr[right] = temp;
                }
                else
                {
                    return right;
                }
            }
        }
    }
}
