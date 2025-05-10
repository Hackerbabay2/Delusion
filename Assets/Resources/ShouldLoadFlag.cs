public class ShouldLoadFlag
{
    public bool NeedToLoad { get; private set; }

    public void IsNeedToLoad(bool value)
    {
        NeedToLoad = value;
    }
}
