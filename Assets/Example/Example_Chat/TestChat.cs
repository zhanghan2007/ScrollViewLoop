using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestChat : MonoBehaviour, LoopScrollDataSource
{
    public class ChatData
    {
        public string content;
        public bool isLeft;
    }

    public LoopVerticalScrollRect osaAdapterDifferentSize;
    public int initNum = 100;

    private List<string> _randomChatContent;
    private List<ChatData> _chatDatas;

    void Start()
    {
        _randomChatContent = new List<string>
        {
            "左侧短左侧短左侧短", 
            "左侧长左侧长左侧长左侧长左侧长左侧长左侧长左侧长左侧长左侧长左侧长左侧长左侧长左侧长左侧长左侧长左侧长左侧长左侧长左侧长左侧长左侧长左侧长左侧长左侧长左侧长左侧长左侧长左侧长左侧长左侧长左侧长左侧长左侧长", 
            "右侧短右侧短右侧短", 
            "右侧长右侧长右侧长右侧长右侧长右侧长右侧长右侧长右侧长右侧长右侧长右侧长右侧长右侧长右侧长右侧长右侧长右侧长右侧长右侧长右侧长右侧长右侧长右侧长右侧长右侧长右侧长右侧长右侧长右侧长右侧长右侧长右侧长右侧长"
        };
        
        _chatDatas = new List<ChatData>();
        for (int i = 0; i < initNum; i++)
        {
            var index = Random.Range(0, _randomChatContent.Count);
            var content = _randomChatContent[index];
            _chatDatas.Add(new ChatData {content = $"{i+1}.{content}", isLeft = index <= 1});
        }
        osaAdapterDifferentSize.dataSource = this;
        osaAdapterDifferentSize.totalCount = _chatDatas.Count;
        osaAdapterDifferentSize.RefillCells();
    }
    
    public void ProvideData(Transform transform, int idx)
    {
        transform.GetComponent<ChatElement>().SetInfo(_chatDatas[idx].content, _chatDatas[idx].isLeft);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _chatDatas.Add(new ChatData {content = $"自己的消息：{_randomChatContent[Random.Range(0, _randomChatContent.Count)]}", 
                isLeft = false});
            osaAdapterDifferentSize.totalCount = _chatDatas.Count;
            osaAdapterDifferentSize.ScrollToCell(0,0);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            osaAdapterDifferentSize.ScrollToCell(30, 0);
        }
    }
}
