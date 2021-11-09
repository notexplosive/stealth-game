using Machina.Components;
using Machina.Engine;
using StealthGame.Data;
using StealthGame.Data.Enemy;

namespace StealthGame.Components
{
    public class EnemyBehavior : BaseComponent
    {
        private readonly IEnemyBehavior behavior;

        public EnemyBehavior(Actor actor, BeatTracker beatTracker, IEnemyBehavior behavior) : base(actor)
        {
            this.behavior = behavior;

            beatTracker.BeatHit += OnBeatHit;
        }

        private void OnBeatHit(int currentBeat)
        {
            this.behavior.OnBeat(currentBeat);
        }
    }
}