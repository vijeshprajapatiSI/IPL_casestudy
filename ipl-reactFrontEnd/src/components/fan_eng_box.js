import csk from '../assets/csk.png';
import rcb from '../assets/rcb.jpg';

const FanEngagementBox = ({ match }) => {
  return (
    <div className="container m-3" key={match.matchId}>
      <div className="card">
        <div className="card-body align-items-center">
          <div className="row">
            <div className="d-flex justify-content-center border col m-2">
              <img
                src={csk}
                alt="player"
                style={{ width: "200px", height: "200px" }}
              />
            </div>
            <h5 className="card-title col text-center">
              {match.team1Name}
              <br />
              -VS-
              <br />
              {match.team2Name}
            </h5>
            <div className="d-flex justify-content-center border col m-2">
              <img
                src={rcb}
                alt="player"
                style={{ width: "100%", height: "200px" }}
              />
            </div>
          </div>
          <div className="card-text text-center">
            <p>{match.matchDate.substring(0, 10)}</p>
            <p>{match.venue}</p>
          </div>
          <div className="card-text text-center">
            <h5>{match.winnerTeamName} Won!!</h5>
          </div>
        </div>
      </div>
    </div>
  );
};

export default FanEngagementBox;
