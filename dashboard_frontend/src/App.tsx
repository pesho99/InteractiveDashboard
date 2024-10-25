import "./App.css";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import PrivateRoute from "./Helpers/PrivateRoute";
import LoginComponent from "./Components/LoginComponent";
import TickerDisplay from "./Components/TickerDisplay";
import RegisterComponent from "./Components/RegisterComnponent";
import Navbar from "./SiteElements/Navbar";

function App() {
  return (
    <BrowserRouter>
    <Navbar/>
      <Routes>
        <Route path="/login" element={<LoginComponent />} />
        <Route path="/register" element={<RegisterComponent />} />
        <Route
          path="/dashboard"
          element={
            <PrivateRoute>
              <TickerDisplay />
            </PrivateRoute>
          }
        />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
