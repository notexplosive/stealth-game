using System.Collections.Generic;

namespace StealthGame.Data.Enemy.Animation
{
    public class ForceSetAngleInstruction : IAnimationBuilderInstruction
    {
        private readonly float angle;

        public ForceSetAngleInstruction(float angle)
        {
            this.angle = angle;
        }
        
        public List<TransformState> Build(TransformState latestState)
        {
            latestState.ForceSetAngle(this.angle);

            return new List<TransformState>(); // empty list on purpose
        }
    }
}