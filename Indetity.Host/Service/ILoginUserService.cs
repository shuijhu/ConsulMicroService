using HeLian.Xiaoyi.ViewModel;
using System.Threading.Tasks;

namespace HeLian.Xiaoyi.Indetity.Host.Service
{
    public interface ILoginUserService
    {
        Task<UserMo> ValidateUser(string userName, string password);
    }
}
