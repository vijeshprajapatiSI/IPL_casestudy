import axios from "axios";

const BASE_URL = "http://localhost:5105/api/Ipl";

const addPlayer = async (player) => {
  try {
    console.log(player);
    const response = await axios.post(`${BASE_URL}/player`, player);
    console.log(response);
    if (response.status === 200 || response.status === 201) {
      return true;
    } else {
      return false;
    }
  } catch (error) {
    console.log(error);
  }
};

const getFanEngagement = async () => {
  try {
    const response = await axios.get(`${BASE_URL}/match/fan-engagement`);

    if (response.status === 200) {
      return response.data;
    }
  } catch (error) {
    console.log(error);
  }
};

const getTopPlayers = async () => {
  try {
    const response = await axios.get(`${BASE_URL}/match/top-players`);

    if (response.status === 200) {
      return response.data;
    }
  } catch (error) {
    console.log(error);
  }
};

const getMatchDetails = async ({ startDate, endDate }) => {
  try {
    console.log(
      `${BASE_URL}/match/match-details?startDate=${startDate}&endDate=${endDate}`
    );
    const response = await axios.get(
      `${BASE_URL}/match/match-details?startDate=${startDate}&endDate=${endDate}`
    );

    console.log(response);

    if (response.status === 200) {
      return response.data;
    }
  } catch (error) {
    console.log(error);
  }
};

export { addPlayer, getFanEngagement, getMatchDetails, getTopPlayers };
