namespace AdventOfCode.Days;

public class Day8Solver
{
    public int Solve(string input)
    {
        var matrix = InputToMatrix(input);
        var visibleTrees = 0;
        for (var row = 0; row < matrix.Length; row++)
        {
            var maxInColumn = 0;
            for (var column = 0; column < matrix[row].Length; column++)
            {
                var currentTree = matrix[row][column];

                if (currentTree > maxInColumn || IsTreeVisibleFromRightTopOrBottom(matrix, currentTree, row, column)) visibleTrees++;

                maxInColumn = Math.Max(maxInColumn, matrix[row][column]);
            }
        }

        Console.WriteLine($"{visibleTrees} trees are visible");
        return visibleTrees;
    }


    public int SolvePart2(string input)
    {
        var matrix = InputToMatrix(input);
        var maxScore = 0;
        for (var row = 0; row < matrix.Length; row++)
        for (var column = 0; column < matrix[row].Length; column++)
        {
            var currentTree = matrix[row][column];
            var newScore = GetScenicScore(matrix, currentTree, row, column);
            maxScore = Math.Max(newScore, maxScore);
        }

        Console.WriteLine($"{maxScore} is the max Scenic score");
        return maxScore;
    }

    private int GetScenicScore(int[][] matrix, int tree, int row, int column)
    {
        if (row == 0 || column == 0 || row == matrix.Length - 1 || column == matrix[row].Length - 1) return 0;

        var rowAsList = matrix[row].ToList();
        var matrixAsList = matrix.ToList();

        var treesToTheRight = rowAsList.GetRange(column + 1, matrix[row].Length - column - 1);
        var treesToTheLeft = rowAsList.GetRange(0, column).ToArray().Reverse();
        var treesToTheTop = matrixAsList.GetRange(0, row).Select(r => r[column]).Reverse();
        var treesToTheBottom = matrixAsList.GetRange(row + 1, matrix.Length - row - 1).Select(r => r[column]);

        return GetVisibleTrees(treesToTheRight, tree) * GetVisibleTrees(treesToTheLeft, tree) * GetVisibleTrees(treesToTheTop, tree) * GetVisibleTrees(treesToTheBottom, tree);
    }

    private int GetVisibleTrees(IEnumerable<int> array, int element)
    {
        var count = 0;
        foreach (var item in array)
        {
            count++;
            if (item >= element) break;
        }

        return count;
    }


    private bool IsTreeVisibleFromRightTopOrBottom(int[][] matrix, int tree, int row, int column)
    {
        if (row == 0 || column == 0) return true;

        return tree > matrix[row].ToList().GetRange(column + 1, matrix[row].Length - column - 1).DefaultIfEmpty(-1).Max() ||
               tree > matrix.ToList().GetRange(row + 1, matrix.Length - row - 1).Select(r => r[column]).DefaultIfEmpty(-1).Max() ||
               tree > matrix.ToList().GetRange(0, row).Select(r => r[column]).DefaultIfEmpty(-1).Max();
    }

    private int[][] InputToMatrix(string input)
    {
        var lines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        var matrix = new int[lines.Length][];
        for (var i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            var numbers = line.ToCharArray();
            matrix[i] = new int[numbers.Length];
            for (var j = 0; j < numbers.Length; j++) matrix[i][j] = (int)char.GetNumericValue(numbers[j]);
        }

        return matrix;
    }
}