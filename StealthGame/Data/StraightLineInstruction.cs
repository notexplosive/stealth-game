using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace StealthGame.Data
{
    public class StraightLineInstruction : IPathInstruction
    {
        private readonly Vector2 start;
        private readonly Vector2 end;
        
        public Vector2 EndPosition => this.end;
        public Vector2 StartPosition => this.start;

        public StraightLineInstruction(Vector2 start, Vector2 end)
        {
            this.start = start;
            this.end = end;
        }
        
        public List<PathPoint> Build()
        {
            var displacement = this.end - this.start;
            var direction = displacement.NormalizedCopy() * PathBuilder.PixelsPerStep;
            var currentPoint = this.start;
            var directionLength = direction.Length();

            var builtPath = new List<PathPoint>();

            while ((currentPoint - this.end).Length() > directionLength)
            {
                currentPoint += direction;
                builtPath.Add(new VectorPathPoint(currentPoint));
            }

            return builtPath;
        }
    }
}