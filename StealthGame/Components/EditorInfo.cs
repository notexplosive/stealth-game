using Machina.Components;
using Machina.Engine;
using StealthGame.Data;

namespace StealthGame.Components
{
    public class EditorInfo : BaseComponent
    {
        private readonly EditorScene editor;

        public EditorInfo(Actor actor, EditorScene editor) : base(actor)
        {
            this.editor = editor;
        }

        public Actor SelectedActor
        {
            get
            {
                if (this.editor != null)
                {
                    var selected = this.editor.selected;
                    if (selected != null)
                    {
                        return selected.actor;
                    }
                }

                return null;
            }
        }
    }
}