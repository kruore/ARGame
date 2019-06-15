using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Item
{

    public int cardInventoryNum;
    public int cardCost;
    public int cardDamage;
    public string cardName;
    public Element cardElement;
    public int cardRank;

    public Item(int num,int cost,  int damage, string name, Element element,int rank)
    {
        cardInventoryNum=num;
        cardCost = cost;
        cardDamage = damage;
        cardName = name;
        cardElement = element;
        cardRank = rank;
    }
}