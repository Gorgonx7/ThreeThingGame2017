using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
namespace Game2
{
    enum PlayerAnimationState
    {
        idol,left,right,jab,punch
    }
    class Player
    {
        PlayerController mControler;
        private Vector2 mPosition;
        private Vector2 mVelocity;
        private Texture2D mWalkTexture;
        private Rectangle mRectange;
        private float mHealth = 100f;
        private int playerNumber;
        private Animator mWalkAnimator, mJumpAnimator, mJabAnimator;
        private Rectangle mLastFrame;
        private Texture2D mIdolTexture;
        private Texture2D mJump;
        private Texture2D mJab;
        private Rectangle mCollisionRect;
        private SoundEffect mPunchSound;
        private bool skipAnimation = false;
        int alt = 5;
        private PlayerAnimationState mAniState;
        bool leftLastPress = false;
        private float mRage;
        private Texture2D mHealthBarFrameTexture, mRageBarFrameTexture, mHealthBarTexture, mRageBarTexture;
        private Rectangle mHealthBarRect, mRageBarRect;
        private const float mResourceBarWidth = 200f;
        private const int mResourceBarHeight = 30;
        //18 / 46
        //31, 63
        public Player(int pplayerNumber)
        {
            playerNumber = pplayerNumber;
            mWalkTexture = TextureDictionary.FindTexture("playerWalk");
            mIdolTexture = TextureDictionary.FindTexture("playerIdol");
            mJab = TextureDictionary.FindTexture("playerJab");
            mPunchSound = AudioDictionary.FindAudio("Punch");
            mControler = new PlayerController(pplayerNumber);
            mPosition = new Vector2(100,550);
            mWalkAnimator = new Animator(1, 31, 63, 1, 6);
            mJabAnimator = new Animator(1, 31, 63, 9, 2);
            mRage = 0f; // Set rage value to 0.
            mHealthBarTexture = TextureDictionary.FindTexture("greenBar"); // Initialise health bar as green @ 100%.
            mRageBarTexture = TextureDictionary.FindTexture("redBar"); // Rage is always red.
        }
        public void setPosition(Vector2 pVector)
        {
            
            mPosition = pVector;
        }
        public Vector2 getPosition()
        {
            return mPosition;
        }
        public void collideWithStreet(Rectangle pStreetRect)
        {
            mCollisionRect = new Rectangle((int)mPosition.X + 13, (int)mPosition.Y + 17, 18, 46);
            if (!pStreetRect.Contains(mCollisionRect))
            {
                if(mCollisionRect.Bottom > pStreetRect.Bottom)
                {
                    mPosition.Y = (pStreetRect.Bottom - mCollisionRect.Height) - 20;
                }
                if(mCollisionRect.Top <= pStreetRect.Top)
                {
                    mPosition.Y = pStreetRect.Top;
                } if(mCollisionRect.Left <= pStreetRect.Left)
                {
                    mPosition.X = pStreetRect.Left;
                } if(mCollisionRect.Right > pStreetRect.Right)
                {
                    mPosition.X = (pStreetRect.Right - mCollisionRect.Width) - 20;
                }
            }
        }
        private Rectangle getDrawingRectangle()
        {
            return new Rectangle((int)mPosition.X, (int)mPosition.Y, 31, 63);
        }
        private Rectangle GetRageBar()
        {
            float ragePercentage = mRage / 100f;
            mHealthBarRect.Width = (int)(ragePercentage * mResourceBarWidth);
            mHealthBarRect.Height = mResourceBarHeight;
            return mRageBarRect;
        }

