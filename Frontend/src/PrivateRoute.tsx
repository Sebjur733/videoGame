import { Navigate } from "react-router-dom";
import type { ReactNode } from "react";
import { isAuthenticated } from "./api/auth";

type Props = {
  children: ReactNode;
};

function PrivateRoute({ children }: Props) {
  return isAuthenticated() ? children : <Navigate to="/login" />;
}

export default PrivateRoute;