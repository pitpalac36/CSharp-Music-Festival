namespace Model.validator
{
    interface IValidator<T>
    {
        void Validate(T elem);
    }
}