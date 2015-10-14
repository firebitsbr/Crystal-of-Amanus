﻿using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Personagens e Monstros/Monstros Sripts/Monster")]
public class Monster : Target
{
    public Animator anim;
    public GameObject Morte;
    public bool HitTeste = false;
    protected int ExpEarn;
    
    public virtual Item Loot()
    {
        return null;
    }
    public virtual void AI()
    {

    }
    protected void DamageCheck()
    {
        if (Vida <= 0)
        {
            print(ExpEarn);
            GameObject.FindGameObjectWithTag("Player").SendMessage("ChecarQuestMonstros", this);
            GameObject.FindGameObjectWithTag("Player").SendMessage("LevelUp", ExpEarn);
            if (Morte)
            {
                Instantiate(Morte, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }
    void OnParticleCollision(GameObject other)
    {
        print(other.name);
        if (other.transform.parent.tag == "Player")
        {
            HitTeste = true;
            if (other.tag != "Reflect")
            {
                HealOrDamage(other.transform.parent.GetComponent<Character>().AtaqueAtual, 0);
            }else
            {
                print("Escudo-particle");
            }
            try
            {
                anim.SetTrigger("Dano");
            }
            catch
            {

            }
        }
    }

}