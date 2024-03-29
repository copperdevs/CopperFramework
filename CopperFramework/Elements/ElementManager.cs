﻿using CopperFramework.Elements.Components;
using CopperFramework.Elements.Systems;

namespace CopperFramework.Elements;

internal static class ElementManager
{
    internal static void Initialize()
    {
        Update(ElementUpdateType.Load);
        SystemManager.Initialize();
    }
    
    internal static void Shutdown()
    {
        Update(ElementUpdateType.Close);
        SystemManager.Shutdown();
    }

    internal static void Update(ElementUpdateType updateType)
    {
        switch (updateType)
        {
            case ElementUpdateType.Load:
                SystemManager.Update(SystemUpdateType.Load);
                break;
            case ElementUpdateType.Update:
                SystemManager.Update(SystemUpdateType.Update);
                break;
            case ElementUpdateType.Render:
                SystemManager.Update(SystemUpdateType.Renderer);
                ComponentRegistry.CurrentComponents.ToList().ForEach(component =>
                {
                    Rlgl.PushMatrix();
                    Rlgl.Translatef(component.Transform.Position.X, component.Transform.Position.Y, 0);
                    Rlgl.Rotatef(component.Transform.Rotation, 0, 0, -1);
                    Rlgl.Scalef(component.Transform.Scale, component.Transform.Scale, 0);
                    component.Update();
                    Rlgl.PopMatrix();
                });
                break;
            case ElementUpdateType.Close:
                SystemManager.Update(SystemUpdateType.Close);
                ComponentRegistry.CurrentComponents.ToList().ForEach(component => component.Stop());
                ComponentRegistry.CurrentComponents.ToList().ForEach(component => component.Sleep());
                break;
            case ElementUpdateType.UiRender:
                SystemManager.Update(SystemUpdateType.UiRenderer);
                ComponentRegistry.CurrentComponents.ToList().ForEach(component => component.UiUpdate());
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(updateType), updateType, null);
        }
    }

    internal enum ElementUpdateType
    {
        Load,
        Update,
        Render,
        UiRender,
        Close
    }
    
}