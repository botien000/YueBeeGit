using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtQuestion;
    [SerializeField] private TextMeshProUGUI[] txtAnswers;

    private int indexTxt;
    private void OnEnable()
    {
        indexTxt = -1;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetTextQuestion(string txt)
    {
        txtQuestion.text = txt;
    }
    public void SetTextAnswers(string answer)
    {
        indexTxt++;
        if (indexTxt == txtAnswers.Length)
            return;
        txtAnswers[indexTxt].text = AddText(indexTxt) + answer;
    }
    private string AddText(int i)
    {
        switch (i)
        {
            case 0:
                return "a.";
            case 1:
                return "b.";
            case 2:
                return "c.";
            case 3:
                return "d.";
            default:
                return "";
        }
    }
}
