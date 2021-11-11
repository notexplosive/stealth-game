using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace StealthGame.Data.PlayerPath
{
    public class WinInstruction : IPathInstruction
    {
        private readonly Vector2 position;

        public WinInstruction(Vector2 position)
        {
            this.position = position;
        }
        
        public List<PathPoint> Build(Vector2 start)
        {
            return new List<PathPoint> { new VectorPathPoint(start)};
        }

        public Vector2 EndPosition => this.position;
    }
}