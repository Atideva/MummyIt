using System;

namespace Ranged
{
    public class RangeFloatAttribute : Attribute
    {
        public RangeFloatAttribute(float min, float max)
        {
            Min = min;
            Max = max;
        }

        public float Min { get; }
        public float Max { get; }
    }
}