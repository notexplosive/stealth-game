using System.Collections;
using System.Collections.Generic;
using Machina.Engine;
using Microsoft.Xna.Framework;

namespace StealthGame.Data
{
    public class PathBuilder
    {
        private readonly List<IPathInstruction> instructions = new List<IPathInstruction>();
        private readonly Vector2 startPosition;
        public const float PixelsPerStep = 20f;

        private Vector2 CurrentPoint
        {
            get
            {
                if (this.instructions.Count > 0)
                {
                    return this.instructions[^1].EndPosition;
                }

                return this.startPosition;
            }
        }

        public PathBuilder(Vector2 start)
        {
            this.startPosition = start;
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

        public void AddInstruction(IPathInstruction instruction)
        {
            MachinaGame.Print(instruction);
            this.instructions.Add(instruction);
        }
    }
}