        private Rectangle GetHealthBar() // Update health bar width (health value) and colour.
        {
            float healthPercentage = mHealth / 100;
            if (healthPercentage > 0.75)
            {
                mHealthBarTexture = TextureDictionary.FindTexture("greenBar");
            }
            else if (healthPercentage > 0.5)
            {
                mHealthBarTexture = TextureDictionary.FindTexture("orangeBar");
            }
            else
            {
                mHealthBarTexture = TextureDictionary.FindTexture("redBar");
            }

            mHealthBarRect.Width = (int)(healthPercentage * mResourceBarWidth);
            mRageBarRect.Height = mResourceBarHeight;
            return mHealthBarRect;
        }
        public void Draw(SpriteBatch pSpriteBatch)
        {
            switch (mAniState)
            {
                case PlayerAnimationState.right:
                if(alt % 5 == 0)
                {
                    mLastFrame = mWalkAnimator.NextFrame();
                    alt = 0;
                }
                alt++;
                pSpriteBatch.Draw(mWalkTexture, getDrawingRectangle(), mLastFrame, Color.White);
                    break;
                case PlayerAnimationState.left:
                    if (alt % 5 == 0)
                    {
                        mLastFrame = mWalkAnimator.NextFrame();
                        alt = 0;
                    }
                    alt++;
                    pSpriteBatch.Draw(mWalkTexture, getDrawingRectangle(), mLastFrame, Color.White,0,Vector2.Zero,SpriteEffects.FlipHorizontally,1);
                    break;
                case PlayerAnimationState.idol:

                    if (!leftLastPress)
                    {
                        alt = 5;
                        pSpriteBatch.Draw(mIdolTexture, new Rectangle((int)mPosition.X + 13, (int)mPosition.Y + 17, mIdolTexture.Width, mIdolTexture.Height), Color.White);
                    }
                    else
                    {
                        pSpriteBatch.Draw(mIdolTexture, new Rectangle((int)mPosition.X + 13, (int)mPosition.Y + 17, 18, 46), new Rectangle(0,0,mIdolTexture.Width,mIdolTexture.Height), Color.White,0,Vector2.Zero,SpriteEffects.FlipHorizontally,1);
                    }
                    break;
                case PlayerAnimationState.jab:
                           if (alt % 12 == 0)
                        {
                            mLastFrame = mJabAnimator.NextFrame();
                            alt = 0;
                        }
                        alt++;
                    if (!leftLastPress)
                    {
                        
                        pSpriteBatch.Draw(mJab, getDrawingRectangle(), mLastFrame, Color.White);
                    }
                    else
                    {
                       
                        pSpriteBatch.Draw(mJab, getDrawingRectangle(), mLastFrame, Color.White,0,Vector2.Zero,SpriteEffects.FlipHorizontally,1);
                    }
                    break;

            }
            pSpriteBatch.Draw(mHealthBarTexture, GetHealthBar(), Color.White);
            // Draw rage bar.
            pSpriteBatch.Draw(mRageBarTexture, GetRageBar(), Color.White);
        }
        public bool HealthCheck()
        {
            if(mHealth <= 0)
            {
                return false;
            }
            return true;
        }
        public void PlayerInputSkipUpdate(bool pBool)
        {
            skipAnimation = pBool;
        }
        public void Update()
        {
            mVelocity = new Vector2();
            if (!skipAnimation)
            {
                if (mControler.leftMouseClick())
                {
                    mAniState = PlayerAnimationState.jab;
                }
                else
                {
                    if (mControler.SKeyisDown())
                    {
                        mAniState = PlayerAnimationState.right;
                        mVelocity.Y = 50;
                    }
                    if (mControler.WKeyisDown())
                    {
                        mAniState = PlayerAnimationState.right;
                        mVelocity.Y = -50;
                    }
                    if (mControler.AKeyisDown())
                    {
                        leftLastPress = true;
                        mAniState = PlayerAnimationState.left;
                        mVelocity.X = -50;
                    }
                    if (mControler.DKeyisDown())
                    {
                        leftLastPress = false;
                        mAniState = PlayerAnimationState.right;
                        mVelocity.X = 50;
                    }
                    if (!(mControler.AKeyisDown()) && !(mControler.DKeyisDown()) && !(mControler.SKeyisDown()) && !(mControler.WKeyisDown()))
                    {
                        mAniState = PlayerAnimationState.idol;
                    }
                }
            }
            else
            {
                mAniState = PlayerAnimationState.right;
            }
            mPosition = mPosition + mVelocity * 1 / 60;
            collideWithStreet(new Rectangle(0, 404, 1200, 386));


        }
        public void Attack(float pPower)
        {
            Rectangle attacRect = new Rectangle(mCollisionRect.X - mCollisionRect.Width, mCollisionRect.Height, mCollisionRect.Width * 3, mCollisionRect.Height);

        }   
    }
}
