using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StealthGame.Data;

namespace StealthGame.Components
{
    public class LineOfSight : BaseComponent
    {
        private readonly Transform playerTransform;
        private readonly Wall[] walls;

        public LineOfSight(Actor actor, Transform playerTransform, Wall[] walls) : base(actor)
        {
            this.playerTransform = playerTransform;
            this.walls = walls;
        }

        public Sightline CreateSightline(Vector2 targetPosition)
        {
            var sightline = new Sightline(transform.Position, targetPosition);
            sightline.ApplyWallCollisions(this.walls);
            return sightline;
        }
    }
}