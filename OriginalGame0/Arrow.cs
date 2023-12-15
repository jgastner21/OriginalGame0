using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Diagnostics;
using Microsoft.Xna.Framework.Audio;
using System.IO;
using OriginalGame0.Collision;

namespace OriginalGame0
{
    public class Arrow
    {

        public bool enable = true;

        public Vector2 arrowPos;
        public Vector2 arrowVelocity;
        public float arrowRotation;
        public BoundingCircle arrowBounds;

        public Arrow(Vector2 pos, Vector2 velocity, float rotation)
        {
            arrowPos = pos;
            arrowVelocity = velocity;
            arrowRotation = rotation;
            arrowBounds = new BoundingCircle(arrowPos, 1);
        }


        /// <summary>
        /// Update the sprite direction and movement
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            if (!enable) return;
            if (arrowPos.X >= 1000) enable = false;
            arrowPos += arrowVelocity;
            arrowRotation = (float)Math.Atan2(arrowVelocity.Y, arrowVelocity.X);
            arrowBounds.Center.X = arrowPos.X;
            arrowBounds.Center.Y = arrowPos.Y;

        }

        /// <summary>
        /// Draw the sprites
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D arrowTexture)
        {
            if (enable)
            {
                spriteBatch.Draw(arrowTexture, arrowPos, new Rectangle(7, 4, 20, 8), Color.White, arrowRotation + (float)Math.PI, new Vector2(4, 4), 1.0f, SpriteEffects.None, 0);
            }

        }
    }
}
