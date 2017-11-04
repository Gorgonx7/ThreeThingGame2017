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
        Song tune;
        List<Enemy> Enemys;
        SpriteFont menuFont;
        Texture2D mainMenu, pauseMenu, deathScreen, background, oldStinker;
        public static bool MainMenu = false;
        bool enterKeyPressed = false;
        bool scene1 = false;
        bool intro2 = false;
        bool ISaySo = false;
        Texture2D mHolder;
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
            TextureDictionary.AddTexture(Content.Load<Texture2D>("ninjaJab"), "ninjaJab");
            TextureDictionary.AddTexture(Content.Load<Texture2D>("dead"), "dead");
            TextureDictionary.AddTexture(Content.Load<Texture2D>("werewolfClaw"), "Claw");
            TextureDictionary.AddTexture(Content.Load<Texture2D>("werewolfidol"), "wolfIdol");
            TextureDictionary.AddTexture(Content.Load<Texture2D>("werewolfTransform"), "Transform");
            TextureDictionary.AddTexture(Content.Load<Texture2D>("werewolfWalk"), "wolfWalk");
            TextureDictionary.AddTexture(Content.Load<Texture2D>("bg1"), "bg1");
            TextureDictionary.AddTexture(Content.Load<Texture2D>("bg2"), "bg2");
            TextureDictionary.AddTexture(Content.Load<Texture2D>("bg3"), "bg3");
            TextureDictionary.AddTexture(Content.Load<Texture2D>("DeathScreen"), "DS");
            TextureDictionary.AddTexture(Content.Load<Texture2D>("menuScreen"), "MainMenu");
            TextureDictionary.AddTexture(Content.Load<Texture2D>("Intro1"), "In1");
            TextureDictionary.AddTexture(Content.Load<Texture2D>("Intro2"), "In2");
            TextureDictionary.AddTexture(Content.Load<Texture2D>("tutorial"), "Tutorial");
            AudioDictionary.AddAudio(Content.Load<SoundEffect>("Punch"), "Punch");
            TextureDictionary.AddTexture(Content.Load<Texture2D>("redNinjaIdol"), "redNinjaIdol");
            TextureDictionary.AddTexture(Content.Load<Texture2D>("redNinjaWalk"), "redNinjaWalk");
            TextureDictionary.AddTexture(Content.Load<Texture2D>("ninjaJabRed"), "ninjaJabRed");
            mHolder = TextureDictionary.FindTexture("In1");
            tune = Content.Load<Song>("ChipBeat");

           
            

       
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




            if (!MainMenu && intro2 & ISaySo)
            {
                manager.Update();
            }
            else
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && !intro2 && !MainMenu)
                {
                    enterKeyPressed = true;
                }
                if (Keyboard.GetState().IsKeyUp(Keys.Enter) && !intro2 && !MainMenu && enterKeyPressed)
                {
                    enterKeyPressed = false;
                    intro2 = true;
                    mHolder = TextureDictionary.FindTexture( "In2");
                }
                if(Keyboard.GetState().IsKeyDown(Keys.Enter) && intro2 & !MainMenu)
                {
                    enterKeyPressed = true;
                }
                if(Keyboard.GetState().IsKeyUp(Keys.Enter) && intro2 && !MainMenu && enterKeyPressed)
                {
                    MainMenu = true;
                    ISaySo = true;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && MainMenu && ISaySo)
                {
                    MainMenu = false;
                    
                    Random RNG = new Random();
                    List<Enemy>[] totem = new List<Enemy>[5];
                    for (int y = 0; y < 5; y++)
                    {
                        List<Enemy> EnemyTestList = new List<Enemy>();
                        for (int x = 0; x < 20; x++)
                        {
                            EnemyTestList.Add(new Enemy(new Vector2(1200, 400 + 50 * x)));
                        }
                        totem[y] = EnemyTestList;
                    }
                    List<Texture2D> Backgrounds = new List<Texture2D>();
                    //Backgrounds.Add(TextureDictionary.FindTexture("Background"));
                    Backgrounds.Add(TextureDictionary.FindTexture("bg3"));
                    Backgrounds.Add(TextureDictionary.FindTexture("bg1"));
                    Backgrounds.Add(TextureDictionary.FindTexture("bg2"));
                    manager = new SceneManager(totem, Backgrounds);
                    
                }
                
            }




            base.Update(gameTime);
        }
        public void startMusic()
        {
            MediaPlayer.Play(tune);
            MediaPlayer.IsRepeating = true;
        }
        protected override void Draw(GameTime gameTime)
        {
            if (!MainMenu && intro2 && ISaySo)
            {
                graphics.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Begin();
                //spriteBatch.Draw(TextureDictionary.FindTexture("Background"), new Rectangle(0, 0, 1600, 800), new Rectangle(0, 0, 1600, 800), Color.White);
                manager.Draw(spriteBatch);
                spriteBatch.End();
            }
            else if(MainMenu)
            {
                graphics.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Begin();
                spriteBatch.Draw(TextureDictionary.FindTexture("MainMenu"), new Rectangle(0, 0, 1200, 800), Color.White);
                spriteBatch.End();
            }
            else
            {
                spriteBatch.Begin();
                spriteBatch.Draw(mHolder, new Rectangle(0, 0, 1200, 800), Color.White);
                spriteBatch.End();
            }
            
           

            base.Draw(gameTime);
        }



    }
}
