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
        GamePadCapabilities mcapabilities;
        PlayerIndex mPlayerIndex;
        GamePadState mState;
        public PlayerController(int PlayerNumber)
        {


            /*switch (PlayerNumber)

            switch (PlayerNumber)

            {
                case 1:
                    mcapabilities = GamePad.GetCapabilities(Microsoft.Xna.Framework.PlayerIndex.One);
                    mPlayerIndex = PlayerIndex.One;
                    break;
                case 2:
                    mcapabilities = GamePad.GetCapabilities(Microsoft.Xna.Framework.PlayerIndex.Two);
                    mPlayerIndex = PlayerIndex.Two;
                    break;
                default:
                    mcapabilities = GamePad.GetCapabilities(Microsoft.Xna.Framework.PlayerIndex.One);
                    mPlayerIndex = PlayerIndex.One;
                    break;
            }
            
            if (!mcapabilities.IsConnected)
            {
                throw new Exception("Controller Not connected, derp");
 
        
       
        public bool UpisDown()

        {
            mState = GamePad.GetState(mPlayerIndex);
            if(mState.DPad.Up == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }
        public bool DownisDown()
        {
            mState = GamePad.GetState(mPlayerIndex);
            if (mState.DPad.Down == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }
        public bool LeftisDown()
        {
            mState = GamePad.GetState(mPlayerIndex);
            if (mState.DPad.Left == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }
        public bool RightisDown()
        {
            mState = GamePad.GetState(mPlayerIndex);
            if (mState.DPad.Right == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }
        public bool AisDown()
        {
            mState = GamePad.GetState(mPlayerIndex);
            if (mState.Buttons.A == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }
        public bool BisDown()
        {
            mState = GamePad.GetState(mPlayerIndex);
            if (mState.Buttons.B == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }
        public bool YisDown()
        {
            mState = GamePad.GetState(mPlayerIndex);
            if (mState.Buttons.Y == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }
        public bool XisDown()
        {
            mState = GamePad.GetState(mPlayerIndex);
            if (mState.Buttons.X == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }
        public bool RightSholderisDown()
        {
            mState = GamePad.GetState(mPlayerIndex);
            if (mState.Buttons.RightShoulder == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }
        public bool LeftSholderisDown()
        {
            mState = GamePad.GetState(mPlayerIndex);
            if (mState.Buttons.LeftShoulder == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }
        public bool StartisDown()
        {
            mState = GamePad.GetState(mPlayerIndex);
            if (mState.Buttons.Start == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }
        public bool SelectisDown()
        {
            mState = GamePad.GetState(mPlayerIndex);
            if (mState.Buttons.Back == ButtonState.Pressed)
            {
                return true;
            }
            return false;

        }*/
        }
            public bool AKeyisDown()
            {
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    return true;
                }
                return false;
            }
            public bool SKeyisDown()
            {
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    return true;
                }
                return false;
            }
            public bool DKeyisDown()
            {
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    return true;
                }
                return false;
            }
            public bool WKeyisDown()
            {
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    return true;
                }
                return false;

            }
            public bool leftMouseClick()
            {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    return true;
                }
                return false;
            }
        }
 }
