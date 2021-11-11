using System.Collections.Generic;

namespace StealthGame.Data.PlayerPath
{
    public class WalkingPath
    {
        public readonly PathPoint[] path;

        public WalkingPath(List<PathPoint> path)
        {
            this.path = path.ToArray();
        }

        public PathPoint PathNodeAtBeat(int beatIndex)
        {
            if (beatIndex < 0)
            {
                return this.path[0];
            }

            if (beatIndex > this.path.Length - 1)
            {
                return this.path[^1];
            }
            
            return this.path[beatIndex];
        }

        public int TotalBeats()
        {
            return this.path.Length;
        }
    }
}