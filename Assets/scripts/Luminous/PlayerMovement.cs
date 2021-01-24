using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator Luminous , Laminus;

    public float LuminousRunSpeed = 60f, LaminusRunSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    bool IsLuminous = true;

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * (IsLuminous? LuminousRunSpeed : LaminusRunSpeed);
        Luminous.SetFloat("Speed",Mathf.Abs(horizontalMove));
        Laminus.SetFloat("Speed",Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            Luminous.SetBool("IsJumping", true);
            Laminus.SetBool("IsJumping", true);
        }
        
        if (Input.GetButtonDown("SwitchPlayer"))
        {
            gameObject.GetComponent<SwitchCharacterScript>().SwitchAvatar();
            IsLuminous = !IsLuminous;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
            Luminous.SetBool("IsCrouching", true);
            Laminus.SetBool("IsCrouching", true);
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
            Luminous.SetBool("IsCrouching", false);
            Laminus.SetBool("IsCrouching", false);
        }
    }

    public void OnLanding()
    {
        Luminous.SetBool("IsJumping", false);
        Laminus.SetBool("IsJumping", false);
    }
    public void OnCrouching(bool IsCrouching)
    {
        Luminous.SetBool("IsCrouching", IsCrouching);
        Laminus.SetBool("IsCrouching", IsCrouching);
    }

    void FixedUpdate(){
        controller.Move(horizontalMove * Time.fixedDeltaTime,crouch, jump);
        jump = false;
    }
}
