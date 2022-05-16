import React from 'react';
import Application from './Application/Application';
import { ListApplicationsInfo } from './ListApplicationsInfo';
import Popup from './Popup/Popup';
import Aside from './Aside/Aside';
import api from '../../api/api';

const ListApplications = () => {
  const [isPopupVisible, setIsPopupVisible] = React.useState(false);
  const [currentApplicationPopup, setCurrentApplicationPopup] = React.useState(null);

  const [applications, setApplications] = React.useState([]);

  const uploadApplications = () => {
    api.get('/Application').then(({ data }) => {
      setApplications(data);
    });
  };

  React.useEffect(() => uploadApplications(), []);

  const handleReadAnswer = (application) => {
    if (application.status != 'Дан ответ') return;
    setIsPopupVisible(true);
    setCurrentApplicationPopup(application);
  };

  const handleFilterButton = (deputyId, status) => {
    if (deputyId && !isNaN(status)) {
      try {
        api.get(`/Application/deputy/${deputyId}?status=${status}`).then(({ data }) => {
          setApplications(data);
        });
      } catch (error) {
        console.log(error);
      }
    } else if (deputyId) {
      try {
        api.get(`/Application/deputy/${deputyId}`).then(({ data }) => {
          setApplications(data);
        });
      } catch (error) {
        console.log(error);
      }
    } else if (!isNaN(status)) {
      try {
        api.get(`/Application/status=${status}`).then(({ data }) => {
          setApplications(data);
        });
      } catch (error) {
        console.log(error);
      }
    } else {
      uploadApplications();
    }
    console.log('q');
  };

  const handleTextSearch = (textSearch) => {
    if (textSearch) {
      try {
        api({
          url: '/Application/filters',
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

  return (
    <div className="wrapper">
      <div className="container_list_applications">
        <Aside handleFilterButton={handleFilterButton} />
        <main>
          <p className="text_search_application">Найдите заявку в поисковой строке</p>
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
        <Popup
          setIsPopupVisible={setIsPopupVisible}
          currentApplicationPopup={currentApplicationPopup}
        />
      )}
    </div>
  );
};

export default ListApplications;
