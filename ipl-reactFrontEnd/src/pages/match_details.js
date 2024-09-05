import { useState } from "react";
import FanEngagementBox from "../components/fan_eng_box";
import { getMatchDetails } from "../services/api_services";

const MatchDetailsPage = () => {
  const [matchDetails, setMatchDetails] = useState([]);
  const [startDate, setStartDate] = useState(formatDate(new Date()));
  const [endDate, setEndDate] = useState(formatDate(new Date()));

  const handleStartDateChange = (event) => {
    setStartDate(event.target.value);
  };

  const handleEndDateChange = (event) => {
    setEndDate(event.target.value);
  };

  const fetchMatchDetails = async () => {
    console.log(startDate, endDate);
    const res = await getMatchDetails({ startDate, endDate });
    setMatchDetails(res);

    console.log(res);
  };

  const renderMatchDetails = () => {
    return matchDetails.map((match) => {
      return <FanEngagementBox match={match} />;
    });
  };

  return (
    <div className="container">
      <h1 className="text-center">Match Details</h1>
      <div className="d-flex justify-content-center">
        <div className="m-3">
          <label>Start Date</label>
          {"\t"}
          <input
            type="date"
            value={startDate}
            onChange={handleStartDateChange}
          />
        </div>
        <div className="m-3">
          <label>End Date</label>
          {"\t"}
          <input type="date" value={endDate} onChange={handleEndDateChange} />
        </div>
      </div>
      <div className="d-flex justify-content-center">
        <button className="btn btn-primary" onClick={fetchMatchDetails}>
          Get Match Details
        </button>
      </div>
      <div className="d-flex flex-column align-items-center">
        {matchDetails.length > 0 ? (
          renderMatchDetails()
        ) : (
          <p>No matches found.</p>
        )}
      </div>
    </div>
  );
};

const formatDate = (date) => {
  return date.toISOString().split("T")[0];
};

export default MatchDetailsPage;
