import React from 'react';

const Search = ({ handleTextSearch }) => {
  return (
    <input
      type="text"
      placeholder="ID заявки или слово из заголовка"
      onChange={(event) => handleTextSearch(event.target.value)}
    />
  );
};

export default Search;
