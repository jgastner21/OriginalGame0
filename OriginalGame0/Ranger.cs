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
        /// <summary>
        /// position of Ranger
        /// </summary>
        public Vector2 Position;

        private Texture2D ranger;
        private Texture2D arrow;

        private Slime slimeSprite = new Slime(new Vector2(600, 280));

        private double directionTimer;
        private double animationTimer;
        private short animationFrame = 1;
        private int action = 0;
        private Rectangle source = new Rectangle(12, 16, 32, 32);

        private Vector2 mousePos;
        private Vector2 direction;
        private float rotation;
        private MouseState previousMouseState;

        private Vector2 arrowPos;
        private Vector2 arrowVelocity;
        private const float arrowSpeed = 8.0f;
        private float arrowRotation;
        private bool drawn;
        private BoundingBox arrowBounds;

        private SoundEffect shootsound;
        private SoundEffect Death;

        

        /// <summary>
        /// loads the bat sprite
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            ranger = content.Load<Texture2D>("RangerBase");
            arrow = content.Load<Texture2D>("HeavyArrow");
            arrowPos = new Vector2(-20,-20);
            slimeSprite.LoadContent(content);
            shootsound = content.Load<SoundEffect>("arrowRelease");
            Death = content.Load<SoundEffect>("77_flesh_02");

        }

        /// <summary>
        /// Update the sprite direction and movement
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime, MouseState currentMouseState)
        {
            mousePos = new Vector2(currentMouseState.X, currentMouseState.Y);
            direction = mousePos - Position;
            rotation = (float)Math.Atan2(direction.Y, direction.X);

            slimeSprite.Update(gameTime);

            if (currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.X > 64)
            {
                action = 1;
                if(previousMouseState.LeftButton == ButtonState.Released)
                {
                    animationFrame = 1;
                }
            }
            else
            {
                action = 0;
            }
            if(drawn == true)
            {
                if(currentMouseState.LeftButton == ButtonState.Released)
                {
                    
                    arrowPos = Position - new Vector2(0, 8);
                    drawn = false;
                    float arrowX = (float)Math.Cos(rotation);
                    float arrowY = (float)Math.Sin(rotation);
                    arrowRotation = rotation;
                    arrowVelocity = new Vector2(arrowX, arrowY) * arrowSpeed;
                    shootsound.Play();
                }
            }

            arrowPos += arrowVelocity;
            //Debug.WriteLine("X: "+arrowPos.X + " Y: " + arrowPos.Y);

            if(arrowPos.X >= 600 && arrowPos.X <= 648 && arrowPos.Y <= 310 && arrowPos.Y >= 240)
            {
                arrowPos.X = 900;
                slimeSprite.Killed = true;
                Death.Play();
            }


            previousMouseState = currentMouseState;
        }

        /// <summary>
        /// Draw the bat
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if(action == 0)
            {
                if (animationTimer > 0.8)
                {
                    animationFrame++;
                    if (animationFrame > 6) animationFrame = 1;
                    animationTimer -= 0.8;
                }

            source = new Rectangle(animationFrame * 48 + 12, 144 * action + 16, 32, 32);
            }
            else
            {
                if (animationTimer > 1.2)
                {

                    if(drawn == false && animationFrame >= 6)
                    {
                        animationFrame++;
                    }
                    if(animationFrame < 5) animationFrame++;
                    if (animationFrame >= 5)
                    {
                        drawn = true;
                    }
                    animationTimer -= 1.2;
                }
                source = new Rectangle(animationFrame * 48 + 12, 144 * action + 16, 32, 32);
            }

            spriteBatch.Draw(ranger, Position, source, Color.White, rotation, new Vector2(8,16), 2.0f, SpriteEffects.None, 0);
            spriteBatch.Draw(arrow, arrowPos, null, Color.White, arrowRotation, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
            slimeSprite.Draw(gameTime, spriteBatch);
        }
    }
}
