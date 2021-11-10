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
    public class BeatAnimationSequence
    {
        private List<TransformState> states = new List<TransformState>();
        private readonly TransformState startingState;
        private readonly Transform transform;
        
        private readonly TweenChain positionTween = new TweenChain();
        private readonly TweenAccessors<Vector2> tweenablePosition;
        
        private readonly TweenChain angleTween = new TweenChain();
        private readonly TweenAccessors<float> tweenableAngle;
        private TransformState previousTargetState;

        public BeatAnimationSequence(Transform transform)
        {
            this.transform = transform;
            this.startingState = new TransformState(transform.Position, transform.Angle);

            this.previousTargetState = this.startingState;
            
            this.tweenablePosition =
                new TweenAccessors<Vector2>(() => transform.Position, val => transform.Position = val);
            this.tweenableAngle =
                new TweenAccessors<float>(() => transform.Angle, val => transform.Angle = val);
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

            return this.states[currentBeat % TotalLength];
        }

        public void UpdateTween(float dt)
        {
            this.angleTween.Update(dt);
            this.positionTween.Update(dt);
        }

        public void ApplyToActor(TransformState state)
        {
            if (state.position != previousTargetState.position)
            {
                this.positionTween.AppendVectorTween(state.position, BeatTracker.SecondsPerBeat,
                    EaseFuncs.Linear,
                    this.tweenablePosition);
            }
            
            if (state.angle != previousTargetState.angle)
            {
                this.positionTween.AppendFloatTween(state.angle, BeatTracker.SecondsPerBeat,
                    EaseFuncs.Linear,
                    this.tweenableAngle);
            }
            
            this.previousTargetState = state;
        }
    }
}