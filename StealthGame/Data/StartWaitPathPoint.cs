using Microsoft.Xna.Framework;

namespace StealthGame.Data
{
    public class StartWaitPathPoint : PathPoint
    {
        public readonly int beatDuration;

        public StartWaitPathPoint(Vector2 position, int beatDuration) : base(position)
        {
            this.beatDuration = beatDuration;
        }
    }
    
    public class EndWaitPathPoint : PathPoint
    {

        public EndWaitPathPoint(Vector2 position) : base(position)
        {
        }
    }
}