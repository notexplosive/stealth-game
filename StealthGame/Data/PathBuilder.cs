using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace StealthGame.Data
{
    public class PathBuilder
    {
        private readonly List<IPathInstruction> instructions = new List<IPathInstruction>();
        public const float PixelsPerStep = 20f;
        private Vector2 CurrentPoint => this.instructions[^1].EndPosition;

        public PathBuilder(Vector2 start)
        {
            this.instructions.Add(new WaitInstruction(start,0));
        }
        
        public PathBuilder AddStraightLine(Vector2 end)
        {
            this.instructions.Add(new StraightLineInstruction(CurrentPoint, end));
            return this;
        }

        public WalkingPath Build()
        {
            var builtPath = new List<PathPoint>();
            foreach (var instruction in Instructions())
            {
                builtPath.AddRange(instruction.Build());
            }

            return new WalkingPath(builtPath);
        }

        public PathBuilder AddWaitPoint(int waitTimeBeats)
        {
            this.instructions.Add(new WaitInstruction(CurrentPoint, waitTimeBeats));
            
            return this;
        }

        public IPathInstruction[] Instructions()
        {
            return this.instructions.ToArray();
        }
    }
}