namespace StealthGame.Data.Enemy
{
    public interface IEnemyBehavior
    {
        void OnBeat(int currentBeat);
        int TotalBeats { get;  }
    }
}