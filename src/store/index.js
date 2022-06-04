import { configureStore } from "@reduxjs/toolkit";
import userReducer from "./userSlice";
import deputiesReducer from "./deputiesSlice";
import applicationsReducer from "./applicationsSlice";
import asideReducer from "./asideSlice";

export default configureStore({
  reducer: {
    user: userReducer,
    deputies: deputiesReducer,
    applications: applicationsReducer,
    aside: asideReducer,
  },
});
