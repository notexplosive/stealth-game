using System.Collections.Generic;

namespace StealthGame.Data.Enemy.Animation
{
    public class TransformBeatAnimation
    {
        private readonly List<TransformState> states;
        public readonly TransformState startingState;

        public TransformBeatAnimation(List<TransformState> states)
        {
            this.states = states;
            this.startingState = states[0];
        }

        public int TotalLength => this.states.Count;

        private TransformState LatestState()
        {
            return this.states.Count == 0 ? this.startingState : this.states[^1];
        }

        public TransformState StateAt(int currentBeat)
        {
            if (currentBeat < 0)
            {
                return this.startingState;
            }

            return this.states[currentBeat % TotalLength];
        }
    }
}