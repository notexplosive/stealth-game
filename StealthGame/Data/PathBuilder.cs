using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace StealthGame.Data
{
    public static class PathBuilder
    {
        public static List<Vector2> InterpolatePoints(Vector2 start, Vector2 end)
        {
            var displacement = end - start;
            var direction = displacement.NormalizedCopy() * 20f;
            var currentPoint = start;

            var result = new List<Vector2>();
            result.Add(currentPoint);
            var directionLength = direction.Length();

            while ((currentPoint - end).Length() > directionLength)
            {
                currentPoint += direction;
                result.Add(currentPoint);
            }

            return result;
        }
    }
}