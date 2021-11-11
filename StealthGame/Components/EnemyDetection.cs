using System.Collections.Generic;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;

namespace StealthGame.Components
{
    public class EnemyDetection : BaseComponent
    {
        private readonly ConeOfVision coneOfVision;
        private readonly LineOfSight lineOfSight;
        private readonly List<EnemyDetection> gameEnemies;

        public EnemyDetection(Actor actor, List<EnemyDetection> gameEnemies) : base(actor)
        {
            this.lineOfSight = RequireComponent<LineOfSight>();
            this.coneOfVision = RequireComponent<ConeOfVision>();
            this.gameEnemies = gameEnemies;
            this.gameEnemies.Add(this);
        }

        public override void OnDeleteFinished()
        {
            this.gameEnemies.Remove(this);
        }

        public bool CanSeePoint(Vector2 destination)
        {
            var sightline = this.lineOfSight.CreateSightline(destination);
            return sightline.IsAbleToSeeTarget() && this.coneOfVision.IsWithinCone(destination);
        }
    }
}