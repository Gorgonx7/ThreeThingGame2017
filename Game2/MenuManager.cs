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
    public class MenuManager
    {
        Menu mMenu;
        public MenuManager()
        {
            mMenu = new Menu();
            mMenu.OnMenuChange += mMenu_OnMenuChange;
        }
        private void mMenu_OnMenuChange(object sender, EventArgs e)
        {
            XmlManager<Menu> xmlMenuManager = new XmlManager<Menu>();
            mMenu.UnloadContent();
            mMenu = xmlMenuManager.Load(mMenu.ID);
        }

        public void LoadContent(string pMenuPath)
        {

        }

        public void LoadContent()
        {

        }

        public void UnloadContent()
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
