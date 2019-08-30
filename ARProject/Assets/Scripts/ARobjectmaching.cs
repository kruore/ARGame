using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARobjectmaching : MonoBehaviour
{
    public UISprite cardinfosprite;
    bool animationset=true;
    int count = 0;
    int nomalbronzepercent = 4500;
    int nomalsilverpercent = 400;
    int nomalgoldpercent = 100;
    int bronzcardnum, silvercardnum, goldcardnum, maxcount,maxtouchcount,bronzecardsolopercent,silvercardsolopercent,goldcardsolopercent,randomcardnumber;
    List<Item> cardobjects = new List<Item>();
    // Start is called before the first frame update
    void Start()
    {
        maxtouchcount = Random.Range(10,20);
        randomcardnumber = Random.Range(0, 10000);
        switch (GameManager.Inst.currentplace.element)
        {
            case Element.Wood:
                gameObject.GetComponent<MeshFilter>().mesh = Resources.Load<Mesh>("tree-oak_T");
                gameObject.GetComponent<MeshRenderer>().sharedMaterial = Resources.Load<Material>("LOW-POLY-COLORS");
                break;
            case Element.Stone:
                gameObject.GetComponent<MeshFilter>().mesh = Resources.Load<Mesh>("Rock_Round_m_2C_01");
                break;
            case Element.Grass:
                gameObject.GetComponent<MeshFilter>().mesh = Resources.Load<Mesh>("grass05_T");
                gameObject.GetComponent<MeshRenderer>().sharedMaterial = Resources.Load<Material>("LOW-POLY-COLORS");
                break;
        }

        for (int i = 0; i < GameDataBase.Inst.cards.Count; i++)
        {
            Item card = GameDataBase.Inst.cards[i];
            if (card.cardElement == GameManager.Inst.currentplace.element || card.cardElement == Element.Chaos)
            {
                cardobjects.Add(GameDataBase.Inst.cards[i]);
                switch (card.cardRank)
                {
                    case 1:
                        bronzcardnum++;
                        break;
                    case 2:
                        silvercardnum++;
                        break;
                    case 3:
                        goldcardnum++;
                        break;
                    default:
                        break;
                }
            }
        }
        switch (GameManager.Inst.objectrank)
        {
            case 1:
                nomalbronzepercent += 5000;
                break;
            case 2:
                nomalsilverpercent += 5000;
                break;
            case 3:
                nomalgoldpercent += 5000;
                break;
        }
        bronzecardsolopercent = nomalbronzepercent / bronzcardnum;
        silvercardsolopercent = nomalsilverpercent / silvercardnum;
        goldcardsolopercent = nomalgoldpercent / goldcardnum;
        maxcount = bronzecardsolopercent * bronzcardnum+ nomalsilverpercent*silvercardnum+nomalgoldpercent*goldcardnum;

        cardobjects.Sort(delegate (Item A, Item B)
        {
            if (A.cardElement > B.cardElement) return 1;
            else if (A.cardElement < B.cardElement) return -1;
            return 0;
        });

        gameObject.AddComponent<CapsuleCollider>();
    }
    private void OnMouseDown()
    {
        if (animationset)
        {
            animationset = false;

            count++;
            if (count >= maxtouchcount)
            {

                insertDB();
            }
            Invoke("touchinvoke",0.5f);
        }
    }
    void touchinvoke()
    {
        animationset = true;
    }
    int insertDB()
    {

        for (int i=1;i<=bronzcardnum ;i++ )
        {
            if(bronzecardsolopercent*i>randomcardnumber)
            {
                Debug.Log(bronzecardsolopercent * i +","+ randomcardnumber);
                Debug.Log(cardobjects[i - 1].cardName);
                insertList(i - 1);
                Destroy(gameObject);
                return 0;
            }
        }

        for (int i = 1; i <= silvercardnum; i++)
        {
            if ((silvercardsolopercent * i)+(bronzecardsolopercent*bronzcardnum) > randomcardnumber)
            {
                Debug.Log(bronzecardsolopercent * i + randomcardnumber);
                Debug.Log(cardobjects[i - 1+bronzcardnum].cardName);
                insertList(i - 1+bronzcardnum);
                Destroy(gameObject);
                return 0;
            }
        }

        for (int i = 1; i <= goldcardnum; i++)
        {
            if ((goldcardsolopercent * i)+ (silvercardsolopercent * silvercardnum) + (bronzecardsolopercent * bronzcardnum) > randomcardnumber)
            {
                Debug.Log(bronzecardsolopercent * i + randomcardnumber);
                Debug.Log(cardobjects[i - 1 + bronzcardnum + silvercardnum].cardName);
                insertList(i - 1 + bronzcardnum + silvercardnum);
                Destroy(gameObject);
                return 0;
            }
        }
       // Debugs.text = "조짐"+ randomcardnumber+ (goldcardsolopercent * goldcardnum) + (silvercardsolopercent * silvercardnum) + (bronzecardsolopercent * bronzcardnum);
        return 0;
    }
    public void insertList(int num)
    {
        GameDataBase.Inst.Ds_InventoryInsert(cardobjects[num].cardInventoryNum);
        Item cardinfo = null;
        foreach (Item card in GameDataBase.Inst.cards)
        {
            if (card.cardName == cardobjects[num].cardName)
            {
                cardinfo = cardobjects[num];
            }
        }
        cardinfosprite.spriteName = cardinfo.cardName;
    }
}
