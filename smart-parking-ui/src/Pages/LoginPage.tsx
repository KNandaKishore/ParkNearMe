import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

function LoginPage() {
    const container = {
  height: "100vh",
  display: "flex",
  justifyContent: "center",
  alignItems: "center",
  background: "linear-gradient(135deg, #4facfe, #00f2fe)",
};

const card = {
  background: "#fff",
  padding: "40px",
  borderRadius: "12px",
  width: "320px",
  textAlign: "center",
};

const input = {
  width: "100%",
  padding: "10px",
  marginBottom: "10px",
};

const loginBtn = {
  width: "100%",
  padding: "10px",
  background: "#4facfe",
  color: "#fff",
  border: "none",
};

const registerBtn = {
  ...loginBtn,
  background: "#28a745",
};

const cancelBtn = {
  ...loginBtn,
  background: "#ccc",
  color: "#000",
};

const overlay = {
  position: "fixed",
  top: 0,
  left: 0,
  width: "100%",
  height: "100%",
  background: "rgba(0,0,0,0.6)",
  display: "flex",
  justifyContent: "center",
  alignItems: "center",
};

const modal = {
  background: "#fff",
  padding: "30px",
  borderRadius: "12px",
  width: "300px",
};
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [fullName, setFullName] = useState("");
  const [showRegister, setShowRegister] = useState(false);

  const navigate = useNavigate();

  // 🔐 LOGIN
  const handleLogin = async () => {
    if (!email || !password) {
      alert("Please enter email and password");
      return;
    }

    try {
      const res = await fetch("http://localhost:5019/api/Auth/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ email, password }),
      });

      if (!res.ok) {
        const error = await res.text();
        throw new Error(error);
      }

      const token = await res.text();
      localStorage.setItem("token", token);

      navigate("/map");
    } catch (err: any) {
      alert(err.message);
    }
  };

  // 📝 REGISTER
  const handleRegister = async () => {
    const res = await fetch("http://localhost:5019/api/Auth/register", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ email, password, fullName }),
    });

    if (res.ok) {
      alert("Registered successfully ✅");
      setShowRegister(false);
    } else {
      const error = await res.text();
      alert(error);
    }
  };

  // 🌐 GOOGLE LOGIN
  const handleGoogleLogin = async (response: any) => {
    const token = response.credential;
    const payload = JSON.parse(atob(token.split(".")[1]));
    const email = payload?.email;

    if (!email) {
      alert("Google email not found");
      return;
    }

    const res = await fetch("http://localhost:5019/api/Auth/google", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ email }),
    });

    const data = await res.text();
    localStorage.setItem("token", data);

    navigate("/map");
  };

  useEffect(() => {
    const token = localStorage.getItem("token");
    if (token) navigate("/map");
  }, []);

  useEffect(() => {
    if (window.google) {
      window.google.accounts.id.initialize({
        client_id: "YOUR_GOOGLE_CLIENT_ID",
        callback: handleGoogleLogin,
      });

      window.google.accounts.id.renderButton(
        document.getElementById("googleSignInDiv"),
        { theme: "outline", size: "large" }
      );
    }
  }, []);

  return (
    <>
      <div style={container}>
        <div style={card}>
          <h2>🚗 ParkNearMe</h2>

          <input
            placeholder="Email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            style={input}
          />

          <input
            type="password"
            placeholder="Password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            style={input}
          />

          <button onClick={handleLogin} style={loginBtn}>
            Login
          </button>

          <button onClick={() => setShowRegister(true)} style={registerBtn}>
            Create Account
          </button>

          <div style={{ margin: "10px 0" }}>OR</div>

          <div id="googleSignInDiv"></div>
        </div>
      </div>

      {showRegister && (
        <div style={overlay}>
          <div style={modal}>
            <h3>Create Account</h3>

            <input
              placeholder="Full Name"
              value={fullName}
              onChange={(e) => setFullName(e.target.value)}
              style={input}
            />

            <input
              placeholder="Email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              style={input}
            />

            <input
              type="password"
              placeholder="Password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              style={input}
            />

            <button onClick={handleRegister} style={registerBtn}>
              Register
            </button>

            <button onClick={() => setShowRegister(false)} style={cancelBtn}>
              Cancel
            </button>
          </div>
        </div>
      )}
    </>
  );

  
}

export default LoginPage;