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

namespace OriginalGame0
{
    public class Arrow
    {
        private Texture2D arrowTexture;

        public bool enable = true;

        public Vector2 arrowPos;
        public Vector2 arrowVelocity;
        public float arrowSpeed = 8.0f;
        public float arrowRotation;
        private BoundingBox arrowBounds;

        public Arrow(Vector2 pos, Vector2 velocity, float rotation)
        {
            arrowPos = pos;
            arrowVelocity = velocity;
            arrowRotation = rotation;
        }

        /// <summary>
        /// loads the sprites
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            arrowTexture = content.Load<Texture2D>("HeavyArrow");
            arrowPos = new Vector2(-20, -20);
        }

        /// <summary>
        /// Update the sprite direction and movement
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {

            arrowPos += arrowVelocity;

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
                spriteBatch.Draw(arrowTexture, arrowPos, null, Color.White, arrowRotation, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
            }

        }
    }
}
