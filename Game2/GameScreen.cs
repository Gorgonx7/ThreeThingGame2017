using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2
{
    public class GameScreen
    {
        protected ContentManager mContent;

        public virtual void LoadContent()
        {
            mContent = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
        }

        public virtual void UnloadContent()
        {
            mContent.Unload();
        }

        public virtual void Update(GameTime pGameTime)
        {

        }

        public virtual void Draw(SpriteBatch pSpritebatch)
        {

        }
    }
}
