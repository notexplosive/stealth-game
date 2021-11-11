using System;
using Machina.Components;
using Machina.Data;
using Machina.Engine;
using Microsoft.Xna.Framework.Input;
using StealthGame.Data;

namespace StealthGame.Components
{
    public class EditModeToggle<TScene> : BaseComponent where TScene : IScene
    {
        private readonly TScene otherScene;
        public event Action<TScene> EditModeToggled;

        public EditModeToggle(Actor actor, TScene scene) : base(actor)
        {
            this.otherScene = scene;
        }

        public override void OnKey(Keys key, ButtonState state, ModifierKeys modifiers)
        {
            if (key == Keys.E && modifiers.Control && state == ButtonState.Pressed)
            {
                this.otherScene.SwitchTo(this.actor.scene);
                EditModeToggled?.Invoke(this.otherScene);
            }
        }
    }
}