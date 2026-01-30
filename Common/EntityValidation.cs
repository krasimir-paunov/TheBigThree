namespace TheBigThree.Common
{
    public static class EntityValidation
    {
        // Genre
        public const int GenreNameMinLength = 3;
        public const int GenreNameMaxLength = 50;

        // Game
        public const int GameTitleMinLength = 1;
        public const int GameTitleMaxLength = 150;
        public const int GameImageUrlMaxLength = 2048;
        public const int GameDescriptionMinLength = 20;
        public const int GameDescriptionMaxLength = 2000;
    }
}
