namespace csharpMusicFestival.validator
{
    interface IValidator<T>
    {
        void validate(T elem);
    }
}