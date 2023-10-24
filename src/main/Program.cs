// ігрок завжди ходить першим
// ігрок завжди ставить X
// бот завжди ставить 0
// бот вибірає випадкову порожню клітину
// індексація рядків та колонок починається з 0
public class Program
{
    private static int ROW_COUNT = 3;
    private static int COL_COUNT = 3;

    private static string CELL_STATE_EMPTY = " ";
    private static string CELL_STATE_X = "X";
    private static string CELL_STATE_O = "O";

    private static string GAME_STATE_X_WON = "X перемогли!";
    private static string GAME_STATE_O_WON = "0 перемогли!";
    private static string GAME_STATE_DRAW = "Нічия";
    private static string GAME_STATE_NOT_FINISHED = "Гра не закончена";
    public static void Main(string[] args)
    {
        while (true)
        {
            startGameRound();
        }
    }

    public static void startGameRound()
    {
        Console.WriteLine("Початок нового раунда: ");

        string[,] board = createBoard();
        startGameLoop(board);
    }

    public static string[,] createBoard()
    {
        string[,] board = new string[ROW_COUNT, COL_COUNT];

        for (int row = 0; row < ROW_COUNT; row++) 
        {
            for(int col = 0; col < COL_COUNT; col++)
            {
                board[row, col] = CELL_STATE_EMPTY;
            }
        }
        return board;
    }

    public static void startGameLoop(string[,] board)
    {
        bool playerTurn = true;
        do{
            if(playerTurn)
            {
                makePlayerTurn(board);
                printBoard(board);
            }
            else
            {
                makeBotTurn(board);
                printBoard(board);
            }

            playerTurn =!playerTurn;

            Console.WriteLine();

            string gameState = checkGameState(board);
            if (gameState != GAME_STATE_NOT_FINISHED)
            {
                Console.WriteLine(gameState);
                return;
            }
        }while(true);
    }

    public static void makePlayerTurn(string[,] board)
    {
        int[] coordinates = inputCellCoordinates(board);
        board[coordinates[0], coordinates[1]] = CELL_STATE_X;
    }

    public static int[] inputCellCoordinates(string[,] board)
    {
        Console.Write("Введіть 2 числа (ряд і колонку) через пробіл від 0 до 2): ");
        do {
            string[] input = Console.ReadLine().Split(" ");

            int row = int.Parse(input[0]);
            int col = int.Parse(input[1]);
            if ((row < 0) || (row >= ROW_COUNT) || (col < 0) || (col >= COL_COUNT))
            {
                Console.Write("Некоректне введення! Введіть 2 значення від 0 до 2 через пробіл: ");
            }
            else if (board[row, col] != CELL_STATE_EMPTY)
            {
                Console.WriteLine("Ця клітка вжє зайнята");
            }
            else
            {
                return new int[] { row, col };
            }
        } while (true);
    }

    public static int[] getRandomEmptyCellCoordinates(string[,] board)
    {
        Random r = new Random();
        do
        {
            int row = r.Next(ROW_COUNT);
            int col = r.Next(COL_COUNT);
            if (board[row, col] == CELL_STATE_EMPTY)
            {
                return new int[] { row, col };
            }

        } while (true);
       }

    public static void makeBotTurn(string[,] board)
    {
        Console.WriteLine("Хід бота");
        int[] coordinates = getRandomEmptyCellCoordinates(board);
        board[coordinates[0], coordinates[1]] = CELL_STATE_O;
    }
    public static string checkGameState(string[,] board)
    {
        List<int> list = new List<int>();

        for (int row = 0; row < ROW_COUNT; row++ )
        {
            int rowSum = 0;
            for (int col = 0; col < COL_COUNT; col++ )
            {
                rowSum += calculateNumValue(board[row, col]);
            }
            list.Add(rowSum);
        }

        for (int col = 0; col < COL_COUNT; col++)
        {
            int colSum = 0;
            for (int row = 0; row < ROW_COUNT; row++)
            {
                colSum += calculateNumValue(board[row, col]);
            }
            list.Add(colSum);
        }

        int leftDiagonalSum = 0;
        for(int i = 0; i < ROW_COUNT; i++)
        {
            leftDiagonalSum += calculateNumValue(board[i, i]);
        }
        list.Add(leftDiagonalSum);

        int rightDiagonalSum = 0;
        for (int i = 0; i < ROW_COUNT; i++)
        {
            rightDiagonalSum += calculateNumValue(board[i, (ROW_COUNT - 1) - i]);
        }
        list.Add(rightDiagonalSum);

        if(list.Count == 3)
        {
            return GAME_STATE_X_WON;
        } 
        else if(list.Count == -3)
        {
            return GAME_STATE_O_WON;
        }
        else if(areAllCellsTaken(board))
        {
            return GAME_STATE_DRAW;
        }
        else
        {
            return GAME_STATE_NOT_FINISHED;
        }
    }

    private static int calculateNumValue(string cellState)
    {
        if(cellState == CELL_STATE_X)
        {
            return 1;
        }
        else if(cellState == CELL_STATE_O)
        {
            return -1;
        }
        else 
        {
            return 0;
        }
    }

    public static bool areAllCellsTaken(string[,] board)
    {
        for (int row = 0; row < ROW_COUNT; row++)
        {
            for (int col = 0; col < COL_COUNT; col++)
            {
                if(board[row, col] == CELL_STATE_EMPTY)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public static void printBoard(string[,] board)
    {
        Console.WriteLine("----------");
        for (int row = 0; row < ROW_COUNT; row++)
        {
            string line = "| ";
            for (int col = 0; col < COL_COUNT; col++)
            {
                line+= board[row, col] + " ";
            }
            line += "|";

            Console.WriteLine(line);
        }
        Console.WriteLine("----------");
    }
}