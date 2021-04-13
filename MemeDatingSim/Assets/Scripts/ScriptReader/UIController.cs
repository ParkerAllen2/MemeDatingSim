using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [HideInInspector] public bool isTyping;
    bool waitForResponse;

    public Text nameTag;
    public Text dialogBox;
    public TextBoxColorer TextBoxColorer;

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

    ParticleSystem[] affectionParticles;

    private void Awake()
    {
        scriptReader = GetComponentInParent<ScriptReader>();
        actManager = GetComponentInParent<ActManager>();
        stage = new Dictionary<string, Image>();
        optionButtons = new Button[0];
        affectionParticles = GetComponentsInChildren<ParticleSystem>();
    }

    private void Start()
    {
        ClearButtons();
    }

    //gets the speaker
    //adds changes speaker, name tag, text box style and add character to stage
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
            TextBoxColorer.ChangeColors(speaker.textboxColors);
            ChangeExpression(0);
        }
    }

    //changes expression of speaker to given int
    public void ChangeExpression(int expression)
    {
        if(expression >= speaker.expressions.Length)
        {
            scriptReader.ScriptError("Array out of Bounds");
        }
        stage[speaker.characterName].sprite = speaker.expressions[expression];
    }

    /*
     * move speaker to position
     * 0 = off stage
     * 1 = left
     * 2 = center
     * 3 = right
     */
    public void MoveCharacter(int position)
    {
        if (position >= imagePositions.Length)
        {
            scriptReader.ScriptError("Array out of Bounds");
        }
        stage[speaker.characterName].rectTransform.SetParent(imagePositions[position]);
        stage[speaker.characterName].rectTransform.anchoredPosition = Vector3.zero;
    }

    //shakes speaker based on values in command
    public void StartShakeCharacter(float mag, float dur)
    {
        StartCoroutine(ShakeCharacter(mag, dur));
    }

    IEnumerator ShakeCharacter(float mag, float dur)
    {
        RectTransform t = stage[speaker.characterName].rectTransform;
        Vector3 origin = t.anchoredPosition;
        float elapsed = 0;
        while(elapsed < dur)
        {
            float x = Random.Range(-1f, 1f) * mag;
            t.anchoredPosition = new Vector3(x, origin.y);
            yield return null;
            elapsed += Time.deltaTime;
        }
        t.anchoredPosition = origin;
    }

    //speaker hops based on values in command
    public void StartCharacterHop(float gravity, float jumpForce)
    {
        StartCoroutine(CharacterHop(gravity, jumpForce));
    }

    IEnumerator CharacterHop(float gra, float force)
    {
        RectTransform t = stage[speaker.characterName].rectTransform;
        Vector3 origin = t.anchoredPosition;
        float f = force + gra;
        float y = origin.y + f;
        while (y > origin.y)
        {
            t.anchoredPosition = new Vector3(origin.x, y);
            yield return null;
            f += gra;
            y += f;
        }
        t.anchoredPosition = origin;
    }

    public void ChangeBackground(string back)
    {
        if(!actManager.HasBackground(back, ref background))
        {
            scriptReader.ScriptError("Background does not exsits");
        }
    }
    
    /*
     * Return if waiting for player response and player clicks text box
     * When text box click either:
     * Stop typing and finish sentence was typing
     * Or Start Typing
     */
    public void ClickTextBox(bool playerClick = false)
    {
        if(waitForResponse && playerClick)
        {
            return;
        }
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

    //Instantly types sentence so not to skip commads
    void CompleteSentence()
    {
        while (isTyping)
        {
            isTyping = scriptReader.TypeNextWord();
        }
    }

    /*
     * Load next line
     * Keep last sentence if told to stop typing
     *      ^ Needed to keep last question up with the responses
     * Else Type sentence
     */
    IEnumerator TypeSentence()
    {
        scriptReader.ReadNextLine();

        string lastLine = dialogBox.text;
        dialogBox.text = "";
        if (!(isTyping = scriptReader.TypeNextWord()))
        {
            dialogBox.text = lastLine;
        }
        while (isTyping = scriptReader.TypeNextWord())
        {
            yield return new WaitForSeconds(Overlord.Instance.player.textSpeed);
        }
    }

    //Used for script reader to type to text box
    public void StartTyping(string word)
    {
        dialogBox.text += word;
    }

    //Creates a button for eache response given
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

            //add onclick lisenter to read commands that were in the option
            string[] arr = responses[i].commands.ToArray();
            optionButtons[i].onClick.AddListener(delegate { scriptReader.ReadArray(arr); });
        }
        waitForResponse = true;
    }

    //Clear buttons after response selected
    public void ClearButtons()
    {
        foreach (Button b in optionButtons)
        {
            Destroy(b.transform.parent.gameObject);
        }
        optionButtons = new Button[0];
        waitForResponse = false;
    }

    public Image GetImageOfCharacter(string characterName)
    {
        return stage[characterName];
    }

    public Image GetImageOfCharacter()
    {
        return stage[speaker.characterName];
    }

    public ParticleSystem[] AffectionParticles
    {
        get { return affectionParticles; }
    }
}
