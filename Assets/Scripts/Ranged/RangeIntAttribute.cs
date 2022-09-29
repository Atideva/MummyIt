using System;

namespace Ranged
{
    public class RangeIntAttribute : Attribute
    {
        public RangeIntAttribute(float min, float max)
        {
            Min = min;
            Max = max;
        }

        public float Min { get; }
        public float Max { get; }
    }
}