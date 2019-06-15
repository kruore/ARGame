using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreateManager : MonoBehaviour
{
    private Item m_playercontect;
    public Item playercontect
    {
        get
        {
            return m_playercontect;
        }
        set
        {
            Button.SetActive(true);
            m_playercontect=value;
        }
    }
    private GameObject m_CurrentCharactor;
    public GameObject CurrentCharactor
    {
        get { return m_CurrentCharactor; }
        set
        {
            if (value != m_CurrentCharactor)
            {
                if (m_CurrentCharactor != null)
                {
                    m_CurrentCharactor.GetComponent<PlayerCreateCharactor>().playAnimationIdle();
                }
                m_CurrentCharactor = value;
                m_CurrentCharactor.GetComponent<PlayerCreateCharactor>().playAnimationSuccess();
            }
        }
    }
    public static PlayerCreateManager Inst;
    public GameObject CardInfo,Button;
    private void Start()
    {
        Inst = this;
        GameObject Fadepanel = GameObject.Instantiate(Resources.Load<GameObject>("FadePanel"));
        Fadepanel.GetComponent<Animator>().Play("FadFanel__");
        Destroy(Fadepanel, 1f);
    }
    public void Activefalse()
    {
        UIButton.current.gameObject.SetActive(false);
    }
    public void DBinsert()
    {
        GameDataBase.Inst.Ds_InventoryInsert(playercontect.cardInventoryNum);
        PlayerPrefs.SetInt("Jewely", 0);
        PlayerPrefs.Save();
        Buttonmanager.Inst.gotoMainscene();
    }
}
