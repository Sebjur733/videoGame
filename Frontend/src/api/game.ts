import type { Game } from "../types/Game";
import axios from 'axios';


export async function getGames() {
    try {
        const token = localStorage.getItem("token");

        const res = await axios.get<Game[]>(
            `${import.meta.env.VITE_API_URL}/api/user/getGames`,
            {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            }
        );

        return res.data;

    } catch (error) {
        console.error(error);
    }
}

export async function getGamesForUser(username: string): Promise<Game[]> {
  const token = localStorage.getItem("token");

  if (!token) {
    throw new Error("You must be logged in to view a user's games.");
  }

  const res = await axios.get<Game[]>(
    `${import.meta.env.VITE_API_URL}/api/user/getGamesForUser/${encodeURIComponent(username)}`,
    {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    }
  );

  return res.data;
}

export async function addGame(game: Game) {
    try {
        const token = localStorage.getItem("token");

        const res = await axios.post(
            `${import.meta.env.VITE_API_URL}/api/user/addGames`,
            {
                Game: {
                    id: game.id,
                    gameName: game.gameName,
                    coverId: game.coverId
                }
            },
            {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            }
        );

        console.log("adding game:", res.data);

    } catch (error) {
        console.error(error);
    }
}