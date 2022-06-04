import React from "react";
import PopupApplication from "./Popup/PopupApplilcation/PopupApplication";
import api from "../../api/api";
import LoadingSpinner from "../Loading/LoadingSpinner";
import { useDispatch, useSelector } from "react-redux";
import { setApplications } from "../../store/applicationsSlice";
import ListApplicationsStandart from "./DifferentViewApplications/ListApplicationsStandart";
import ListApplicationsDeputy from "./DifferentViewApplications/ListApplicationsDeputy";
import PopupApplicationDeputy from "./Popup/PopupApplilcation/PopupApplicationDeputy";

const ListApplications = () => {
  const { currentUser, isUserLoadedPage } = useSelector((state) => ({
    currentUser: state.user.user,
    isUserLoadedPage: state.user.isUserLoadedPage,
  }));

  const getListApplication = () => {
    if (isUserLoadedPage) {
      if (currentUser && currentUser.role == 2) {
        return <ListApplicationsDeputy />;
      }
      return <ListApplicationsStandart />;
    }
  };

  return <div className="wrapper">{getListApplication()}</div>;
};

export default ListApplications;
