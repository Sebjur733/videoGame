import { useState } from "react";
import "./ListGroup.css";

type Game = {
  id: number;
  name: string;
  cover?: {
    image_id: string;
  };
};

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

      <ul>
  {games.map((g) => (
  <li key={g.id} className="game-item">
    <img
      src={`https://images.igdb.com/igdb/image/upload/t_cover_big/${g.cover?.image_id}.jpg`}
      alt={g.name}
    />
    <span>{g.name}</span>

    <button onClick={() => addGame(g)}>
      Add
    </button>
  </li>
))}
  
</ul>
    </>
  );
}

function addGame(game: Game) {
  console.log("Legger til:", game);
}

export default ListGroup;