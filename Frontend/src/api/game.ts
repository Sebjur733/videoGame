import type { Game } from "../types/Game";
import axios from 'axios';


export async function getGames() {
    const user = localStorage.getItem("username");
    try {
    const res = await axios.get<Game[]>(`${import.meta.env.VITE_API_URL}/api/user/getGames/${user}`);
    res.data.map(g => console.log(g.gameName));

    return res.data;
    
    } catch (error) {
        console.error(error);
    }
    
}

export async function addGame(game:Game) {
    const user = localStorage.getItem("username");
    console.log("before http: " + game.id);
    try {
        const res = await axios.post<string>(`${import.meta.env.VITE_API_URL}/api/user/addGames`, {
            Game: {
        id: game.id,
        gameName: game.gameName,
        coverId: game.coverId
    },
    User: user
        });
        console.log("adding game: " + res.data);

    } catch (error) {
        console.error(error);
    }

}