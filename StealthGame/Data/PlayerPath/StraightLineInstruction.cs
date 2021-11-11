using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace StealthGame.Data.PlayerPath
{
    public class StraightLineInstruction : IPathInstruction
    {
        private readonly Vector2 end;
        
        public Vector2 EndPosition => this.end;

        public StraightLineInstruction(Vector2 end)
        {
            this.end = end;
        }
        
        public List<PathPoint> Build(Vector2 start)
        {
            var displacement = this.end - start;
            var direction = displacement.NormalizedCopy() * PlayerPathBuilder.PixelsPerStep;
            var currentPoint = start;
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