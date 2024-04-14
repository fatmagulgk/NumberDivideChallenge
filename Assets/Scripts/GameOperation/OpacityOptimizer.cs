using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OpacityOptimizer : MonoBehaviour
{
    public GameObject AlphaPanel;
    private void Start()
    {
        AlphaPanel.GetComponent<CanvasGroup>().DOFade(0, 2f);
    }
}
