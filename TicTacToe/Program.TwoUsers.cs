using Board;

partial class Program
{
    /// <summary>
    /// Метод, реализующий игру между двумя человеками.
    /// </summary>
    private static void GameByTwoUsers()
    {
        // Создаем объект доски.
        PlayingDesk board = new PlayingDesk();
        
        int playerCounter = 0;
        
        while (!board.IsWin)
        {
            board.ShowBoard(0);

            // Проверяем, что еще есть свободные клетки 
            if (board.IsEveryoneEngaged())
            {
                Console.WriteLine("\nDraw.");
                break;
            }

            int player = playerCounter % 2 == 0 ? 1 : 2;
            Console.WriteLine($"Player №{(player)} walks.");
            
            int digit;
            Console.WriteLine("Write the number where you want to put a move.");
            
            // Проверяем на корректность введеной цифры.
            while (!int.TryParse(Console.ReadLine(), out digit) || digit < 1 || digit > 9)
            {
                Console.WriteLine("The digit is not recognized. Please repeat input.");
            }
            
            // Проверяем, что поле еще свободное.
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
            board.SetTheMove(digit, player,0);
            
            // Увеличим значение playerCounter.
            playerCounter += 1;
            if (!board.IsWin)
            {
                Console.WriteLine("\n\n");
            }
        }
        Console.WriteLine("End");
    }
}