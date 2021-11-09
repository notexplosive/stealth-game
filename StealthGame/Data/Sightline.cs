using System.Collections.Generic;
using Machina.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using StealthGame.Components;

namespace StealthGame.Data
{
    public class Sightline
    {
        private readonly Vector2 start;
        private readonly Vector2 direction;
        private readonly Vector2 destination;
        private readonly List<Vector2> collidePoints = new List<Vector2>(); 

        public Sightline(Vector2 start, Vector2 destination)
        {
            this.start = start;
            this.direction = (destination - start).NormalizedCopy();
            this.destination = destination;
        }

        public void DebugDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawLine(this.start, this.destination, Color.White, 1f, new Depth(0));
            foreach (var collidePoint in this.collidePoints)
            {
                spriteBatch.DrawCircle(new CircleF(collidePoint, 10), 10, Color.White, 1f, 0f);
            }
        }

        public void ApplyWallCollisions(Wall[] walls)
        {
            foreach(var wall in walls)
            {
                ApplyWallCollision(wall);
            }
        }

        private void ApplyWallCollision(Wall wall)
        {
            var collidePoints = new LineSegment(this.start, this.destination).GetCollidePoints(wall.Rectangle());
            foreach (var collidePoint in collidePoints)
            {
                AddCollidePoint(collidePoint);
            }
        }

        private void AddCollidePoint(Vector2 collidePoint)
        {
            this.collidePoints.Add(collidePoint);
        }

        public bool IsAbleToSeeTarget()
        {
            return this.collidePoints.Count == 0;
        }
    }
}