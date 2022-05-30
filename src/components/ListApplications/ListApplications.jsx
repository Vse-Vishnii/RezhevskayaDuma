import React from 'react';
import PopupApplication from './Popup/PopupApplilcation/PopupApplication';
import api from '../../api/api';
import LoadingSpinner from '../Loading/LoadingSpinner';
import { useDispatch, useSelector } from 'react-redux';
import { setApplications } from '../../store/applicationsSlice';
import ListApplicationsStandart from './DifferentViewApplications/ListApplicationsStandart';
import ListApplicationsDeputy from './DifferentViewApplications/ListApplicationsDeputy';
import PopupApplicationDeputy from './Popup/PopupApplilcation/PopupApplicationDeputy';

const ListApplications = () => {
  const [isPopupVisible, setIsPopupVisible] = React.useState(false);
  const [currentApplicationPopup, setCurrentApplicationPopup] = React.useState(null);
  const { currentUser, isUserLoadedPage } = useSelector((state) => ({
    currentUser: state.user.user,
    isUserLoadedPage: state.user.isUserLoadedPage,
  }));

  const handleClickButton = (application) => {
    setIsPopupVisible(true);
    setCurrentApplicationPopup(application);
  };

  const getListApplication = () => {
    if (isUserLoadedPage) {
      if (currentUser && currentUser.role == 2) {
        return <ListApplicationsDeputy handleClickButton={handleClickButton} />;
      }
      return <ListApplicationsStandart handleClickButton={handleClickButton} />;
    }
  };

  const getNeedPopup = () => {
    if (!isUserLoadedPage) return;
    if (currentUser) {
      if (currentUser.role == 2) {
        return (
          <PopupApplicationDeputy
            setIsPopupVisible={setIsPopupVisible}
            application={currentApplicationPopup}
          />
        );
      }
    }
    return (
      <PopupApplication
        setIsPopupVisible={setIsPopupVisible}
        application={currentApplicationPopup}
      />
    );
  };

  return <div className="wrapper">{getListApplication()}</div>;
};

export default ListApplications;
