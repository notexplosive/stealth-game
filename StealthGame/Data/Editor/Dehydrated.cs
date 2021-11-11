using Machina.Components;
using Microsoft.Xna.Framework;

namespace StealthGame.Data.Editor
{
    public abstract class Dehydrated<T>
    {
        protected readonly Vector2 position;
        protected readonly float angle;

        protected Dehydrated(Vector2 position, float angle)
        {
            this.position = position;
            this.angle = angle;
        }

        public abstract string Serialize();
    }
}