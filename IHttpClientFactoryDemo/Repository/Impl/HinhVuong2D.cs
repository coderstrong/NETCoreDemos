using System;

public class HinhVuong2D : IHinh2D
{
    public string GetGuid()
    {
        return Guid.NewGuid().ToString();
    }
}