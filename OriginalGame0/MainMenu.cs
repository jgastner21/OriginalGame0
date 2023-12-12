using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace OriginalGame0
{
    /// <summary>
    /// Main menu scene
    /// </summary>
    public class MainMenu
    {
        private SpriteFont alkhemikalTitle;
        private ButtonMed exitButton = new ButtonMed() { Position = new Vector2(464, 220), Text = "Exit"};
        private ButtonMed playButton = new ButtonMed() { Position = new Vector2(208, 220), Text = "Play"};
        private Texture2D background;
        private Texture2D dungeonAtlas;

        private Song BGM;
        private Song battleMusic;

        private SoundEffect Press;

        /// <summary>
        /// loads the scene
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager Content)
        {
            background = Content.Load<Texture2D>("backgroundColorForest");//y 512
            dungeonAtlas = Content.Load<Texture2D>("Dungeon");
            exitButton.LoadContent(Content);
            playButton.LoadContent(Content);

            alkhemikalTitle = Content.Load<SpriteFont>("AlkhemikalTitle");

            Press = Content.Load<SoundEffect>("070_Equip_10");

            battleMusic = Content.Load<Song>("BattleLoop");
            BGM = Content.Load<Song>("Long Road Ahead");
            MediaPlayer.Volume = 0.3f;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(BGM);
        }

        /// <summary>
        /// Update the scene
        /// </summary>
        /// <param name="gameTime"></param>
        public Scene Update(GameTime gameTime, MouseState currentMouseState, Game game)
        {

            if(exitButton.Update(gameTime, currentMouseState)){
                Press.Play();
                game.Exit();
            }
            if (playButton.Update(gameTime, currentMouseState))
            {
                Press.Play();
                MediaPlayer.Play(battleMusic);
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
            spriteBatch.Draw(background, new Vector2(0, -200), null, Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);//Nature background
            spriteBatch.Draw(background, new Vector2(512, -200), null, Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
            for (int i = 0; i < 7; i++) //Draw repeating background components
            {
                spriteBatch.Draw(dungeonAtlas, new Vector2(134 * i, 230), new Rectangle(0, 0, 128, 116), Color.White, 0, new Vector2(0, 0), 1.05f, SpriteEffects.None, 0);
                spriteBatch.Draw(dungeonAtlas, new Vector2(128 * i, 315), new Rectangle(20, 282, 64, 60), Color.White, 0, new Vector2(0, 0), 2f, SpriteEffects.None, 0);
                spriteBatch.Draw(dungeonAtlas, new Vector2(128 * i, 410), new Rectangle(20, 300, 64, 60), Color.White, 0, new Vector2(0, 0), 2f, SpriteEffects.None, 0);
            }
            spriteBatch.Draw(dungeonAtlas, new Vector2(game.Viewport.Width / 2 - 54, 340), new Rectangle(78, 456, 36, 60), Color.White, 0, new Vector2(0, 0), 3f, SpriteEffects.None, 0);
            exitButton.Draw(gameTime, spriteBatch);
            playButton.Draw(gameTime, spriteBatch);

            spriteBatch.DrawString(alkhemikalTitle, "CORRUPT", new Vector2(300, 0), Color.Red, 1, new Vector2(0,0), 1.0f, SpriteEffects.None, 0);//Text
        }
    }
}
