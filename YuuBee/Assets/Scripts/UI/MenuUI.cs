using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    private GameManager instanceGM;
    // Start is called before the first frame update
    void Start()
    {
        instanceGM = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BtnSubject(SctbSubject sctbSubject)
    {
        if (sctbSubject == null)
            return;
        instanceGM.TakeSubject(sctbSubject);
    }
}
