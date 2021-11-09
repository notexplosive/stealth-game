using System;

namespace StealthGame.Data
{
    public static class AngleUtil
    {
        /// <summary>
        /// Takes any angle and normalizes it to [0, 2 * PI]
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static float NormalizedPositive(float angle)
        {
            var tau = MathF.PI * 2;
            var remainder = MathF.IEEERemainder(angle, tau);

            if (remainder >= 0)
            {
                return remainder;
            }
            else
            {
                return MathF.PI * 2 - MathF.Abs(remainder);
            }
        }

        public static float NormalizedPI(float angle)
        {
            return NormalizedPositive(angle + MathF.PI) - MathF.PI;
        }

        /// <summary>
        /// Computes A - B and returns an angle in range [-PI, PI]
        /// </summary>
        /// <param name="angle1"></param>
        /// <param name="angle2"></param>
        /// <returns></returns>
        public static float AngleShortestDifference(float angle1, float angle2)
        {
            return NormalizedPI(angle1 - angle2);
        }

        public static float Lerp(float startAngle, float endAngle, float percent)
        {
            return startAngle + (endAngle - startAngle) * percent;
        }
    }
}