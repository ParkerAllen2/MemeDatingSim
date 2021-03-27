using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public float typingSpeed = .02f;
    [HideInInspector] public bool isTyping;

    public Text nameTag;
    public Text dialogBox;
    public GameObject dialogPanel;

    public GameObject optionPrefab;
    public Transform optionsPanel;
    Button[] optionButtons;

    public Image portraitPrefab;
    public Transform[] imagePositions;
    Dictionary<string, Image> stage;
    Character speaker;

    public SpriteRenderer background;

    ActManager actManager;
    ScriptReader scriptReader;

    private void Awake()
    {
        scriptReader = GetComponentInParent<ScriptReader>();
        actManager = GetComponentInParent<ActManager>();
        stage = new Dictionary<string, Image>();
        optionButtons = new Button[0];
    }

    private void Start()
    {
        ClearButtons();
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
            nameTag.text = speaker.characterName;
            ChangeExpression(0);
        }
    }

    public void ChangeExpression(int expression)
    {
        if(expression >= speaker.expressions.Length)
        {
            scriptReader.ScriptError("Array out of Bounds");
        }
        stage[speaker.characterName].sprite = speaker.expressions[expression];
    }

    public void MoveCharacter(int position)
    {
        if (position >= imagePositions.Length)
        {
            scriptReader.ScriptError("Array out of Bounds");
        }
        stage[speaker.characterName].transform.SetParent(imagePositions[position]);
        stage[speaker.characterName].transform.localPosition = Vector3.zero;
    }

    public void ChangeAffection(int amount)
    {
        speaker.affection += amount;
    }

    public void ChangeBackground(string back)
    {
        if(actManager.HasBackground(back, background.sprite))
        {
            scriptReader.ScriptError("Background does not exsits");
        }
    }

    public void ClickTextBox()
    {
        StopCoroutine(TypeSentence());
        if (isTyping)
        {
            CompleteSentence();
            isTyping = false;
        }
        else
        {
            StartCoroutine(TypeSentence());
        }
    }

    void CompleteSentence()
    {
        while (isTyping)
        {
            isTyping = scriptReader.TypeNextWord();
        }
    }

    IEnumerator TypeSentence()
    {
        scriptReader.ReadNextLine();
        isTyping = true;
        dialogBox.text = "";
        while (isTyping)
        {
            isTyping = scriptReader.TypeNextWord();
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void StartTyping(string word)
    {
        dialogBox.text += word;
    }

    public void CreateButton(Response[] responses)
    {
        int size = responses.Length;
        optionButtons = new Button[size];

        for (int i = 0; i < size; i++)
        {
            int x = i;
            //Instatiant buttons as children of dialogue panel
            optionButtons[i] = Instantiate(optionPrefab, optionsPanel).GetComponentInChildren<Button>();
            optionButtons[i].GetComponentInChildren<Text>().text = responses[i].reply;

            string[] arr = responses[i].commands.ToArray();

            //add onclick lisenter to start next dialogue
            optionButtons[i].onClick.AddListener(delegate { scriptReader.ReadArray(arr); });
        }
    }

    public void ClearButtons()
    {
        foreach (Button b in optionButtons)
        {
            Destroy(b.transform.parent.gameObject);
        }
        optionButtons = new Button[0];
    }
}
