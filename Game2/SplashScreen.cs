using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Game2
{
    public class SplashScreen : GameScreen
    {
        Texture2D mImage;
        public string mPath;

        public override void LoadContent()
        {
            base.LoadContent();
            mPath = "3tgSplash";
            mImage = mContent.Load<Texture2D>(mPath);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch pSpritebatch)
        {
            //base.Draw(pSpritebatch);
            pSpritebatch.Begin();
            pSpritebatch.Draw(mImage, Vector2.Zero, Color.White);
            pSpritebatch.End();
        }
    }
}
