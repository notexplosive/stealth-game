using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using StealthGame.Data.PlayerPath;

namespace StealthGame.Data.Enemy.Animation
{
    public class MoveToInstruction : IAnimationBuilderInstruction
    {
        private readonly Vector2 destinationPosition;

        public MoveToInstruction(Vector2 destinationPosition)
        {
            this.destinationPosition = destinationPosition;
        }

        public List<TransformState> Build(TransformState latestState)
        {
            var states = new List<TransformState>();
            var start = latestState.position;
            var displacement = this.destinationPosition - start;
            var direction = displacement.NormalizedCopy() * PathBuilder.PixelsPerStep;
            var currentPoint = start;
            var directionLength = direction.Length();

            while ((currentPoint - this.destinationPosition).Length() > directionLength)
            {
                currentPoint += direction;
                states.Add(new TransformState(currentPoint, latestState.Angle));
            }

            states.Add(new TransformState(this.destinationPosition, latestState.Angle));
            return states;
        }

        public TransformState EndState(TransformState prevState)
        {
            return new TransformState(this.destinationPosition, prevState.Angle);
        }
    }
}