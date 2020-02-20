using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris
{
    public class Field
    {
        public int[,] fieldArray;

        private int rowSize = 28;
        private int columnSize = 14;

        public Field()
        {
            fieldArray = new int[rowSize, columnSize];
            SetField();
        }

        private void SetField()
        {
            for (int i = 0; i < rowSize; ++i)
            {
                for (int j = 0; j < columnSize; ++j)
                {
                    if (i == 0 || i == 1 || (j == 0 || j == 1 || j == columnSize - 2 || j == columnSize - 1) && i < 6)
                    {
                        fieldArray[i, j] = 6;
                    }
                    else if (i == rowSize - 2 || i == rowSize - 1 || j == 0 || j == 1|| j == columnSize - 2 || j == columnSize - 1)
                    {
                        fieldArray[i, j] = 4;
                    }
                    else
                    {
                        fieldArray[i, j] = 0;
                    }
                }
            }
        }

        public void ShowField()
        {
            for(int i = 0; i < 10; ++i)
            {
                Console.WriteLine(" ");
            }
            for (int i = 0; i < rowSize; ++i)
            {
                Console.Write("                                                     ");
                for (int j = 0; j < columnSize; ++j)
                {
                    switch (fieldArray[i, j]) 
                    {
                        case 0:
                            Console.Write('ㅤ');
                            break;
                        case 1:
                            Console.Write('□');
                            break;
                        case 2:
                            Console.Write('□');
                            break;
                        case 3:
                            break;
                        case 4:
                            Console.Write('▧');
                            break;
                        case 6:
                            Console.Write('■');
                            break;
                    }
                }
                Console.WriteLine();
            }
        }

        public int BreakLines()
        {
            int brokenLines = 0;
            for (int i = 2; i < rowSize - 2; ++i)
            {
                bool bIsEmpty = false;
                for (int j = 2; j < columnSize - 2; ++j)
                {
                    if (fieldArray[i, j] != 2)
                    {
                        bIsEmpty = true;
                    }
                }
                if (!bIsEmpty)
                {
                    for(int k = 2; k < columnSize - 2; ++k)
                    {
                        fieldArray[i, k] = 0;
                    }
                    brokenLines++;
                    Console.Clear();
                    ShowField();
                    System.Threading.Thread.Sleep(300);
                    for (int a = i; a > 2; --a)
                    {
                        for (int b = 2; b < columnSize - 2; ++b)
                        {
                            if (fieldArray[a, b] == 2)
                            {
                                fieldArray[a, b] = 0;
                                fieldArray[a + 1, b] = 2;
                            }
                        }
                    }
                }
            }
            return brokenLines;
        }
        
        public bool IsCrossBorder()
        {
            for (int i = 0; i < columnSize - 1; ++i)
            {
                if (fieldArray[5, i] == 2)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
