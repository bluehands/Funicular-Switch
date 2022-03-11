namespace FunicularSwitch
{
    public class Unit
    {
        public static readonly Unit Instance = new();

        Unit()
        {
        }

        public override string ToString() => nameof(Unit);
    }

    public static class No
    {
        public static Unit Thing => Unit.Instance;
    }
}
