using System;
using System.Threading.Tasks;

namespace Tetris
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player("계피츄러스");
            while (true)
            {
                Console.Clear();
                Console.WriteLine("테트리스");
                Console.WriteLine("R: 시작 ESC: 나가기");
                Console.WriteLine($"방금점수: {player.Score}");
                Console.WriteLine($"최고점수: {Player.BestScore}");
                ConsoleKeyInfo key;

                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.R: //start
                        player.ResetCurrentScore();
                        player.PlayGame();
                        Console.WriteLine("게임오버");
                        if (Player.BestScore < player.Score)
                        {
                            Player.BestScore = player.Score;
                        }
                        break;
                    case ConsoleKey.Escape:
                        break;
                }
                if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }

        }
    }
}
