import React from "react";
import Application from "./Application/Application";
import PopupApplication from "./Popup/PopupApplilcation/PopupApplication";
import Aside from "./Aside/Aside";
import api from "../../api/api";
import LoadingSpinner from "./../Loading/LoadingSpinner";
import { useDispatch, useSelector } from "react-redux";
import { setApplications } from "../../store/applicationsSlice";
import { setDeputies } from "../../store/deputiesSlice";

const ListApplications = () => {
  const [isPopupVisible, setIsPopupVisible] = React.useState(false);
  const [currentApplicationPopup, setCurrentApplicationPopup] = React.useState(
    null
  );
  const dispatch = useDispatch();
  // const {
  //   isLoadingApplications,
  //   isLoadingDeputies,
  //   applications,
  // } = useSelector((state) => {
  //   state.applications.isLoadingApplications,
  //     state.deputies.isLoadingDeputies,
  //     state.applications.applications;
  // });
  const isLoadingApplications = useSelector(
    (state) => state.deputies.isLoadingApplications
  );
  const isLoadingDeputies = useSelector(
    (state) => state.deputies.isLoadingDeputies
  );
  const applications = useSelector((state) => state.applications.applications);

  const uploadApplications = async () => {
    await api.get("/Application").then(({ data }) => {
      dispatch(setApplications(data));
    });
  };

  const uploadDeputies = async () => {
    await api.get("/User/deputies/filters").then(({ data }) => {
      dispatch(setDeputies(data));
    });
  };

  React.useEffect(() => {
    uploadApplications();
    uploadDeputies();
  }, []);

  const handleReadAnswer = (application) => {
    setIsPopupVisible(true);
    setCurrentApplicationPopup(application);
  };

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
    console.log("q");
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
        }).then(({ data }) => setApplications(data));
      } catch (error) {
        console.log(error);
      }
    } else {
      uploadApplications();
    }
  };

  console.log("pererender list applications");

  return (
    <>
      {isLoadingApplications || isLoadingDeputies ? (
        <LoadingSpinner />
      ) : (
        <div className="wrapper">
          <div className="container_list_applications">
            <Aside handleFilterButton={handleFilterButton} />
            <main>
              <p className="text_search_application">
                Найдите заявку в поисковой строке
              </p>
              <input
                type="text"
                placeholder="ID заявки или слово из заголовка"
                onChange={(event) => handleTextSearch(event.target.value)}
              />
              <div className="list_applications">
                {applications.map((application) => (
                  <Application
                    application={application}
                    handleReadAnswer={handleReadAnswer}
                    key={application.id}
                  />
                ))}
              </div>
            </main>
          </div>
          {isPopupVisible && (
            <PopupApplication
              setIsPopupVisible={setIsPopupVisible}
              application={currentApplicationPopup}
            />
          )}
        </div>
      )}
    </>
  );
};

export default ListApplications;
