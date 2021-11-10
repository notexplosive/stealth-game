using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Machina.Components;
using MonoGame.Extended;

namespace StealthGame.Data.Enemy
{
    public class BeatAnimationSequence
    {
        private List<TransformState> states = new List<TransformState>();
        private readonly TransformState startingState;
        private readonly Transform transform;

        public BeatAnimationSequence(Transform transform)
        {
            this.transform = transform;
            this.startingState = new TransformState(transform.Position, transform.Angle);
        }

        public int TotalLength => this.states.Count;

        private TransformState LatestState()
        {
            return this.states.Count == 0 ? this.startingState : this.states[^1];
        }

        public BeatAnimationSequence LookTo(float destinationAngle, int beatCount)
        {
            var startingAngle = LatestState().angle;
            var angleDisplacement = destinationAngle - startingAngle;
            var angleIncrement = angleDisplacement / beatCount;
            for (int i = 0; i < beatCount; i++)
            {
                AddAngleState(startingAngle + angleIncrement * i);
            }

            return this;
        }

        private void AddAngleState(float angle)
        {
            this.states.Add(new TransformState(LatestState().position, angle));
        }

        public TransformState StateAt(int currentBeat)
        {
            if (currentBeat < 0)
            {
                return this.startingState;
            }

            if (currentBeat >= TotalLength)
            {
                return LatestState();
            }
            
            return this.states[currentBeat];
        }

        public void ApplyToActor(TransformState state)
        {
            this.transform.Position = state.position;
            this.transform.Angle = state.angle;
        }
    }
}