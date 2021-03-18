using UnityEngine;

[CreateAssetMenu]
public class Dialog : ScriptableObject
{
    public Character character;
    public Sentence[] sentences;
    public Option[] options;
    public int characterPosition;
}

[System.Serializable]
public class Sentence
{
    [TextArea(2, 10)]
    public string sentence;
    public int expression;
}

[System.Serializable]
public class Option
{
    public int affectionGiven;
    public string option;
    public Sentence[] response;
    public Dialog nextDialog;
}
