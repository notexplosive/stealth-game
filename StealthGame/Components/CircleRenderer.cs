using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace StealthGame.Components
{
    public class CircleRenderer : BaseComponent
    {
        private readonly float radius;
        private readonly Color color;

        public CircleRenderer(Actor actor, float radius, Color color) : base(actor)
        {
            this.radius = radius;
            this.color = color;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawCircle(new CircleF(transform.Position,this.radius), 10, this.color, this.radius, transform.Depth);
        }
    }
}