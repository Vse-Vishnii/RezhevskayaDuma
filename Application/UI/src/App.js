import React from "react";
import { Route, Routes } from "react-router-dom";
import "./style.css";
import Header from "./components/Header/Header";
import MainPage from "./components/MainPage/MainPage";
import FirstStep from "./components/SubmissionsApplications/FirstStep/FirstStep";
import SecondStep from "./components/SubmissionsApplications/SecondStep/SecondStep";
import ThirdStep from "./components/SubmissionsApplications/ThirdStep/ThirdStep";
import Gratitude from "./components/SubmissionsApplications/Gratitude/Gratitude";
import FourthStep from './components/SubmissionsApplications/FourthStep/FourthStep';
import ListApplications from './components/ListApplications/ListApplications';

function App() {
  const [activeCategory, setActiveCategory] = React.useState("");
  const [activeArea, setActiveArea] = React.useState("");
  const [activeDeputy, setActiveDeputy] = React.useState("");

  return (
    <div className="wrapper">
      <Header />
      <Routes>
        <Route path="/" element={<MainPage />} />
        <Route path="/first_step" element={<FirstStep activeItem={activeCategory} setActiveItem={setActiveCategory} />} />
        <Route path="/second_step" element={<SecondStep activeItem={activeArea} setActiveItem={setActiveArea}  />} />
        <Route path="/third_step" element={<ThirdStep activeItem={activeDeputy} setActiveItem={setActiveDeputy}  />} />
        <Route path="/fourth_step" element={<FourthStep activeCategory={activeCategory} activeArea={activeArea} activeDeputy={activeDeputy} />} />
        <Route path="/gratitude" element={<Gratitude />} />
        <Route path="/list_applications" element={<ListApplications />} />
      </Routes>
    </div>
  );
}

export default App;
