using System;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;
using StealthGame.Data;
using StealthGame.Data.PlayerPath;

namespace StealthGame.Components
{
    public class PathInstructionWrapper : BaseComponent
    {
        private readonly IPathInstruction originalInstruction;

        public PathInstructionWrapper(Actor actor, IPathInstruction originalInstruction) : base(actor)
        {
            this.originalInstruction = originalInstruction;
        }

        public IPathInstruction Rebuild(Vector2 nextPosition)
        {
            if (this.originalInstruction is StraightLineInstruction)
            {
                return new StraightLineInstruction(nextPosition);
            }

            if (this.originalInstruction is WaitInstruction wait)
            {
                return new WaitInstruction(transform.Position, wait.waitTimeBeats);
            }

            if (this.originalInstruction is WinInstruction)
            {
                return new WinInstruction(transform.Position);
            }

            throw new InvalidCastException();
        }
    }
}