using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class OverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtScore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void SetTextScore(int numberOfRightAns, int total)
    {
        txtScore.text = numberOfRightAns + " / " + total;
    }
    public void BtnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
