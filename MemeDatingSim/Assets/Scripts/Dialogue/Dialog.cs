using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public string sentence;
    public int expression;
    public int fontSize = 18;
}

[System.Serializable]
public class Option
{
    public int affectionGiven;
    public string option;
    public Sentence[] response;
    public Dialog nextDialog;
}
