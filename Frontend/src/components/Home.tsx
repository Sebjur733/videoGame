import  Message  from "./Message";
import ListGroup from "./ListGroup";
import { logoutUser } from "../api/auth";
import { useNavigate } from "react-router-dom";

function Home() {
const navigate = useNavigate();
  const handleLogout = () => {
    logoutUser();
    navigate("/login");
  };
    
    return (
        <div>
      <div className="message-container">
        <Message />
      </div>
      <div className="list-container">
        <ListGroup />
      </div>
      <button onClick={handleLogout}>Logout</button>
    </div>
    )
}

export default Home;