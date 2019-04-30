using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardForm : MonoBehaviour
{
    public bool Front = false;

    public GameObject Card;
    public GameObject CardPos;
    public GameObject prefab;
    public GameObject PlayerFieldSpawnPos;

    Vector3 Currentpos;
    Vector3 CurrentRot;
    private void Awake()
    {
        Card = Resources.Load("Card") as GameObject;
        CardPos = GameObject.Find("UI Root/CardPos");
        PlayerFieldSpawnPos = GameObject.Find("PlayerField/PlayerPos");
        prefab = Resources.Load("Misaki_SchoolUniform_summer") as GameObject;

        Currentpos = gameObject.transform.position;
        CurrentRot = gameObject.GetComponent<Transform>().transform.eulerAngles;
    }


    void CardReverse()
    {
        if (Front == false)
        {
            gameObject.GetComponent<UISprite>().spriteName = "다운로드";
        }
        else if (Front == true)
        {
            gameObject.GetComponent<UISprite>().spriteName = "C_108634010";
        }
    }

    public void CardUnitCareater()
    {
       
        if (gameObject.transform.localPosition.y > 300.0f)
        {
            Instantiate(prefab, PlayerFieldSpawnPos.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (gameObject.transform.localScale.y < 300.0f)
        {
            gameObject.transform.position = Currentpos;
            gameObject.transform.rotation = Quaternion.Euler(CurrentRot);
        }
        gameObject.GetComponent<UISprite>().depth = 2;
    }
}
