

using System;

namespace AnilTools
{
    public static class ReadyVeriables
    {
        public static readonly Action EmptyAction = () => {};
        public static readonly Func<bool> TrueFunc = () => true;
    }
}
