﻿using CopperCore;
using CopperFramework.Elements.Components;

namespace CopperFramework.Testing;

public class LogComponent : GameComponent
{
    private bool shouldLog;

    public override void Start()
    {
        if (shouldLog)
            Log.Info("Start");
    }

    public override void Update()
    {
        if (shouldLog)
            Log.Info("Update");
    }

    public override void FixedUpdate()
    {
        if (shouldLog)
            Log.Info("FixedUpdate");
    }

    public override void Stop()
    {
        if (shouldLog)
            Log.Info("Stop");
    }

    public override void UiUpdate()
    {
        if (shouldLog)
            Log.Info("UiUpdate");
    }
}