using System.Collections.Generic;

namespace StealthGame.Data.Enemy.Animation
{
    public class TransformBeatAnimation
    {
        private readonly List<TransformState> states;
        public readonly TransformState startingState;
        public readonly AnimationBuilder originalBuilder;

        public TransformBeatAnimation(AnimationBuilder originalBuilder, TransformState startingState)
        {
            this.startingState = startingState;
            this.states = originalBuilder.Build(startingState);
            this.originalBuilder = originalBuilder;
        }

        public int TotalLength => this.states.Count;

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