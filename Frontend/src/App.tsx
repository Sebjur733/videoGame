import Home from "./pages/Home";
import Mygames from "./pages/MyGames";
import Login from "./pages/Login";
import PrivateRoute from "./PrivateRoute";
import { BrowserRouter, Routes, Route, Link } from "react-router-dom";
import "./App.css";
import { FaUser } from "react-icons/fa";

function App() {
  return (
    <BrowserRouter>
      <nav>
        <Link to="/">Home</Link>
        <Link to="/login">Login</Link>
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

  <Route path="/login" element={<Login />} />
</Routes>
    </BrowserRouter>
  );
}

export default App;