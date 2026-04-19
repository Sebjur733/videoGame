import { useState } from "react";
import ListGroup from "./components/ListGroup";
import  Message  from "./Message";

function App() {
  return (
    <div>
      <div className="message-container">
        <Message />
      </div>
      <div className="list-container">
        <ListGroup />
      </div>
    </div>
  );
}
export default App;