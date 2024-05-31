﻿using System.Diagnostics;
using CopperDevs.Core;
using CopperDevs.Framework.Elements;
using CopperDevs.Framework.Elements.Systems;
using CopperDevs.Framework.Utility;

namespace CopperDevs.Framework;


public class Engine : Singleton<Engine>
{
    public static EngineWindow CurrentWindow => Instance.window;

    private readonly EngineWindow window;
    internal readonly EngineSettings Settings;

    public bool ShouldRun;

    private readonly Stopwatch stopwatch;

    public Action OnLoad = null!;

    public Engine() : this(EngineSettings.DefaultSettings)
    {
    }

    public Engine(EngineSettings settings)
    {
        stopwatch = Stopwatch.StartNew();
        
        CopperLogger.Initialize();
        ConsoleUtil.Initialize();

        SetInstance(this);

        Settings = settings;
        window = new EngineWindow(settings);

        ShouldRun = true;
        
        Log.Info($"Time elapsed during engine creation: {stopwatch.Elapsed}");
    }
    
    public void Run()
    {
        Start();

        while (!Raylib.WindowShouldClose() && ShouldRun)
            Update();

        Stop();
    }

    private void Start()
    {
        window.Start();
        
        ElementManager.Initialize();
        
        OnLoad?.Invoke();
        
        Log.Info($"Time elapsed starting the engine: {stopwatch.Elapsed}");
    }

    private void Update()
    {
        window.Update(() =>
            {
                ElementManager.Update(ElementManager.ElementUpdateType.Update);
                ElementManager.Update(ElementManager.ElementUpdateType.Render);

                if (DebugSystem.Instance.DebugEnabled)
                    ElementManager.Update(ElementManager.ElementUpdateType.Debug);
            }, () =>
            {
                //
                ElementManager.Update(ElementManager.ElementUpdateType.UiRender);
            },
            () =>
            {
                //
                ElementManager.Update(ElementManager.ElementUpdateType.Fixed);
            });
    }

    private void Stop()
    {
        Log.Info($"Time elapsed during the runtime of the engine: {stopwatch.Elapsed}");
        ElementManager.Shutdown();
        window.Shutdown();
    }

    public void SetWindowColor(Color color) => window.SetWindowColor(color);
    public void SetWindowShader(Shader shader) => window.SetScreenShader(shader);
    public void SetWindowShader(Shader.IncludedShaders includedShader) => window.SetScreenShader(Shader.Load(includedShader));
}