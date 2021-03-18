using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogUIController : MonoBehaviour
{
    public float typingSpeed = .02f;
    [HideInInspector] public bool isTyping;

    public Image[] imagePositions;
    public Text nameTag;
    public Text dialogBox;
    public GameObject dialogPanel;

    public GameObject optionPrefab;
    public GameObject optionsPanel;
    Button[] optionButtons;

    DialogManager dialogManager;

    Dictionary<string, int> stage;
    Character speaker;
    int speakerPosition;

    private void Awake()
    {
        stage = new Dictionary<string, int>();
        dialogManager = GetComponent<DialogManager>();
        optionButtons = new Button[0];

        foreach(Image i in imagePositions)
        {
            i.enabled = false;
        }
    }

    public void PlaceCharacter(Character character, int position)
    {
        if (!stage.ContainsKey(character.name))
        {
            stage.Add(character.name, position);
        }

        imagePositions[stage[character.name]].enabled = false;
        imagePositions[position].enabled = true;
        imagePositions[position].sprite = character.expressions[0];

        nameTag.text = character.characterName;

        speaker = character;
        speakerPosition = position;
    }

    public void CompleteSentence()
    {
        isTyping = false;
    }

    public void StartTypingSentence(Sentence sentence)
    {
        StartCoroutine(TypeSentence(sentence.sentence));
        imagePositions[speakerPosition].sprite = speaker.expressions[sentence.expression];
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogBox.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            if (!isTyping)
            {
                break;
            }
            dialogBox.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        dialogBox.text = sentence;
        isTyping = false;
    }

    public void CreateButtons(Option[] options)
    {
        int size = options.Length;
        optionButtons = new Button[size];

        for (int i = 0; i < size; i++)
        {
            int x = i;
            //Instatiant buttons as children of dialogue panel
            optionButtons[i] = Instantiate(optionPrefab, optionsPanel.transform).GetComponentInChildren<Button>();
            optionButtons[i].GetComponentInChildren<Text>().text = options[i].option;

            //add onclick lisenter to start next dialogue
            optionButtons[i].onClick.AddListener(delegate { dialogManager.SelectOption(x); });
        }
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
