import React from "react";
import { TailSpin } from "react-loader-spinner";

const LoadingSpinner = () => {
  return (
    <div className="loading">
      <div className="loading_spinner">
        <TailSpin
          height="250"
          width="250"
          color="rgb(0, 153, 255)"
          ariaLabel="loading"
        />
      </div>
    </div>
  );
};

export default LoadingSpinner;
