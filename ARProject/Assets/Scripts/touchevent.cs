using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class touchevent : MonoBehaviour
{
    private Touch playerTouch1;
    private Touch playerTouch2;
    private Vector2 touchedPos;
    public GameObject cameraaixs;
    private float touchInterval = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            switch (Input.touchCount)
            {
                case 1:
                    if (EventSystem.current.IsPointerOverGameObject(0) == false)
                    {
                        playerTouch1 = Input.GetTouch(0);
                        if (playerTouch1.phase == TouchPhase.Began)
                        {
                            touchedPos = playerTouch1.position;
                        }
                        else if (playerTouch1.phase == TouchPhase.Moved)
                        {
                            cameraaixs.transform.Rotate(new Vector3(0, (touchedPos.x - playerTouch1.position.x) * 0.25f, 0));
                            touchedPos = playerTouch1.position;
                        }
                    }

                    break;
                case 2:
                    playerTouch1 = Input.GetTouch(0);
                    playerTouch2 = Input.GetTouch(1);
                    if (playerTouch2.phase == TouchPhase.Began)
                    {
                        touchInterval = Mathf.Abs(playerTouch1.position.x - playerTouch2.position.x);
                    }
                    float currentInterval = Mathf.Abs(playerTouch1.position.x - playerTouch2.position.x);
                    float Intervala = Mathf.Abs(currentInterval - touchInterval) / 10;
                    if (currentInterval < touchInterval)
                    {
                        if (Camera.main.transform.localPosition.y + Intervala < 20)
                        {
                            Camera.main.GetComponent<cameralook>().Localpositiony= Camera.main.transform.localPosition.y + Intervala;
                        }
                        else
                        {
                            Camera.main.GetComponent<cameralook>().Localpositiony = 20;
                        }
                    }
                    else
                    {
                        if (Camera.main.transform.localPosition.y - Intervala > 5)
                        {
                            //Camera.main.transform.localPosition = new Vector3(0, Camera.main.transform.position.y - Intervala,-7);
                            Camera.main.GetComponent<cameralook>().Localpositiony = Camera.main.transform.localPosition.y - Intervala;
                        }
                        else
                        {
                            //Camera.main.transform.localPosition = new Vector3(0, 5, -7);
                            Camera.main.GetComponent<cameralook>().Localpositiony = 5;
                        }
                    }
                    if (playerTouch2.phase == TouchPhase.Ended|| playerTouch1.phase == TouchPhase.Ended)
                    {
                        touchedPos = playerTouch1.position;
                    }
                    touchInterval = Mathf.Abs(playerTouch1.position.x - playerTouch2.position.x);
                    break;
            }
        }
    }
}

