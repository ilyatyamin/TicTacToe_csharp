// Tyamin Ilya (tg: @mrshrimp_it)

partial class Program
{
    public static void Main(string[] args)
    {
        do
        {
            int command;

            Console.WriteLine("TicTacToe game. how to choose to play: with a computer or together?");
            Console.WriteLine("Print 1 if you want to play with human (together) or 2 if you want to play with computer");
            // Повторяем ввод, пока команда будет задана верно.
            while (!int.TryParse(Console.ReadLine(), out command) || (command < 1) || (command > 2))
            {
                Console.WriteLine("Please repeat the input. The command is not correct");
            }

            switch (command)
            {
                case 1:
                    GameByTwoUsers();
                    break;
                case 2:
                    GameWithComputer();
                    break;
            }
            Console.WriteLine("Do you wanna play again? Put any button except spacebar. \n");
        } while (Console.ReadKey().Key != ConsoleKey.Spacebar);
    }
}