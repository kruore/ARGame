using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class Item
{
    public enum CardElement { Stone, Grass, Tree };

    public int cardHp;
    public int cardCost;
    public int cardDamage;
    public string cardName;
    public CardElement cardElement;
 

    public Item(int num, int hp, int damage,string name,CardElement element)
    {
        cardCost = num;
        cardHp = hp;
        cardDamage = damage;
        cardName = name;
        cardElement = element;
    }
}