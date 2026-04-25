import  Message  from "./Message";
import ListGroup from "./ListGroup";

function Home() {
    
    return (
        <div>
      <div className="message-container">
        <Message />
      </div>
      <div className="list-container">
        <ListGroup />
      </div>
    </div>
    )
}

export default Home;