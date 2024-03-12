﻿using CopperCore;
using CopperFramework.Data;
using CopperFramework.Elements;
using CopperFramework.Elements.Components;
using CopperPlatformer.Core.Utility;
using Raylib_cs;

namespace CopperFramework;

public class Engine : Singleton<Engine>
{
    private EngineWindow window;
    public EngineSettings Settings { get; private set; }

    public Engine() : this(new EngineSettings())
    {
    }

    public Engine(EngineSettings settings)
    {
        CopperLogger.Initialize();
        ConsoleUtil.Initialize();

        SetInstance(this);

        Settings = settings;
        window = new EngineWindow();
    }

    public void Run()
    {
        Start();
        
        while (!Raylib.WindowShouldClose()) 
            Update();
        
        Stop();
    }

    private void Start()
    {
        window.Start();

        ElementManager.Initialize();
    }

    private void Update()
    {
        window.Update(() =>
            {
                ElementManager.Update(ElementManager.ElementUpdateType.Update);
                ElementManager.Update(ElementManager.ElementUpdateType.Render);
            }, () =>
            {
                ElementManager.Update(ElementManager.ElementUpdateType.UiRender);
            },
            () =>
            {
                ComponentRegistry.CurrentComponents.ToList().ForEach(component => component.FixedUpdate());
            });
    }

    private void Stop()
    {
        ElementManager.Shutdown();
        window.Shutdown();
    }
}