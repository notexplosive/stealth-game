using Machina.Components;

namespace StealthGame.Data.Enemy
{
    public class CameraPanning : IEnemyBehavior
    {
        private readonly BeatAnimationSequence beatAnimationSequence;

        public CameraPanning(BeatAnimationSequence beatAnimationSequence)
        {
            this.beatAnimationSequence = beatAnimationSequence;
        }

        public void OnBeat(int currentBeat)
        {
            var state = this.beatAnimationSequence.StateAt(currentBeat);
            this.beatAnimationSequence.ApplyToActor(state);
        }

        public int TotalBeats => this.beatAnimationSequence.TotalLength;
    }
}