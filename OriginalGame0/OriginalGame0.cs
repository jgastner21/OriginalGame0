using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;

namespace OriginalGame0
{

    /// <summary>
    /// enum representing scene
    /// </summary>
    public enum Scene
    {
        Main = 0,
        Game = 1
    }


    /// <summary>
    /// My first original "game" but I have an idea for the future
    /// </summary>
    public class OriginalGame0 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;

        private MainMenu mainMenu;
        private GameScene gameScene;

        protected BlendState blendState = BlendState.AlphaBlend;

        bool prev = true;

        /// <summary>
        /// Camera shake
        /// </summary>
        public Camera camera;

        MouseState currentMouseState;

        public Scene scene = Scene.Main;


        public OriginalGame0()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        /// <summary>
        /// Initialize components
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            mainMenu = new MainMenu();
            gameScene = new GameScene();

            camera = new Camera();

            base.Initialize();
        }
        /// <summary>
        /// Load contnet
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            mainMenu.LoadContent(Content);
            gameScene.LoadContent(Content);

        }
        /// <summary>
        /// Update the logic
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            currentMouseState = Mouse.GetState();

            camera.Update(gameTime);

            // TODO: Add your update logic here
            if(scene == Scene.Main)
            {
                scene = mainMenu.Update(gameTime, currentMouseState, this);
            }
            if(scene == Scene.Game)
            {
                gameScene.Update(gameTime, currentMouseState, this);
            }


            //Debug.WriteLine(currentMouseState.X+ " " + currentMouseState.Y);
            base.Update(gameTime);
        }

        /// <summary>
        /// Draw the components of the game
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here 800 x 480
            spriteBatch.Begin(transformMatrix: camera.Transform, blendState: blendState);
            if(scene == Scene.Main)
            {
                mainMenu.Draw(gameTime, spriteBatch, this.GraphicsDevice);
            }
            if(scene == Scene.Game)
            {
                if (prev == true) camera.ApplyShake(.2f, 1f);
                gameScene.Draw(gameTime, spriteBatch, this.GraphicsDevice);
                prev = false;
            }           

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}