using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2
{
    enum AnimationState { Idol,Left,Right,Punching }
    class Enemy
    {
        private Animator enemyAnimator;
        private static Random RNG = new Random();
        private Vector2 mPosition;
        private Vector2 mVelocity;
        private Texture2D mWalkTexture;
        private Texture2D mIdolTexture;
        private Rectangle mRectangle;
        public bool mActive;
        private float mHealth = 100f;
        private float mDamage = 5f;
        private float mSpeed = 75;
        private int alt = 5;
        private int timer = 0;
        private AnimationState mAnimationState;
        private AnimationState mLastState;
        public Enemy(Vector2 position)
        {
            enemyAnimator = new Animator(1, 31, 63, 1, 6);
            mPosition = position;
            mWalkTexture = TextureDictionary.FindTexture("ninjaWalk");
            mIdolTexture = TextureDictionary.FindTexture("ninjaIdol");
            mActive = true;
        }

        public void Update(Vector2 pposition)
        {
            timer++;

            if (mHealth <= 0)
            {
                mActive = false;
            }
            else if (timer > 270)
            {
                timer = 0;
            }

            if (mAnimationState != AnimationState.Idol)
            {
                mLastState = mAnimationState;
            }
            mVelocity = new Vector2();
            if (timer > 100)
            {
                mAnimationState = AnimationState.Idol;
            }
            else if (mPosition.X < pposition.X && mPosition.Y < pposition.Y)
            {
                mVelocity.X = mSpeed; mVelocity.Y = mSpeed;
                mAnimationState = AnimationState.Right;
            }
            else if (mPosition.X > pposition.X && mPosition.Y > pposition.Y)
            {
                mVelocity.X = -mSpeed; mVelocity.Y = -mSpeed;
                mAnimationState = AnimationState.Left;
            }
            else if (mPosition.X < pposition.X && mPosition.Y > pposition.Y)
            {
                mVelocity.X = mSpeed; mVelocity.Y = -mSpeed;
                mAnimationState = AnimationState.Right;
            }
            else
            {
                mVelocity.X = -mSpeed; mVelocity.Y = mSpeed;
                mAnimationState = AnimationState.Left;
            }
            
            mPosition = mPosition + mVelocity * 1 / 60;
        }

        public void Draw(SpriteBatch pSpriteBatch)
        {
            
            switch (mAnimationState)
            {
                case AnimationState.Right:
                    if (alt % 5 == 0)
                    {
                        mRectangle = enemyAnimator.NextFrame();
                        alt = 0;
                    }
                    pSpriteBatch.Draw(mWalkTexture, new Rectangle((int)mPosition.X, (int)mPosition.Y, 31, 63), mRectangle, Color.White);
                    break;
                case AnimationState.Left:
                    if (alt % 5 == 0)
                    {
                        mRectangle = enemyAnimator.NextFrame();
                        alt = 0;
                    }
                    pSpriteBatch.Draw(mWalkTexture, new Rectangle((int)mPosition.X, (int)mPosition.Y, 31, 63), mRectangle, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 1);
                    break;
                case AnimationState.Idol:
                    if (mLastState == AnimationState.Right)
                    {
                        pSpriteBatch.Draw(mIdolTexture, new Rectangle((int)mPosition.X + 13, (int)mPosition.Y + 17, mIdolTexture.Width, mIdolTexture.Height), Color.White);
                    }
                    else
                    {
                        pSpriteBatch.Draw(mIdolTexture, new Rectangle((int)mPosition.X + 13, (int)mPosition.Y + 17, mIdolTexture.Width, mIdolTexture.Height), new Rectangle(0, 0, 16, 46), Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 1);
                    }
                    break;
            }

            alt++;
        }
    }
}
