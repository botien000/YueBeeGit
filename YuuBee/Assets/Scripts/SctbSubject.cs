using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Subjects", menuName = "ScriptableObject/Create subject")]
public class SctbSubject : ScriptableObject
{
    public enum Subject
    {
        C_Sharp,Java,C,Python
    }
    public Subject curSubject;
    public List<Lession> lessions;
}
[System.Serializable]
public class Lession
{
    public string question;
    public string rightAnswer;
    public List<string> otherAnswer;
}
