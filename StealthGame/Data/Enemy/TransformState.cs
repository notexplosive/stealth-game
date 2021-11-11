using Machina.Components;
using Microsoft.Xna.Framework;

namespace StealthGame.Data.Enemy
{
    public class TransformState
    {
        public bool WasForceModified { get; private set; }
        public float Angle { private set; get; }
        public readonly Vector2 position;

        public TransformState(Vector2 position, float angle)
        {
            this.position = position;
            Angle = angle;
        }

        public TransformState(Transform transform)
        {
            this.position = transform.Position;
            Angle = transform.Angle;
        }

        public void ForceSetAngle(float angle)
        {
            Angle = angle;
            WasForceModified = true;
        }
    }
}