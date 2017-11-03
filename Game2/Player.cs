using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
namespace Game2
{
    class Player
    {
        PlayerController mControler;
        private Vector2 mPosition;
        private Vector2 mVelocity;
        public Player(int playerNumber)
        {
            mControler = new PlayerController(playerNumber);
        }
        public void Update()
        {
            mPosition = mPosition * mVelocity * 1 / 60;
        }
    }
}
