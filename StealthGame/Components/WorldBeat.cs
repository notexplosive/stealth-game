using Machina.Components;
using Machina.Engine;
using StealthGame.Data;

namespace StealthGame.Components
{
    public class WorldBeat : BaseComponent
    {
        private readonly BeatTracker beatTracker;

        public WorldBeat(Actor actor, BeatTracker beatTracker) : base(actor)
        {
            this.beatTracker = beatTracker;
        }

        public override void Update(float dt)
        {
            this.beatTracker.AddBeat(dt);
        }
    }
}