using IPL.Models;
using Npgsql;

namespace IPL.DataAccessLayer
{
    public class IplDAO : IIplDAO
    {
        private readonly NpgsqlConnection _connection;

        public IplDAO(NpgsqlConnection connection)
        {
            this._connection = connection;
        }

        //public async Task<List<Player>> GetAllPlayers() {
        //    string query = "SELECT * FROM ipl.players";
        //    List<Player> players = [];
        //    try {
        //        await _connection.OpenAsync();
        //        var cmd = new NpgsqlCommand(query, _connection);
        //        using(cmd) {
        //            var reader = cmd.ExecuteReader();
        //            while(reader.Read()) {
        //                players.Add(
        //                    new Player() {
        //                        PlayerId = int.TryParse(reader["player_id"].ToString(), out int id) ? id : -1,
        //                        PlayerName = reader["player_name"].ToString() ?? "",
        //                        TeamId = int.TryParse(reader["team_id"].ToString(), out int teamId) ? teamId : -1,
        //                        Role = reader["role"].ToString() ?? "",
        //                        Age = int.TryParse(reader["age"].ToString(), out int age) ? age : -1,
        //                        MatchesPlayed = int.TryParse(reader["matches_played"].ToString(), out int matchesPlayed) ? matchesPlayed : -1
        //                    }
        //                );
        //            }
        //        }
        //    } catch(Exception ex) {
        //        Console.WriteLine(ex.Message);
        //    }

        //    return players;
        //}

        //public async Task<Player> GetPlayerById(int playerId) {
        //    string query = "SELECT * FROM ipl.players WHERE player_id = @playerId";
        //    Player player = null;

        //    try {
        //        await _connection.OpenAsync();
        //        var cmd = new NpgsqlCommand(query, _connection);

        //        using(cmd) {
        //            cmd.Parameters.AddWithValue("playerId", playerId);
        //            var reader = cmd.ExecuteReader();
        //            using(reader) {
        //                while(reader.Read()) {
        //                    player = new Player {
        //                        PlayerId = int.TryParse(reader["player_id"].ToString(), out int id) ? id : -1,
        //                        PlayerName = reader["player_name"].ToString() ?? "",
        //                        TeamId = int.TryParse(reader["team_id"].ToString(), out int teamId) ? teamId : -1,
        //                        Role = reader["role"].ToString() ?? "",
        //                        Age = int.TryParse(reader["age"].ToString(), out int age) ? age : -1,
        //                        MatchesPlayed = int.TryParse(reader["matches_played"].ToString(), out int matchesPlayed) ? matchesPlayed : -1
        //                    };
        //                }
        //            }
        //        }
        //    } catch(Exception ex) {
        //        Console.WriteLine(ex.ToString());
        //    }

        //    return player == null ? new Player() { PlayerId = -1 } : player;
        //}

