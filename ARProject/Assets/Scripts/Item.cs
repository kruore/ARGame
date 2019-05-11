using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Item
{

    public int cardInventoryNum;
    public int cardHp;
    public int cardCost;
    public int cardDamage;
    public string cardName;
    public Element cardElement;


    public Item(int num,int cost, int hp, int damage, string name, Element element)
    {
        cardInventoryNum=num;
        cardCost = cost;
        cardHp = hp;
        cardDamage = damage;
        cardName = name;
        cardElement = element;
    }
}