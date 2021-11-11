using System.Collections.Generic;

namespace StealthGame.Data.Enemy.Animation
{
    public interface IAnimationBuilderInstruction
    {
        public List<TransformState> Build(TransformState latestState);
        TransformState EndState(TransformState prevState);
    }
}