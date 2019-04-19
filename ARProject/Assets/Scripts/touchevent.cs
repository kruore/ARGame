using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class touchevent : MonoBehaviour
{
    private Touch PlayerTouch;
    private Vector2 touchedPos;
    public GameObject cameraaixs;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount>0)
        {
            if(EventSystem.current.IsPointerOverGameObject(0)==false)
            {
                PlayerTouch = Input.GetTouch(0);
                if (PlayerTouch.phase == TouchPhase.Began)
                {
                    touchedPos = PlayerTouch.position;
                }
                else if (PlayerTouch.phase == TouchPhase.Moved)
                {
                        cameraaixs.transform.Rotate(new Vector3(0, (touchedPos.x-PlayerTouch.position.x)*0.5f,0));
                    touchedPos = PlayerTouch.position;
                }
                else if(PlayerTouch.phase == TouchPhase.Ended)
                {
                    touchedPos = new Vector2(0,0);
                }
            }
        }
    }
}
