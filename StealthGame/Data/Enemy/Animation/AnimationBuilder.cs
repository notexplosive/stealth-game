using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace StealthGame.Data.Enemy.Animation
{
    public class AnimationBuilder
    {
        public readonly List<IAnimationBuilderInstruction> instructions = new List<IAnimationBuilderInstruction>();

        public AnimationBuilder()
        {
        }

        public AnimationBuilder AddInstruction(IAnimationBuilderInstruction instruction)
        {
            this.instructions.Add(instruction);
            return this;
        }

        public AnimationBuilder LookTo(float destinationAngle, int beatCount)
        {
            this.instructions.Add(new LookToInstruction(destinationAngle, beatCount));
            return this;
        }

        public AnimationBuilder ForceSetAngle(float f)
        {
            this.instructions.Add(new ForceSetAngleInstruction(f));
            return this;
        }

        public AnimationBuilder WaitFor(int beats)
        {
            this.instructions.Add(new WaitForInstruction(beats));
            return this;
        }

        public AnimationBuilder MoveTo(Vector2 destination)
        {
            this.instructions.Add(new MoveToInstruction(destination));
            return this;
        }

        public List<TransformState> Build(TransformState startingState)
        {
            var currentState = startingState;
            var fullList = new List<TransformState>();
            foreach (var instruction in this.instructions)
            {
                fullList.AddRange(instruction.Build(currentState));
                currentState = fullList[^1];
            }

            return fullList;
        }
    }
}