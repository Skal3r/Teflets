using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class HealthBehavior : MonoBehaviour
{

    [SerializeField]
    private int MaxHP = 100;
    private int currentHP;
    // Start is called before the first frame update
    void Start()
    {
        currentHP = MaxHP;
    }


    //destroys this game object when health is gone
    void Update()
    {
        if (currentHP <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    //increase health points
    public void Heal(int heal)
    {
        currentHP += heal;

    }
    //decrease Health points
    public void DoDamage(int amount)
    {

        currentHP -= amount;
    }
    public int getCurrentHP() {
        return currentHP;
    }
    public int getMaxHP() {
        return MaxHP;
    }

}
