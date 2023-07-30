using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Net;

namespace MapEditor
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        Editor editor;
        Sprite SquarePath;
        Sprite Start;
        Sprite End;
        Sprite[] Grass;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            editor = new Editor();
            int count = 0;
            Texture2D grass = Content.Load<Texture2D>("Grass");
            int x = GraphicsDevice.Viewport.Height /grass.Width;
            int y = GraphicsDevice.Viewport.Height / grass.Height;
            Grass = new Sprite[x*y];
            for (int i = 0;i < y;i++)
            {
                for (int z = 0; z < x; z++)
                {
                    Grass[count] = new Sprite(Color.White,new Vector2(grass.Width*z,grass.Height*i),grass,0,Vector2.Zero,Vector2.One);
                    count++;
                }
            }
            Start = new Sprite(Color.White, new Vector2(700,0),Content.Load<Texture2D>("Start"),0,Vector2.Zero,Vector2.One);
            End = new Sprite(Color.White, new Vector2(650, 0), Content.Load<Texture2D>("End"), 0, Vector2.Zero, Vector2.One);
            SquarePath = new Sprite(Color.White, new Vector2(650, 450), Content.Load<Texture2D>("Path"), 0, Vector2.Zero,Vector2.One);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            editor.Update(SquarePath,GraphicsDevice,Grass);
            editor.Update(Start, GraphicsDevice, Grass);
            editor.Update(End, GraphicsDevice, Grass);
            SquarePath.Position = new Vector2(650,450);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(); 
            SquarePath.Draw(spriteBatch);
            Start.Draw(spriteBatch);
            End.Draw(spriteBatch);
            foreach (var item in Grass)
            {
                item.Draw(spriteBatch);
            }
            editor.Draw(spriteBatch);
            // TODO: Add your drawing code here
            
            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}