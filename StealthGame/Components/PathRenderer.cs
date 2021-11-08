using System;
using System.Runtime;
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
        private float currentTime;

        public PathRenderer(Actor actor, WalkingPath walkingPath) : base(actor)
        {
            this.walkingPath = walkingPath;
        }

        public override void Update(float dt)
        {
            this.currentTime += dt;

            this.currentTime %= this.walkingPath.TotalBeats();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var beatIndex = 0;
            foreach (var point in this.walkingPath.path)
            {
                var timeAtIndex = BeatTracker.TimeAt(beatIndex);
                var durationOfWholePath = this.walkingPath.TotalBeats();
                var p1 = Math.Abs(this.currentTime - timeAtIndex);
                var p2 = Math.Abs((this.currentTime + durationOfWholePath / 2) % durationOfWholePath -
                                  timeAtIndex);
                var p3 = Math.Abs((this.currentTime + durationOfWholePath / 4) % durationOfWholePath -
                                  timeAtIndex);
                var p4 = Math.Abs((this.currentTime + durationOfWholePath * 3 / 4) % durationOfWholePath -
                                  timeAtIndex);

                var highlight =
                        p1 < BeatTracker.durationOfOneBeat
                        || p2 < BeatTracker.durationOfOneBeat
                        || p3 < BeatTracker.durationOfOneBeat
                        || p4 < BeatTracker.durationOfOneBeat
                    ;

                if (point is VectorPathPoint)
                {
                    var radius = highlight ? 8f : 5f;
                    var color = highlight ? Color.Red : Color.White;
                    spriteBatch.DrawCircle(new CircleF(point.position, radius), 10, color, 1f, transform.Depth);
                }
                
                if (point is StartWaitPathPoint waitPathPoint)
                {
                    var radius = highlight ? 10f : 8f;
                    var color = highlight ? Color.Red : Color.White;
                    spriteBatch.DrawCircle(new CircleF(point.position, radius), 10, color, 1f, transform.Depth);
                    spriteBatch.DrawString(
                        MachinaGame.Assets.GetSpriteFont("DefaultFont"),
                        waitPathPoint.beatDuration.ToString(), 
                        waitPathPoint.position, 
                        Color.White, 
                        0f, 
                        Vector2.Zero, 
                        Vector2.One, 
                        SpriteEffects.None, 
                        transform.Depth);
                }

                beatIndex++;
            }
        }
    }
}