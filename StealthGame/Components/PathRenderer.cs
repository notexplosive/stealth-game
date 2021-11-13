using System;
using System.Collections.Generic;
using System.Runtime;
using Machina.Components;
using Machina.Data;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using StealthGame.Data;
using StealthGame.Data.PlayerPath;

namespace StealthGame.Components
{
    public class PathRenderer : BaseComponent
    {
        private readonly WalkingPath walkingPath;
        private float currentBeat;
        private readonly Dictionary<Vector2,PathPoint> nodesToRender;
        private readonly List<EnemyDetection> enemies;

        public PathRenderer(Actor actor, WalkingPath walkingPath, List<EnemyDetection> enemies) : base(actor)
        {
            this.walkingPath = walkingPath;
            this.nodesToRender = new Dictionary<Vector2, PathPoint>();
            this.enemies = enemies;

            foreach (var node in this.walkingPath.path)
            {
                if (!this.nodesToRender.ContainsKey(node.position))
                {
                    this.nodesToRender.Add(node.position, node);
                }
                else
                {
                    var oldNode = this.nodesToRender[node.position];
                    if (oldNode.RenderPriority < node.RenderPriority)
                    {
                        this.nodesToRender[node.position] = node;
                    }
                }
            }
        }

        public override void Update(float dt)
        {
            this.currentBeat += BeatTracker.Seconds2Beats(dt);

            this.currentBeat %= this.walkingPath.TotalBeats();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var beatIndex = 0;
            foreach (var renderedNode in this.nodesToRender.Values)
            {
                var beatDurationOfWholePath = this.walkingPath.TotalBeats();

                var highlight = false;
                var isWithinCone = false;

                foreach (var cone in this.enemies)
                {
                    isWithinCone = isWithinCone || cone.CanSeePoint(renderedNode.position);
                }

                var depthOffset = highlight ? -1 : 0;

                if (renderedNode is VectorPathPoint)
                {
                    var radius = highlight ? 8f : 5f;
                    var color = highlight ? Color.Red : Color.White;
                    if (isWithinCone)
                    {
                        color = Color.OrangeRed;
                    }
                    spriteBatch.DrawCircle(new CircleF(renderedNode.position, radius), 10, color, 1f, transform.Depth - depthOffset);
                }
                
                if (renderedNode is StartWaitPathPoint waitPathPoint)
                {
                    var radius = highlight ? 10f : 8f;
                    var color = highlight ? Color.Red : Color.White;
                    if (isWithinCone)
                    {
                        color = Color.OrangeRed;
                    }
                    
                    spriteBatch.DrawCircle(new CircleF(renderedNode.position, radius), 10, color, 1f, transform.Depth - depthOffset);
                    spriteBatch.DrawString(
                        MachinaGame.Assets.GetSpriteFont("DefaultFont"),
                        BeatTracker.Beat2Seconds(waitPathPoint.beatDuration).ToString(), 
                        waitPathPoint.position, 
                        Color.White, 
                        0f, 
                        Vector2.Zero, 
                        Vector2.One, 
                        SpriteEffects.None, 
                        transform.Depth - 10);
                }

                beatIndex++;
            }
        }
    }
}