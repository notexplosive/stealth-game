using Machina.Components;
using Machina.Engine;
using StealthGame.Data.Enemy;

namespace StealthGame.Components
{
    public class AnimatedEnemy : BaseComponent, IEnemyBehavior
    {
        private readonly BeatAnimationSequence beatAnimationSequence;

        public AnimatedEnemy(Actor actor, BeatAnimationSequence beatAnimationSequence) : base(actor)
        {
            this.beatAnimationSequence = beatAnimationSequence;
        }

        public override void Update(float dt)
        {
            this.beatAnimationSequence.UpdateTween(dt);
        }

        public void OnBeat(int currentBeat)
        {
            var state = this.beatAnimationSequence.StateAt(currentBeat);
            this.beatAnimationSequence.ApplyToActor(state);
        }

        public int TotalBeats => this.beatAnimationSequence.TotalLength;
    }
}