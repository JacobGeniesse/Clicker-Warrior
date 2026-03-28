using UnityEngine;

namespace NumberControlExtension //Namespace for sorting numbers
{
    public static class NumberSorting{
        private static float[] NumberBlocks = new float[4]; //Number block float array, used to store nums during calculations

        public static void Sort(this float[] Blocks, float NumberToSort)
        {
            Blocks[3] = Mathf.Floor(NumberToSort / 1000000000); //Divide by billion and round down, then store number
            NumberToSort -= NumberBlocks[3] * 1000000000; //remove stored billion from temp storage
            Blocks[2] = Mathf.Floor(NumberToSort / 1000000); //Divide by Million and round down, then store number
            NumberToSort -= NumberBlocks[2] * 1000000; //Removed stored million from temp storage
            Blocks[1] = Mathf.Floor(NumberToSort / 1000); //Divide by thousand and round down, then store number
            NumberToSort -= NumberBlocks[1] * 1000; //Remove stored thousand from temp storage
            Blocks[0] = NumberToSort; //Remainder goes into 1s block
        }

    }
    
    public static class BloatManagement
    {
        public static void ManageNumbers(this float[] Nums)
        {
            for (int i = 0; i + 1 < Nums.Length; i++)
            {
                while (Nums[i] >= 1000) //If above 1000 give 1 to the next block and take 1000 from the current block
                {
                    Nums[i] -= 1000;
                    Nums[i + 1] += 1;
                }
                while (Nums[i] < 0) //If below 0 take 1 from the next block and give 1000 to this block
                {
                    Nums[i] += 1000;
                    Nums[i + 1] -= 1;
                }
            }
            if (Nums[Nums.Length - 1] < 0) //If this is the last block
            {
                for(int j = Nums.Length - 1; j > -1; j--)
                {
                    Nums[j] = 0; //Set all values to 0 if below 0 so the thing can die properly (Most likely an enemy)
                }
            }
        }
    }

    public static class BlockMath
    {
        public static void BlockAddition(this float[] BlockA, float[] BlockB) //Adding two blocks together
        {
            if(BlockA.Length == BlockB.Length)
            {
                for(int i = 0; i < BlockA.Length; i++)
                {
                    BlockA[i] += BlockB[i];
                }
                BlockA.ManageNumbers();
            }
            else
            {
                Debug.LogWarning("Blocks are not the same size! Cannot Compute!");
            }
        }

        public static void BlockSubtraction(this float[] BlockC, float[] BlockD) //Subtracting one block from another
        {
            if (BlockC.Length == BlockD.Length)
            {
                for (int i = 0; i < BlockC.Length; i++)
                {
                    BlockC[i] -= BlockD[i];
                }
                BlockC.ManageNumbers();
            }
            else
            {
                Debug.LogWarning("Blocks are not the same size! Cannot Compute!");
            }
        }

        public static void BlockMultiplication(this float[] BlockE, float[] BlockF) //Multiplying two blocks together
        {
            if (BlockE.Length == BlockF.Length)
            {
                for (int i = 0; i < BlockE.Length; i++)
                {
                    BlockE[i] *= BlockF[i];
                }
                BlockE.ManageNumbers();
            }
            else
            {
                Debug.LogWarning("Blocks are not the same size! Cannot Compute!");
            }
        }
    }
}
