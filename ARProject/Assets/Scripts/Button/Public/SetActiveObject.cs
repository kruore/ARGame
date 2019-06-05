using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveObject : MonoBehaviour
{
    public List<GameObject> activeObject;
    public List<GameObject> deactiveObject;

    public void ObjectControl()
    {
        foreach (GameObject obj in activeObject)
        {
            obj.SetActive(true);
        }
        foreach (GameObject obj in deactiveObject)
        {
            obj.SetActive(false);
        }
    }
}
