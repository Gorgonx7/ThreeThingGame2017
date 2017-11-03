using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Game2
{
    public class ScreenManager
    {
        private static ScreenManager mInstance;
        public Vector2 mDimensions { private set; get; }
        public ContentManager Content { private set; get; }
        public GameScreen mCurrentSreen;

        public ScreenManager()
        {
            mDimensions = new Vector2(1200, 800);
            mCurrentSreen = new SplashScreen();
        }
        public static ScreenManager Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new ScreenManager();
                }
                return mInstance;
            }
        }

        public void LoadContent(ContentManager Content)
        {
            this.Content = new ContentManager(Content.ServiceProvider, "Content");
            mCurrentSreen.LoadContent();
        }

        public void UnloadContent()
        {
            mCurrentSreen.UnloadContent();

        }

        public void Update(GameTime gameTime)
        {
            mCurrentSreen.Update(gameTime);

        }

        public void Draw(SpriteBatch pSpriteBatch)
        {
            mCurrentSreen.Draw(pSpriteBatch);

        }
    }
}
