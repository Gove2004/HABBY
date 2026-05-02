


# 环境搭建

## 引擎版本
- Unity **6.0.X**

## 环境依赖 (Package Manager)
编辑项目目录下的 `Packages/manifest.json` 文件，在 `dependencies` 中添加以下内容。
> **说明**：其中 `YooAsset`、`HybridCLR`、`UniTask` 为 `GoveKits` 的前置依赖，即使项目中未直接调用也必须保留。

```json
{
  "dependencies": {
    "com.gove.kits": "https://github.com/Gove2004/GoveKits.git",
    "com.tuyoogame.yooasset": "https://github.com/tuyoogame/YooAsset.git?path=Assets/YooAsset#2.3.18",
    "com.code-philosophy.hybridclr": "https://github.com/focus-creative-games/hybridclr_unity.git",
    "com.cysharp.unitask": "https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask#2.5.0"
  }
}
```

## 第三方插件
- **DoTween**: 动画插件，请在 Asset Store 获取并导入。
- **TextMesh Pro (TMP)**: UI 字体插件，请通过菜单栏 `Window > TextMeshPro > Import TMP Essential Resources` 导入基础资源。