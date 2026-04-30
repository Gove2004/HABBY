using GoveKits.Runtime.Core;
using UnityEngine;

public class MyInputManager : MonoSingleton<MyInputManager>
{
    public Vector2 InputDirection { get; private set; }
    public Vector2 MousePosition => Input.mousePosition;
    public bool IsMouseClicked => Input.GetMouseButtonDown(0);  // 左键点击
    public bool IsMouseHeld => Input.GetMouseButton(0);  // 左键持续按下
    public bool IsMouseReleased => Input.GetMouseButtonUp(0);  // 左键释放
    public bool IsSkillUsed => Input.GetMouseButtonDown(1);  // 右键点击
    public bool IsSkillHeld => Input.GetMouseButton(1);  // 右键持续按下
    public bool IsSkillReleased => Input.GetMouseButtonUp(1);  // 右键释放

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        InputDirection = new Vector2(horizontal, vertical).normalized;
    }
}
