namespace TheBigThree.GCommon
{
    public static class RankHelper
    {
        public static string GetRank(int totalStars) => totalStars switch
        {
            >= 100 => "Legendary Collector",
            >= 30 => "Superstar Collector",
            >= 10 => "Popular Collector",
            >= 5 => "Rising Star",
            >= 1 => "Novice Collector",
            _ => "Newcomer"
        };
    }
}