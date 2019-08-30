using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class CardInfo : MonoBehaviour
{
    public static CardInfo cardinfo;
    public const int HandMAX = 3;
    public List<Item> CardDeck = new List<Item>();
    public List<Item> hands = new List<Item>();

    //Card
    public UISprite[] CardImage = new UISprite[3];

    private void Awake()
    {
        cardinfo = this;
    }

    private void Start()
    {
        foreach (Item item in GameDataBase.Instance.cards_Deck)
        {
            CardDeck.Add(item);
        }
        CardShuffle();
        CardDrow();
        CardGenerateAsPrepab();
        //CardImage[0].GetComponent<UIButton>().tweenTarget = 

    }
    private void CardShuffle()
    {
        Shuffle<Item>(CardDeck);
        Debug.Log("카드 섞임");
    }
    public void CardDrow()
    {
        if (hands.Count < HandMAX && CardDeck.Count > 0)
        {
            hands.Add(CardDeck[0]);
            CardDeck.RemoveAt(0);
        }
        if (hands.Count < HandMAX && CardDeck.Count > 0)
        {
            hands.Add(CardDeck[0]);
            CardDeck.RemoveAt(0);
        }
        if (hands.Count < HandMAX && CardDeck.Count > 0)
        {
            hands.Add(CardDeck[0]);
            CardDeck.RemoveAt(0);
        }
        if (CardDeck.Count == 0)
        {
            return;
        }
        Debug.Log("카드 배분 종료");
        for (int i = 0; i < HandMAX; i++)
        {
            CardImage[i].spriteName = hands[i].cardName;
            Debug.Log("이미지 분류 끝");
        }
        if (hands.Count > 4)
        {
            Debug.Log("절대 안넘음 ㅋㅋㅋㅋ");
        }
    }
    public void CardGenerateAsPrepab()
    {


    }
    public static void Shuffle<T>(List<T> list)
    {
        Random rng = new Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
