import React from 'react';
import Application from './Application/Application';
import { ListApplicationsInfo } from './ListApplicationsInfo';
import Popup from './Popup/Popup';
import Aside from './Aside/Aside';

const ListApplications = () => {
  const [isPopupVisible, setIsPopupVisible] = React.useState(false);
  const [currentApplicationPopup, setCurrentApplicationPopup] = React.useState(null);
  const [filterApplications, setFilterApplications] = React.useState(
    ListApplicationsInfo.applications,
  );
  const [searchResultsApplications, setSearchResultsApplications] = React.useState(
    ListApplicationsInfo.applications,
  );

  const handlerReadAnswer = (application) => {
    if (application.status != 'Дан ответ') return;
    setIsPopupVisible(true);
    setCurrentApplicationPopup(application);
  };

  const handlerFilterButton = (deputy, status) => {
    let applications = ListApplicationsInfo.applications.filter((application) => {
      return (
        (application.deputy == deputy || deputy == null) &&
        (application.status == status || status == null)
      );
    });
    setFilterApplications(applications);
    setSearchResultsApplications(applications);
  };

  const handlerTextSearch = (textSearch) => {
    let applications = filterApplications.filter((application) => {
      return (
        application.id.toLowerCase().includes(textSearch.toLowerCase()) ||
        application.title.toLowerCase().includes(textSearch.toLowerCase())
      );
    });
    setSearchResultsApplications(applications);
  };

  return (
    <div className="wrapper">
      <div className="container_list_applications">
        <Aside handlerFilterButton={handlerFilterButton} />
        <main>
          <p className="text_search_application">Найдите заявку в поисковой строке</p>
          <input
            type="text"
            placeholder="ID заявки или слово из заголовка"
            onChange={(event) => handlerTextSearch(event.target.value)}
          />
          <div className="list_applications">
            {searchResultsApplications.map((application) => (
              <Application
                application={application}
                handlerReadAnswer={handlerReadAnswer}
                key={application.id}
              />
            ))}
          </div>
        </main>
      </div>
      {isPopupVisible && (
        <Popup
          setIsPopupVisible={setIsPopupVisible}
          currentApplicationPopup={currentApplicationPopup}
        />
      )}
    </div>
  );
};

export default ListApplications;
