using System;
using System.Collections.Generic;
using Machina.Data;
using Microsoft.Xna.Framework;

namespace StealthGame.Data
{
    public readonly struct LineSegment
    {
        private readonly Vector2 p1;
        private readonly Vector2 p2;

        public LineSegment(Vector2 p1, Vector2 p2)
        {
            this.p1 = p1;
            this.p2 = p2;
        }

        private Tuple<float, float> GetCollisionValues(LineSegment other)
        {
            var a = ((other.p2.X - other.p1.X) * (this.p1.Y - other.p1.Y) -
                     (other.p2.Y - other.p1.Y) * (this.p1.X - other.p1.X)) /
                    ((other.p2.Y - other.p1.Y) * (this.p2.X - this.p1.X) -
                     (other.p2.X - other.p1.X) * (this.p2.Y - this.p1.Y));
            var b = ((this.p2.X - this.p1.X) * (this.p1.Y - other.p1.Y) -
                     (this.p2.Y - this.p1.Y) * (this.p1.X - other.p1.X)) /
                    ((other.p2.Y - other.p1.Y) * (this.p2.X - this.p1.X) -
                     (other.p2.X - other.p1.X) * (this.p2.Y - this.p1.Y));

            return new Tuple<float, float>(a, b);
        }

        public bool IsCollidingLineSegment(LineSegment other)
        {
            var col = GetCollisionValues(other);
            if (col.Item1 >= 0 && col.Item1 <= 1 && col.Item2 >= 0 && col.Item2 <= 1)
            {
                return true;
            }

            return false;
        }

        public Vector2 GetCollidePoint(LineSegment other)
        {
            var col = GetCollisionValues(other);
            return new Vector2(
                this.p1.X + col.Item1 * (this.p2.X - this.p1.X),
                this.p1.Y + col.Item1 * (this.p2.Y - this.p1.Y));
        }

        public Vector2[] GetCollidePoints(Rectangle rectangle)
        {
            var sides = new LineSegment[]
            {
                new LineSegment(
                    new Vector2(rectangle.Left, rectangle.Bottom),
                    new Vector2(rectangle.Left, rectangle.Top)),
                new LineSegment(
                    new Vector2(rectangle.Right, rectangle.Bottom),
                    new Vector2(rectangle.Right, rectangle.Top)),
                new LineSegment(
                    new Vector2(rectangle.Left, rectangle.Top),
                    new Vector2(rectangle.Right, rectangle.Top)),
                new LineSegment(
                    new Vector2(rectangle.Left, rectangle.Bottom),
                    new Vector2(rectangle.Right, rectangle.Bottom))
            };

            var result = new List<Vector2>();
            foreach (var side in sides)
            {
                if (IsCollidingLineSegment(side))
                {
                    result.Add(GetCollidePoint(side));
                }
            }

            return result.ToArray();
        }
    }
}