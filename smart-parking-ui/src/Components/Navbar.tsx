import { useNavigate } from "react-router-dom";

function Navbar() {
  const navigate = useNavigate();

  const nav = {
    height: "60px",
    background: "#1e293b",
    color: "white",
    display: "flex",
    justifyContent: "space-between",
    alignItems: "center",
    padding: "0 20px",
    position: "relative",
    zIndex: 1000,
  };

  const logo = {
    fontSize: "18px",
    fontWeight: "bold",
  };

  const btn = {
    marginRight: "10px",
    padding: "6px 12px",
    background: "#334155",
    color: "white",
    border: "none",
    borderRadius: "5px",
    cursor: "pointer",
  };

  const logoutBtn = {
    ...btn,
    background: "#dc2626",
  };

  const handleLogout = () => {
    localStorage.removeItem("token");
    navigate("/");
  };

  return (
    <div style={nav}> {/* 🔥 APPLY STYLE HERE */}
      <div style={logo}>🚗 ParkNearMe</div>

      <div>
        <button style={btn} onClick={() => navigate("/map")}>
          🏠 Map
        </button>

        <button style={btn} onClick={() => navigate("/create-parking")}>
          ➕ Add Parking
        </button>

        <button style={logoutBtn} onClick={handleLogout}>
          Logout
        </button>
      </div>
    </div>
  );
}

export default Navbar;