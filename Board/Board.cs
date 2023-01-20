// Tyamin Ilya (tg: @mrshrimp_it)

namespace Board;

/// <summary>
/// Класс игральной доски с вспомогательными методами.
/// </summary>
public class PlayingDesk
{
    private readonly char _cross = '✕';
    private readonly char _circle = '◯';
    
    private char[] _desk = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    private bool _isWin = false;

    public bool IsWin => _isWin;

    public char this[int index] => _desk[index];

    /// <summary>
    /// Выводит на экран поле.
    /// </summary>
    /// <param name="typeOfPlay">описывает тип игры (с компьютером или 2 человека)</param>
    public void ShowBoard(int typeOfPlay)
    {
        if (typeOfPlay == 0) Console.WriteLine("Player 1 moves by crosses, player 2 by circles");
        if (typeOfPlay == 1) Console.WriteLine("Computer moves by crosses, human moves by circles");
        Console.WriteLine($"  {_desk[0]}  |  {_desk[1]}  |  {_desk[2]}  ");
        Console.WriteLine("_____|_____|_____ ");
        Console.WriteLine($"  {_desk[3]}  |  {_desk[4]}  |  {_desk[5]}  ");
        Console.WriteLine("_____|_____|_____ ");
        Console.WriteLine($"  {_desk[6]}  |  {_desk[7]}  |  {_desk[8]}  ");
        Console.WriteLine("     |     |      ");
    }

    /// <summary>
    /// Проверяет, стоит ли на этом месте еще цифра, или уже нет.
    /// </summary>
    /// <param name="digit">проверяемое поле</param>
    /// <returns>Является ли это поле пустым или занятым</returns>
    public bool CheckTheDigit(int digit)
    {
        return char.IsDigit(_desk[digit - 1]);
    }

    /// <summary>
    /// Ставит ход на поле. Проверяет, выиграл ли один из игроков.
    /// </summary>
    /// <param name="digit">куда ставить ход</param>
    /// <param name="player">игрок (крестик или нолик)</param>
    /// <param name="typeOfPlay">описывает тип игры (с компьютером или 2 человека)</param>
    public void SetTheMove(int digit, int player, int typeOfPlay)
    {
        // Ставим ход в зависимости от типа игрока.
        if (player == 1)
        {
            _desk[digit - 1] = _cross;
        }
        else
        {
            _desk[digit - 1] = _circle;
        }

        // Возможные выигрышные комбинации:
        string[] combinations = { "1 2 3", "4 5 6", "7 8 9", "1 4 7", "2 5 8", "3 6 9", "1 5 9", "3 5 7" };
        foreach (string str in combinations)
        {
            // Разбиваем в массив целых чисел.
            int[] combInts = str.Split().Select(int.Parse).ToArray();
            
            // Проверяем, что кто-то выиграл.
            if (_desk[combInts[0]-1] == _desk[combInts[1]-1] && _desk[combInts[1]-1] == _desk[combInts[2]-1])
            {
                _isWin = true;
                ShowBoard(typeOfPlay);
                Console.WriteLine($"Player {_desk[combInts[0]-1]} won");
                break;
            }
        }
    }
    
    /// <summary>
    /// Проверяет что не все клетки заняты
    /// </summary>
    /// <returns>True, если все заняты, False - если хотя бы одна свободная есть.</returns>
    public bool IsEveryoneEngaged()
    {
        foreach (var x in _desk)
        {
            if (char.IsDigit(x)) return false;
        }
        return true;
    }

    /// <summary>
    /// Метод, который возвращает информацию о всех выигрышных комбинациях и их состоянии в текущий момент игры
    /// </summary>
    /// <returns>Массив из string, содержащих информацию о трех элементах из выигрышной комбинации</returns>
    public string[] GetStrategyCombinations()
    {
        string[] combinations = { "1 2 3", "4 5 6", "7 8 9", "1 4 7", "2 5 8", "3 6 9", "1 5 9", "3 5 7" };
        string[] output = new string[combinations.Length];

        for (int i = 0; i < combinations.Length; i++)
        {
            int[] digits = combinations[i].Split().Select(int.Parse).ToArray();
            output[i] = $"{_desk[digits[0] -1]} {_desk[digits[1] -1]} {_desk[digits[2] -1]}";
        }
        return output;
    }

    /// <summary>
    /// Метод, возвращающий массив всех свободных ячеек
    /// </summary>
    /// <returns>Массив из символов - свободных ячеек</returns>
    public char[] GetAvailableMoves()
    {
        return Array.FindAll(_desk, char.IsDigit);
    }
}