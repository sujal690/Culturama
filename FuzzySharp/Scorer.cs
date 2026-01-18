using FuzzySharp.SimilarityRatio.Scorer;

namespace FuzzySharp
{
    internal class Scorer
    {
        public static IRatioScorer LevenshteinRatio { get; internal set; }
    }
}