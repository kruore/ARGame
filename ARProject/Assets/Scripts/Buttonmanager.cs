using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Buttonmanager : MonoBehaviour
{
    public void aabuttonclick()
    {
        SceneManager.LoadScene("mapscene");
    }
    public void bbbutonclick()
    {
        SceneManager.LoadScene("Inventory");
    }
    public void InventoryToggle()
    {
        Debug.Log(UIToggle.current.value.ToString());
        if (UIToggle.current.value == false) return;
    }
    public void ToggleChange()
    {
        //gameObject.GetComponent<UIToggle>().c
    }
}
