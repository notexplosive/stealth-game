using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace StealthGame.Data
{
    public class WaitInstruction : IPathInstruction
    {
        private readonly Vector2 position;
        public readonly int waitTimeBeats;

        public WaitInstruction(Vector2 position, int waitTimeBeats)
        {
            this.position = position;
            this.waitTimeBeats = waitTimeBeats;
        }
        
        public List<PathPoint> Build(Vector2 start)
        {
            var builtPath = new List<PathPoint>();

            if (this.waitTimeBeats == 0)
            {
                builtPath.Add(new VectorPathPoint(this.position));
                return builtPath;
            }
            
            builtPath.Add(new StartWaitPathPoint(this.position, this.waitTimeBeats));

            for (int i = 0; i < this.waitTimeBeats; i++)
            {
                builtPath.Add(new VectorPathPoint(this.position));
            }
            
            builtPath.Add(new EndWaitPathPoint(this.position));

            return builtPath;
        }

        public Vector2 EndPosition => this.position;
    }
}