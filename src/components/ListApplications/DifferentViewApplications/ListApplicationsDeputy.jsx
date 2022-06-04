import React from "react";
import Search from "./../Search/Search";
import Application from "./../Application/Application";
import { useDispatch, useSelector } from "react-redux";
import api from "../../../api/api";
import { setApplications } from "../../../store/applicationsSlice";
import { sortByTextApplications } from "../UsefulMethods";
import PopupApplicationDeputy from "./../Popup/PopupApplilcation/PopupApplicationDeputy";
import PopupApplication from "../Popup/PopupApplilcation/PopupApplication";

const ListApplicationsDeputy = () => {
  const [isPopupVisible, setIsPopupVisible] = React.useState(false);
  const [currentApplicationPopup, setCurrentApplicationPopup] = React.useState(
    null
  );
  const [statusApplications, setStatusApplications] = React.useState("active");
  const [filteredApplications, setFilteredApplications] = React.useState([]);
  const dispatch = useDispatch();
  const currentUser = useSelector((state) => state.user.user);
  const applications = useSelector((state) => state.applications.applications);

  const uploadApplications = async (status) => {
    setStatusApplications(status == 1 ? "active" : "made");
    await api
      .get(`/Application/deputy/${currentUser.id}?status=${status}`)
      .then(({ data }) => {
        dispatch(setApplications(data));
        setFilteredApplications(data);
      });
  };

  React.useEffect(() => {
    uploadApplications(1);
  }, []);

  const handleTextSearch = (text) => {
    setFilteredApplications(sortByTextApplications(applications, text));
  };

  const handleClickButton = (application) => {
    setIsPopupVisible(true);
    setCurrentApplicationPopup(application);
  };

  const changedStatusApplication = () => {
    uploadApplications(statusApplications == "active" ? 1 : 2);
  };

  const getNeedPopup = () => {
    if (statusApplications == "active") {
      return (
        <PopupApplicationDeputy
          setIsPopupVisible={setIsPopupVisible}
          application={currentApplicationPopup}
          changedStatusApplication={changedStatusApplication}
        />
      );
    }
    return (
      <PopupApplication
        setIsPopupVisible={setIsPopupVisible}
        application={currentApplicationPopup}
      />
    );
  };

  return (
    <>
      <div className="container_list_applications container_deputy_applications">
        <main>
          <div className="buttons_sort_applications_deputy">
            <div
              className={
                statusApplications == "active"
                  ? "button active_applications choiced_view_applications"
                  : "button active_applications"
              }
              onClick={() => uploadApplications(1)}
            >
              <p>Активные заявки</p>
            </div>
            <div
              className={
                statusApplications == "made"
                  ? "button made_applications choiced_view_applications"
                  : "button made_applications"
              }
              onClick={() => uploadApplications(2)}
            >
              <p>Выполненные</p>
            </div>
          </div>

          <Search handleTextSearch={handleTextSearch} />
          <div className="list_applications deputy_applications">
            {filteredApplications.map((application) => (
              <Application
                application={application}
                handleClickButton={handleClickButton}
                statusApplications={statusApplications}
                textButton={
                  statusApplications == "active" ? "Ответить" : "Открыть"
                }
                key={application.id}
              />
            ))}
          </div>
        </main>
      </div>
      {isPopupVisible && getNeedPopup()}
    </>
  );
};

export default ListApplicationsDeputy;
