using System.Collections.Generic;

namespace StealthGame.Data.Enemy.Animation
{
    public class LookToInstruction : IAnimationBuilderInstruction
    {
        private readonly float destinationAngle;
        private readonly int beatCount;

        public LookToInstruction(float destinationAngle, int beatCount)
        {
            this.destinationAngle = destinationAngle;
            this.beatCount = beatCount;
        }
        
        public List<TransformState> Build(TransformState latestState)
        {
            var startingAngle = latestState.Angle;
            var angleDisplacement = this.destinationAngle - startingAngle;
            var angleIncrement = angleDisplacement / this.beatCount;
            var states = new List<TransformState>();
            for (int i = 1; i < this.beatCount; i++)
            {
                states.Add(new TransformState(latestState.position, startingAngle + angleIncrement * i));
            }

            states.Add(new TransformState(latestState.position, this.destinationAngle));
            return states;
        }
    }
}