﻿namespace CopperDearImGui.ReflectionRenderers;

public abstract class FieldRenderer
{
    public abstract void ReflectionRenderer(FieldInfo fieldInfo, object component, int id);
    public abstract void ValueRenderer(ref object value, int id);
}