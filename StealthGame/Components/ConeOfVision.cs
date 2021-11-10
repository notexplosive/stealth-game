using System;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using StealthGame.Data;

namespace StealthGame.Components
{
    public class ConeOfVision : BaseComponent
    {
        private readonly float visionWidth;
        private readonly FacingDirection facingDirection;
        private bool isActive = true;

        public ConeOfVision(Actor actor, float visionWidth) : base(actor)
        {
            this.visionWidth = visionWidth;
            this.facingDirection = RequireComponent<FacingDirection>();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!this.isActive)
            {
                return;
            }
            
            spriteBatch.DrawLine(transform.Position,
                transform.Position + new Angle(LeftAngle).ToUnitVector() * 100, Color.White, 1f,
                transform.Depth);

            spriteBatch.DrawLine(transform.Position,
                transform.Position + new Angle(RightAngle).ToUnitVector() * 100, Color.White, 1f,
                transform.Depth);
        }

        private float HalfVisionWidth => this.visionWidth / 2;

        private float LeftAngle => AngleUtil.NormalizedPI(this.facingDirection.Angle - HalfVisionWidth);
        private float RightAngle => AngleUtil.NormalizedPI(this.facingDirection.Angle + HalfVisionWidth);

        public bool IsWithinCone(Vector2 destination)
        {
            var displacement = destination - transform.Position;
            return this.isActive &&
                   new Angle(LeftAngle).ToUnitVector().Dot(displacement) > 0 &&
                   new Angle(RightAngle).ToUnitVector().Dot(displacement) > 0;
        }

        public void SetActive(bool active)
        {
            this.isActive = active;
        }
    }
}