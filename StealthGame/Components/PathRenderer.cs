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

namespace StealthGame.Components
{
    public class PathRenderer : BaseComponent
    {
        private readonly WalkingPath walkingPath;
        private float currentTime;
        private readonly Dictionary<Vector2,PathPoint> nodesToRender;

        public PathRenderer(Actor actor, WalkingPath walkingPath) : base(actor)
        {
            this.walkingPath = walkingPath;
            this.nodesToRender = new Dictionary<Vector2, PathPoint>();

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
            this.currentTime += dt;

            this.currentTime %= this.walkingPath.TotalBeats();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var beatIndex = 0;
            foreach (var renderedNode in this.nodesToRender.Values)
            {
                var timeAtIndex = BeatTracker.Beat2Seconds(beatIndex);
                var durationOfWholePath = this.walkingPath.TotalBeats();

                float[] increments = new float[] { 0f, durationOfWholePath / 4, durationOfWholePath / 2, durationOfWholePath * 3 / 4 };

                var highlight = false;

                foreach (var increment in increments)
                {
                    var alongTime = (this.currentTime + increment) % durationOfWholePath;
                    var beat = (int) BeatTracker.Seconds2Beats(alongTime);
                    var positionAtBeat = this.walkingPath.PathNodeAtBeat(beat).position;

                    highlight = highlight || (positionAtBeat == renderedNode.position);
                }

                var depthOffset = highlight ? -1 : 0;

                if (renderedNode is VectorPathPoint)
                {
                    var radius = highlight ? 8f : 5f;
                    var color = highlight ? Color.Red : Color.White;
                    spriteBatch.DrawCircle(new CircleF(renderedNode.position, radius), 10, color, 1f, transform.Depth - depthOffset);
                }
                
                if (renderedNode is StartWaitPathPoint waitPathPoint)
                {
                    var radius = highlight ? 10f : 8f;
                    var color = highlight ? Color.Red : Color.White;
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