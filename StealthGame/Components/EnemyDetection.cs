using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;

namespace StealthGame.Components
{
    public class EnemyDetection : BaseComponent
    {
        private readonly ConeOfVision coneOfVision;
        private readonly LineOfSight lineOfSight;

        public EnemyDetection(Actor actor) : base(actor)
        {
            this.lineOfSight = RequireComponent<LineOfSight>();
            this.coneOfVision = RequireComponent<ConeOfVision>();
        }

        public bool CanSeePoint(Vector2 destination)
        {
            var sightline = this.lineOfSight.CreateSightline(destination);
            return sightline.IsAbleToSeeTarget() && this.coneOfVision.IsWithinCone(destination);
        }
    }
}