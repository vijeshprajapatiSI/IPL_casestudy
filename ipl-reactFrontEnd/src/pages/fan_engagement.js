import { useEffect, useState } from "react";
import FanEngagementBox from "../components/fan_eng_box";
import { getFanEngagement } from "../services/api_services";

const FanEngagementPage = () => {
  const [matchDetails, setMatchDetails] = useState([]);

  const fetchMatchDetails = async () => {
    const res = await getFanEngagement();
    setMatchDetails(res);
  };

  useEffect(() => {
    fetchMatchDetails();
  }, []);

  const renderMatchDetails = () => {
    return matchDetails.map((match) => {
      return <FanEngagementBox match={match} />;
    });
  };

  return (
    <div className="container" style={{ opacity: 0.8 }}>
      <h1 className="text-center">Fan Engagement Page</h1>
      <h3>Match Details</h3>
      {renderMatchDetails()}
    </div>
  );
};

export default FanEngagementPage;
