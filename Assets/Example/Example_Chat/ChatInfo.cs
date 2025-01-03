using UnityEngine;
using UnityEngine.UI;

public class ChatInfo : MonoBehaviour
{
    public Text txtContent;

    public void SetInfo(string content)
    {
        txtContent.text = content;
    }
}
