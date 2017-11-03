using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Media;

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
        Player player;



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

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
            player = new Player(1);
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.


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


            player.Update();

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            
            spriteBatch.Begin();
            player.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }




    }
}
