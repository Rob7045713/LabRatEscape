using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabRatEscape
{
    class GameValue
    {
        decimal minValue;
        decimal maxValue;
        decimal value;
        decimal initialValue;

        public GameValue(decimal initialValue, decimal minValue, decimal maxValue)
        {
            this.initialValue = initialValue;
            this.value = initialValue;
            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        // getter
        public decimal getValue()
        {
            return value;
        }

        // setter
        public void setValue(decimal value)
        {
            this.value = value;
        }

        // adds
        public void Add(decimal add)
        {
            value = value + add;
            CutOff();
        }

        // multiplies
        public void Multiply(decimal multiply)
        {
            value = value * multiply;
            CutOff();
        }

        // cuts off at max and min
        private void CutOff()
        {
            if (value < minValue)
            {
                value = minValue;
            }

            if (value > maxValue)
            {
                value = maxValue;
            }
        }

        // resets to initial value
        public void Reset()
        {
            value = initialValue;
        }

    }
}

