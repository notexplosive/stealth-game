using System;
using Machina.Components;
using Machina.Engine;
using StealthGame.Data;

namespace StealthGame.Components
{
    public class Editable : BaseComponent
    {
        private readonly EditModeToggle editMode;
        private EditorHandle editorHandle;
        private readonly Action<IScene> callback;

        public Editable(Actor actor, EditModeToggle editMode, Action<IScene> callback) : base(actor)
        {
            this.editMode = editMode;
            this.callback = callback;

            this.editMode.EditModeToggled += OnToggleEditMode;
        }

        public override void OnDeleteFinished()
        {
            this.editMode.EditModeToggled -= OnToggleEditMode;
        }

        private void OnToggleEditMode(IScene editorScene)
        {
            callback(editorScene);
        }
    }
}