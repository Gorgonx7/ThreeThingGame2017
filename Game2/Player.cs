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
        idol,left,right,jab,claw, transform
    }
    class Player
    {
        PlayerController mControler;
        private Vector2 mPosition;
        private Vector2 mVelocity;
        private Texture2D mWalkTexture;
        private Rectangle mRectange;
        private float mHealth;
        private int playerNumber;
        private Animator mWalkAnimator, mJumpAnimator, mJabAnimator, wolfClawAnimator, transformAnimator;
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
        private Texture2D mHealthBarFrameTexture, mRageBarFrameTexture, mHealthBarTexture, mRageBarTexture, wolfClaw, dead, wolfTrans;
        private Rectangle mHealthBarFrameRect, mRageBarFrameRect, mHealthBarRect, mRageBarRect;
        private const float mResourceBarWidth = 300f;
        private const int mResourceBarHeight = 20;
        private const int mResourceLimit = 100;
        private bool hasPunched = true;
        private bool isAWolf = false;
        private int jabTime, clawTime, transformTime;
        private List<Enemy> currentFoes;
        private bool enterKeyPressed;
        //18 / 46
        //31, 63
        public Player(int pplayerNumber)
        {
            playerNumber = pplayerNumber;
            mWalkTexture = TextureDictionary.FindTexture("playerWalk");
            mIdolTexture = TextureDictionary.FindTexture("playerIdol");
            mJab = TextureDictionary.FindTexture("playerJab");
            mPunchSound = AudioDictionary.FindAudio("Punch");
            wolfClaw = TextureDictionary.FindTexture("Claw");
            wolfClawAnimator = new Animator(1, 31, 63, 5, 4);
            mControler = new PlayerController(pplayerNumber);
            mPosition = new Vector2(100, 550);
            mWalkAnimator = new Animator(1, 31, 63, 1, 6);
            mJabAnimator = new Animator(1, 31, 63, 9, 2);
            transformAnimator = new Animator(1, 31, 63, 1, 3);
            wolfTrans = TextureDictionary.FindTexture("Transform");
            mRage = 0f; // Set rage value to 0.
            mHealthBarTexture = TextureDictionary.FindTexture("greenBar"); // Initialise health bar as green @ 100%.
            mRageBarTexture = TextureDictionary.FindTexture("redBar"); // Rage is always red.
            mHealthBarFrameTexture = TextureDictionary.FindTexture("redBar");
            mRageBarFrameTexture = TextureDictionary.FindTexture("redBar");
            mHealthBarFrameRect = new Rectangle(25, 25, (int)mResourceBarWidth + 4, (int)mResourceBarHeight + 4); // 2 pixel border.
            mRageBarFrameRect = new Rectangle(25, (int)mResourceBarHeight + 35, (int)mResourceBarWidth + 4, (int)mResourceBarHeight + 4); // 2 pixel border.
            mHealthBarRect = new Rectangle(27, 27, (int)mResourceBarWidth, (int)mResourceBarHeight);
            mRageBarRect = new Rectangle(27, (int)mResourceBarHeight + 37, 0, (int)mResourceBarHeight);
            mRage = 50;
            mHealth = 100;
        }
        public Rectangle getCollision()
        {
            return new Rectangle((int)mPosition.X + 13, (int)mPosition.Y + 17, 18, 46);
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
        private void addRage(float pAdd)
        {
            mRage += pAdd;
            mRage = MathHelper.Clamp(mRage, 0, 100);
        }
        private Rectangle getDrawingRectangle()
        {
            return new Rectangle((int)mPosition.X, (int)mPosition.Y, 31, 63);
        }
        private Rectangle GetRageBar()
        {
            float ragePercentage = mRage / mResourceLimit;
            mRageBarRect.Width = (int)(ragePercentage * mResourceBarWidth);
            mRageBarRect.Height = mResourceBarHeight;
            return mRageBarRect;
        }

        private Rectangle GetHealthBar() // Update health bar width (health value) and colour.
        {
            float healthPercentage = mHealth / mResourceLimit;
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
            mHealthBarRect.Height = mResourceBarHeight;
            return mHealthBarRect;
        }
        public void Draw(SpriteBatch pSpriteBatch)
        {
            if (HealthCheck())
            {
                if (jabTime > 0 && jabTime < 30)
                {
                    mAniState = PlayerAnimationState.jab;
                }
                else if (jabTime >= 30)
                {
                    Attack(50, currentFoes);
                    mAniState = PlayerAnimationState.idol;
                    jabTime = 0;
                }
                if (clawTime > 0 && clawTime < 60)
                {

                    mAniState = PlayerAnimationState.claw;
                }
                else if (clawTime >= 60)
                {
                    Attack(62, currentFoes);
                    mAniState = PlayerAnimationState.idol;
                    clawTime = 0;
                }
                if (transformTime > 0 && transformTime < 60)
                {
                    mAniState = PlayerAnimationState.transform;
                }
                else if (transformTime >= 60)
                {
                    mAniState = PlayerAnimationState.idol;
                    transformTime = 0;
                }
                switch (mAniState)
                {

                    case PlayerAnimationState.right:
                        if (alt % 5 == 0)
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
                        pSpriteBatch.Draw(mWalkTexture, getDrawingRectangle(), mLastFrame, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 1);
                        break;
                    case PlayerAnimationState.idol:

                        if (!leftLastPress)
                        {
                            alt = 5;
                            pSpriteBatch.Draw(mIdolTexture, new Rectangle((int)mPosition.X + 13, (int)mPosition.Y + 17, mIdolTexture.Width, mIdolTexture.Height), Color.White);
                        }
                        else
                        {
                            pSpriteBatch.Draw(mIdolTexture, new Rectangle((int)mPosition.X + 13, (int)mPosition.Y + 17, 18, 46), new Rectangle(0, 0, mIdolTexture.Width, mIdolTexture.Height), Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 1);
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

                            pSpriteBatch.Draw(mJab, getDrawingRectangle(), mLastFrame, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 1);
                        }
                        jabTime++;
                        break;
                    case PlayerAnimationState.claw:
                        if (alt % 12 == 0)
                        {

                            mLastFrame = wolfClawAnimator.NextFrame();
                            alt = 0;

                        }

                        alt++;
                        if (!leftLastPress)
                        {

                            pSpriteBatch.Draw(wolfClaw, getDrawingRectangle(), mLastFrame, Color.White);
                        }
                        else
                        {

                            pSpriteBatch.Draw(wolfClaw, getDrawingRectangle(), mLastFrame, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 1);
                        }
                        clawTime++;

                        break;
                    case PlayerAnimationState.transform:
                        if (alt % 12 == 0)
                        {

                            mLastFrame = transformAnimator.NextFrame();
                            alt = 0;

                        }

                        alt++;
                        if (!leftLastPress)
                        {

                            pSpriteBatch.Draw(wolfTrans, getDrawingRectangle(), mLastFrame, Color.White);
                        }
                        else
                        {

                            pSpriteBatch.Draw(wolfTrans, getDrawingRectangle(), mLastFrame, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 1);
                        }
                        transformTime++;
                        break;

                }
                DrawResourceBars(pSpriteBatch);
            }
            else
            {
                pSpriteBatch.Draw(TextureDictionary.FindTexture("DS"), new Rectangle(0, 0, 1200, 800), Color.White);
            }
        }
        private void DrawResourceBars(SpriteBatch pSpriteBatch)
        {

            // Draw health bar.
            pSpriteBatch.Draw(mHealthBarFrameTexture, mHealthBarFrameRect, Color.Black);
            pSpriteBatch.Draw(mHealthBarTexture, GetHealthBar(), Color.White);

            // Draw rage bar.
            pSpriteBatch.Draw(mRageBarFrameTexture, mRageBarFrameRect, Color.Black);
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
        public void damage(float pPower)
        {
            mHealth -= pPower;
        }
        public void PlayerInputSkipUpdate(bool pBool)
        {
            skipAnimation = pBool;
        }
        private void TransformWolf()
        {
            if (isAWolf && mRage <= 0)
            {
                mWalkTexture = TextureDictionary.FindTexture("playerWalk");
                mIdolTexture = TextureDictionary.FindTexture("playerIdol");
                isAWolf = false;
            }
            else
            {
                mWalkTexture = TextureDictionary.FindTexture("wolfWalk");
                mIdolTexture = TextureDictionary.FindTexture("wolfIdol");
                isAWolf = true;

            }
        }
        public void Update(List<Enemy> pEnemies)
        {
            if (HealthCheck())
            {
                if (isAWolf)
                {
                    mRage = mRage - 0.25f;
                }
                if (mRage <= 0)
                {

                    TransformWolf();

                }
                if(mRage >= 100)
                {
                    TransformWolf();
                    
                }
                currentFoes = pEnemies;
                mVelocity = new Vector2();
                if (!skipAnimation)
                {
                    if (mControler.leftMouseClick())
                    {

                        if (!isAWolf)
                        {
                            mAniState = PlayerAnimationState.jab;
                        }
                        else
                        {
                            mAniState = PlayerAnimationState.claw;
                        }

                    }
                    
                    else
                    {
                        if (mControler.SKeyisDown())
                        {
                            mAniState = PlayerAnimationState.right;
                            mVelocity.Y = 150;
                        }
                        if (mControler.WKeyisDown())
                        {
                            mAniState = PlayerAnimationState.right;
                            mVelocity.Y = -150;
                        }
                        if (mControler.AKeyisDown())
                        {
                            leftLastPress = true;
                            mAniState = PlayerAnimationState.left;
                            mVelocity.X = -150;
                        }
                        if (mControler.DKeyisDown())
                        {
                            leftLastPress = false;
                            mAniState = PlayerAnimationState.right;
                            mVelocity.X = 150;
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
            else
            {
                //die

                if (mControler.enterButtonPressed())
                {
                    enterKeyPressed = true;  
                }
                if (!mControler.enterButtonPressed() && enterKeyPressed)
                {
                    Game1.MainMenu = true;
                }

            }

        }
        public void Attack(float pPower, List<Enemy> pEnemies)
        {
            Rectangle attacRect = getCollision();
            attacRect.Width = 40;
            foreach (Enemy i in pEnemies)
            {
                Rectangle comparitor = i.getCollisionRectangle();
                if (comparitor.Intersects(attacRect))
                {
                    i.Damage(pPower);
                    if (!isAWolf)
                    {
                        addRage(2);
                    }
                    if (isAWolf)
                    {
                        mHealth += 1; // Add 5 health if you kill an enemy as werewolf.
                        mHealth = MathHelper.Clamp(mHealth, 0, 100);

                    }
                }
            }
        }
    }
}
