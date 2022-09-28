
using MergerBay.Domain.Entities.Login;
using MergerBay.Domain.Model;
using System.Threading.Tasks;

namespace MergerBay.Infrastructure.Interfaces.Login
{
    //Interface of Login class
    public interface ILogin
    {
        Task<UserLoginReponse> UserAuthentication(UserSignIn model);
        Task<bool> MatchUserPassword(PasswordRequestModel model);
    }
}
