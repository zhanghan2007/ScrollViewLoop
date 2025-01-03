using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LoopScrollRect))]
[DisallowMultipleComponent]
public class TestStart : MonoBehaviour, LoopScrollDataSource
{
    public int totalCount = -1;

    public void ProvideData(Transform transform, int idx)
    {
        transform.SendMessage("ScrollCellIndex", idx);
    }

    void Start()
    {
        var ls = GetComponent<LoopScrollRect>();
        ls.dataSource = this;
        ls.totalCount = totalCount;
        ls.RefillCells();
    }
}
