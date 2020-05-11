using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ButtonHandler : MonoBehaviour
    {
        public Player player;
        public string Name;
        public Vector2 vec = Vector2.zero;

        void OnEnable()
        {

        }

        public void SetDownState()
        {
            CrossPlatformInputManager.SetButtonDown(Name);
             
            player.attack(player.directionVec);
        }


        public void SetUpState()
        {
        CrossPlatformInputManager.SetButtonUp(Name);
            
            player.animator.SetBool("attack", false);
        }


        public void SetAxisPositiveState()
        {
            CrossPlatformInputManager.SetAxisPositive(Name);
        }


        public void SetAxisNeutralState()
        {
            CrossPlatformInputManager.SetAxisZero(Name);
        }


        public void SetAxisNegativeState()
        {
            CrossPlatformInputManager.SetAxisNegative(Name);
        }

        public void Update()
        {

        }
    }
