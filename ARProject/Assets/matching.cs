using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class matching : MonoBehaviour
{
    public UILabel matchinguILabel;
    private void Start()
    {
        StartCoroutine("MatchingStart");
    }

    public IEnumerator MatchingStart()
    {
        matchinguILabel.GetComponent<UILabel>().text = "matching.";
        yield return new WaitForSeconds(0.5f);
        matchinguILabel.GetComponent<UILabel>().text = "matching..";
        yield return new WaitForSeconds(0.5f);
        matchinguILabel.GetComponent<UILabel>().text = "matching...";
        yield return new WaitForSeconds(0.5f);
        StopCoroutine("MatchingStart");
        //오브젝트 삭제하기;
        this.Start();
    }
}
