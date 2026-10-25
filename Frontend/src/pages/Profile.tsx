import { useState, type FormEvent } from "react";
import { useNavigate } from "react-router-dom";
import { FaChevronRight } from "react-icons/fa";
import { searchUsers, type UserSearchResult } from "../api/user";
import "./Profile.css";

function Profile() {
    const navigate = useNavigate();
  const [query, setQuery] = useState("");
  const [users, setUsers] = useState<UserSearchResult[]>([]);
  const [hasSearched, setHasSearched] = useState(false);
  const [isSearching, setIsSearching] = useState(false);
  const [errorMessage, setErrorMessage] = useState("");

  async function handleSearch(event: FormEvent<HTMLFormElement>) {
    event.preventDefault();
    const searchQuery = query.trim();

    if (!searchQuery) {
      setUsers([]);
      setHasSearched(false);
      setErrorMessage("");
      return;
    }

    setIsSearching(true);
    setErrorMessage("");

    try {
      const results = await searchUsers(searchQuery);
      setUsers(results);
      setHasSearched(true);
    } catch (error) {
      setUsers([]);
      setHasSearched(false);
      setErrorMessage(
        error instanceof Error
          ? `Couldn't search for users: ${error.message}`
          : "Couldn't search for users. Please try again."
      );
    } finally {
      setIsSearching(false);
    }
  }

  function openUserProfile(username: string) {
    navigate(`/users/${encodeURIComponent(username)}`);
  }

  return (
    <main className="profile-page">
      <h1>Find users</h1>

      <form className="profile-search-form" onSubmit={handleSearch}>
        <input
          aria-label="Search by username"
          value={query}
          onChange={(event) => setQuery(event.target.value)}
          placeholder="Search by username"
        />
        <button type="submit" disabled={isSearching || !query.trim()}>
          {isSearching ? "Searching..." : "Search"}
        </button>
      </form>

      {errorMessage && (
        <p className="profile-search-message profile-search-error" role="alert">
          {errorMessage}
        </p>
      )}

      {hasSearched && users.length === 0 && !errorMessage && (
        <p className="profile-search-message">No users found.</p>
      )}

      {users.length > 0 && (
        <ul className="profile-user-list" aria-live="polite">
          {users.map((user) => (
            <li className="profile-user-item" key={user.username}>
              <span>{user.username}</span>
              <button
                className="profile-user-open-button"
                type="button"
                aria-label={`View ${user.username}'s profile`}
                onClick={() => openUserProfile(user.username)}
              >
                <FaChevronRight aria-hidden="true" />
              </button>
            </li>
          ))}
        </ul>
      )}
    </main>
  );
}

export default Profile;