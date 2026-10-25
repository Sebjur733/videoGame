import axios from "axios";

export type UserSearchResult = {
  username: string;
};

export async function searchUsers(query: string): Promise<UserSearchResult[]> {
  const token = localStorage.getItem("token");

  const { data } = await axios.get<UserSearchResult[]>(
    `${import.meta.env.VITE_API_URL}/api/user/findUser`,
    {
      params: { userName: query },
      headers: {
        Authorization: `Bearer ${token}`,
      },
    }
  );

  return data;
}
