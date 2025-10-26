using PokemonPRNG.LCG64;

namespace SinnohUndergroundRNGLibrary.Util
{
    public static class Extensions
    {
        public static uint GetRand(this ref ulong seed, uint m)
            => (uint)(seed.GetRand() * m >> 32);

        public static int GetRand(this ref ulong seed, int m)
            => (int)(seed.GetRand() * (ulong)m >> 32);

    }
}
