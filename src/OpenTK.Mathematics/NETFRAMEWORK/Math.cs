#if NETFRAMEWORK
using System.Runtime.CompilerServices;

namespace System
{
    public static class MathEx
    {
        /// <summary>
        /// Clamp a value to the inclusive range [min, max].
        /// </summary>
        /// <remarks>
        /// In newer versions of the .NET Framework, there is a System.Math.Clamp() method.
        /// </remarks>
        /// <typeparam name="T">The type of value.</typeparam>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The clamped value.</returns>
        public static T Clamp<T>(T value, T min, T max) where T : System.IComparable<T>
        {
            if (value.CompareTo(max) > 0)
            {
                return max;
            }

            if (value.CompareTo(min) < 0)
            {
                return min;
            }

            return value;
        }

        public static float Min(float val1, float val2)
        {
            if (val1 > val2)
            {
                return val2;
            }

            return val1;
        }

        public static float Max(float val1, float val2)
        {
            if (val1 > val2)
            {
                return val1;
            }

            return val2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short Abs(short value)
        {
            if (value < 0)
            {
                value = (short)-value;
                if (value < 0)
                {
                    ThrowAbsOverflow();
                }
            }
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Abs(int value)
        {
            if (value < 0)
            {
                value = -value;
                if (value < 0)
                {
                    ThrowAbsOverflow();
                }
            }
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long Abs(long value)
        {
            if (value < 0)
            {
                value = -value;
                if (value < 0)
                {
                    ThrowAbsOverflow();
                }
            }
            return value;
        }

       // [MethodImpl(MethodImplOptions.AggressiveInlining)]
       // [CLSCompliant(false)]
        public static sbyte Abs(sbyte value)
        {
            if (value < 0)
            {
                value = (sbyte)-value;
                if (value < 0)
                {
                    ThrowAbsOverflow();
                }
            }
            return value;
        }

        // [DoesNotReturn]
        // [StackTraceHidden]
        private static void ThrowAbsOverflow()
        {
            throw new OverflowException("AbsOverflow");
        }
    }
}


#endif
