﻿using CopperDevs.Core.Utility;
using CopperDevs.DearImGui.Attributes;
using Raylib_CSharp.Transformations;

namespace CopperDevs.Framework.Ui;

public abstract class UiElement
{
    [Seperator($"Base {nameof(UiElement)} Settings")]
    public string Name;

    [Range(0, 1, TargetRangeType = RangeType.Drag, Speed = 0.005f)]
    public Vector2 Position;
    [Range(0, 1, TargetRangeType = RangeType.Drag, Speed = 0.005f)]
    public Vector2 Size;

    public Vector2 ScaledPosition => Position.Remap(Vector2.Zero, Vector2.One, Vector2.Zero, Engine.Instance.WindowSize);
    public Vector2 ScaledSize => Size.Remap(Vector2.Zero, Vector2.One, Vector2.Zero, Engine.Instance.WindowSize);

    public abstract void DrawElement();

    protected UiElement(string name)
    {
        Name = name;
    }

    public static implicit operator Rectangle(UiElement uiElement)
    {
        return new Rectangle(uiElement.ScaledPosition.X, uiElement.ScaledPosition.Y, uiElement.ScaledSize.X, uiElement.ScaledSize.Y);
    }
}