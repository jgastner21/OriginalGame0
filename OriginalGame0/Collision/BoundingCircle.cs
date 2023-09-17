using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace OriginalGame0.Collision
{
    /// <summary>
    /// Struct representing circular bounds
    /// </summary>
    public struct BoundingCircle
    {
        /// <summary>
        /// center of cicrles bounds
        /// </summary>
        public Vector2 Center;

        /// <summary>
        /// Radius of bounding circle
        /// </summary>
        public float Radius;

        /// <summary>
        /// Constructs a new bounding circle
        /// </summary>
        /// <param name="center">center of circle</param>
        /// <param name="radius">radius of circle</param>
        public BoundingCircle(Vector2 center, float radius)
        {
            Center = center;
            Radius = radius;
        }
        /// <summary>
        /// Tests for collision between this and another bounding circle
        /// </summary>
        /// <param name="other">other bounding circle</param>
        /// <returns>true if collides</returns>
        public bool CollidesWith(BoundingCircle other)
        {
            return CollisionHelper.Collides(this, other);
        }

        public bool CollidesWith(BoundingRectangle other)
        {
            return CollisionHelper.Collides(this, other);
        }
    }
}
