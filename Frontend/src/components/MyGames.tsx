import React, { useState, useEffect } from "react";
import { getGames } from "../api/game";
import type { Game } from "./ListGroup";

const testGames: Game[] = [
  {
    id: 1,
    gameName: "Chess",
    alreadyInLibrary: true,
    coverId: "chess.jpg"
  },
  {
    id: 2,
    gameName: "Go",
    alreadyInLibrary: false
  },
  {
    id: 3,
    gameName: "Sudoku",
    alreadyInLibrary: true,
    coverId: "sudoku.png"
  }
];

function MyGames() {
  const [games, setGames] = useState<Game[]>([]);

  // Assuming this is in a React component with useState<Game[]>
useEffect(() => {
  const fetchGames = async () => {
    const games = await getGames();
    if (games) setGames(games); // Handle potential undefined
  };
  fetchGames();
}, []);


  return (
    <div>
      <h1>My Games</h1>
    
      {games.map((game: Game) => (
        <div key={game.id}>
          <h2>{game.gameName}</h2>
          {game.coverId && <img src={game.coverId} alt={game.gameName} />}
        </div>
      ))}
    </div>
  );
}
export default MyGames;