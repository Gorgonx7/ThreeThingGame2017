using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game2
{
    enum AnimationState { Idol,Left,Right,Jab }
    class Enemy
    {
        private Animator enemyAnimator;
        private static Random RNG = new Random();
        private Vector2 mPosition;
        private Vector2 mVelocity;
        private Texture2D mWalkTexture;
        private Texture2D mIdolTexture;
        private Rectangle mRectangle;
        public Animator jabAnimator;
        private Texture2D mJabTexture;
        public bool mActive;
        private float mHealth = 100f;
        private float mDamage = 10f;
        private float mSpeed = 100f;
        private int alt = 5;
        private int timer = 0;
        private AnimationState mAnimationState;
        public int randomness = 50;
        private AnimationState mLastState;
        public Enemy(Vector2 position)
        {
            enemyAnimator = new Animator(1, 31, 63, 1, 6);
            jabAnimator = new Animator(1, 31, 63, 1, 2);
            mPosition = position;
            if (RNG.Next(0, 100) > 50)
            {
                mWalkTexture = TextureDictionary.FindTexture("ninjaWalk");
                mIdolTexture = TextureDictionary.FindTexture("ninjaIdol");
                mJabTexture = TextureDictionary.FindTexture("ninjaJab");
            }
            else
            {
                mWalkTexture = TextureDictionary.FindTexture("redNinjaWalk");
                mIdolTexture = TextureDictionary.FindTexture("redNinjaIdol");
                mJabTexture = TextureDictionary.FindTexture("ninjaJabRed");
            }
            
            mActive = true;
        }
        public Rectangle getCollisionRectangle()
        {
            return new Rectangle((int)mPosition.X + 13, (int)mPosition.Y + 17, 18, 46);
        }
        public void Damage(float pDamage)
        {
            mHealth -= pDamage;
            if (mHealth <= 0)
            {
                mActive = false;
            }
        }
        public bool HealthCheck()
        {
            if(mHealth <= 0)
            {
                return true;
            }
            return false;
        }
        public void Collision(List<Enemy> pList)
        {
            foreach(Enemy i in pList)
            {
                if(i != this)
                {
                    if (i.getCollisionRectangle().Intersects(getCollisionRectangle()))
                    {
                        Rectangle collider = i.getCollisionRectangle();
                        if(collider.Top < getCollisionRectangle().Bottom)
                        {
                            mPosition.Y = collider.Y + collider.Height + 1;
                        }
                        if (collider.Bottom > getCollisionRectangle().Top)
                        {
                            mPosition.Y = collider.Y - collider.Height - 1;
                        } 
                        if(collider.Left < getCollisionRectangle().Right)
                        {
                            mPosition.X = collider.X + collider.Width + 1;
                        }
                        if(collider.Right > getCollisionRectangle().Left)
                        {
                            mPosition.X = collider.X - collider.Width - 1;
                        }
                    }
                }
            }
        }
        public void Update(Vector2 pposition, Rectangle pColision, Player pPlayer, List<Enemy> pfoes)
        {
            mVelocity = new Vector2();
            if (mHealth <= 0)
            {
                mActive = false;
            }

            Rectangle comp = new Rectangle(pColision.X, pColision.Y, 30, pColision.Height);
            if (mPosition == pposition || getCollisionRectangle().Intersects(comp))
            {
                mAnimationState = AnimationState.Jab;
                if (alt % 25 == 0)
                {
                    attack(2,pPlayer);

                }
                Collision(pfoes);
                //attack
            }
            else { 
            timer++;
             if (timer > RNG.Next(200,400))
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
                mVelocity.X -= Enemy.RNG.Next(0, randomness);
                mVelocity.Y -= RNG.Next(0, randomness);
                mAnimationState = AnimationState.Right;
            }
            else if (mPosition.X > pposition.X && mPosition.Y > pposition.Y)
            {
                
                mVelocity.X = -mSpeed; mVelocity.Y = -mSpeed;
                mVelocity.X -= Enemy.RNG.Next(0, randomness);
                mVelocity.Y -= RNG.Next(0, randomness);
                mAnimationState = AnimationState.Left;
            }
            else if (mPosition.X < pposition.X && mPosition.Y > pposition.Y)
            {
                
                mVelocity.X = mSpeed; mVelocity.Y = -mSpeed;
                mVelocity.X -= Enemy.RNG.Next(0, randomness);
                mVelocity.Y -= RNG.Next(0, randomness);
                mAnimationState = AnimationState.Right;
            }
            else
            {
                
                mVelocity.X = -mSpeed; mVelocity.Y = mSpeed;
                mVelocity.X -= Enemy.RNG.Next(0, randomness);
                mVelocity.Y -= RNG.Next(0, randomness);
                mAnimationState = AnimationState.Left;
            }

                mPosition = mPosition + mVelocity * 1 / 60;
                Collision(pfoes);
            }
            
        }
        private void attack(float pDamage, Player pPlayer)
        {
            pPlayer.damage(pDamage);
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
                case AnimationState.Jab:
                    if (alt % 25 == 0)
                    {
                        mRectangle = jabAnimator.NextFrame();
                        alt = 0;

                    }

                    if (mLastState == AnimationState.Right)
                    {
                        pSpriteBatch.Draw(mJabTexture, new Rectangle((int)mPosition.X, (int)mPosition.Y, 31, 63), mRectangle, Color.White);
                    } else
                    {

                        pSpriteBatch.Draw(mJabTexture, new Rectangle((int)mPosition.X, (int)mPosition.Y, 31, 63), mRectangle, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 1);
                    }
                    break;
            }
           // pSpriteBatch.Draw(TextureDictionary.FindTexture("Background"), getCollisionRectangle(), Color.Beige);
            alt++;
        }
    }
}
