using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Game2
{
    /*
     * Animation stages
     * Player --------------------------------
     * Right
     * left
     * Up
     * Down
     * Death
     * Fire
     * Working
     * ---------------------------------------    
     */

    class Animator
    {
        private Rectangle[] m_Frames;
        private int m_CurrentFrame;
        int mBorder;
        int mSpriteWidth;
        int mSpritHeight;
        int distanceBetween;
        public Animator(int pBorder, int pSpriteWidth, int pSpriteHeight, int pDistanceBetween, int pNumberOfFrames)
        {
            m_Frames = new Rectangle[pNumberOfFrames];
            int current = 0;
            for(int x = pBorder; x < pNumberOfFrames * pSpriteWidth + pNumberOfFrames * pDistanceBetween; x += (pSpriteWidth + pDistanceBetween))
            {
                m_Frames[current] = new Rectangle(x, pBorder, pSpriteWidth, pSpriteHeight);
                current++;
            }
           m_CurrentFrame = 0;
        }

        public Rectangle NextFrame()
        {
            if (m_CurrentFrame > m_Frames.Length - 1)
            {
                m_CurrentFrame = 0;
            }
            Rectangle Holder = m_Frames[m_CurrentFrame];
            m_CurrentFrame++;
            return Holder;
        }

    }
}
