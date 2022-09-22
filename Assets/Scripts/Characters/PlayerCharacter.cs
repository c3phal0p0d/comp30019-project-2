using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : BaseCharacter
{

    // List<Pickup> pickups;
    private Vector3 playerMovementInput;

    [SerializeField]
    private CharacterController characterController;
    public PlayerCharacter()
    {
        CharacterType = "player";
        CharacterDescription = "Hero that is trying to escape the maze";
        Stamina = 13;
        Strength = 12;
        Speed = 12;
        HealthBar = 100;


    }

    public void update()
    {



        playerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")) * Speed * Time.deltaTime;

        move();
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            attack();
        }
    }

    public void move()
    {
        characterController.Move(playerMovementInput);
    }

    public void pickup()
    {

    }

    public void lightBeacon()
    {

    }


    private void attack()
    {

    }




}
