import { useEffect, useState } from "react";
import { getTopPlayers } from "../services/api_services";
import iplImg from "../assets/kohli.jpg"

const TopPlayersPage = () => {
  const [players, setPlayers] = useState([]);

  const fetchPlayers = async () => {
    const res = await getTopPlayers();
    setPlayers(res);
  };

  useEffect(() => {
    fetchPlayers();
  }, []);

  const renderPlayerDetails = () => {
    return players.map((player) => {
      return (
        <div className="card m-3" style={{ opacity: 0.8}}>
          <div className="card-body">
            <div className="d-flex justify-content-center border">
              <img
                src={iplImg}
                alt="player"
                style={{ width: "100%", height: "200px" }}
              />
            </div>
            <h5 className="card-title text-center">{player.playerName}</h5>
            <p className="card-text">Matches Played: {player.matchesPlayed}</p>
            <p className="card-text">Role: {player.role}</p>
            <p className="card-text">Age: {player.age}</p>
            <p className="card-text">
              Total Fan Engagement: {player.totalFanEngagement}
            </p>
          </div>
        </div>
      );
    });
  };

  return (
    <div className="container">
      <h1 className="text-center">Top Players</h1>
      <h5>Top 5 Most Popular Players</h5>
      {renderPlayerDetails()}
    </div>
  );
};

export default TopPlayersPage;
