using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabRatEscape
{
    class Modifier
    {
        private double change = 0;
        private int addSubtract = 1;
        private int rangeChange = 0;
        private double multiplier = 1;
        private int multiplyDivide = 1;
        private int rangeMultiplier = 0;
        private double resetChance = 0;
        private Random random;
        // constants
        public static int ADD = 1;
        public static int SUBTRACT = -1;
        public static int ADD_OR_SUBTRACT = 0;
        public static int MULTIPLY = 1;
        public static int DIVIDE = -1;
        public static int MULTIPLY_OR_DIVIDE = 0;
        public static int DONT_RANGE = 0;
        public static int RANGE = 1;

        public Modifier(Random random)
        {
            this.random = random;
        }

        public void setChange(double change)
        {
            if (addSubtract != ADD_OR_SUBTRACT)
            {
                addSubtract = Math.Sign(change);
            }
            this.change = Math.Abs(change);
        }
        public void setAddSubtract(int addSubtract)
        {
            this.addSubtract = addSubtract;
        }
        public void setRangeChange(int rangeChange)
        {
            this.rangeChange = rangeChange;
        }

        public void setMultiplier(double multiplier)
        {
            if (multiplyDivide != MULTIPLY_OR_DIVIDE && multiplier != 1)
            {
                multiplyDivide = Math.Sign(multiplier - 1);
            }
            this.multiplier = Math.Pow(multiplier, Math.Sign(multiplier - 1));
        }
        public void setMultiplyDivide(int multiplyDivide)
        {
            this.multiplyDivide = multiplyDivide;
        }
        public void setRangeMultiplier(int rangeMultiplier)
        {
            this.rangeMultiplier = rangeMultiplier;
        }

        public void setResetChance(double resetChance)
        {
            this.resetChance = resetChance;
        }

        public void Modify(GameValue gameValue)
        {
            gameValue.Add((decimal)(change * Math.Pow(random.NextDouble(), rangeChange) * Math.Sign(random.NextDouble() - .5 + addSubtract)));
            gameValue.Multiply((decimal)Math.Pow((multiplier - 1) * Math.Pow(random.NextDouble(), rangeMultiplier) + 1, Math.Sign(random.NextDouble() - .5 + multiplyDivide)));
            if (random.NextDouble() < resetChance)
            {
                gameValue.Reset();
            }
        }

        public string Describe(string toDescribe)
        {
            string multiplyDivideOperator = "";
            if (multiplyDivide < 0)
            {
                multiplyDivideOperator += "/";
            }
            if (multiplyDivide > 0)
            {
                multiplyDivideOperator += "*";
            }
            if (multiplyDivide == 0)
            {
                multiplyDivideOperator += "* or /";
            }
            if (rangeMultiplier == RANGE)
            {
                multiplyDivideOperator += " up to";
            }

            string addSubtractOperator = "";
            if (addSubtract < 0)
            {
                addSubtractOperator += "-";
            }
            if (addSubtract > 0)
            {
                addSubtractOperator += "+";
            }
            if (addSubtract == 0)
            {
                addSubtractOperator += "+ or -";
            }
            if (rangeChange == RANGE)
            {
                addSubtractOperator += " up to";
            }

            string description = "";

            if (change != 0)
            {
                description += toDescribe + " " + addSubtractOperator + " " + change + ";  ";
            }
            if (multiplier != 1)
            {
                description += toDescribe + " " + multiplyDivideOperator + " " + multiplier + ";  ";
            }
            if (resetChance != 0)
            {
                description += resetChance * 100 + "% reset " + toDescribe + ";  ";
            }

            return description;
        }

    }
}
