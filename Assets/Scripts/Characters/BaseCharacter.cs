using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{

    private string characterType;
    private string characterDescription;
    private int strength;
    private int speed;
    private int stamina;
    private int healthBar;


    public string CharacterType
    {
        get { return characterType; }
        set { characterType = value; }

    }

    public string CharacterDescription
    {
        get { return characterDescription; }
        set { characterDescription = value; }

    }

    public int Strength
    {
        get { return strength; }
        set { strength = value; }

    }

    public int Stamina
    {
        get { return stamina; }
        set { stamina = value; }

    }
    public int Speed
    {
        get { return speed; }
        set { speed = value; }

    }

    public int HealthBar
    {
        get { return healthBar; }
        set { healthBar = value; }

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void attack()
    {

    }
}
