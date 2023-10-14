using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalGame0
{
    public class PauseMenu
    {
        private ButtonMed exitButton = new ButtonMed() { Position = new Vector2(464, 220), Text = "Exit" };
        private ButtonMed playButton = new ButtonMed() { Position = new Vector2(208, 220), Text = "Play" };
        private Texture2D dungeonAtlas;

        private SoundEffect Press;

        /// <summary>
        /// loads the scene
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager Content)
        {
            
            exitButton.LoadContent(Content);
            playButton.LoadContent(Content);

            Press = Content.Load<SoundEffect>("070_Equip_10");
        }

        /// <summary>
        /// Update the scene
        /// </summary>
        /// <param name="gameTime"></param>
        public Scene Update(GameTime gameTime, MouseState currentMouseState, Game game)
        {

            if (exitButton.Update(gameTime, currentMouseState))
            {
                Press.Play();
                game.Exit();
            }
            if (playButton.Update(gameTime, currentMouseState))
            {
                Press.Play();
                return Scene.Game;
            }
            return Scene.Main;
        }

        /// <summary>
        /// Draw the scene
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDevice game)
        {
            exitButton.Draw(gameTime, spriteBatch);
            playButton.Draw(gameTime, spriteBatch);

        }
    }
}
