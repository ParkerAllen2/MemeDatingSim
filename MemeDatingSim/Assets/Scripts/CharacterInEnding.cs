using UnityEngine;

public class CharacterInEnding : MonoBehaviour
{
    public Character character;
    void Start()
    {
        if(character.affection < 0)
        {
            gameObject.SetActive(false);
        }
    }
}
