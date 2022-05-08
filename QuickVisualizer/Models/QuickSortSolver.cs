using QuickVisualizer.Data;

namespace QuickVisualizer.Models
{
    public class QuickSortSolver
    {
        public static QuickSortSolution QuickSort(int[] arr, bool showSwitches)
        {
            var solution = new QuickSortSolution();

            QuickSortSolver.QuickSort(arr, showSwitches, 0, arr.Length - 1, solution);

            solution.Steps.Add(new QuickSortSolutionStep(arr.ToArray()));

            return solution;
        }

        public static void QuickSort(int[] arr, bool showSwitches, int left, int right, QuickSortSolution solution)
        {
            if (left < right)
            {
                int newPivotIndex = Partition(arr, showSwitches, left, right, solution);

                if (newPivotIndex > 1)
                {
                    QuickSort(arr, showSwitches, left, newPivotIndex - 1, solution);
                }
                if (newPivotIndex + 1 < right)
                {
                    QuickSort(arr, showSwitches, newPivotIndex + 1, right, solution);
                }
            }
        }

        private static int Partition(int[] arr, bool showSwitches, int left, int right, QuickSortSolution solution)
        {
            int pivotIndex = right;
            int pivot = arr[pivotIndex];

            var stepStart = new QuickSortSolutionStep(arr.ToArray(), pivotIndex);
            solution.Steps.Add(stepStart);

            while (true)
            {
                bool leftFound = false;
                for (int i = left; i < right; i++)
                {
                    left = i;
                    if (i != pivotIndex && arr[i] > pivot)
                    {
                        leftFound = true;
                        break;
                    }
                }

                bool rightFound = false;
                for (int i = right; i > left; i--)
                {
                    right = i;
                    if (i != pivotIndex && arr[i] < pivot)
                    {
                        rightFound = true;
                        break;
                    }
                }

                if (!rightFound || !leftFound)
                {
                    break;
                }

                int temp = arr[left];
                arr[left] = arr[right];
                arr[right] = temp;

                if (showSwitches)
                {
                    var step = new QuickSortSolutionStep(arr.ToArray(), pivotIndex);
                    step.Switch = new SwitchData(left, right);
                    solution.Steps.Add(step);
                }
            }

            int target = arr[left] > pivot ? left : right;
            if(target == pivotIndex)
            {
                return pivotIndex;
            }

            arr[pivotIndex] = arr[target];
            arr[target] = pivot;

            if (showSwitches)
            {
                var pivotStep = new QuickSortSolutionStep(arr.ToArray(), target);
                pivotStep.Switch = new SwitchData(target, pivotIndex);
                solution.Steps.Add(pivotStep);
            }

            pivotIndex = target;

            return pivotIndex;
        }
    }
}
