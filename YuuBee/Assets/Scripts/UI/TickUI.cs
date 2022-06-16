using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TickUI : MonoBehaviour
{
    [SerializeField] private Sprite sptR, sptW;
    [SerializeField] private Image imgLessionPrefab;
    [SerializeField] private OverUI overUI;

    private int numberOfRightAns;
    private List<Image> imgLession = new List<Image>();
    private GameManager instanceGM;
    // Start is called before the first frame update
    void Start()
    {
        numberOfRightAns = 0;
        instanceGM = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(instanceGM.CurStateGame == GameManager.StateGame.OverGame)
        {
            overUI.SetTextScore(numberOfRightAns, imgLession.Count);
        }
    }

    public void SetTickOfLession(int count)
    {
        for (int i = 0; i < count; i++)
        {
            imgLession.Add(Instantiate(imgLessionPrefab, transform));
        }
    }

    public void SetTick(bool right, int indexLession)
    {
        //imgLession[indexLession].sprite = right ? sptR : sptW;
        if (right)
        {
            numberOfRightAns++;
            imgLession[indexLession].sprite = sptR;
        }
        else
        {
            imgLession[indexLession].sprite = sptW;
        }
    }
}
