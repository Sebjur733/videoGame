import { useState } from "react";

function AuthPage() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const sendRequest = async (url: string) => {
    const res = await fetch(url, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        username,
        password,
      }),
    });

    const data = await res.json();
    console.log(data);
  };

  return (
    <form>
      <input
        placeholder="brukernavn"
        value={username}
        onChange={(e) => setUsername(e.target.value)}
      />

      <input
        type="password"
        placeholder="passord"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
      />

      <button
        type="button"
        onClick={() => sendRequest("http://localhost:5200/api/user/login")}
      >
        Login
      </button>

      <button
        type="button"
        onClick={() => sendRequest("http://localhost:5200/api/user/register")}
      >
        Register
      </button>
    </form>
  );
}

export default AuthPage;