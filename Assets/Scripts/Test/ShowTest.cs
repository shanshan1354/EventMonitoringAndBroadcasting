using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTest : MonoBehaviour
{
    void Awake()
    {
        gameObject.SetActive(false);
        EventCenter.AddListener(EventType.ShowTest, Show);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventType.ShowTest, Show);
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
}
