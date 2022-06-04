import React from "react";
import api from "../../../api/api";
import { useDispatch, useSelector } from "react-redux";
import { setApplications } from "../../../store/applicationsSlice";
import { setDeputies } from "../../../store/deputiesSlice";
import LoadingSpinner from "../../Loading/LoadingSpinner";
import Search from "./../Search/Search";
import Aside from "./../Aside/Aside";
import Application from "./../Application/Application";
import PopupApplication from "../Popup/PopupApplilcation/PopupApplication";
import PopupApplicationOperator from "../Popup/PopupApplilcation/PopupApplicationOperator";

const ListApplicationsStandart = () => {
  const [isPopupVisible, setIsPopupVisible] = React.useState(false);
  const [currentApplicationPopup, setCurrentApplicationPopup] = React.useState(
    null
  );
  const dispatch = useDispatch();
  const {
    isLoadingApplications,
    isLoadingDeputies,
    applications,
    currentUser,
  } = useSelector((state) => ({
    isLoadingApplications: state.applications.isLoadingApplications,
    isLoadingDeputies: state.applications.isLoadingApplications,
    applications: state.applications.applications,
    currentUser: state.user.user,
  }));

  const uploadDeputies = async () => {
    await api.get("/User/deputies/filters").then(({ data }) => {
      dispatch(setDeputies(data));
    });
  };

  const uploadApplications = async () => {
    await api.get("/Application").then(({ data }) => {
      dispatch(setApplications(data));
    });
  };

  const handleClickButton = (application) => {
    setIsPopupVisible(true);
    setCurrentApplicationPopup(application);
  };

  const handleTextSearch = (textSearch) => {
    if (textSearch) {
      try {
        api({
          url: "/Application/filters",
          params: {
            name: textSearch,
            id: textSearch,
          },
        }).then(({ data }) => dispatch(setApplications(data)));
      } catch (error) {
        console.log(error);
      }
    } else {
      uploadApplications();
    }
  };

  React.useEffect(() => {
    uploadApplications();
    uploadDeputies();
  }, []);

  const handleFilterButton = (deputyId, status) => {
    if (deputyId && !isNaN(status)) {
      try {
        api
          .get(`/Application/deputy/${deputyId}?status=${status}`)
          .then(({ data }) => {
            dispatch(setApplications(data));
          });
      } catch (error) {
        console.log(error);
      }
    } else if (deputyId) {
      try {
        api.get(`/Application/deputy/${deputyId}`).then(({ data }) => {
          dispatch(setApplications(data));
        });
      } catch (error) {
        console.log(error);
      }
    } else if (!isNaN(status)) {
      try {
        api.get(`/Application/status=${status}`).then(({ data }) => {
          dispatch(setApplications(data));
        });
      } catch (error) {
        console.log(error);
      }
    } else {
      uploadApplications();
    }
  };

  const getNeedPopup = () => {
    if (isPopupVisible) {
      if (currentUser && currentUser.role == 1) {
        return (
          <PopupApplicationOperator
            setIsPopupVisible={setIsPopupVisible}
            application={currentApplicationPopup}
          />
        );
      } else {
        return (
          <PopupApplication
            setIsPopupVisible={setIsPopupVisible}
            application={currentApplicationPopup}
          />
        );
      }
    }
  };

  return (
    <>
      {isLoadingApplications || isLoadingDeputies ? (
        <LoadingSpinner />
      ) : (
        <div className="container_list_applications">
          <Aside handleFilterButton={handleFilterButton} />
          <main>
            <p className="text_search_application">
              Найдите заявку в поисковой строке
            </p>
            <Search handleTextSearch={handleTextSearch} />
            <div className="list_applications">
              {applications.map((application) => (
                <Application
                  application={application}
                  handleClickButton={handleClickButton}
                  textButton={
                    currentUser && currentUser.role == 1
                      ? "Открыть"
                      : "Прочитать ответ"
                  }
                  key={application.id}
                />
              ))}
            </div>
          </main>
        </div>
      )}
      {getNeedPopup()}
    </>
  );
};

export default ListApplicationsStandart;
