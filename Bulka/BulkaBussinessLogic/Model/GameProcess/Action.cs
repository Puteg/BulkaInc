namespace BulkaBussinessLogic.Model.GameProcess
{
    public class Action
    {
        public int GameProcessId { get; set; }
        public int PlayerId { get; set; }
        public decimal Amount { get; set; }

        public ActionType Type { get; set; }
    }
}
