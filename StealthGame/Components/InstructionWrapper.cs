using System;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;
using StealthGame.Data;

namespace StealthGame.Components
{
    public class InstructionWrapper : BaseComponent
    {
        private readonly IPathInstruction originalInstruction;

        public InstructionWrapper(Actor actor, IPathInstruction originalInstruction) : base(actor)
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