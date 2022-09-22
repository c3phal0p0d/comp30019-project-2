
using UnityEngine;

[System.Serializable]
public class CharacterStats : MonoBehaviour
{
    public int totalhealth = 100;
    public int totalArmor = 100;
    public int currentHealth { get; private set; }
    public int currentArmor { get; private set; }




    void Awake()
    {
        currentHealth = totalhealth;
    }

  
    public void TakeDamage(int damage)
    {

        if (currentArmor > 0)
        {
            currentArmor -= damage;
        }
        else
        {
            currentHealth -= damage;
        }


        if (currentHealth <= 0)
        {

            //kill character
        }

        Debug.Log(transform.name + "takes " + damage + "damage.");
    }
}
