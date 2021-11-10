using Machina.Components;
using Machina.Engine;
using StealthGame.Data;
using StealthGame.Data.Enemy;

namespace StealthGame.Components
{
    public class EnemyBehavior : BaseComponent
    {
        public EnemyBehavior(Actor actor, BeatTracker beatTracker, IEnemyBehavior behavior) : base(actor)
        {
            beatTracker.BeatHit += behavior.OnBeat;
            beatTracker.AppendTotalBeatCount(behavior.TotalBeats);
        }
    }
}