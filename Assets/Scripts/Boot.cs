using GoveKits.Runtime.Core;
using GoveKits.Runtime.Storage;
using GoveKits.Runtime.UI;


public class GoveKitsManager : MonoSingleton<GoveKitsManager>
{

    #region 生命周期

    private void Awake() => Initialize();
    private void Initialize()
    {
        // Dependency
        // YooAssets.Initialize(new YooLogger());

        // Core
        LogCore.InfuseLogger(new UnityLogger());
        // RandomCore.Initialize(new NormalRNG(Environment.TickCount));
        // TimeCore.Initialize(16, 128);
        // TimeCore.RigisterWheel(TimeCore.NormalWheelName, 0.05f, 512);
        // TimeCore.RigisterWheel(TimeCore.UnscaledWheelName, 0.05f, 512);

        // Storage
        // 1. 资源热更新
        // await ResCore.PackageWorkflowAsync(new AutoOfflinePackageConfig("DefaultPackage"), new UpdateCallbacks());
        // 2. 加载 AOT 泛型元数据
        // await HotfixCore.LoadAotMetadataAsync({ "XXX.dll", "YYY.dll" });
        // 3. 加载 热更新 程序集
        // await HotfixCore.LoadHotfixAssemblyAsync("Hotfix.dll");
        // 4. 加载资源
        ConfigCore.InfuseParser(new JsonConfigParser());
        ConfigCore.InfuseParser(new CsvConfigParser());
        // ConfigCore.Initialize();
        // 5. 使用资源
        // LocalizationCore.Initialize();
        AudioCore.Initialize(16);
        SaveCore.Initialize(new JsonSerializer());

        // Log
        LogCore.Success(nameof(GoveKitsManager), "Bootstrap completed.");




        // 正式开启场景加载
        VMContainer.Get<GameViewModel>().Initialize();
        SceneCore.Load("Home", UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }


    private void Update()
    {
        // // 驱动时间轮
        // TimeCore.Update(TimeCore.NormalWheelName, UnityEngine.Time.deltaTime);
        // TimeCore.Update(TimeCore.UnscaledWheelName, UnityEngine.Time.unscaledDeltaTime);
    }


    private void OnApplicationQuit()
    {

    }

    #endregion
}

