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
    class Menu
    {
        public event EventHandler OnMenuChange;

        public string mAxis, mEffects;
        [XmlElement("Item")]
        public List<MenuItem> mItems;
        int mItemNumber;
        string mID;

        public Menu()
        {
            mID = String.Empty;
            mItemNumber = 0;
            mEffects = String.Empty;
            mAxis = "Y";
        }

        private void AlignMenuItems()
        {

        }

        public string ID
        {
            get { return mID; }
            set
            {
                mID = value;
                OnMenuChange(this, null);
            }
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
