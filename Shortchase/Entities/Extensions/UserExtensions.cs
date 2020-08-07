using Shortchase.Dtos;

namespace Shortchase.Entities.Extensions
{
    public static class UserExtensions
    {
        public static EditUserDto ToEditUser(this User u)
        {
            return new EditUserDto
            {
                FirstName = u.FirstName,
                LastName = u.LastName,
                PhoneNumber = u.PhoneNumber
            };
        }
    }
}