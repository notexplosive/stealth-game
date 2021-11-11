using Machina.Components;
using Machina.Engine;

namespace StealthGame.Components
{
    public class EditorInfo : BaseComponent
    {
        public EditorInfo(Actor actor) : base(actor)
        {
        }

        public Actor SelectedActor
        {
            get
            {
                if (StealthGame.CurrentEditor != null)
                {
                    var selected = StealthGame.CurrentEditor.selected;
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