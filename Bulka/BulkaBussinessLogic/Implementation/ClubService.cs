using System.Linq;
using Bulka.DataAccess;
using Bulka.DataModel;
using Bulka.Repository;
using BulkaBussinessLogic.Model.Club;

namespace BulkaBussinessLogic.Implementation
{
    public class ClubService
    {
        private readonly ClubRepository _clubRepository;

        public ClubService(BulkaContext context)
        {
            _clubRepository = new ClubRepository(context);
        }

        public Clubs GetAll()
        {
            var items = _clubRepository.GetAll().Select(c => new ClubItem
            {
                Id = c.Id, 
                Name = c.Name
            });

            var clubs = new Clubs
            {
                Items = items.ToList()
            };

            return clubs;
        }

        public ClubEdit Get(int? id)
        {
            var edit = new ClubEdit();
            var isNew = id == null;
            
            if (isNew)
            {
                }
            else
            {
                var club = _clubRepository.GetAll().First(c => c.Id == id);
                edit = new ClubEdit {Id = club.Id, Name = club.Name};
            }

            return edit;
        }

        public bool Edit(ClubEdit edit)
        {
            var isNew = edit.Id == 0;

            if (isNew)
            {
                var club = new Club() {Name = edit.Name};
                _clubRepository.Add(club);
            }
            else
            {
                var club = _clubRepository.GetAll().First(c => c.Id == edit.Id);
                club.Name = edit.Name;
            }
            _clubRepository.Save();
            return true;
        }

        public void Delete(int id)
        {
            var club = _clubRepository.GetAll().First(c => c.Id == id);
            _clubRepository.Delete(club);
            _clubRepository.Save();
        }
    }
}
