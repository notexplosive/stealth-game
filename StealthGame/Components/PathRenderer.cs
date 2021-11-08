using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using StealthGame.Data;

namespace StealthGame.Components
{
    public class PathRenderer : BaseComponent
    {
        private readonly WalkingPath walkingPath;

        public PathRenderer(Actor actor, WalkingPath walkingPath) : base(actor)
        {
            this.walkingPath = walkingPath;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var point in this.walkingPath.path)
            {
                spriteBatch.DrawCircle(new CircleF(point, 5), 10, Color.White, 1f, transform.Depth);
            }
        }
    }
}