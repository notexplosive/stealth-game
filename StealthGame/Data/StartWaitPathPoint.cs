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

        public override int RenderPriority => 10;
    }
    
    public class EndWaitPathPoint : PathPoint
    {

        public EndWaitPathPoint(Vector2 position) : base(position)
        {
        }

        public override int RenderPriority => 0;
    }
}