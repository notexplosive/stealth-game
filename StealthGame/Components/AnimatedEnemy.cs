using System;
using Machina.Components;
using Machina.Data;
using Machina.Engine;
using Machina.ThirdParty;
using Microsoft.Xna.Framework;
using StealthGame.Data;
using StealthGame.Data.Enemy;

namespace StealthGame.Components
{
    public class AnimatedEnemy : BaseComponent, IEnemyBehavior
    {
        private readonly TransformBeatAnimation transformBeatAnimation;

        private TransformState previousTargetState;

        private readonly TweenChain positionTween = new TweenChain();
        private readonly TweenAccessors<Vector2> tweenablePosition;

        private readonly TweenChain angleTween = new TweenChain();
        private readonly TweenAccessors<float> tweenableAngle;

        public AnimatedEnemy(Actor actor, TransformBeatAnimation transformBeatAnimation) : base(actor)
        {
            this.transformBeatAnimation = transformBeatAnimation;
            this.tweenablePosition =
                new TweenAccessors<Vector2>(() => transform.Position, val => transform.Position = val);
            this.tweenableAngle =
                new TweenAccessors<float>(() => transform.Angle, val => transform.Angle = val);
            this.previousTargetState = this.transformBeatAnimation.StateAt(0);
        }

        public override void Update(float dt)
        {
            this.angleTween.Update(dt);
            this.positionTween.Update(dt);
        }

        public void OnBeat(int currentBeat)
        {
            var state = this.transformBeatAnimation.StateAt(currentBeat);
            ApplyToActor(state);
        }

        private void ApplyToActor(TransformState state)
        {
            this.angleTween.Clear();
            this.positionTween.Clear();
            
            if (state.position != this.previousTargetState.position)
            {
                this.positionTween.AppendVectorTween(state.position, BeatTracker.SecondsPerBeat,
                    EaseFuncs.Linear,
                    this.tweenablePosition);
            }

            if (state.Angle != this.previousTargetState.Angle)
            {
                this.angleTween.AppendFloatTween(state.Angle, BeatTracker.SecondsPerBeat,
                    EaseFuncs.Linear,
                    this.tweenableAngle);

                if (state.WasForceModified)
                {
                    this.angleTween.SkipToEnd();
                    transform.Angle = state.Angle;
                }
            }

            this.previousTargetState = state;
        }

        public int TotalBeats => this.transformBeatAnimation.TotalLength;
    }
}