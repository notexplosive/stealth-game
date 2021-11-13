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
        private readonly PlayerInput player;

        public EnemyDetection(Actor actor, List<EnemyDetection> gameEnemies, PlayerInput player) : base(actor)
        {
            this.lineOfSight = RequireComponent<LineOfSight>();
            this.coneOfVision = RequireComponent<ConeOfVision>();
            this.gameEnemies = gameEnemies;
            this.gameEnemies.Add(this);
            this.player = player;
        }

        public override void Update(float dt)
        {
            if (CanSeePoint(this.player.transform.Position))
            {
                this.player.Caught();
            }
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