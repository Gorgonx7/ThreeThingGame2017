using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2
{
    enum PickupType { Health,Speed,Rage }
    class Pickup
    {
        static Random rng = new Random();
        private Animator pickupAnimator;

        private Vector2 mPosition;
        private Texture2D mTexture;
        private Rectangle mRectangle;
        public bool mActive;
        private float mHealthUp;
        private float mSpeedUp;
        private float mRageUp;
        private int Alt;
        private PickupType mPickup;

        public Pickup()
        {
            pickupAnimator = new Animator(1,70,108,0,1);
            mPosition.X = rng.Next(0, 760);
            mPosition.Y = rng.Next(450, 760);
            mTexture = TextureDictionary.FindTexture("chipParticulates");
            mActive = true;
        }

        public void Update()
        {
            if (true) { }
        }

        public void Draw(SpriteBatch pSpriteBatch)
        {
            if(Alt % 5 == 0)
            {
                mRectangle = pickupAnimator.NextFrame();
            }
            pSpriteBatch.Draw(mTexture, new Rectangle((int)mPosition.X, (int)mPosition.Y, mTexture.Width, mTexture.Height), Color.White);
        }
    }
}
