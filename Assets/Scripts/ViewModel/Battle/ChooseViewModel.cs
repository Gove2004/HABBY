

using GoveKits.Runtime.Core;
using GoveKits.Runtime.UI;

public class ChooseViewModel : ViewModel
{
    public BaseBuff chooseBuff1;
    public BaseBuff chooseBuff2;
    public BaseBuff chooseBuff3;


    public void Refresh()
    {
        // 刷新选择buff的逻辑
        (chooseBuff1, chooseBuff2, chooseBuff3) = BuffManager.Instance.GetThreePlayerBuffs();
    }

    public void Choose(int index)
    {
        switch (index)
        {
            case 1:
                // 处理选择1的逻辑
                chooseBuff1.Apply(VMContainer.Get<BattleViewModel>().playerCharacter);
                break;
            case 2:
                // 处理选择2的逻辑
                chooseBuff2.Apply(VMContainer.Get<BattleViewModel>().playerCharacter);
                break;
            case 3:
                // 处理选择3的逻辑
                chooseBuff3.Apply(VMContainer.Get<BattleViewModel>().playerCharacter);
                break;
            default:
                LogCore.Error(nameof(ChooseViewModel), $"未知的选择索引: {index}");
                break;
        }
    }
}