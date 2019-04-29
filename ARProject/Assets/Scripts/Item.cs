using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class Item
{


    public int cardHp;
    public int cardCost;
    public int cardDamage;
    public string cardName;
    public Element cardElement;


    public Item(int num, int hp, int damage, string name, Element element)
    {
        cardCost = num;
        cardHp = hp;
        cardDamage = damage;
        cardName = name;
        cardElement = element;
    }
}