import { configureStore } from "@reduxjs/toolkit";
import userReducer from "./userSlice";
import deputiesReducer from "./deputiesSlice";
import applicationsReducer from "./applicationsSlice";

export default configureStore({
  reducer: {
    user: userReducer,
    deputies: deputiesReducer,
    applications: applicationsReducer,
  },
});
