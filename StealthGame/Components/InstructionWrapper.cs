using Machina.Components;
using Machina.Engine;
using StealthGame.Data;

namespace StealthGame.Components
{
    public class InstructionWrapper : BaseComponent
    {
        public readonly IPathInstruction instruction;

        public InstructionWrapper(Actor actor, IPathInstruction instruction) : base(actor)
        {
            this.instruction = instruction;
        }
    }
}