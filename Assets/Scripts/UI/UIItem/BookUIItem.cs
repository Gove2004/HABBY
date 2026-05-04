using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BookUIItem : MonoBehaviour
{
    [SerializeField] private Button itemBtn;
    [SerializeField] private TextMeshProUGUI nameTMP;

    public event System.Action<BookItem> OnClick;

    public void SetData(BookItem data)
    {
        nameTMP.text = data.Name;
        itemBtn.onClick.RemoveAllListeners();
        itemBtn.onClick.AddListener(() => OnClick?.Invoke(data));
    }
}
