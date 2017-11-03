using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
namespace Game2
{
    class Player
    {
        PlayerController mControler;
        private Vector2 mPosition;
        private Vector2 mVelocity;
        private Texture2D mTexture;
        private Rectangle mRectange;
        private float mHealth = 100f;
        private int playerNumber;
        public Player(int pplayerNumber)
        {
            playerNumber = pplayerNumber;
            mTexture = TextureDictionary.FindTexture("player");
            mControler = new PlayerController(pplayerNumber);
        }
        public void setPosition(Vector2 pVector)
        {
            mPosition = pVector;
        }
        public void Draw(SpriteBatch pSpriteBatch)
        {
            pSpriteBatch.Draw(mTexture, mRectange, Color.White);
        }
        public void Update()
        {
            mPosition = mPosition * mVelocity * 1 / 60;
        }
    }
}
