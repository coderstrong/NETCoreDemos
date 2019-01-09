using System;

public class Hinh3D : IHinh3D
{
    public string GetGuid()
    {
        return Guid.NewGuid().ToString();
    }
}