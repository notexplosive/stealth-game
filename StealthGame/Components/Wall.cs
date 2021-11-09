using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;
using StealthGame.Data;

namespace StealthGame.Components
{
    public class Wall : BaseComponent
    {
        private readonly BoundingRect boundingRect;

        public Wall(Actor actor) : base(actor)
        {
            this.boundingRect = RequireComponent<BoundingRect>();
        }

        public Rectangle Rectangle()
        {
            return this.boundingRect.Rect;
        }
    }
}