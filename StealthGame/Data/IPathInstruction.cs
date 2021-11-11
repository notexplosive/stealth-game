using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace StealthGame.Data
{
    public interface IPathInstruction
    {
        public List<PathPoint> Build(Vector2 start);
        public Vector2 EndPosition { get; }
    }
}