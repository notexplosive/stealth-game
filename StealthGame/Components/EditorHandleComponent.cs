using Machina.Components;
using Machina.Data;
using Machina.Engine;

namespace StealthGame.Components
{
    public class EditorHandleComponent : BaseComponent
    {
        private readonly Actor attachedActor;

        public EditorHandleComponent(Actor actor, Actor attachedActor) : base(actor)
        {
            this.attachedActor = attachedActor;
        }

        public override void Update(float dt)
        {
            this.attachedActor.transform.Position = this.actor.transform.Position;
            this.actor.transform.Depth = this.attachedActor.transform.Depth - 10;
        }
    }
}