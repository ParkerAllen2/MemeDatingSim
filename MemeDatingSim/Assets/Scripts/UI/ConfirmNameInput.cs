using UnityEngine;
using UnityEngine.UI;

public class ConfirmNameInput : MonoBehaviour
{
    [SerializeField] InputField nameInput;

    public void Confirm()
    {
        if(nameInput.text.Equals(""))
        {
            return;
        }
        Overlord.Instance.player.playerName = nameInput.text;
        Overlord.Instance.LoadScene("Map");
    }
}
