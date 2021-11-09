using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace StealthGame.Components
{
    public class FacingDirection : BaseComponent
    {
        private readonly float facingAngle;

        public FacingDirection(Actor actor, float facingAngle) : base(actor)
        {
            this.facingAngle = facingAngle;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawLine(transform.Position, transform.Position + FacingVector * 20, Color.White, 1f, transform.Depth);
        }

        public Vector2 FacingVector => new Angle(this.facingAngle).ToUnitVector();
        public float Angle => this.facingAngle;
    }
}