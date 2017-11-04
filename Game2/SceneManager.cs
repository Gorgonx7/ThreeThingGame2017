using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
namespace Game2
{
    class SceneManager
    {
        private Random RNG;
        Player player;
        private List<Enemy>[] TotalSceneEnemies;
        private List<Texture2D> sceneBackgounds;
        private List<Enemy> sceneEnemys;
        Texture2D CurrentSceneTexture;
        Texture2D NextSceneTexture;
        Vector2 mCurrentScenePosition;
        Vector2 mNextScenePosition;
        Vector2 mTransform;
        private int CurrentScene;
        bool switchNextScene = false;
        List<Enemy>[] CurrentWaves;
        int currentWave = 0;
        bool translateBackground = false;
        public SceneManager(List<Enemy>[] pTotalEnemies, List<Texture2D> pSceneBackgrounds)
        {
            RNG = new Random();
            sceneBackgounds = pSceneBackgrounds;
            mCurrentScenePosition = new Vector2();
            mNextScenePosition = new Vector2(1600, 0);
            NextSceneTexture = pSceneBackgrounds[1];
            TotalSceneEnemies = pTotalEnemies;
            sceneEnemys = pTotalEnemies[0];
            CurrentWaves = Setenemys();
            mTransform = new Vector2(50, 0);
            CurrentScene = 0;
            CurrentSceneTexture = pSceneBackgrounds[0];
            player = new Player(1);
            
        }
        private void SetNextScene()
        {
            CurrentScene++;
            if(CurrentScene > sceneBackgounds.Count)
            {
                //ending sequence;
                return;
            }
            sceneEnemys = TotalSceneEnemies[CurrentScene];
            CurrentWaves = Setenemys();
            
            NextSceneTexture = sceneBackgounds[CurrentScene];
            CurrentSceneTexture = sceneBackgounds[CurrentScene - 1];
            translateBackground = true;
        }
        private List<Enemy>[] Setenemys()
        {
            int thirdEnemies = sceneEnemys.Count / 3;
            int RAD = RNG.Next(0, thirdEnemies) + 3;
            int RAD2 = RNG.Next(RAD + 1, thirdEnemies * 2) - RAD;
            int RAD3 = sceneEnemys.Count - RAD2 - RAD;
            List<Enemy> Wave1 = new List<Enemy>();
            List<Enemy> Wave2 = new List<Enemy>();
            List<Enemy> Wave3 = new List<Enemy>();
            for(int x = 0; x < RAD; x++)
            {
                Wave1.Add(sceneEnemys[x]);
            }
            for (int x = RAD; x < (RAD2 + RAD); x++)
            {
                Wave2.Add(sceneEnemys[x]);
            }
            for (int x = RAD2; x < sceneEnemys.Count; x++)
            {
                Wave3.Add(sceneEnemys[x]);
            }
            return new List<Enemy>[] { Wave1, Wave2, Wave3 };
        }
        public void Update()
        {
            if (!translateBackground)
            {
                player.PlayerInputSkipUpdate(false);
                player.Update(CurrentWaves[currentWave]);
                Vector2 target = new Vector2(player.getPosition().X, player.getPosition().Y);
                bool hasAlive = false;
                if (!switchNextScene)
                {
                    for (int x = 0; x < CurrentWaves[currentWave].Count; x++)
                    {
                        if (CurrentWaves[currentWave][x].mActive)
                        {
                            CurrentWaves[currentWave][x].Update(target, player.getCollision(), player, CurrentWaves[currentWave]);
                            hasAlive = true;
                        }

                    }
                    if (!hasAlive && currentWave != 2)
                    {
                        currentWave++;
                    }
                    else if (!hasAlive && currentWave == 2)
                    {
                        switchNextScene = true;
                    }


                }
            } else if (translateBackground)
            {
                player.PlayerInputSkipUpdate(true);
                mCurrentScenePosition = mCurrentScenePosition + new Vector2(-500, 0) * 1 / 60;
                mNextScenePosition = mNextScenePosition + new Vector2(-500, 0) * 1 / 60;
                if(mNextScenePosition.X <= 0)
                {
                    translateBackground = false;
                }
            }
            if (switchNextScene)
            {
                SetNextScene();

                switchNextScene = false;
            }
            

        }
        public void Draw(SpriteBatch pSpriteBatch)
        {
            pSpriteBatch.Draw(CurrentSceneTexture, new Rectangle((int)mCurrentScenePosition.X, (int)mCurrentScenePosition.Y, 1600, 800), Color.White);
            pSpriteBatch.Draw(NextSceneTexture, new Rectangle((int)mNextScenePosition.X, (int)mNextScenePosition.Y, 1600, 800), Color.White);
            for(int x = 0; x < CurrentWaves[currentWave].Count; x++)
            {
                if (CurrentWaves[currentWave][x].mActive)
                {
                    CurrentWaves[currentWave][x].Draw(pSpriteBatch);
                }
            }
            player.Draw(pSpriteBatch);

        }
    }
}
