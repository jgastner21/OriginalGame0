using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OriginalGame0.Collision;
using Microsoft.Xna.Framework.Audio;

namespace OriginalGame0
{

    /// <summary>
    /// Represents the medium sized button
    /// </summary>
    public class ButtonMed
    {
        private Texture2D texture;

        /// <summary>
        /// position of button
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// text of button
        /// </summary>
        public String Text;

        private Rectangle source = new Rectangle(16, 96, 64, 32);
        
        private SpriteFont alkhemikal;

        private SoundEffect Hover;
        private bool hoverOn = false;

        /// <summary>
        /// adjusted text pos (will change)
        /// </summary>
        public Vector2 textPosition;

        /// <summary>
        /// loads the button sprite
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager Content)
        {
            alkhemikal = Content.Load<SpriteFont>("Alkhemikal");
            texture = Content.Load<Texture2D>("UserInterfaceButtons");
            Hover = Content.Load<SoundEffect>("001_Hover_01");
        }

        /// <summary>
        /// Update the sprite
        /// </summary>
        /// <param name="gameTime"></param>
        public bool Update(GameTime gameTime, MouseState currentMouseState)
        {
            textPosition.X = Position.X + 30;
            if (!(currentMouseState.X < Position.X || currentMouseState.X > Position.X + 128 ||
            currentMouseState.Y < Position.Y || currentMouseState.Y > Position.Y + 64))
            {
                if (currentMouseState.LeftButton == ButtonState.Released)
                {
                    //Hover
                    source.X = 112;
                    source.Y = 130;
                    source.Height = 32;
                    Position.Y = 255;
                    textPosition.Y = Position.Y + 7;
                    if (!hoverOn)
                    {
                        hoverOn = true;
                        Hover.Play();
                    }
                    return false;
                }
                else
                {
                    //Pressed
                    source.X = 16;
                    source.Y = 164;
                    source.Height = 28;
                    Position.Y = 259;
                    textPosition.Y = Position.Y + 6;
                    hoverOn = false;
                    return true;
                }
            }
            else
            {
                //Off
                source.X = 16;
                source.Y = 96;
                source.Height = 32;
                Position.Y = 250;
                textPosition.Y = Position.Y + 8;
                hoverOn = false;
                return false;
            }
        }

        /// <summary>
        /// Draw the button
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, source, Color.White, 0, new Vector2(0, 0), 2f, SpriteEffects.None, 0);
            spriteBatch.DrawString(alkhemikal, Text, textPosition, Color.Red);
        }
    }
}
