using System;

public class LifetimeScope : ILifetimeScope
{
    public string GUID { get; set; }

    public LifetimeScope()
    {
        this.GUID = Guid.NewGuid().ToString();
    }
    public string GetGuid()
    {
        return this.GUID;
    }
}