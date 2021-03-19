using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScriptReader))]
public class UIController : MonoBehaviour
{
    public float typingSpeed = .02f;
    [HideInInspector] public bool isTyping;

    public Text nameTag;
    public Text dialogBox;
    public GameObject dialogPanel;

    public GameObject optionPrefab;
    public GameObject optionsPanel;
    Button[] optionButtons;

    public Image portraitPrefab;
    public Transform[] imagePositions;
    Dictionary<string, Image> stage;
    Character speaker;


    public ScriptReader scriptReader;

    private void Awake()
    {
        scriptReader = GetComponent<ScriptReader>();
        stage = new Dictionary<string, Image>();
        optionButtons = new Button[0];
    }

    public Character Speaker
    {
        get { return speaker; }
        set
        {
            if (!stage.ContainsKey(value.characterName))
            {
                stage.Add(value.characterName, Instantiate(portraitPrefab, imagePositions[0]));
            }
            speaker = value;
        }
    }

    public void ChangeExpression(int expression)
    {
        stage[speaker.characterName].sprite = speaker.expressions[expression];
    }

    public void MoveCharacter(int position)
    {
        stage[speaker.characterName].transform.position = imagePositions[position].position;
    }

    public void ChangeAffection(int amount)
    {
        speaker.affection += amount;
    }

    public void CompleteSentence(string words)
    {
        dialogBox.text += words;
    }

    public void StartTyping(string word)
    {
        dialogBox.text += word;
        StartCoroutine(TypeSentence());
    }

    IEnumerator TypeSentence()
    {
        yield return new WaitForSeconds(typingSpeed);
        scriptReader.ReadNextWord();
    }

    public void CreateButton(Response[] responses)
    {

    }

    public void ClearButtons()
    {
        foreach (Button b in optionButtons)
        {
            Destroy(b.gameObject);
        }
        optionButtons = new Button[0];
    }
}