        public async Task<int> AddPlayer(Player player)
        {
            Console.WriteLine(player.PlayerName + " " + player.TeamId + " " + player.Role + " " + player.Age + " " + player.MatchesPlayed);
            string query = "INSERT INTO ipl.players (player_name, team_id, role, age, matches_played) VALUES (@playerName, @teamId, @role, @age, @matchesPlayed)";

            try
            {
                await _connection.OpenAsync();
                using (var cmd = new NpgsqlCommand(query, _connection))
                {
                    cmd.Parameters.AddWithValue("playerName", NpgsqlTypes.NpgsqlDbType.Text, player.PlayerName);
                    cmd.Parameters.AddWithValue("teamId", NpgsqlTypes.NpgsqlDbType.Integer, player.TeamId);
                    cmd.Parameters.AddWithValue("role", NpgsqlTypes.NpgsqlDbType.Text, player.Role);
                    cmd.Parameters.AddWithValue("age", NpgsqlTypes.NpgsqlDbType.Integer, player.Age);
                    cmd.Parameters.AddWithValue("matchesPlayed", NpgsqlTypes.NpgsqlDbType.Integer, player.MatchesPlayed);

                    return await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return -1;
        }

        public async Task<List<Match>> GetMatchDetails()
        {

            string query = @"SELECT
                    m.match_id, 
                    m.match_date,
                    m.venue, 
                    t1.team_name AS team1, 
                    t2.team_name AS team2,
                    t.team_name AS winner_team,
                    COUNT(fe.engagement_id) AS total_fan_engagements
                FROM 
                    ipl.matches m
                JOIN 
                    ipl.teams t1 ON m.team1_id = t1.team_id
                JOIN 
                    ipl.teams t2 ON m.team2_id = t2.team_id
                JOIN
                    ipl.teams t ON m.winner_team_id = t.team_id
                LEFT JOIN 
                    ipl.fan_engagement fe ON m.match_id = fe.match_id
                GROUP BY 
                    m.match_id, m.match_date, m.venue, t1.team_name, t2.team_name, t.team_name";
            var cmd = new NpgsqlCommand(query, _connection);

            List<Match> matches = [];
            try
            {
                await _connection.OpenAsync();

                using (cmd)
                {
                    var reader = cmd.ExecuteReader();
                    using (reader)
                    {
                        while (reader.Read())
                        {
                            matches.Add(
                                new Match
                                {
                                    MatchId = int.TryParse(reader["match_id"].ToString(), out int id) ? id : -1,
                                    MatchDate = DateTime.TryParse(reader["match_date"].ToString(), out DateTime date) ? date : DateTime.MinValue,
                                    Venue = reader["venue"].ToString() ?? "",
                                    Team1Name = reader["team1"].ToString() ?? "",
                                    Team2Name = reader["team2"].ToString() ?? "",
                                    WinnerTeamName = reader["winner_team"].ToString() ?? "",
                                    FanEngagementCount = int.TryParse(reader["total_fan_engagements"].ToString(), out int totalFanEngagements) ? totalFanEngagements : -1
                                }
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return matches;
        }


        public async Task<List<Player>> TopPlayersFanEngagement()
        {
            string query = @"
                select 
                    p.*, sum(f.engagement_id) as total_fan_engagements
                from 
                    ipl.players p
                join 
                    ipl.matches m on p.team_id = m.team1_id or p.team_id = m.team2_id
                left join 
                    ipl.fan_engagement f on m.match_id = f.match_id
                group by 
                    p.player_id, p.player_name, p.matches_played
                order by 
                    p.matches_played desc, total_fan_engagements desc
                limit 5";

            var cmd = new NpgsqlCommand(query, _connection);

            List<Player> players = [];
            try
            {
                await _connection.OpenAsync();
                using (cmd)
                {
                    var reader = cmd.ExecuteReader();
                    using (reader)
                    {
                        while (reader.Read())
                        {
                            players.Add(
                                new Player
                                {
                                    PlayerId = int.TryParse(reader["player_id"].ToString(), out int id) ? id : -1,
                                    PlayerName = reader["player_name"].ToString() ?? "",
                                    TeamId = int.TryParse(reader["team_id"].ToString(), out int teamId) ? teamId : -1,
                                    Role = reader["role"].ToString() ?? "",
                                    Age = int.TryParse(reader["age"].ToString(), out int age) ? age : -1,
                                    MatchesPlayed = int.TryParse(reader["matches_played"].ToString(), out int matchesPlayed) ? matchesPlayed : -1,
                                    TotalFanEngagement = int.TryParse(reader["total_fan_engagements"].ToString(), out int totalFanEngagements) ? totalFanEngagements : -1
                                }
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return players;
        }

        public async Task<List<Match>> GetMatches(DateTime startDate, DateTime endDate)
        {
            Console.WriteLine(startDate + " " + endDate);
            string query = @"
                SELECT m.match_id, m.venue, m.match_date, 
                t1.team_name AS team1_name, 
                t2.team_name AS team2_name, 
                t.team_name AS winner_team_name
                FROM ipl.matches m
                JOIN ipl.teams t1 ON m.team1_id = t1.team_id
                JOIN ipl.teams t2 ON m.team2_id = t2.team_id
                JOIN ipl.teams t ON m.winner_team_id = t.team_id
                WHERE m.match_date BETWEEN @startDate AND @endDate";

            var cmd = new NpgsqlCommand(query, _connection);

            List<Match> matches = [];
            try
            {
                await _connection.OpenAsync();
                using (cmd)
                {
                    cmd.Parameters.AddWithValue("startDate", startDate);
                    cmd.Parameters.AddWithValue("endDate", endDate);

                    var reader = cmd.ExecuteReader();
                    using (reader)
                    {
                        while (reader.Read())
                        {
                            matches.Add(
                                new Match
                                {
                                    MatchId = int.TryParse(reader["match_id"].ToString(), out int id) ? id : -1,
                                    Venue = reader["venue"].ToString() ?? "",
                                    MatchDate = DateTime.TryParse(reader["match_date"].ToString(), out DateTime date) ? date : DateTime.MinValue,
                                    Team1Name = reader["team1_name"].ToString() ?? "",
                                    Team2Name = reader["team2_name"].ToString() ?? "",
                                    WinnerTeamName = reader["winner_team_name"].ToString() ?? ""
                                }
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return matches;
        }
    }
}
