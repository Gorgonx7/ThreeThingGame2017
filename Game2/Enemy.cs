using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2
{
    class Enemy
    {
        public Animator EnemyAnimation;
        public Vector2 Position;
        public bool Active;
        public float Health = 100;
        public float Damage = 100;
        public int Width;
        public int Height;
        float enemyMoveSpeed;

        public void Initialize(Animator animation,Vector2 position)
        {
            EnemyAnimation = animation;
            Position = position;
            Active = true;
            Health = 100;
            Damage = 5;
            enemyMoveSpeed = 6f;
        }

        public void Update()
        {
            EnemyAnimation.Position = Position;
            EnemyAnimation.Update();
            if(Health <= 0)
            {
                Active = false;
            }
        }

        public void Draw()
        {
            EnemyAnimation
        }
    }
}
