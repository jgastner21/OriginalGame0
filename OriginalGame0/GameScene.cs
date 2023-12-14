using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Xna.Framework.Media;

namespace OriginalGame0
{
    /// <summary>
    /// Actual game scene
    /// </summary>
    public class GameScene
    {

        private Texture2D tinyTown;
        private Texture2D dungeonAtlas;
        private Ranger ranger = new Ranger() {Position = new Vector2(64, 280) };
        private Texture2D Arrow;




        private PauseMenu pauseMenu;

        /// <summary>
        /// loads the scene
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager Content)
        {
            tinyTown = Content.Load<Texture2D>("town-packed");
            dungeonAtlas = Content.Load<Texture2D>("Dungeon");
            ranger.LoadContent(Content);
            Arrow = Content.Load<Texture2D>("HeavyArrow");


        }

        /// <summary>
        /// Update the scene
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime, MouseState currentMouseState, Game game)
        {
            ranger.Update(gameTime, currentMouseState);
        }

        /// <summary>
        /// Draw the scene
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDevice game)
        {
            for (int i = 0; i < 6; i++)
            {
                for(int j = 1; j < 5; j++)
                {
                    spriteBatch.Draw(dungeonAtlas, new Vector2(134 * i, (j * 94) - 19), new Rectangle(1, 8, 126, 102), Color.White, 0, new Vector2(0, 0), 1.2f, SpriteEffects.None, 0);
                }
                spriteBatch.Draw(dungeonAtlas, new Vector2(134 * i, -8), new Rectangle(0, 377, 64, 56), Color.White, 0, new Vector2(0, 0), 2.1f, SpriteEffects.None, 0);
                ranger.Draw(gameTime, spriteBatch);

            }

        }
    }
}
