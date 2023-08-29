using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame;
using MyGame.interfaces;

namespace GameLogic
{
    /// This is the main type for your game.  
    public class Game1 : Game
    {
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        IMyGraphic myGraphic;
        public MyGame.MyGame myGame;
        long PrevTime = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "ContentLink";

            // graphics.IsFullScreen = true;
            //graphics.HardwareModeSwitch = false;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
        }

        /// Allows the game to perform any initialization it needs to before starting  
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here  

            base.Initialize();
        }

        /// LoadContent will be called once per game and is the place to load  
        /// all of your content.  
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.  
            spriteBatch = new SpriteBatch(GraphicsDevice);

            myGraphic = new MyGraphic(Content, spriteBatch, GraphicsDevice);
            myGame = new MyGame.MyGame(myGraphic);

            // load game data
            myGame.OnInit();
        }

        /// UnloadContent will be called once per game and is the place to unload  
        /// game-specific content.  
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here  
            myGame.OnExit();
        }

        /// Allows the game to run logic such as updating the world,  
        /// checking for collisions, gathering input, and playing audio.  
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            // skip
            if (((long)gameTime.TotalGameTime.TotalMilliseconds - PrevTime) < myGame.TimeStepInMilliseconds)
                return;

            // set prev time
            PrevTime = (long)gameTime.TotalGameTime.TotalMilliseconds;

            // touch for Windows computer !!!!!!
            var mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                myGame.OnTouch(mouseState.X, mouseState.Y);
            }
            else
            {
                // touch for Android !!!!!!!!
                var touches = Microsoft.Xna.Framework.Input.Touch.TouchPanel.GetState();
                foreach (var touch in touches)
                {
                    if (touch.State == Microsoft.Xna.Framework.Input.Touch.TouchLocationState.Pressed)
                    {
                        myGame.OnTouch((int)touch.Position.X, (int)touch.Position.Y);
                    }
                }
            }

            // next
            myGame.OnNextTurn((long)gameTime.TotalGameTime.TotalMilliseconds);

            // base
            base.Update(gameTime);
        }

        /// This is called when the game should draw itself.  
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(MyGraphic.GetDefaultBgColor());

            // resize if changed width or height
            int screenWidth = graphics.GraphicsDevice.Viewport.Width;
            int screenHeight = graphics.GraphicsDevice.Viewport.Height;
            if (myGraphic.ScreenWidth != screenWidth || myGraphic.ScreenHeight != screenHeight)
            {
                myGame.OnChangeWindowSize(screenWidth, screenHeight);
            }

            spriteBatch.Begin();
            myGame.OnDraw();
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}