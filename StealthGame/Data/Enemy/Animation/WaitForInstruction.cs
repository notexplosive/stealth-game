using System.Collections.Generic;

namespace StealthGame.Data.Enemy.Animation
{
    public class WaitForInstruction : IAnimationBuilderInstruction
    {
        private readonly int beats;

        public WaitForInstruction(int beats)
        {
            this.beats = beats;
        }
        
        public List<TransformState> Build(TransformState latestState)
        {
            var states = new List<TransformState>();
            for (int i = 0; i < this.beats; i++)
            {
                states.Add(latestState);
            }

            return states;
        }

        public TransformState EndState(TransformState prevState)
        {
            return prevState;
        }
    }
}