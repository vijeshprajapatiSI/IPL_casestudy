import { useState } from "react";
import { addPlayer } from "../services/api_services";

const AddPlayerPage = () => {
  const [player, setPlayer] = useState({});
  const [errors, setErrors] = useState({});

  const addPlayerDetails = (event) => {
    const { id, value } = event.target;
    setPlayer({ ...player, [id]: value });

    validateField(id, value);
  };

  const validateField = (field, value) => {
    let errorMsg = "";

    switch (field) {
      case "playerName":
        if (!value.trim()) errorMsg = "Player Name is required";
        break;
      case "teamId":
        if (!value) errorMsg = "Team ID is required";
        if (value <= 0) errorMsg = "Team ID must be a positive number";
        break;
      case "role":
        if (!value.trim()) errorMsg = "Player Role is required";
        break;
      case "age":
        if (!value) errorMsg = "Player Age is required";
        if (value <= 0) errorMsg = "Player Age must be a positive number";
        break;
      case "matchesPlayed":
        if (!value) errorMsg = "Matches Played is required";
        if (value < 0) errorMsg = "Matches Played cannot be negative";
        break;
      default:
        break;
    }

    setErrors((prevErrors) => ({ ...prevErrors, [field]: errorMsg }));
  };

  const validateForm = () => {
    const newErrors = {};
    if (!player.playerName) newErrors.playerName = "Player Name is required";
    if (!player.teamId) newErrors.teamId = "Team ID is required";
    if (player.teamId <= 0)
      newErrors.teamId = "Team ID must be a positive number";
    if (!player.role) newErrors.role = "Player Role is required";
    if (!player.age) newErrors.age = "Player Age is required";
    if (player.age <= 0) newErrors.age = "Player Age must be a positive number";
    if (player.matchesPlayed < 0)
      newErrors.matchesPlayed = "Matches Played cannot be negative";
    setErrors(newErrors);

    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = async (event) => {
    event.preventDefault();

    if (validateForm()) {
      const res = await addPlayer(player);
      console.log(res);
      if (res) {
        alert("Player added successfully");
      } else {
        alert("Failed to add player");
      }
    } else {
      alert("Please fix the validation errors before submitting");
    }
  };

  return (
    <div className="container align-items-center">
      <h1 className="text-center">Add Player Page</h1>
      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label className="form-label">Player Name</label>
          <input
            type="text"
            className="form-control"
            id="playerName"
            onChange={addPlayerDetails}
          />
          {errors.playerName && (
            <div className="text-danger">{errors.playerName}</div>
          )}
        </div>
        <div className="mb-3">
          <label className="form-label">Player Team Id</label>
          <input
            type="number"
            className="form-control"
            id="teamId"
            onChange={addPlayerDetails}
          />
          {errors.teamId && <div className="text-danger">{errors.teamId}</div>}
        </div>
        <div className="mb-3">
          <label className="form-label">Player Role</label>
          <input
            type="text"
            className="form-control"
            id="role"
            onChange={addPlayerDetails}
          />
          {errors.role && <div className="text-danger">{errors.role}</div>}
        </div>
        <div className="mb-3">
          <label className="form-label">Player Age</label>
          <input
            type="number"
            className="form-control"
            id="age"
            onChange={addPlayerDetails}
          />
          {errors.age && <div className="text-danger">{errors.age}</div>}
        </div>
        <div className="mb-3">
          <label className="form-label">Matches Played</label>
          <input
            type="number"
            className="form-control"
            id="matchesPlayed"
            onChange={addPlayerDetails}
          />
          {errors.matchesPlayed && (
            <div className="text-danger">{errors.matchesPlayed}</div>
          )}
        </div>
        <button type="submit" className="btn btn-primary">
          Submit
        </button>
      </form>
    </div>
  );
};

export default AddPlayerPage;
