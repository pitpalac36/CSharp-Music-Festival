using System.Text;
using Model.domain;

namespace Model.validator
{
    class UserValidator : IValidator<User>
    {
        public void Validate(User elem)
        {
            StringBuilder errors = new StringBuilder("");
            if (elem.Name.Trim().Equals(""))
                errors.Append("\nUsername cannot be empty!!\n");
            if (elem.Password.Trim().Equals(""))
                errors.Append("Password cannot be empty!!\n");
            if (!errors.ToString().Equals(""))
                throw new ValidationException(errors.ToString());
        }
    }
}