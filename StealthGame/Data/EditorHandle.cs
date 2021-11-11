using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;
using StealthGame.Components;

namespace StealthGame.Data
{
    public class EditorHandle
    {
        public readonly Actor actor;
        private readonly Actor editActor;

        public EditorHandle(Actor actor, EditorScene editorScene)
        {
            this.actor = actor;
            this.editActor =
                actor.scene.AddActor("EditorHandle:" + this.actor.name, this.actor.transform.Position);

            var gameActorBoundingRect = actor.GetComponent<BoundingRect>();
            var size = new Point(64, 64);
            var defaultRect = new Rectangle(size.X / 2, size.Y / 2, size.X, size.Y);

            if (gameActorBoundingRect != null)
            {
                new BoundingRect(this.editActor, gameActorBoundingRect.Size, gameActorBoundingRect.Offset);
            }
            else
            {
                new BoundingRect(this.editActor, defaultRect.Size, defaultRect.Location.ToVector2());
            }

            new BoundingRectFill(this.editActor, new Color(Color.Cyan, 0.5f));
            new Hoverable(this.editActor);
            var clickable = new Clickable(this.editActor);
            new Draggable(this.editActor);
            new MoveOnDrag(this.editActor);
            new EditorHandleComponent(this.editActor, this.actor);

            clickable.OnClick += (button) =>
            {
                if (button == MouseButton.Left)
                {
                    editorScene.Select(this);
                }
            };
        }

        public void Destroy()
        {
            this.editActor.Destroy();
        }
    }
}