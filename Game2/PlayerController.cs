using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
namespace Game2
{
    class PlayerController
    {
        GamePadCapabilities capabilities;
        public PlayerController(int PlayerNumber)
        {
            
            switch (PlayerNumber)
            {
                case 1:
                    capabilities = GamePad.GetCapabilities(Microsoft.Xna.Framework.PlayerIndex.One);
                    break;
                case 2:
                    capabilities = GamePad.GetCapabilities(Microsoft.Xna.Framework.PlayerIndex.Two);
                    break;
                default:
                    capabilities = GamePad.GetCapabilities(Microsoft.Xna.Framework.PlayerIndex.One);
                    break;
            }
            
            if (!capabilities.IsConnected)
            {
                throw new Exception("Controller Not connected, derp");
            }
        }
        public void Update()
        {

        }

    }
}
