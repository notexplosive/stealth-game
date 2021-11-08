using Machina.Components;
using Machina.Data;
using Machina.Engine;
using Machina.ThirdParty;
using Microsoft.Xna.Framework;
using StealthGame.Data;

namespace StealthGame.Components
{
    public class PlayerMovement : BaseComponent
    {
        private readonly BeatTracker tracker;
        private readonly WalkingPath path;
        private readonly TweenChain tween = new TweenChain();
        private Vector2 currentTargetPosition;
        private readonly TweenAccessors<Vector2> tweenablePosition;

        public PlayerMovement(Actor actor, BeatTracker tracker, WalkingPath path) : base(actor)
        {
            this.tracker = tracker;
            this.path = path;

            transform.Position = this.path.GetPathNodeAt(this.tracker.CurrentBeat).position;
            this.currentTargetPosition = this.path.GetPathNodeAt(this.tracker.CurrentBeat).position;
            this.tweenablePosition =
                new TweenAccessors<Vector2>(() => transform.Position, val => transform.Position = val);
        }

        public override void Update(float dt)
        {
            MoveToNextPoint();
            this.tween.Update(dt);
        }

        private void MoveToNextPoint()
        {
            var previousTarget = this.currentTargetPosition;
            this.currentTargetPosition = this.path.GetPathNodeAt(this.tracker.CurrentBeat).position;

            if (this.currentTargetPosition != previousTarget)
            {
                this.tween.AppendVectorTween(this.currentTargetPosition, BeatTracker.durationOfOneBeat,
                    EaseFuncs.Linear,
                    this.tweenablePosition);
            }

        }
    }
}