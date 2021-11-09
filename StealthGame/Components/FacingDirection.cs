using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace StealthGame.Components
{
    public class FacingDirection : BaseComponent
    {
        public FacingDirection(Actor actor, float facingAngle) : base(actor)
        {
            transform.Angle = facingAngle;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawLine(transform.Position, transform.Position + FacingVector * 20, Color.White, 1f, transform.Depth);
        }

        public Vector2 FacingVector => new Angle(transform.Angle).ToUnitVector();
        public float Angle => transform.Angle;
    }
}