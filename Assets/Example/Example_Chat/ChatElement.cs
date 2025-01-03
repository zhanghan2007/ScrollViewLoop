using UnityEngine;
using UnityEngine.UI;

public class ChatElement : MonoBehaviour
{
    public ChatInfo left;
    public ChatInfo right;
    public RectTransform rectTransform;

    public void SetInfo(string content, bool isLeft)
    {
        left.gameObject.SetActive(isLeft);
        right.gameObject.SetActive(!isLeft);
        var c = isLeft? left : right;
        c.SetInfo(content);
        
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
    }
}
