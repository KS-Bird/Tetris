using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris
{
    public class Player
    {
        public static int BestScore = 0;
        public string Name { get; private set; }
        public int Score { get; private set; } = 0;
        public Player(string name)
        {
            Name = name;
        }
        public void PlayGame()
        {
            Field field = new Field();
            int[,] fieldArray = field.fieldArray;

            Random random = new Random();
            EBlockType blockType = (EBlockType)random.Next(0, 5);

            Block block = new Block(blockType);
            bool bIsBlockStatic = true;
            ConsoleKeyInfo key;

            while (true)
            {
                if (bIsBlockStatic)
                {
                    blockType = (EBlockType)random.Next(0, 5);
                    block = new Block(blockType);

                    bIsBlockStatic = false;
                }

                if (Console.KeyAvailable)
                {
                    key = Console.ReadKey(true);

                    switch (key.Key)
                    {
                        case ConsoleKey.RightArrow:
                            block.MoveRight(fieldArray);
                            break;
                        case ConsoleKey.LeftArrow:
                            block.MoveLeft(fieldArray);
                            break;
                        case ConsoleKey.DownArrow:
                            if (!block.MoveDown(fieldArray))
                            {
                                block.Solidify(fieldArray);
                                if (field.IsCrossBorder())
                                {
                                    break; //gameover
                                }
                                int brokenLines = field.BreakLines();
                                UpdateScore(brokenLines);
                                bIsBlockStatic = true;
                            }
                            break;
                        case ConsoleKey.Enter:
                            block.Rotate(fieldArray);
                            break;
                        case ConsoleKey.Spacebar:
                            block.DropBlock(fieldArray);
                            break;
                        default:
                            break;
                    }
                    if (field.IsCrossBorder())
                    {
                        break;
                    }
                }

                field.ShowField();
                Console.WriteLine($"현재점수: {Score}");
                Console.WriteLine($"최고점수: {BestScore}");
                System.Threading.Thread.Sleep(100);
                if (!bIsBlockStatic)
                {
                    if (!block.MoveDown(fieldArray))
                    {
                        block.Solidify(fieldArray);
                        if (field.IsCrossBorder())
                        {
                            break; //gameover
                        }
                        int brokenLines = field.BreakLines();
                        UpdateScore(brokenLines);
                        bIsBlockStatic = true;
                    }
                }
                Console.Clear();
            }
        }
        public void UpdateScore(int brokenLines)
        {
            Score += brokenLines * 10;
        }
        public void ResetCurrentScore()
        {
            Score = 0;
        }
    }
}
