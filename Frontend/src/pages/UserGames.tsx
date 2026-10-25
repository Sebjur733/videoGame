import { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import { getGamesForUser } from "../api/game";
import type { Game } from "../types/Game";
import "./UserGames.css";

function UserGames() {
  const { username } = useParams<{ username: string }>();
  const [games, setGames] = useState<Game[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [errorMessage, setErrorMessage] = useState("");

  useEffect(() => {
    if (!username) {
      setIsLoading(false);
      setErrorMessage("User not found.");
      return;
    }

    let isActive = true;
    const currentUsername = username;

    async function loadGames() {
      try {
        const result = await getGamesForUser(currentUsername);

        if (isActive) {
          setGames(result);
        }
      } catch (error) {
        if (isActive) {
          setErrorMessage(
            error instanceof Error
              ? `Couldn't load this user's games: ${error.message}`
              : "Couldn't load this user's games."
          );
        }
      } finally {
        if (isActive) {
          setIsLoading(false);
        }
      }
    }

    void loadGames();

    return () => {
      isActive = false;
    };
  }, [username]);

   return (
    <main className="user-games-page">
      <Link className="user-games-back-link" to="/profile">
        Back to user search
      </Link>

      <h1>{username ?? "User"}</h1>
      <h2>Games</h2>

      {isLoading && <p>Loading games...</p>}

      {errorMessage && <p role="alert">{errorMessage}</p>}

      {!isLoading && !errorMessage && games.length === 0 && (
        <p>This user has no games in their library.</p>
      )}

      <div className="game-grid">
        {games.map((game) => (
          <article className="game-card" key={game.id}>
            {game.coverId && (
              <img
                src={`https://images.igdb.com/igdb/image/upload/t_cover_big/${game.coverId}.jpg`}
                alt={game.gameName}
              />
            )}
            <h3>{game.gameName}</h3>
          </article>
        ))}
      </div>
    </main>
  );
}

export default UserGames;