using Machina.Components;
using Machina.Data;
using Machina.Engine;
using Microsoft.Xna.Framework.Input;
using StealthGame.Data;

namespace StealthGame.Components
{
    public class AdvanceWorldBeat : BaseComponent
    {
        private readonly BeatTracker beatTracker;
        private bool shiftIsDown;
        private bool isPaused;

        public AdvanceWorldBeat(Actor actor, BeatTracker beatTracker) : base(actor)
        {
            this.beatTracker = beatTracker;
        }

        public override void Update(float dt)
        {
            if (!this.isPaused)
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
        }

        public override void OnKey(Keys key, ButtonState state, ModifierKeys modifiers)
        {
            this.shiftIsDown = modifiers.Shift;
        }

        public void OnEditModeToggled(bool on)
        {
            if (on)
            {
                Pause();
            }
            else
            {
                Play();
            }
        }

        private void Pause()
        {
            this.isPaused = true;
        }

        private void Play()
        {
            this.isPaused = false;
        }
    }
}