using Microsoft.Xna.Framework;

namespace StealthGame.Data.PlayerPath
{
    public abstract class PathPoint
    {
        public readonly Vector2 position;

        protected PathPoint(Vector2 position)
        {
            this.position = position;
        }

        public abstract int RenderPriority { get; }
    }
}