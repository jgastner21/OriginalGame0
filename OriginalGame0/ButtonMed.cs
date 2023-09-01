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

namespace OriginalGame0
{
    /// <summary>
    /// enum representing button state
    /// </summary>
    public enum Pressed
    {
        Off = 0,
        Hover = 1,
        Clicked = 2
    }

    /// <summary>
    /// Represents the medium sized button
    /// </summary>
    public class ButtonMed
    {
        private Texture2D texture;

        /// <summary>
        /// state of the button
        /// </summary>
        public Pressed pressed = Pressed.Off;

        /// <summary>
        /// position of button
        /// </summary>
        public Vector2 Position;

        private Rectangle source = new Rectangle(16, 96, 64, 32);

        /// <summary>
        /// loads the button sprite
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("UserInterfaceButtons");
        }

        /// <summary>
        /// Update the sprite direction and movement
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {

            switch (pressed)
            {
                case Pressed.Off:
                    source.X = 16;
                    source.Y = 96;
                    source.Height = 32;
                    Position.Y = 250;
                    break;
                case Pressed.Hover:
                    source.X = 112;
                    source.Y = 130;
                    source.Height = 32;
                    Position.Y = 255;
                    break;
                case Pressed.Clicked:
                    source.X = 16;
                    source.Y = 164;
                    source.Height = 28;
                    Position.Y = 259;
                    break;
            }
        }

        /// <summary>
        /// Draw the button
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {;
            spriteBatch.Draw(texture, Position, source, Color.White, 0, new Vector2(0, 0), 2f, SpriteEffects.None, 0);
        }
    }
}
