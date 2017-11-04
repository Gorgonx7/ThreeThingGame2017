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
        int currentWave;
        public SceneManager(List<Enemy>[] pTotalEnemies, List<Texture2D> pSceneBackgrounds)
        {
            RNG = new Random();
            sceneBackgounds = pSceneBackgrounds;
            mCurrentScenePosition = new Vector2();
            mNextScenePosition = new Vector2(1600, 0);
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
            sceneEnemys = TotalSceneEnemies[CurrentScene];
            CurrentWaves = Setenemys();
            
            NextSceneTexture = sceneBackgounds[CurrentScene];
            CurrentSceneTexture = sceneBackgounds[CurrentScene - 1];
        }
        private List<Enemy>[] Setenemys()
        {
            int thirdEnemies = sceneEnemys.Count / 3;
            int RAD = RNG.Next(0, thirdEnemies);
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
            player.Update();
            if (!switchNextScene) {
                for (int x = 0; x < CurrentWaves[currentWave].Count; x++)
                {
                    if (CurrentWaves[currentWave][x].mActive)
                    {
                        CurrentWaves[currentWave][x].Update(player.getPosition());
                    }
                }
                    for (int x = 0; x < sceneEnemys.Count; x++)
                    {
                        if (sceneEnemys[x].mActive)
                        {
                            break;
                        }
                        switchNextScene = true;
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
