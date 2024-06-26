﻿using System.Diagnostics.CodeAnalysis;
using CopperDevs.DearImGui;

namespace CopperDevs.Framework.Rendering.DearImGui;

[SuppressMessage("Performance", "CA1822:Mark members as static")]
public class CopperRlImGui : IImGuiRenderer
{
    public void Setup()
    {
        rlImGui.Setup(true, true);
    }

    public void Begin()
    {
        rlImGui.Begin();
    }

    public void End()
    {
        rlImGui.End();
    }

    public void Shutdown()
    {
        rlImGui.Shutdown();
    }
}