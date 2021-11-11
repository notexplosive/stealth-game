using System;
using Machina.Components;
using Machina.Data;
using Machina.Engine;
using Microsoft.Xna.Framework.Input;

namespace StealthGame.Components
{
    public class EditModeToggle : BaseComponent
    {
        public bool isEditModeEnabled { private set; get; }
        public event Action<bool> EditModeToggled;

        public EditModeToggle(Actor actor) : base(actor)
        {
        }

        public override void OnKey(Keys key, ButtonState state, ModifierKeys modifiers)
        {
            if (key == Keys.E && modifiers.Control && state == ButtonState.Pressed)
            {
                isEditModeEnabled = !isEditModeEnabled;
                EditModeToggled?.Invoke(isEditModeEnabled);
                MachinaGame.Print("Edit mode:",this.isEditModeEnabled);
            }
        }
    }
}