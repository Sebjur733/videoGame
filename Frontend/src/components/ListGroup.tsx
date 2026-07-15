import { useState } from "react";
import "./ListGroup.css";
import { FaPlus } from "react-icons/fa";
import {addGame} from "../api/game"
import type { Game } from "../types/Game";





type GameList = Game[];

function ListGroup() {
  const [games, setGames] = useState<Game[]>([]);

  async function searchGame(e: React.FormEvent) {
    e.preventDefault();

    const nameInput = document.getElementById("searchInput") as HTMLInputElement;

    const res = await fetch("http://localhost:5200/api/game/send", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(nameInput.value),
    });

    const data: GameList = await res.json();
    console.log(data);

    setGames(data);

    
  }


  return (
    <>
      <h1>List of games:</h1>

      <form onSubmit={searchGame}>
        <input id="searchInput" />
        <button type="submit">Search</button>
      </form>

      <div className="game-list">
  {games.map((g) => (
    <div key={g.id} className="game-item">
    <img
  src={`https://images.igdb.com/igdb/image/upload/t_cover_big/${g.coverId}.jpg`}
  alt={g.gameName}
/>
    <span>{g.gameName}</span>

    <button onClick={() => addGame(g)}>
      <FaPlus />
    </button>
    </div>
))}
</div>
    </>
  );
}


export default ListGroup;