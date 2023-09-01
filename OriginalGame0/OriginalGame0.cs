using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OriginalGame0
{
    /// <summary>
    /// My first original "game" but I have an idea for the future
    /// </summary>
    public class OriginalGame0 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;

        //Separate fonts
        private SpriteFont alkhemikal;
        private SpriteFont alkhemikalTitle;

        private Texture2D background;
        private Texture2D dungeonAtlas;
        private ButtonMed uiButton;
        private Vector2 textPosition = new Vector2(370, 230);

        MouseState currentMouseState;


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
            uiButton = new ButtonMed() { Position = (new Vector2(336, 220)) };


            base.Initialize();
        }
        /// <summary>
        /// Load contnet
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            background = Content.Load<Texture2D>("backgroundColorForest");//y 512
            dungeonAtlas = Content.Load<Texture2D>("Dungeon");
            uiButton.LoadContent(Content);
            alkhemikal = Content.Load<SpriteFont>("Alkhemikal");
            alkhemikalTitle = Content.Load<SpriteFont>("AlkhemikalTitle");

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

            // TODO: Add your update logic here
            if(!(currentMouseState.X < 336 || currentMouseState.X > 336 + 128 ||
                currentMouseState.Y < 250 || currentMouseState.Y > 250 + 64 ))
            {
                if(currentMouseState.LeftButton == ButtonState.Released)
                {
                    uiButton.pressed = Pressed.Hover;
                    textPosition.Y = 262;
                }
                else
                {
                    uiButton.pressed = Pressed.Clicked;
                    textPosition.Y = 265;
                    Exit();
                }
            }
            else
            {
                uiButton.pressed = Pressed.Off;
                textPosition.Y = 258;
            }

            uiButton.Update(gameTime);

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
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Vector2(0, -200), null, Color.White, 0, new Vector2(0,0), 0.5f, SpriteEffects.None, 0);//Nature background
            spriteBatch.Draw(background, new Vector2(512, -200), null, Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
            for(int i = 0; i < 7; i++) //Draw repeating background components
            {
                spriteBatch.Draw(dungeonAtlas, new Vector2(134 * i, 230), new Rectangle(0, 0, 128, 116), Color.White, 0, new Vector2(0, 0), 1.05f, SpriteEffects.None, 0);
                spriteBatch.Draw(dungeonAtlas, new Vector2(128 * i, 315), new Rectangle(20, 282, 64, 60), Color.White, 0, new Vector2(0, 0), 2f, SpriteEffects.None, 0);
                spriteBatch.Draw(dungeonAtlas, new Vector2(128 * i, 410), new Rectangle(20, 300, 64, 60), Color.White, 0, new Vector2(0, 0), 2f, SpriteEffects.None, 0);
            }
            spriteBatch.Draw(dungeonAtlas, new Vector2(this.GraphicsDevice.Viewport.Width/2 - 54, 340), new Rectangle(78, 456, 36, 60), Color.White, 0, new Vector2(0, 0), 3f, SpriteEffects.None, 0);
            uiButton.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(alkhemikal, "Exit", textPosition, Color.Red);
            spriteBatch.DrawString(alkhemikalTitle, "RUNGEON", new Vector2(245, 80), Color.Red);//Text
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}