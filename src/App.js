import React from "react";
import { Route, Routes } from "react-router-dom";
import "./style.css";
import Header from "./components/Header/Header";
import MainPage from "./components/MainPage/MainPage";
import ListApplications from "./components/ListApplications/ListApplications";
import SubmissionApplication from "./components/SubmissionsApplications/SubmissionApplication";
import Gratitude from "./components/SubmissionsApplications/Gratitude/Gratitude";

function App() {
  const [applicationId, setApplicationId] = React.useState(null);

  return (
    <div className="wrapper">
      <Header />
      <Routes>
        <Route path="/" element={<MainPage />} />
        <Route
          path="submission_application"
          element={
            <SubmissionApplication setApplicationId={setApplicationId} />
          }
        />
        <Route
          path="/gratitude"
          element={<Gratitude applicationId={applicationId} />}
        />
        <Route path="/list_applications" element={<ListApplications />} />
      </Routes>
    </div>
  );
}

export default App;
