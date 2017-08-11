using BulkaBussinessLogic.Model.GameProcess;

namespace Bulka.Models.GameProcess
{
    public class ActionEditModel
    {
        public ActionEditModel()
        {
            Amount = 6000;
        }

        public int GameProcessId { get; set; }
        public int PlayerId { get; set; }
        public decimal Amount { get; set; }

        public ActionType Type { get; set; }
    }
}