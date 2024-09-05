import { BrowserRouter, Route, Routes } from "react-router-dom";
import NavbarComponent from "../components/navbar";
import AddPlayerPage from "../pages/add_player_page";
import FanEngagementPage from "../pages/fan_engagement";
import HomePage from "../pages/home_page";
import MatchDetailsPage from "../pages/match_details";
import TopPlayersPage from "../pages/top_players";

const RouterConfig = () => {
  //   const isAuthenticated = useSelector((state) => state.auth.isAuthenticated);
  return (
    <BrowserRouter>
      <NavbarComponent />
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/add-player" element={<AddPlayerPage />} />
        <Route path="/fan-engagement" element={<FanEngagementPage />} />
        <Route path="top-players" element={<TopPlayersPage />} />
        <Route path="match-details" element={<MatchDetailsPage />} />
      </Routes>
    </BrowserRouter>
  );
};

export default RouterConfig;
