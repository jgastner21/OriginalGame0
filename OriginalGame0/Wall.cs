using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using OriginalGame0.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalGame0
{
    public class Wall
    {

        public bool enable = true;

        public Vector2 wallPosition;
        public float wallRotation;
        public BoundingRectangle wallBounds;

        public BoundingRectangle Bounds => wallBounds;

        public Wall(Vector2 pos, float rotation)
        {
            wallPosition = pos;

            wallRotation = rotation;
            wallBounds = new BoundingRectangle(new Vector2(wallPosition.X, wallPosition.Y), 39, 39);
        }

        /// <summary>
        /// Update the sprite direction and movement
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {

        }

        /// <summary>
        /// Draw the sprites
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D wallTexture)
        {
            if (enable)
            {
                spriteBatch.Draw(wallTexture, new Vector2(wallPosition.X, wallPosition.Y), new Rectangle(6, 83, 32, 42), Color.White, wallRotation, Vector2.Zero, 1.21f, SpriteEffects.None, 0);
                spriteBatch.Draw(wallTexture, new Vector2(wallPosition.X, wallPosition.Y), new Rectangle(63, 377, 33, 7), Color.White, wallRotation, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                spriteBatch.Draw(wallTexture, new Vector2(wallPosition.X, wallPosition.Y), new Rectangle(96, 384, 7, 32), Color.White, wallRotation, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                spriteBatch.Draw(wallTexture, new Vector2(wallPosition.X, wallPosition.Y + 32), new Rectangle(63, 409, 40, 7), Color.White, wallRotation, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                spriteBatch.Draw(wallTexture, new Vector2(wallPosition.X + 32, wallPosition.Y), new Rectangle(96, 384, 7, 32), Color.White, wallRotation, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
            }

        }
    }
}
