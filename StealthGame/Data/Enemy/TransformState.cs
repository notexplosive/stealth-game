using Microsoft.Xna.Framework;

namespace StealthGame.Data.Enemy
{
    public class TransformState
    {
        public readonly float angle;
        public readonly Vector2 position;

        public TransformState(Vector2 position, float angle)
        {
            this.position = position;
            this.angle = angle;
        }
    }
}