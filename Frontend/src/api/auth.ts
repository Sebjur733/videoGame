import axios from "axios";

type LoginResponse = {
  token: string;
};

export const loginUser = async (
  username: string,
  password: string
) => {
  const { data } = await axios.post<LoginResponse>(
    "http://localhost:5200/api/user/login",
    { username, password }
  );

  localStorage.setItem("token", data.token);
};

export const registerUser = async (
  username: string,
  password: string
) => {
  const { data } = await axios.post<string>(
    "http://localhost:5200/api/user/register",
    { username, password }
  );
console.log(data);
};


export const logoutUser = () => {
  localStorage.removeItem("token");
};

export const isAuthenticated = () => {
  return !!localStorage.getItem("token");
};