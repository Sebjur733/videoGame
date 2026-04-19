import { useState } from "react";

type Game = {
  id: number;
  name: string;
};

type GameList = Game[];

function ListGroup() {
  const [gameList, setGameList] = useState<string[]>([]);

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

    const gameNames = data.map(g => g.name);

    setGameList(gameNames);
  }

  return (
    <>
      <h1>List of games:</h1>

      <form onSubmit={searchGame}>
        <input id="searchInput" />
        <button type="submit">Search</button>
      </form>

      <ul>
        {gameList.map((g, i) => (
          <li key={i}>{g}</li>
        ))}
      </ul>
    </>
  );
}

export default ListGroup;