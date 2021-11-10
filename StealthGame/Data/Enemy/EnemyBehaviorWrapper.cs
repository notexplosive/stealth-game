namespace StealthGame.Data.Enemy
{
    public class EnemyBehaviorWrapper
    {
        public EnemyBehaviorWrapper(BeatTracker beatTracker, IEnemyBehavior behavior)
        {
            beatTracker.BeatHit += behavior.OnBeat;
            beatTracker.AppendTotalBeatCount(behavior.TotalBeats);
        }
    }
}