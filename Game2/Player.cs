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
        private Texture2D mWalkTexture;
        private Rectangle mRectange;
        private float mHealth = 100f;
        private int playerNumber;
        private Animator mAnimator;

        //18 / 46
        //31, 63
        public Player(int pplayerNumber)
        {
            playerNumber = pplayerNumber;
            mWalkTexture = TextureDictionary.FindTexture("playerWalk");
            mControler = new PlayerController(pplayerNumber);
            mAnimator = new Animator(1, 31, 63, 1, 6);
        }
        public void setPosition(Vector2 pVector)
        {
            mPosition = pVector;
        }
        public void Draw(SpriteBatch pSpriteBatch)
        {
            pSpriteBatch.Draw(mWalkTexture, new Rectangle((int)mPosition.X,(int)mPosition.Y, 31,63),mAnimator.NextFrame(), Color.White);
        }
        public void Update()
        {
            mPosition = mPosition * mVelocity * 1 / 60;
        }
    }
}
