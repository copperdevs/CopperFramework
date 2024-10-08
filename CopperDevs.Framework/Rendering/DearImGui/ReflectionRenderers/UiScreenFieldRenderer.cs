﻿using System.Reflection;
using CopperDevs.Core;
using CopperDevs.Core.Utility;
using CopperDevs.DearImGui;
using CopperDevs.DearImGui.ReflectionRenderers;
using CopperDevs.Framework.Attributes;
using CopperDevs.Framework.Ui;

namespace CopperDevs.Framework.Rendering.DearImGui.ReflectionRenderers;

public class UiScreenFieldRenderer : FieldRenderer
{
    private static UiScreen? currentScreen = null!;

    public UiScreenFieldRenderer()
    {
        CopperImGui.RegisterPopup("UiRenderSystemAddElementPopup", () =>
        {
            foreach (var type in ComponentTypes.Where(type => type != typeof(UiElement)))
            {
                CopperImGui.Selectable(type.Name, () =>
                {
                    currentScreen?.Add(((UiElement)Activator.CreateInstance(type)!));
                    currentScreen = null;
                });
            }
        });
    }

    public override void ReflectionRenderer(FieldInfo fieldInfo, object component, int id, Action valueChanged = null!)
    {
        var value = (UiScreen)(fieldInfo.GetValue(component) ?? CreateUiScreen());
        UiScreenRenderer($"{fieldInfo.Name.ToTitleCase()}##{fieldInfo.Name}{id}", value, id);
    }

    public override void ValueRenderer(ref object value, int id, Action valueChanged = null!)
    {
        UiScreenRenderer($"{value.GetType().Name.ToTitleCase()}##{id}", (UiScreen)value, id);
    }

    private void UiScreenRenderer(string title, UiScreen screen, int id, Action valueChanged = null!)
    {
        CopperImGui.CollapsingHeader(title, () =>
        {
            for (var i = 0; i < screen.UiElements.Count; i++)
            {
                var element = screen.UiElements[i];

                CopperImGui.CollapsingHeader($"{element.Name}###{i + id}", () =>
                {
                    // ReSharper disable once AccessToModifiedClosure
                    CopperImGui.RenderObjectValues(ref element, i + id + element.GetHashCode(), RenderingType.All, valueChanged);
                });
            }

            CopperImGui.ForceRenderPopup("UiRenderSystemAddElementPopup");
            CopperImGui.Selectable("Add New Element", () =>
            {
                CopperImGui.ShowPopup("UiRenderSystemAddElementPopup");
                currentScreen = screen;
            });
        });
    }

    private static UiScreen CreateUiScreen()
    {
        var guid = Guid.NewGuid().ToString();
        return new UiScreen(guid, guid);
    }

    internal static List<Type> ComponentTypes { get; private set; } = AppDomain.CurrentDomain.GetAssemblies()
        .SelectMany(assembly => assembly.GetTypes())
        .Where(type => type.IsSubclassOf(typeof(UiElement)))
        .Where(type => !Attribute.IsDefined(type, typeof(DisabledAttribute))).ToList();
}