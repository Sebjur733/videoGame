import { Navigate } from "react-router-dom";
import type { ReactNode } from "react";

type Props = {
  children: ReactNode;
};

export default function PrivateRoute({ children }: Props) {
  const isAuthenticated = localStorage.getItem("auth") === "true";

  return isAuthenticated ? <>{children}</> : <Navigate to="/login" />;
}