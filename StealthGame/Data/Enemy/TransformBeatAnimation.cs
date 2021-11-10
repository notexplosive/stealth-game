using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Machina.Components;
using Machina.Data;
using Machina.ThirdParty;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace StealthGame.Data.Enemy
{
    public class TransformBeatAnimation
    {
        private List<TransformState> states = new List<TransformState>();
        public readonly TransformState startingState;

        public TransformBeatAnimation(TransformState startingState)
        {
            this.startingState = startingState;
        }

        public int TotalLength => this.states.Count;

        private TransformState LatestState()
        {
            return this.states.Count == 0 ? this.startingState : this.states[^1];
        }

        public TransformBeatAnimation LookTo(float destinationAngle, int beatCount)
        {
            var startingAngle = LatestState().Angle;
            var angleDisplacement = destinationAngle - startingAngle;
            var angleIncrement = angleDisplacement / beatCount;
            for (int i = 1; i < beatCount; i++)
            {
                AddAngleState(startingAngle + angleIncrement * i);
            }
            
            AddAngleState(destinationAngle);

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

            return this.states[currentBeat % TotalLength];
        }

        public TransformBeatAnimation WaitFor(int beats)
        {
            for (int i = 0; i < beats; i++)
            {
                this.states.Add(LatestState());
            }

            return this;
        }

        public TransformBeatAnimation MoveTo(Vector2 target)
        {
            var start = LatestState().position;
            var displacement = target - start;
            var direction = displacement.NormalizedCopy() * PathBuilder.PixelsPerStep;
            var currentPoint = start;
            var directionLength = direction.Length();

            while ((currentPoint - target).Length() > directionLength)
            {
                currentPoint += direction;
                AddPositionState(currentPoint);
            }
            
            AddPositionState(target);
            
            return this;
        }

        private void AddPositionState(Vector2 position)
        {
            this.states.Add(new TransformState(position, LatestState().Angle));
        }

        public TransformBeatAnimation ForceSetAngle(float f)
        {
            LatestState().ForceSetAngle(f);
            return this;
        }
    }
}