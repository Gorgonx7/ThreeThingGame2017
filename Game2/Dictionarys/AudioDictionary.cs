using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
namespace Game2
{
    public struct audioSet
    {
        public SoundEffect m_Effect;
        public string m_Reference;
        public audioSet(SoundEffect pEffect, string pReference)
        {
            m_Effect = pEffect;
            m_Reference = pReference;
        }
    }
    public static class AudioDictionary
    {
        private static List<audioSet> m_audioDirecotry = new List<audioSet>();
        public static void AddAudio(SoundEffect pTexture, string pReference)
        {
            m_audioDirecotry.Add(new audioSet(pTexture, pReference));
        }
        public static void RemoveAudio(string pReference)
        {
            for (int x = 0; x < m_audioDirecotry.Count; x++)
            {
                if (m_audioDirecotry[x].m_Reference == pReference)
                {
                    m_audioDirecotry.Remove(m_audioDirecotry[x]);
                }
            }

        }
        public static SoundEffect FindAudio(string pReference)
        {
            for (int x = 0; x < m_audioDirecotry.Count; x++)
            {
                if (m_audioDirecotry[x].m_Reference == pReference)
                {
                    return m_audioDirecotry[x].m_Effect;
                }
            }
            throw new Exception("Audio Not Found Exception");
        }
    }

}

