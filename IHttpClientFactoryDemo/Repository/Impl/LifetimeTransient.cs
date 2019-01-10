using System;

public class LifetimeTransient : ILifetimeTransient
{
    public string GUID { get; set; }

    public LifetimeTransient(){
        this.GUID = Guid.NewGuid().ToString();
    }

    public string GetGuid()
    {
        return this.GUID;
    }
}