using IPL.Models;

namespace IPL.DataAccessLayer {
    public interface IIplDAO {
        //public Task<List<Player>> GetAllPlayers();
        //public Task<Player> GetPlayerById(int playerId);
        public Task<int> AddPlayer(Player player);
        public Task<List<Match>> GetMatchDetails();
        public Task<List<Player>> TopPlayersFanEngagement();
        public Task<List<Match>> GetMatches(DateTime startDate, DateTime endDate);
    }
}
