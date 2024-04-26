﻿using CopperCore.Data;
using CopperDearImGui.Attributes;

namespace CopperFramework.Data;

public class EngineSettings
{
    [HideInInspector] public ConfigFlags WindowFlags = ConfigFlags.Msaa4xHint | ConfigFlags.VSyncHint |
                                                       ConfigFlags.ResizableWindow | ConfigFlags.AlwaysRunWindow;

    public Vector2Int WindowSize = new(650, 400);
    [Range(-1, 10000)] public int TargetFps = 60;
    public string WindowTitle = "Window";
    public bool DisableDevTools = true;
    public bool EnableDevToolsAtStart;

    public static EngineSettings DefaultSettings => new()
    {
        WindowFlags = ConfigFlags.Msaa4xHint | ConfigFlags.VSyncHint | ConfigFlags.ResizableWindow |
                      ConfigFlags.AlwaysRunWindow,
        WindowSize = new Vector2Int(650, 400),
        TargetFps = 60,
        WindowTitle = "CopperFramework - Default",
        DisableDevTools = true,
        EnableDevToolsAtStart = false
    };

    public static EngineSettings UncappedFps => new()
    {
        WindowFlags = ConfigFlags.Msaa4xHint | ConfigFlags.ResizableWindow | ConfigFlags.AlwaysRunWindow,
        WindowSize = new Vector2Int(650, 400),
        TargetFps = 10000,
        WindowTitle = "CopperFramework - Uncapped Fps",
        DisableDevTools = true,
        EnableDevToolsAtStart = false
    };

    public static EngineSettings Development => new()
    {
        WindowFlags = ConfigFlags.Msaa4xHint | ConfigFlags.ResizableWindow | ConfigFlags.AlwaysRunWindow,
        WindowSize = new Vector2Int(650, 400),
        TargetFps = 10000,
        WindowTitle = "CopperFramework - Development",
        DisableDevTools = false,
        EnableDevToolsAtStart = true
    };
}