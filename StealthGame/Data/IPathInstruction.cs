using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace StealthGame.Data
{
    public interface IPathInstruction
    {
        public List<PathPoint> Build();
        public Vector2 EndPosition { get; }
        public Vector2 StartPosition { get; }
    }
}