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
        private bool shiftIsDown;
        private bool hasBeenCaught;

        public PlayerInput(Actor actor, BeatTracker beatTracker) : base(actor)
        {
            this.beatTracker = beatTracker;
        }

        public override void Update(float dt)
        {
            if (this.hasBeenCaught)
            {
                this.beatTracker.SubtractBeat(dt * 60);
                if (this.beatTracker.CurrentBeat == 0)
                {
                    this.hasBeenCaught = false;
                }
            }
            else
            {
                if (this.shiftIsDown)
                {
                    this.beatTracker.SubtractBeat(dt);
                }

                if (this.spaceIsPressed)
                {
                    this.beatTracker.AddBeat(dt);
                }   
            }
        }

        public override void OnKey(Keys key, ButtonState state, ModifierKeys modifiers)
        {
            this.shiftIsDown = modifiers.Shift;

            if (key == Keys.Space)
            {
                this.spaceIsPressed = state == ButtonState.Pressed;
            }
        }

        public void Caught()
        {
            this.hasBeenCaught = true;
        }
    }
}