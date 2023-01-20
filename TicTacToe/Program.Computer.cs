using Board;

partial class Program
{
    /// <summary>
    /// Метод, реализующий игру с компьютером.
    /// </summary>
    private static void GameWithComputer()
    {
        // Создаем объект доски.
        PlayingDesk board = new PlayingDesk();

        Random rnd = new Random();

        // Определяем, кто будет ходить первыый
        int theFirstStep = rnd.Next(0, 2);
        int playerCounter = theFirstStep;

        while (!board.IsWin)
        {
            // Проверяем, что есть свободные ячейки.
            if (board.IsEveryoneEngaged())
            {
                board.ShowBoard(2);
                Console.WriteLine("\nDraw.");
                break;
            }
            
            // Тогда первым ходит игрок (человек)
            if (playerCounter == 0 && !board.IsEveryoneEngaged())
            {
                board.ShowBoard(1);

                int digit;
                Console.WriteLine("Write the number where you want to put a move.");
                while (!int.TryParse(Console.ReadLine(), out digit) || digit < 1 || digit > 9) {
                    Console.WriteLine("The digit is not recognized. Please repeat input.");
                }

                // Проверяем, что эта ячейка не занята.
                while (!board.CheckTheDigit(digit))
                {
                    Console.WriteLine("This cell is already filled. Try another one");
                    Console.WriteLine("Write the number where you want to put a move.");
                    while (!int.TryParse(Console.ReadLine(), out digit))
                    {
                        Console.WriteLine("The digit is not recognized. Please repeat input.");
                    }
             
                }
                // Установим ход.
                board.SetTheMove(digit, playerCounter,1);
            
                // Увеличим значение playerCounter.
                playerCounter = (playerCounter + 1) % 2;
            }
            else if (playerCounter ==1 && !board.IsEveryoneEngaged())
            {
                // Получим информацию обо всех выигрышных стратегиях.
                string[] positions = board.GetStrategyCombinations();

                string[] ways;

                bool isNext = false;
                
                // 1. Проверям есть ли позиции, где 2/3 заняты нашими, а 1 нет.
                // 2. Если да, то ставим на эту.
                foreach (var position in positions)
                {
                    ways = position.Split();

                    if (ways.Count(x => x == "✕") == 2 && ways.Count(x => x== "◯") == 0 && isNext == false)
                    {
                        int wayDigit = int.Parse(Array.Find(ways, x => int.TryParse(x, out int wayDigit) == true));
                        board.SetTheMove(wayDigit,playerCounter,1);
                        isNext = true;
                        break;
                    }
                }
                
                // 3. Проверяем, есть ли позиции врага такие что, он практичски выиграл (он_он_свободное).
                // 4. Если да, то мешаем ему и ставим.
                foreach (var position in positions)
                {
                    ways = position.Split();

                    if (ways.Count(x => x == "◯") == 2 && ways.Count(x => x== "✕") == 0 && isNext == false)
                    {
                        int wayDigit = int.Parse(Array.Find(ways, x => int.TryParse(x, out int wayDigit) == true));
                        board.SetTheMove(wayDigit,playerCounter,1);
                        isNext = true;
                        break;
                    }
                    
                }
                
                // 5. Если таких нет, то смотрим, есть ли стратегии, где 1/3 заняты нашими, а 2 нет.
                // 6. Если да, то ставим на любую из них.
                foreach (var position in positions)
                {
                    ways = position.Split();
                    
                    if (ways.Count(x => x == "✕") == 1 && ways.Count(x => x== "◯") != 2 && isNext == false)
                    {
                        string[] wayDigits = Array.FindAll(ways, x => int.TryParse(x, out int wayDigit) == true);
                        int wayDigit = int.Parse(wayDigits[rnd.Next(0,wayDigits.Length)]);
                        board.SetTheMove(wayDigit,playerCounter,1);
                        isNext = true;
                        break;
                    }
                }
                
                // 7. Если таких нет, то ставим на любую свободную (в преимуществе в середину).
                char[] waysChars = board.GetAvailableMoves();
                if (waysChars.Contains('5') && isNext == false) board.SetTheMove(5, playerCounter, 1);
                else if (isNext == false)
                {
                    board.SetTheMove(int.Parse($"{waysChars[rnd.Next(0, waysChars.Length)]}"), playerCounter, 1);
                }

                // Увеличим значение playerCounter.
                playerCounter = (playerCounter+1) % 2;
                
                Console.WriteLine("The computer move is done.");
            }
        }
    }
}