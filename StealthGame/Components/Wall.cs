using System.Collections.Generic;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;
using StealthGame.Data;

namespace StealthGame.Components
{
    public class Wall : BaseComponent
    {
        private readonly BoundingRect boundingRect;
        private readonly List<Wall> wallList;

        public Wall(Actor actor, List<Wall> wallList) : base(actor)
        {
            this.wallList = wallList;
            this.boundingRect = RequireComponent<BoundingRect>();
            
            this.wallList.Add(this);
        }

        public override void OnDeleteFinished()
        {
            this.wallList.Remove(this);
        }

        public Rectangle Rectangle()
        {
            return this.boundingRect.Rect;
        }
    }
}