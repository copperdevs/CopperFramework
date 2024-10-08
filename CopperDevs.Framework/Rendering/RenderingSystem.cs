﻿using CopperDevs.Framework.Common.Utility;
using CopperDevs.Framework.Resources.Fonts;
using CopperDevs.Framework.Utility;

namespace CopperDevs.Framework.Rendering;

public class RenderingSystem : Singleton<RenderingSystem>
{
    public Dictionary<Type, List<BaseRenderable>> LoadedRenderableItems { get; private set; } = new();

    public void RegisterRenderableItem<T>(T renderable) where T : BaseRenderable
    {
        if (LoadedRenderableItems.TryGetValue(typeof(T), out var value))
            value.Add(renderable);
        else
            LoadedRenderableItems.Add(typeof(T), [renderable]);
    }

    public List<T> GetRenderableItems<T>() where T : BaseRenderable
    {
        if (LoadedRenderableItems.ContainsKey(typeof(T)))
            return LoadedRenderableItems[typeof(T)].Cast<T>().ToList();
        LoadedRenderableItems.Add(typeof(T), []);
        return LoadedRenderableItems[typeof(T)].Cast<T>().ToList();
    }

    public void DeregisterRenderableItem<T>(T renderable) where T : BaseRenderable
    {
        var targetList = LoadedRenderableItems[typeof(T)];
        targetList.Remove(renderable);
        LoadedRenderableItems[typeof(T)] = targetList;
    }

    internal void Start()
    {
        BaseRenderable.LoadQueuedItems();

        Shader.Load("Empty");

        Font.Load("Inter", FontRegistry.Instance.Inter.Regular);
        Font.Load();
    }

    internal void Stop()
    {
        foreach (var renderable in LoadedRenderableItems.Values.SelectMany(renderables => renderables).ToList())
            renderable.UnLoadRenderable();
    }
}