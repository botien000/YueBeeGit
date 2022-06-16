using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum StateGame
    {
        MenuGame, PlayGame, QuestionPanelGame, OverGame
    }

    [SerializeField] private float timeQuestion;
    [SerializeField] private int numberOfQuestions;
    [SerializeField] private MenuUI menuUI;
    [SerializeField] private QuestionUI questionUI;
    [SerializeField] private OverUI overUI;
    [SerializeField] private GameObject playUI;
    [SerializeField] private TickUI tickUI;
    [SerializeField] private AnswerManager prefabAnsManager;
    [SerializeField] private Transform posSpawnAnswer;

    private Player player;
    private StateGame curStateGame;
    private float curTimeQuestion;
    private List<Lession> lessions;
    private int indexLession, indexRight;
    //Singleton
    public static GameManager instance;

    public StateGame CurStateGame { get => curStateGame; set => curStateGame = value; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            instance = null;
        }
        lessions = new List<Lession>();
    }
    // Start is called before the first frame update
    void Start()
    {
        indexLession = -1;
        curTimeQuestion = timeQuestion;
        player = FindObjectOfType<Player>();
        SetState(StateGame.MenuGame);
    }
    // Update is called once per frame
    void Update()
    {
        if (CurStateGame == StateGame.QuestionPanelGame)
        {
            curTimeQuestion -= Time.deltaTime;
            if (curTimeQuestion <= 0)
            {
                curTimeQuestion = timeQuestion;
                //stateplaygame
                SetState(StateGame.PlayGame);
            }
        }
    }
    //Function này để chuyển sang câu hỏi tiếp
    private void NextLession()
    {
        if (lessions.Count == 0)
        {
            Debug.LogError("Không tìm thấy câu hỏi");
            return;
        }
        //index phần câu hỏi-trả lời 
        indexLession++;
        if (indexLession == lessions.Count)
        {
            //GameOver
            SetState(StateGame.OverGame);
            return;
        }   
        //Thực hiện show câu hỏi-trả lời
        ShowQuestion(lessions[indexLession].question);
        ShowAnswer(lessions[indexLession].rightAnswer, lessions[indexLession].otherAnswer);
    }

    private void ShowAnswer(string rightAnswer, List<string> otherAnswer)
    {
        indexRight = Mathf.RoundToInt(Random.Range(0f, otherAnswer.Count));
        int indexWrong = -1;
        for (int i = 0; i < otherAnswer.Count + 1; i++)
        {
            if (i != indexRight)
            {
                indexWrong++;
                //show wrongAnswer to UI
                questionUI.SetTextAnswers(otherAnswer[indexWrong]);
            }
            else
            {
                //show rightAnswer to UI
                questionUI.SetTextAnswers(rightAnswer);
            }
        }
    }

    private void ShowQuestion(string question)
    {
        questionUI.SetTextQuestion(question);
    }

    /// <summary>
    /// Function này lấy ScriptableObj Subject khi người dùng nhận nút chọn một môn nào đó (Java,C#,...) 
    /// </summary>
    /// <param name="sctbSubject"></param>
    public void TakeSubject(SctbSubject sctbSubject)
    {
        //xáo trộn câu hỏi
        MixQuestion(sctbSubject);
        SetState(StateGame.QuestionPanelGame);
    }
    private void MixQuestion(SctbSubject subject)
    {
        //Kiểm tra số lượng câu hỏi
        if (subject.lessions.Count < numberOfQuestions)
        {
            Debug.LogError("Không đủ số lượng câu hỏi đã yêu cầu.Fix Please");
            return;
        }
        //Trả về 0
        lessions.Clear();
        int indexSubject;
        //vòng lặp for nhận lession
        for (int i = 0; i < numberOfQuestions; i++)
        {
            //tại đây sẽ xử lý random lấy câu hỏi ngẫu nhiên từ subject mà không bị trùng với list của lessions
            do
            {
                indexSubject = Mathf.RoundToInt(Random.Range(0f, subject.lessions.Count - 1));
            } while (lessions.Contains(subject.lessions[indexSubject]));
            lessions.Add(subject.lessions[indexSubject]);
        }
        tickUI.SetTickOfLession(lessions.Count);

    }
    public void GetInteractAnswer(bool right)
    {
        tickUI.SetTick(right, indexLession);
        if(indexLession == lessions.Count - 1)
        {
            SetState(StateGame.QuestionPanelGame);
            return;
        }
        StartCoroutine("TimeWait");
    }
    IEnumerator TimeWait()
    {
        yield return new WaitForSeconds(3f);
        if (curStateGame != StateGame.OverGame)
        {
            SetState(StateGame.QuestionPanelGame);
        }
        StopAllCoroutines();
    }
    #region State
    /// <summary>
    /// Function dùng để thay đổi trạng thái game
    /// </summary>
    /// <param name="state"></param>
    public void SetState(StateGame state)
    {
        CurStateGame = state;
        switch (state)
        {
            case StateGame.MenuGame:
                player.SetGravityScale(0);
                break;
            case StateGame.PlayGame:
                questionUI.gameObject.SetActive(false);
                player.SetGravityScale(1);
                AnswerManager answerManager = Instantiate(prefabAnsManager, posSpawnAnswer.transform.position, Quaternion.identity, playUI.transform).GetComponent<AnswerManager>();
                answerManager.Init(lessions[indexLession], indexRight);
                break;
            case StateGame.QuestionPanelGame:
                menuUI.gameObject.SetActive(false);
                questionUI.gameObject.SetActive(true);
                player.SetGravityScale(0);
                NextLession();
                break;
            case StateGame.OverGame:
                overUI.gameObject.SetActive(true);
                questionUI.gameObject.SetActive(false);
                player.SetGravityScale(1);
                break;
        }
    }
    #endregion

}
