using Bulka.DataModel;

namespace BulkaBussinessLogic
{
    public interface IGameProcessService
    {
        void StopProcess(int id);

        bool Seat(int playerId, decimal amount, int gameProcessId);
        bool Rebuy(int playerId, decimal amount, int gameProcessId);
        bool SeatOut(int playerId, decimal amount, int gameProcessId);
    }
}
