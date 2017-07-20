using System.Linq;
using Bulka.DataAccess;
using Bulka.Repository;
using BulkaBussinessLogic.Model.Profile;

namespace BulkaBussinessLogic.Implementation
{
    public class ProfileService
    {
        private BulkaContext Context { get; set; }
        private ProfileRepository ProfileRepository { get; set; }

        public ProfileService(BulkaContext context)
        {
            Context = context;
            ProfileRepository = new ProfileRepository(context);
        }

        public ProfileModel Get(string id)
        {
            var model = ProfileRepository.GetAll().First(c => c.Id == id);
            var vm = new ProfileModel()
            {
                Id = model.Id,
                Phone = model.PhoneNumber,
                Email = model.Email,
                ImageUrl = model.ImageUrl,
                Login = model.UserName
            };

            return vm;
        }

        public ProfileEdit GetEdit(string id)
        {
            var model = ProfileRepository.GetAll().First(c => c.Id == id);
            var vm = new ProfileEdit()
            {
                Id = model.Id,
                Phone = model.PhoneNumber,
                Email = model.Email,
                ImageUrl = model.ImageUrl,
                Login = model.UserName
            };

            return vm;
        }

        public bool Edit(ProfileEdit edit)
        {
            var model = ProfileRepository.GetAll().First(c => c.Id == edit.Id);

            model.UserName = edit.Login;
            model.PhoneNumber = edit.Phone;
            model.Email = edit.Email;
            model.ImageUrl = edit.ImageUrl;

            ProfileRepository.Save();

            return true;
        }
    }
}
