using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickVisualizer.Models;

namespace Tests
{
    [TestClass]
    public class TestQuickSortSolver
    {
        [TestMethod]
        public void UebungsblattDaten()
        {
            var arr = new int[] { 2, 5, -4, 11, 0, 18, 22, 67, 51, 6 };

            var solution = QuickSortSolver.QuickSort(arr, showSwitches: true);
        }
    }
}