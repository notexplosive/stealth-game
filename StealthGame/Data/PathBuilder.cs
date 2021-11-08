using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace StealthGame.Data
{
    public class PathBuilder
    {
        private readonly List<PathPoint> builtPath = new List<PathPoint>();
        public const float stepLength = 20f;
        public Vector2 CurrentPoint => this.builtPath[^1].position;

        public PathBuilder(Vector2 start)
        {
            this.builtPath.Add(new VectorPathPoint(start));
        }
        
        public PathBuilder StraightLine(Vector2 end)
        {
            var start = this.builtPath[^1].position;
            var displacement = end - start;
            var direction = displacement.NormalizedCopy() * stepLength;
            var currentPoint = start;
            var directionLength = direction.Length();

            while ((currentPoint - end).Length() > directionLength)
            {
                currentPoint += direction;
                this.builtPath.Add(new VectorPathPoint(currentPoint));
            }
            
            return this;
        }

        public WalkingPath Build()
        {
            return new WalkingPath(this.builtPath);
        }

        public PathBuilder WaitPoint(int waitTimeBeats)
        {
            this.builtPath.Add(new StartWaitPathPoint(CurrentPoint, waitTimeBeats));

            for (int i = 0; i < waitTimeBeats; i++)
            {
                this.builtPath.Add(new VectorPathPoint(CurrentPoint));
            }
            
            this.builtPath.Add(new EndWaitPathPoint(CurrentPoint));
            
            return this;
        }
    }
}