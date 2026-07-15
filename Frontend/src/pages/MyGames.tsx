import React, { useState, useEffect } from "react";
import { getGames } from "../api/game";
import type { Game } from "../types/Game";
import "./MyGames.css";


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
  <div className="my-games">
    <h1>My Games</h1>

    <div className="game-grid">
      {games.map((game: Game) => (
        <div className="game-card" key={game.id}>
          {game.coverId && (
  <img
    src={`https://images.igdb.com/igdb/image/upload/t_cover_big/${game.coverId}.jpg`}
    alt={game.gameName}
  />
)}

          <h2>{game.gameName}</h2>
        </div>
      ))}
    </div>
  </div>
);
}
export default MyGames;