using UnityEngine;
using UnityEngine.InputSystem;

public class inputManager : MonoBehaviour
{
    [HideInInspector] public bool runRight = false;
    [HideInInspector] public bool runLeft = false;
    [HideInInspector] public bool runForward = false;
    [HideInInspector] public bool runBack = false;
    [HideInInspector] public bool walk = false;
    [HideInInspector] public bool crouch = false;
    [HideInInspector] public bool attack = false;
    [HideInInspector] public bool interact = false;
    [HideInInspector] public bool pause = false;
    

    void Update()
    {
        #region Gamapad
        //Setup
        bool GP_leftdpad = false;
        bool GP_rightdpad = false;
        bool GP_updpad = false;
        bool GP_downdpad = false;
        bool GP_shoot = false;
        bool GP_walk = false;
        bool GP_crounch = false;
        bool GP_interact = false;
        bool GP_escape = false;
        

        if(Gamepad.all.Count > 0)
        {
            GP_leftdpad = Gamepad.all[0].dpad.left.isPressed;
            GP_rightdpad = Gamepad.all[0].dpad.right.isPressed;
            GP_updpad = Gamepad.all[0].dpad.up.isPressed;
            GP_downdpad = Gamepad.all[0].dpad.down.isPressed;
            GP_walk = Gamepad.all[0].aButton.isPressed;
            GP_crounch = Gamepad.all[0].leftStickButton.isPressed;

            GP_shoot = Gamepad.all[0].xButton.wasPressedThisFrame;
            GP_interact = Gamepad.all[0].bButton.wasPressedThisFrame;
            GP_interact = Gamepad.all[0].startButton.wasPressedThisFrame;
        }
        #endregion

        #region Keyboard

        #region Moviment
        //Right
        if (Keyboard.current.dKey.isPressed || GP_rightdpad)
        {
            runRight = true;
        }
        else
        {
            runRight = false;
        }

        //Left
        if (Keyboard.current.aKey.isPressed || GP_leftdpad)
        {
            runLeft = true;
        }
        else
        {
            runLeft = false;
        }

        //Up
        if (Keyboard.current.wKey.isPressed || GP_updpad)
        {
            runForward = true;
        }
        else
        {
            runForward = false;
        }

        //Down
        if (Keyboard.current.sKey.isPressed || GP_downdpad)
        {
            runBack = true;
        }
        else
        {
            runBack = false;
        }

        //Walk
        if (Keyboard.current.shiftKey.isPressed || GP_walk)
        {
            walk = true;
        }
        else
        {
            walk = false;
        }

        //Crouch
        if (Keyboard.current.leftCtrlKey.isPressed || GP_rightdpad)
        {
            crouch = true;
        }
        else
        {
            crouch = false;
        }
        #endregion

        #region Attack
        //Attack
        if (Mouse.current.leftButton.wasPressedThisFrame || GP_shoot)
        {
            attack = true;
        }
        else
        {
            attack = false;
        }
        #endregion

        #region Interact
        //Interact
        if (Keyboard.current.eKey.wasPressedThisFrame || GP_interact)
        {
            interact = true;
        }
        else
        {
            interact = false;
        }
        #endregion

        #region Pause
        //Pause
        if (Keyboard.current.escapeKey.isPressed || GP_escape)
        {
            pause = true;
        }
        else
        {
            pause = false;
        }
        #endregion

        #endregion
    }
}
