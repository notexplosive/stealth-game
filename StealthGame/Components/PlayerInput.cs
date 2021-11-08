using Machina.Components;
using Machina.Data;
using Machina.Engine;
using Microsoft.Xna.Framework.Input;
using StealthGame.Data;

namespace StealthGame.Components
{
    public class PlayerInput : BaseComponent
    {
        private readonly BeatTracker beatTracker;
        private bool spaceIsPressed;

        public PlayerInput(Actor actor, BeatTracker beatTracker) : base(actor)
        {
            this.beatTracker = beatTracker;
        }

        public override void Update(float dt)
        {
            if (this.spaceIsPressed)
            {
                this.beatTracker.AddBeat(dt);
            }
        }

        public override void OnKey(Keys key, ButtonState state, ModifierKeys modifiers)
        {
            if (key == Keys.Space)
            {
                this.spaceIsPressed = state == ButtonState.Pressed;
            }
        }
    }
}