using System;
using Machina.Components;
using Machina.Engine;
using StealthGame.Data;

namespace StealthGame.Components
{
    public class Editable<TScene> : BaseComponent where TScene : IScene
    {
        private readonly EditModeToggle<TScene> editMode;
        private readonly Action<TScene> callback;

        public Editable(Actor actor, EditModeToggle<TScene> editMode, Action<TScene> callback) : base(actor)
        {
            this.editMode = editMode;
            this.callback = callback;

            this.editMode.EditModeToggled += OnToggleEditMode;
        }

        public override void OnDeleteFinished()
        {
            this.editMode.EditModeToggled -= OnToggleEditMode;
        }

        private void OnToggleEditMode(TScene editorScene)
        {
            callback(editorScene);
        }
    }
}