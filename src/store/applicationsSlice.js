import { createSlice } from "@reduxjs/toolkit";

const applicationsSlice = createSlice({
  name: "user",
  initialState: { applications: [], isLoadingApplications: true },
  reducers: {
    setApplications(state, action) {
      state.applications = action.payload;
      state.isLoadingApplications = false;
    },
  },
});

export const { setApplications } = applicationsSlice.actions;

export default applicationsSlice.reducer;
