using Machina.Components;
using Machina.Engine;
using StealthGame.Data;

namespace StealthGame.Components
{
    public class Editable : BaseComponent
    {
        private readonly EditModeToggle editMode;
        private EditorHandle editorHandle;

        public Editable(Actor actor, EditModeToggle editMode) : base(actor)
        {
            this.editMode = editMode;
            OnToggleEditMode(editMode.isEditModeEnabled);
            
            this.editMode.EditModeToggled += OnToggleEditMode;
        }

        public override void OnDeleteFinished()
        {
            this.editMode.EditModeToggled -= OnToggleEditMode;
        }

        private void OnToggleEditMode(bool on)
        {
            if (on)
            {
                this.editorHandle = new EditorHandle(this.actor);
            }
            else
            {
                this.editorHandle?.Destroy();
                this.editorHandle = null;
            }
        }
    }
}