using Machina.Components;
using Machina.Engine;

namespace StealthGame.Components
{
    public class EditorTitle : BaseComponent
    {
        private readonly BoundedTextRenderer textRenderer;
        private readonly EditorInfo editorInfo;

        public EditorTitle(Actor actor) : base(actor)
        {
            this.textRenderer = RequireComponent<BoundedTextRenderer>();
            this.editorInfo = RequireComponent<EditorInfo>();
        }

        public override void Update(float dt)
        {
            if (this.editorInfo.SelectedActor != null)
            {
                this.textRenderer.Text = this.editorInfo.SelectedActor.name;
            }
        }
    }
}