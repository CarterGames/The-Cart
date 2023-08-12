namespace CarterGames.Common.Random
{
    public interface ISeededRngProvider : IRngProvider
    {
        void GenerateSeed();
    }
}