using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StealthGame.Data;

namespace StealthGame.Components
{
    public class Vision : BaseComponent
    {
        private readonly Transform playerTransform;
        private readonly Wall[] walls;

        public Vision(Actor actor, Transform playerTransform, Wall[] walls) : base(actor)
        {
            this.playerTransform = playerTransform;
            this.walls = walls;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var sightline = CreateSightline(this.playerTransform.Position);
            sightline.ApplyWallCollisions(this.walls);
            sightline.DebugDraw(spriteBatch);
        }

        public Sightline CreateSightline(Vector2 targetPosition)
        {
            return new Sightline(transform.Position, targetPosition);
        }
    }
}