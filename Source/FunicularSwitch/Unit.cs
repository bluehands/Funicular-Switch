namespace FunicularSwitch
{
    public class Unit
    {
        public static readonly Unit Instance = new Unit();

        private Unit()
        {
        }
    }

    public static class No
    {
        public static Unit Thing => Unit.Instance;
    }
}
