using System;
using Machina.Components;
using Machina.Data;
using Machina.Engine;
using Microsoft.Xna.Framework.Input;

namespace StealthGame.Components
{
    public class EditModeToggle : BaseComponent
    {
        private bool isEditModeEnabled;
        public event Action<bool> EditModeToggled;
        
        public EditModeToggle(Actor actor) : base(actor)
        {
        }

        public override void OnKey(Keys key, ButtonState state, ModifierKeys modifiers)
        {
            if (key == Keys.E && modifiers.Control)
            {
                this.isEditModeEnabled = !this.isEditModeEnabled;
                EditModeToggled?.Invoke(this.isEditModeEnabled);
            }
        }
    }
}