import Home from "./pages/Home";
import Mygames from "./pages/MyGames";
import Login from "./pages/Login";
import PrivateRoute from "./PrivateRoute";
import { BrowserRouter, Routes, Route, Link, Navigate, useNavigate   } from "react-router-dom";
import "./App.css";
import { FaUser } from "react-icons/fa";
import { logoutUser, isAuthenticated } from "./api/auth";
import { useState } from "react";

function App() {

  const [isLoggedIn, setIsLoggedIn] = useState(isAuthenticated());

const handleLogout = () => {
  logoutUser();
  setIsLoggedIn(false);
  window.location.href = "/login";
};

  return (
    <BrowserRouter>
      <nav>
        <Link to="/">Home</Link>

         {isLoggedIn ? (
          <button onClick={handleLogout}>Logout</button>
        ) : (
          <Link to="/login">Login</Link>
        )}
        <Link to="/MyGames">My games</Link>
        <Link to="/profile" className="profile-link"><FaUser /></Link>
      </nav>

      <Routes>
  <Route
    path="/"
    element={
      <PrivateRoute>
        <Home />
      </PrivateRoute>
    }
  />

  <Route
    path="/mygames"
    element={
      <PrivateRoute>
        <Mygames />
      </PrivateRoute>
    }
  />
<Route
  path="/login"
  element={
    isLoggedIn ? (
      <Navigate to="/" />
    ) : (
      <Login onLogin={() => setIsLoggedIn(true)} />
    )
  }
/>
</Routes>
    </BrowserRouter>
  );
}

export default App;