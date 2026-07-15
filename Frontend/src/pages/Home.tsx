import "./Home.css";
import  Message  from "../components/Message";
import ListGroup from "../components/ListGroup";
import { logoutUser } from "../api/auth";
import { useNavigate } from "react-router-dom";

function Home() {
const navigate = useNavigate();
  const handleLogout = () => {
    logoutUser();
    navigate("/login");
  };
    
    return (
        <div className="home">
      <div className="message-container">
        <Message />
      </div>
      <div className="list-container">
        <ListGroup />
      </div>
      <button className="logout-button" onClick={handleLogout}>
  Logout
</button>
    </div>
    )
}

export default Home;