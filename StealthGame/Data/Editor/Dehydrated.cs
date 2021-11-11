using Machina.Components;
using Microsoft.Xna.Framework;

namespace StealthGame.Data.Editor
{
    public abstract class Dehydrated<T>
    {
        protected readonly Vector2 position;
        protected readonly float angle;

        protected Dehydrated(Transform transform)
        {
            this.position = transform.Position;
            this.angle = transform.Angle;
        }

        public abstract string Serialize();
    }
}