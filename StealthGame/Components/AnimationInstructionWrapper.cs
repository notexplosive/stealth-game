using System;
using Machina.Components;
using Machina.Engine;
using StealthGame.Data.Enemy;
using StealthGame.Data.Enemy.Animation;

namespace StealthGame.Components
{
    public class AnimationInstructionWrapper : BaseComponent
    {
        private readonly IAnimationBuilderInstruction instruction;

        public AnimationInstructionWrapper(Actor actor, IAnimationBuilderInstruction instruction) : base(actor)
        {
            this.instruction = instruction;
        }

        public IAnimationBuilderInstruction Rebuild(TransformState prevPos)
        {
            if (this.instruction is ForceSetAngleInstruction)
            {
                return new ForceSetAngleInstruction(transform.Angle);
            }
            
            if (this.instruction is LookToInstruction)
            {
                return new LookToInstruction(transform.Angle, 20);
            }
            
            if (this.instruction is MoveToInstruction)
            {
                return new MoveToInstruction(transform.Position);
            }

            if (this.instruction is WaitForInstruction)
            {
                return this.instruction;
            }
            
            throw new ArgumentException();
        }
    }
}