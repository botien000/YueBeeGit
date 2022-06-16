using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswerManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] ansObjs;

    private int numberOfInteract;
    private OffsetRoad offsetRoad;
    private Answer[] answers;
    // Start is called before the first frame update
    void Start()
    {
        numberOfInteract = 0;
        offsetRoad = FindObjectOfType<OffsetRoad>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * (offsetRoad.GetValue() / 0.05f);
    }

    /// <summary>
    /// Function thực hiện khi khởi tạo Object
    /// </summary>
    /// <param name="lessions"></param>
    public void Init(Lession lessions, int indexRight)
    {
        answers = new Answer[lessions.otherAnswer.Count + 1];
        int indexR = Mathf.RoundToInt(Random.Range(0, answers.Length - 1));
        if (ansObjs.Length != answers.Length)
        {
            Debug.LogError("Không đủ số lượng answer");
            return;
        }
        List<int> indexWrongs = new List<int>();
        //Ngẫu nhiên vị trí câu hỏi
        for (int indexW = 0; indexW < answers.Length; indexW++)
        {
            if (indexW == indexR)
            {
                answers[indexR] = new Answer(indexRight, true);
                ansObjs[indexR].text = ConvertToString(indexRight);
                continue;
            }
            int indexWrong;
            do
            {
                indexWrong = Mathf.RoundToInt(Random.Range(0f, answers.Length - 1));
            } while (indexWrong == indexRight || indexWrongs.Contains(indexWrong));
            indexWrongs.Add(indexWrong);
            answers[indexW] = new Answer(indexWrong, false);
            ansObjs[indexW].text = ConvertToString(indexWrong);
        }
    }

    public void Interact(GameObject _gameObject)
    {
        numberOfInteract++;
        if (numberOfInteract > 1)
            return;
        for (int i = 0; i < ansObjs.Length; i++)
        {
            if (ansObjs[i].GetComponentInParent<Image>().gameObject == _gameObject)
            {
                if (answers[i].rightAnswer)
                {
                    GameManager.instance.GetInteractAnswer(true);
                }
                else
                {
                    GameManager.instance.GetInteractAnswer(false);
                }
                break;
            }
        }
        Destroy(gameObject);
    }

    private string ConvertToString(int index)
    {
        switch (index)
        {
            case 0:
                return "A";
            case 1:
                return "B";
            case 2:
                return "C";
            case 3:
                return "D";
            default:
                return "";
        }
    }

    public class Answer
    {
        public int sentence;
        public bool rightAnswer;

        public Answer(int sentence, bool rightAnswer)
        {
            this.sentence = sentence;
            this.rightAnswer = rightAnswer;
        }
    }
}
