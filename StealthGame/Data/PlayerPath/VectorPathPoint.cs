using Microsoft.Xna.Framework;

namespace StealthGame.Data.PlayerPath
{
    public class VectorPathPoint : PathPoint
    {
        public VectorPathPoint(Vector2 position) : base(position)
        {
        }

        public override int RenderPriority => 0;
    }
}