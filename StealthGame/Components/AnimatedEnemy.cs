using Machina.Components;
using Machina.Engine;
using StealthGame.Data.Enemy;

namespace StealthGame.Components
{
    public class AnimatedEnemy : BaseComponent, IEnemyBehavior
    {
        private readonly TransformBeatAnimation transformBeatAnimation;

        public AnimatedEnemy(Actor actor, TransformBeatAnimation transformBeatAnimation) : base(actor)
        {
            this.transformBeatAnimation = transformBeatAnimation;
        }

        public override void Update(float dt)
        {
            this.transformBeatAnimation.UpdateTween(dt);
        }

        public void OnBeat(int currentBeat)
        {
            var state = this.transformBeatAnimation.StateAt(currentBeat);
            this.transformBeatAnimation.ApplyToActor(state);
        }

        public int TotalBeats => this.transformBeatAnimation.TotalLength;
    }
}