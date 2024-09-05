namespace IPL.Models {
    public class Match {
        public int MatchId { get; set; }
        public DateTime MatchDate { get; set; }
        public string Venue { get; set; }
        public string Team1Name { get; set; }
        public string Team2Name { get; set; }
        public string WinnerTeamName { get; set; }
        public int FanEngagementCount { get; set; }

        //public int Team1Id { get; set; }
        //public int Team2Id { get; set; }
        //public int? WinnerTeamId { get; set; }

        //public Team? Team1 { get; set; }
        //public Team? Team2 { get; set; }
        //public Team? WinnerTeam { get; set; }
    }
}
