using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using StealthGame.Data.PlayerPath;

namespace StealthGame.Data.Enemy.Animation
{
    public class MoveToInstruction : IAnimationBuilderInstruction
    {
        private readonly Vector2 target;

        public MoveToInstruction(Vector2 target)
        {
            this.target = target;
        }

        public List<TransformState> Build(TransformState latestState)
        {
            var states = new List<TransformState>();
            var start = latestState.position;
            var displacement = this.target - start;
            var direction = displacement.NormalizedCopy() * PathBuilder.PixelsPerStep;
            var currentPoint = start;
            var directionLength = direction.Length();

            while ((currentPoint - this.target).Length() > directionLength)
            {
                currentPoint += direction;
                states.Add(new TransformState(currentPoint, latestState.Angle));
            }

            states.Add(new TransformState(this.target, latestState.Angle));
            return states;
        }
    }
}