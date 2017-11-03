using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2
{
    class Enemy
    {
        private Animator enemyAnimator;
        private Vector2 mPosition;
        private Vector2 mVelocity;
        private Texture2D mTexture;
        private Rectangle mRectangle;
        private bool mActive;
        private float mHealth = 100f;
        private float mDamage = 5f;

        public Enemy(Animator animation, Vector2 position)
        {
            enemyAnimator = new Animator(1, 31, 63, 1, 6);
            mPosition = position;
            mTexture = TextureDictionary.FindTexture("enemy");
            mActive = true;
        }

        public void Update()
        {
            mPosition = mPosition * mVelocity * 1 / 60;
            if (mHealth <= 0)
            {
                mActive = false;
            }
        }

        public void Draw(SpriteBatch pSpriteBatch)
        {
            pSpriteBatch.Draw(mTexture, new Rectangle((int)mPosition.X,(int)mPosition.Y,31,63),mRectangle, Color.White);
        }
    }
}
