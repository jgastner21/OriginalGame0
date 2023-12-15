using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using OriginalGame0.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using System.IO;

namespace OriginalGame0
{
    /// <summary>
    /// Class representing the current simple "enemy"
    /// </summary>
    public class Slime
    {
        private const float ANIMATION_SPEED = 0.8f;

        private double animationTimer;

        private int animationFrame = 1;

        private Vector2 position;

        private Texture2D texture;

        private BoundingCircle bounds;

        public int animated = 0;
        private bool started = false;

        float currentAlpha = 1.0f;
        float fadeSpeed = 0.02f;
        bool isFading = false;


        /// <summary>
        /// Tells if the slime has been shot
        /// </summary>
        public bool Killed { get; set; } = false;

        /// <summary>
        /// bounding volume of the sprite
        /// </summary>
        public BoundingCircle Bounds => bounds;

        /// <summary>
        /// Creates a new slime sprite
        /// </summary>
        /// <param name="position">The position of the sprite in the game</param>
        public Slime(Vector2 position)
        {
            this.position = position;
            this.bounds = new BoundingCircle(position + new Vector2(18, 24), 24);
        }

        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("slime");
        }

        public void Update(GameTime gameTime)
        {

            if (isFading)
            {
                currentAlpha -= fadeSpeed;

                if (currentAlpha <= 0)
                {
                    currentAlpha = 0;
                    isFading = false;
                }
            }
        }

            /// <summary>
            /// Draws the animated sprite using the supplied SpriteBatch
            /// </summary>
            /// <param name="gameTime">The game time</param>
            /// <param name="spriteBatch">The spritebatch to render with</param>
            public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (animationTimer > ANIMATION_SPEED)
            {
                animationFrame++;
                if (animationFrame > 3) animationFrame = 0;
                animationTimer -= ANIMATION_SPEED;
            }

            if (Killed)
            {
                if (!started) animationFrame = 1;
                
                started = true;
                var death = new Rectangle(animationFrame * 32 + 8, 132, 16, 16);
                if (animated <=108)
                {
                    spriteBatch.Draw(texture, position, death, Color.Red, 0, Vector2.Zero, 3f, SpriteEffects.FlipHorizontally, 0);
                    animated++;
                }
                else
                {
                    death = new Rectangle(4 * 32 + 8, 132, 16, 16);
                    isFading = true;
                    spriteBatch.Draw(texture, position, death, Color.Red * currentAlpha, 0, Vector2.Zero, 3f, SpriteEffects.FlipHorizontally, 0);
                }
                return;
            }

            var source = new Rectangle(animationFrame * 32 + 8, 12, 16, 16);
            spriteBatch.Draw(texture, position, source, Color.White, 0, Vector2.Zero, 3f, SpriteEffects.FlipHorizontally, 0);
        }
    }
}
