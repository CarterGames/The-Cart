namespace Scarlet.Random
{
    public interface ISeededRngProvider : IRngProvider
    {
        void GenerateSeed();
    }
}