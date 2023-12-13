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
    public class Ranger
    {
        /// <summary>
        /// position of Ranger
        /// </summary>
        public Vector2 Position;

        Random random = new Random();

        private Texture2D ranger;
        private Texture2D wallTexture;

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

        private List<Arrow> arrowList = new List<Arrow>();
        private List<Wall> baseWalls = new List<Wall>();
        private List<Wall> lvl1Walls = new List<Wall>();
        private List<Wall> lvl2Walls = new List<Wall>();
        private List<Wall> lvl3Walls = new List<Wall>();


        private Texture2D arrowTexture;
        private Vector2 arrowPos;
        private Vector2 arrowVelocity;
        private const float arrowSpeed = 8.0f;
        private float arrowRotation;
        private BoundingBox arrowBounds;

        private bool drawn;

        private SoundEffect shootsound;
        private SoundEffect Death;

        private int level = 1;
        


        /// <summary>
        /// loads the sprites
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            ranger = content.Load<Texture2D>("RangerBase");
            arrowTexture = content.Load<Texture2D>("HeavyArrow");
            slimeSprite.LoadContent(content);
            wallTexture = content.Load<Texture2D>("Dungeon");
            shootsound = content.Load<SoundEffect>("arrowRelease");
            Death = content.Load<SoundEffect>("77_flesh_02");

            #region level walls

            //Side walls
            for (int i = -1; i < 24; i++)
            {
                baseWalls.Add(new Wall(new Vector2(i * 38, 0), 0));
                baseWalls.Add(new Wall(new Vector2(i * 38, 440), 0));
            }

            //Level 1 walls
            for (int i = 0; i < 3; i++)
            {
                lvl1Walls.Add(new Wall(new Vector2(228, 38 * i + 38), 0));
                lvl1Walls.Add(new Wall(new Vector2(492, 297 + i * 38), 0));
            }

            #endregion level walls
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

            Debug.WriteLine(rotation);

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

                    arrowList.Add(new Arrow(arrowPos, arrowVelocity, arrowRotation));

                    shootsound.Play();
                }
            }

            //arrowPos += arrowVelocity;
            //Debug.WriteLine("X: "+arrowPos.X + " Y: " + arrowPos.Y);


            //I used a for loop to be able to manipulate the List while iterating through it
            //and decided not to swap back to a foreach loop.
            for(int i = 0; i < arrowList.Count(); i++)
            {
                arrowList[i].Update(gameTime);

                if(!slimeSprite.Killed && slimeSprite.Bounds.CollidesWith(arrowList[i].arrowBounds))
                {
                    arrowList[i].enable = false;
                    slimeSprite.Killed = true;
                    Death.Play();
                }
                if (level == 1 || level == 3)
                {
                    foreach (Wall wall in baseWalls)
                    {
                        if (wall.wallBounds.CollidesWith(arrowList[i].arrowBounds))
                        {
                            float angle = (float)Math.Atan2(wall.wallPosition.Y + 20 - arrowList[i].arrowPos.Y, wall.wallPosition.X + 20 - arrowList[i].arrowPos.X);
                            //Debug.WriteLine(angle);
                            if (angle > 0.7853f && angle < 2.3561)
                            {
                                arrowList[i].arrowVelocity.Y *= -1;
                            }
                            else if (angle >= 2.3561 || angle <= -2.3561)
                            {
                                arrowList[i].arrowVelocity.X *= -1;
                            }
                            else if (angle <= 0.7853f && angle >= -0.7853)
                            {
                                arrowList[i].arrowVelocity.X *= -1;
                            }
                            else if (angle < -0.7853 && angle > -2.3561)
                            {
                                arrowList[i].arrowVelocity.Y *= -1;
                            }
                        }
                    }
                }
                if (level == 1)
                {
                    foreach (Wall wall in lvl1Walls)
                    {
                        if (wall.wallBounds.CollidesWith(arrowList[i].arrowBounds))
                        {
                            float angle = (float)Math.Atan2(wall.wallPosition.Y + 20 - arrowList[i].arrowPos.Y, wall.wallPosition.X + 20 - arrowList[i].arrowPos.X);
                            //Debug.WriteLine(angle);
                            if (angle > 0.7853f && angle < 2.3561)
                            {
                                arrowList[i].arrowVelocity.Y *= -1;
                            }
                            else if (angle >= 2.3561 || angle <= -2.3561)
                            {
                                arrowList[i].arrowVelocity.X *= -1;
                            }
                            else if (angle <= 0.7853f && angle >= -0.7853)
                            {
                                arrowList[i].arrowVelocity.X *= -1;
                            }
                            else if (angle < -0.7853 && angle > -2.3561)
                            {
                                arrowList[i].arrowVelocity.Y *= -1;
                            }
                        }
                    }
                }

            }


            previousMouseState = currentMouseState;
        }

        /// <summary>
        /// Draw the sprites
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
            foreach (Arrow arrow in arrowList)
            {
                arrow.Draw(gameTime, spriteBatch, arrowTexture);
            }
            if (level == 1 || level == 3)
            {
                foreach (Wall wall in baseWalls)
                {
                    wall.Draw(gameTime, spriteBatch, wallTexture);
                }
            }
            if (level == 1)
            {
                foreach(Wall wall in lvl1Walls)
                {
                    wall.Draw(gameTime, spriteBatch, wallTexture);
                }
            }
            else if (level == 2)
            {
                foreach (Wall wall in lvl2Walls)
                {
                    wall.Draw(gameTime, spriteBatch, wallTexture);
                }
            }
            else if (level == 3)
            {
                foreach (Wall wall in lvl3Walls)
                {
                    wall.Draw(gameTime, spriteBatch, wallTexture);
                }
            }

            //spriteBatch.Draw(arrow, arrowPos, null, Color.White, arrowRotation, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
            slimeSprite.Draw(gameTime, spriteBatch);

        }
    }
}
