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

namespace Game2.Helpers
{
    public class Menu
    {
        public event EventHandler OnMenuChange;

        public string mAxis, mEffects;
        [XmlElement("Item")]
        public List<MenuItem> mItems;

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
