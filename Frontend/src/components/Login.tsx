import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { loginUser, registerUser } from "../api/auth";
import "./Login.css";

function Login() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const navigate = useNavigate();

  const handleLogin = async () => {
    try {
      await loginUser(username, password);
      navigate("/");
    } catch {
      console.log("Login failed");
    }
  };

  

  const handleRegister = async () => {
    try {
      await registerUser(username, password);
    } catch {
      console.log("Login failed");
    }
  };

  return (
    <>
    <div className="main">
      <h1>Login</h1>

      <input
        placeholder="Username"
        onChange={(e) => setUsername(e.target.value)}
      />

      <input
        type="password"
        placeholder="Password"
        onChange={(e) => setPassword(e.target.value)}
      />

      <button onClick={handleLogin}>Login</button>
      <button onClick={handleRegister}>Register</button>
    </div>

    <div className="games">
      <div className="game-image">
      <img src="https://www.crossovergaming.no/wp-content/uploads/2026/04/Resident-Evil-Requiem_cover-683x1024.jpg" alt="" />
      <h1>Resident evil requiem</h1>
      <p>New resident evil game out now!</p>
      </div>

      
      <div className="game-image">
      <img src="https://www.crossovergaming.no/wp-content/uploads/2026/04/Resident-Evil-Requiem_cover-683x1024.jpg" alt="" />
      <h1>Resident evil requiem</h1>
      <p>New resident evil game out now!</p>
      </div>

      
      <div className="game-image">
      <img src="https://www.crossovergaming.no/wp-content/uploads/2026/04/Resident-Evil-Requiem_cover-683x1024.jpg" alt="" />
      <h1>Resident evil requiem</h1>
      <p>New resident evil game out now!</p>
      </div>
      
    </div>
    </>
  );
}

export default Login;