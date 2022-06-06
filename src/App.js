import React from "react";
import { Route, Routes } from "react-router-dom";
import "./style.css";
import Header from "./components/Header/Header";
import MainPage from "./components/MainPage/MainPage";
import ListApplications from "./components/ListApplications/ListApplications";
import SubmissionApplication from "./components/SubmissionsApplications/SubmissionApplication";
import Gratitude from "./components/SubmissionsApplications/Gratitude/Gratitude";
import Login from "./components/Login/Login";
import { useDispatch, useSelector } from "react-redux";
import { isUserLoadedPage, setUser } from "./store/userSlice";
import api from "./api/api";

function App() {
  const [applicationId, setApplicationId] = React.useState(null);
  const dispatch = useDispatch();
  const currentUser = useSelector(state => state.user.user);

  React.useEffect(() => {
    api.post('/authenticate', ).then(data => console.log(data));
    const loggedInUser = localStorage.getItem("user");
    if (loggedInUser) {
      const foundUser = JSON.parse(loggedInUser);
      dispatch(setUser(foundUser));
    }
    dispatch(isUserLoadedPage());
  }, []);

  React.useEffect(() => {
    api.post('/authenticate', currentUser ? currentUser.token : '').then(data => console.log(data));
  }, [currentUser]);

  return (
    <div className="wrapper">
      <Header />
      <Routes>
        <Route path="/" element={<MainPage />} />
        <Route path="/admin" element={<Login />} />
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
        <Route path="/list_applications" element={<ListApplications/>} />
      </Routes>
    </div>
  );
}

export default App;
