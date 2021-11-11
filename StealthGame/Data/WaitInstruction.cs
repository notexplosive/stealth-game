using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace StealthGame.Data
{
    public class WaitInstruction : IPathInstruction
    {
        private readonly Vector2 start;
        private readonly int waitTimeBeats;

        public WaitInstruction(Vector2 start, int waitTimeBeats)
        {
            this.start = start;
            this.waitTimeBeats = waitTimeBeats;
        }
        
        public List<PathPoint> Build()
        {
            var builtPath = new List<PathPoint>();

            if (this.waitTimeBeats == 0)
            {
                builtPath.Add(new VectorPathPoint(this.start));
                return builtPath;
            }
            
            builtPath.Add(new StartWaitPathPoint(this.start, this.waitTimeBeats));

            for (int i = 0; i < this.waitTimeBeats; i++)
            {
                builtPath.Add(new VectorPathPoint(this.start));
            }
            
            builtPath.Add(new EndWaitPathPoint(this.start));

            return builtPath;
        }

        public Vector2 EndPosition => this.start;
    }
}