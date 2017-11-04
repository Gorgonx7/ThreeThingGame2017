using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
namespace Game2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    /// 
    /* Stuff todo
     * sort out ship interior
     * figure out enemy ai UPDATE i have now learnt how to program logic in virtual inteligence
     * while I'm at it figure out friendly ai UPDATE pathfinding using nodes and the golden algorithm
     * 
     * Research network programming in XNA UPDATE ha nahhhhh
     * get enemys up and running not
     * 
     */
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SceneManager manager;
        
        List<Enemy> Enemys;
        SpriteFont menuFont;
        Texture2D mainMenu, pauseMenu, deathScreen, background, oldStinker;
        // SoundEffect ;
        // Song ;




        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 1200;




        }


        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            TextureDictionary.AddTexture(Content.Load<Texture2D>("PlayerWalk"), "playerWalk");
            TextureDictionary.AddTexture(Content.Load<Texture2D>("idol"), "playerIdol");
            TextureDictionary.AddTexture(Content.Load<Texture2D>("BJLibBackground"), "Background");
            TextureDictionary.AddTexture(Content.Load<Texture2D>("playerJab"), "playerJab");
            TextureDictionary.AddTexture(Content.Load<Texture2D>("ninjaIdol"), "ninjaIdol");
            TextureDictionary.AddTexture(Content.Load<Texture2D>("ninjaWalk"), "ninjaWalk");
            TextureDictionary.AddTexture(Content.Load<Texture2D>("redBar"), "redBar");
            TextureDictionary.AddTexture(Content.Load<Texture2D>("orangeBar"), "orangeBar");
            TextureDictionary.AddTexture(Content.Load<Texture2D>("greenBar"), "greenBar");
            AudioDictionary.AddAudio(Content.Load<SoundEffect>("Punch"), "Punch");
            Random RNG = new Random();


            List<Enemy>[] totem = new List<Enemy>[5];
            for (int y = 0; y < 5; y++) {
                List<Enemy> EnemyTestList = new List<Enemy>();
                for (int x = 0; x < 20; x++)
                {
                    EnemyTestList.Add(new Enemy(new Vector2(1300 + RNG.Next(0,100), 400 + 50 * x)));
                }
                totem[y] = EnemyTestList;
            }
            List<Texture2D> Backgrounds = new List<Texture2D>();
            Backgrounds.Add(TextureDictionary.FindTexture("Background"));
            manager = new SceneManager(totem, Backgrounds);
            

       
            //Content.Load<Texture2D>("assetname");

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.



            spriteBatch = new SpriteBatch(GraphicsDevice);
          


        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here


           

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {





            manager.Update();




            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {

            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            //spriteBatch.Draw(TextureDictionary.FindTexture("Background"), new Rectangle(0, 0, 1600, 800), new Rectangle(0, 0, 1600, 800), Color.White);
            manager.Draw(spriteBatch);
            spriteBatch.End();

            
           

            base.Draw(gameTime);
        }



    }
}
