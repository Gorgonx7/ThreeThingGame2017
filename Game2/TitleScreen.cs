using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Media;

namespace Game2
{
    class TitleScreen
    {
        public class MainMenu : Game1
        {
            SpriteFont mTitle, mFooter;
            SpriteFont[] mMenuItems;

            protected override void LoadContent()
            {

            }

            protected override void UnloadContent()
            {

            }
            protected override void Update(GameTime gameTime)
            {
                base.Update(gameTime);
            }

            protected override void Draw(GameTime gameTime)
            {
                base.Draw(gameTime);
            }
        }
    }
}

