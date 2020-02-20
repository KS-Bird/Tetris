using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Tetris
{
    public class Block
    {
        public EBlockType BlockType;
        public const char BLOCK_CHAR = '□';

        public int rowPos = 1;
        public int columnPos = 5;

        int[,] blockArray;
        List<int[,]> blockArrayList = new List<int[,]>(4);

        private int blockArrayListIndex = 0;

        public Block(EBlockType blockType)
        {
            BlockType = blockType;

            switch (BlockType)
            {
                case EBlockType.Block1:
                    blockArrayList.Add(new int[4, 4]
                    {
                        {0, 0, 0, 0 },
                        {0, 1, 1, 0 },
                        {0, 1, 1, 0 },
                        {0, 0, 0, 0 }
                    });
                    break;
                case EBlockType.Block2:
                    blockArrayList.Add(new int[4, 4]
                    {
                        {0, 0, 0, 0 },
                        {1, 1, 0, 0 },
                        {0, 1, 1, 0 },
                        {0, 0, 0, 0 }
                    });
                    blockArrayList.Add(new int[4, 4]
                    {
                        {0, 0, 1, 0 },
                        {0, 1, 1, 0 },
                        {0, 1, 0, 0 },
                        {0, 0, 0, 0 }
                    });
                    break;
                case EBlockType.Block3:
                    blockArrayList.Add(new int[4, 4]
                    {
                        {0, 1, 0, 0 },
                        {0, 1, 0, 0 },
                        {0, 1, 1, 0 },
                        {0, 0, 0, 0 }
                    });
                    blockArrayList.Add(new int[4, 4]
                    {
                        {0, 0, 0, 0 },
                        {0, 1, 1, 1 },
                        {0, 1, 0, 0 },
                        {0, 0, 0, 0 }
                    });
                    blockArrayList.Add(new int[4, 4]
                    {
                        {0, 1, 1, 0 },
                        {0, 0, 1, 0 },
                        {0, 0, 1, 0 },
                        {0, 0, 0, 0 }
                    });
                    blockArrayList.Add(new int[4, 4]{
                        {0, 0, 0, 0 },
                        {0, 0, 0, 1 },
                        {0, 1, 1, 1 },
                        {0, 0, 0, 0 }
                    });
                    break;
                case EBlockType.Block4:
                    blockArrayList.Add(new int[4, 4]
                    {
                        {0, 0, 0, 0 },
                        {0, 0, 0, 0 },
                        {1, 1, 1, 1 },
                        {0, 0, 0, 0 }
                    });
                    blockArrayList.Add(new int[4, 4]
                    {
                        {0, 1, 0, 0 },
                        {0, 1, 0, 0 },
                        {0, 1, 0, 0 },
                        {0, 1, 0, 0 }
                    });
                    blockArrayList.Add(new int[4, 4]
                    {
                        {0, 0, 0, 0 },
                        {1, 1, 1, 1 },
                        {0, 0, 0, 0 },
                        {0, 0, 0, 0 }
                    });
                    blockArrayList.Add(new int[4, 4]
                    {
                        {0, 0, 1, 0 },
                        {0, 0, 1, 0 },
                        {0, 0, 1, 0 },
                        {0, 0, 1, 0 }
                    });
                    break;
                case EBlockType.Block5:
                    blockArrayList.Add(new int[4, 4]
                    {
                        {0, 0, 0, 0 },
                        {0, 0, 1, 0 },
                        {0, 1, 1, 1 },
                        {0, 0, 0, 0 }
                    });
                    blockArrayList.Add(new int[4, 4]
                    {
                        {0, 0, 0, 0 },
                        {0, 0, 1, 0 },
                        {0, 0, 1, 1 },
                        {0, 0, 1, 0 }
                    });
                    blockArrayList.Add(new int[4, 4]
                    {
                        {0, 0, 0, 0 },
                        {0, 0, 0, 0 },
                        {0, 1, 1, 1 },
                        {0, 0, 1, 0 }
                    });
                    blockArrayList.Add(new int[4, 4]
                    {
                        {0, 0, 0, 0 },
                        {0, 0, 1, 0 },
                        {0, 1, 1, 0 },
                        {0, 0, 1, 0 }
                    });
                    break;
                default:
                    Debug.Assert(false, "Logic Error");
                    break;
            }
            blockArray = blockArrayList[blockArrayListIndex];
        }
        public bool MoveDown(int[,] fieldArray)
        {
            if (IsCollideDown(fieldArray))
            {
                return false;
            }

            clearTrack(fieldArray);

            rowPos++;

            DrawBlock(fieldArray);

            return true;
        }
        public bool MoveRight(int[,] fieldArray)
        {
            if (IsCollideRight(fieldArray))
            {
                return false;
            }
            clearTrack(fieldArray);

            columnPos++;
            
            DrawBlock(fieldArray);

            return true;
        }
        public bool MoveLeft(int[,] fieldArray)
        {
            if (IsCollideLeft(fieldArray))
            {
                return false;
            }
            clearTrack(fieldArray);
           
            columnPos--;

            DrawBlock(fieldArray);

            return true;
        }
        public bool DropBlock(int[,] fieldArray)
        {
            return false;
        }
        public bool Rotate(int[,] fieldArray)
        {
            int count = 1;
            int rightCount = 0;
            int leftCount = 0;
            while (count <= 3)
            {
                int collisionInfo = GetCollisionInfoIfRotate(fieldArray);
                switch (collisionInfo)
                {
                    case 3:
                        columnPos = columnPos - rightCount + leftCount;
                        return false;
                    case 2:
                        columnPos++;
                        rightCount++;
                        break;
                    case 1:
                        columnPos--;
                        leftCount++;
                        break;
                    case 0:
                        break;
                    default:
                        Debug.Assert(false, "Logic Error");
                        return false;
                }
                if (collisionInfo == 0)
                {
                    break;
                }
                count++;
            }
            if (count == 4)
            {
                columnPos = columnPos - rightCount + leftCount;
                return false;
            }
            clearTrack(fieldArray);

            blockArrayListIndex = (blockArrayListIndex + 1) % blockArrayList.Count;
            blockArray = blockArrayList[blockArrayListIndex];

            DrawBlock(fieldArray);
            return true;
        }
        private void clearTrack(int[,] fieldArray)
        {
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    if (fieldArray[rowPos + i, columnPos + j] == 1)
                    {
                        fieldArray[rowPos + i, columnPos + j] = 0;
                    }
                }
            }
        }
        private void DrawBlock(int[,] fieldArray)
        {
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    fieldArray[rowPos + i, columnPos + j] += blockArray[i, j];
                }
            }
        }
        public bool IsCollideDown(int[,] fieldArray)
        {
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    if (blockArray[i, j] != 0)
                    {
                        int sum = blockArray[i, j] + fieldArray[rowPos + i + 1, columnPos + j];
                        if (sum == 3 || sum == 5 || sum == 7)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public bool IsCollideRight(int[,] fieldArray)
        {
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    if (blockArray[i, j] != 0)
                    {
                        int sum = blockArray[i, j] + fieldArray[rowPos + i, columnPos + j + 1];
                        if (sum == 3 || sum == 5 || sum == 7)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public bool IsCollideLeft(int[,] fieldArray)
        {
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    if (blockArray[i, j] != 0)
                    {
                        int sum = blockArray[i, j] + fieldArray[rowPos + i, columnPos + j - 1];
                        if (sum == 3 || sum == 5 || sum == 7)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public int GetCollisionInfoIfRotate(int[,] fieldArray)
        {
            bool bIsCollidedWithRightSide = false;
            bool bIsCollidedWithLeftSide = false;
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    int[,] rotatedArray = blockArrayList[(blockArrayListIndex + 1) % blockArrayList.Count];
                    int sum = rotatedArray[i, j] + fieldArray[rowPos + i, columnPos + j];

                    if (sum == 3 || sum == 5 || sum == 7)
                    {
                        if (j == 0 || j == 1)
                        {
                            bIsCollidedWithLeftSide = true;
                        }
                        else
                        {
                            bIsCollidedWithRightSide = true;
                        }
                    }
                }
            }
            if (bIsCollidedWithLeftSide && bIsCollidedWithRightSide)
            {
                return 3;
            }
            if (bIsCollidedWithLeftSide)
            {
                return 2;
            }
            if (bIsCollidedWithRightSide)
            {
                return 1;
            }
            return 0;
        }
        public void Solidify(int[,] fieldArray)
        {
            clearTrack(fieldArray);
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    if (blockArray[i, j] == 1)
                    {
                        blockArray[i, j] = 2;
                    }
                }
            }
            DrawBlock(fieldArray);
        }
    }
}
