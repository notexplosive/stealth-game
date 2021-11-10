using Machina.Components;
using Machina.Data;
using Machina.Engine;
using Microsoft.Xna.Framework.Input;
using StealthGame.Data;

namespace StealthGame.Components
{
    public class WorldBeat : BaseComponent
    {
        private readonly BeatTracker beatTracker;
        private bool shiftIsDown;

        public WorldBeat(Actor actor, BeatTracker beatTracker) : base(actor)
        {
            this.beatTracker = beatTracker;
        }

        public override void Update(float dt)
        {
            if (this.shiftIsDown)
            {
                this.beatTracker.SubtractBeat(dt);
            }
            else
            {
                this.beatTracker.AddBeat(dt);
            }
        }

        public override void OnKey(Keys key, ButtonState state, ModifierKeys modifiers)
        {
            this.shiftIsDown = modifiers.Shift;
        }
    }
}