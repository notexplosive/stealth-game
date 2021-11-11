using System;
using Machina.Components;
using Machina.Data;
using Machina.Engine;
using Microsoft.Xna.Framework.Input;
using StealthGame.Data;

namespace StealthGame.Components
{
    public class EditModeToggle : BaseComponent
    {
        private readonly IScene editorScene;
        public event Action<IScene> EditModeToggled;

        public EditModeToggle(Actor actor, IScene scene) : base(actor)
        {
            this.editorScene = scene;
        }

        public override void OnKey(Keys key, ButtonState state, ModifierKeys modifiers)
        {
            if (key == Keys.E && modifiers.Control && state == ButtonState.Pressed)
            {
                this.editorScene.SwitchTo(this.actor.scene);
                EditModeToggled?.Invoke(this.editorScene);
            }
        }
    }
}