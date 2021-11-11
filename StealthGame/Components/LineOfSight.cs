using System;
using System.Collections.Generic;
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
        private readonly Func<IList<Wall>> getWalls;

        public LineOfSight(Actor actor, GameScene gameScene, Func<IList<Wall>> getWalls) : base(actor)
        {
            this.playerTransform = gameScene.PlayerTransform;
            this.getWalls = getWalls;
        }

        public Sightline CreateSightline(Vector2 targetPosition)
        {
            var sightline = new Sightline(transform.Position, targetPosition);
            sightline.ApplyWallCollisions(this.getWalls);
            return sightline;
        }
    }
}