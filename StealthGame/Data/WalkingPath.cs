using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace StealthGame.Data
{
    public class WalkingPath
    {
        public readonly Vector2[] path;

        public WalkingPath(List<Vector2> path)
        {
            this.path = path.ToArray();
        }

        public Vector2 GetPathNodeAt(int beatIndex)
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

        public float TotalBeats()
        {
            return this.path.Length * BeatTracker.durationOfOneBeat;
        }
    }
}